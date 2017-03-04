using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Microsoft.Build.Framework;
using Microsoft.Win32;

namespace DllExport.Tasks
{
	public class DllExport : ITask
	{
		public IBuildEngine BuildEngine { get; set; }
		public ITaskHost HostObject { get; set; }

		public string ILDAsm { get; set; }
		public string ILAsm { get; set; }
		[Required]
		public string InputPath { get; set; }
		public string OutputPath { get; set; }
		public string AttributeName { get; set; } = "System.Runtime.InteropServices.DllExportAttribute";
		public string Configuration { get; set; }

		public bool Execute()
		{
			if (string.IsNullOrEmpty(InputPath)) throw new ArgumentNullException(nameof(InputPath));
			if (!File.Exists(InputPath)) throw new FileNotFoundException($"{nameof(InputPath)} not found.", InputPath);

			var dasm = new Disassembler(ILDAsm ?? GetDisassemblerPath());
			var asm = new Assembler(ILAsm ?? GetAssemblerPath())
			{
				Target = AssemblerTarget.Library,
				Debug = Configuration == "Debug",
			};
			var tempPath = Path.GetTempFileName();

			try
			{
				dasm.Disassemble(InputPath, tempPath);
				File.WriteAllText(tempPath, Export(File.ReadAllText(tempPath)));
				asm.Assemble(tempPath, OutputPath ?? InputPath);
			}
			finally
			{
				File.Delete(tempPath);
			}

			return true;
		}

		static string GetDisassemblerPath()
		{
			using (var sdks = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Microsoft\Microsoft SDKs\NETFXSDK") ??
							  Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Microsoft SDKs\NETFXSDK"))
			{
				if (sdks == null)
					throw new Exception(@"HKLM\SOFTWARE\Microsoft\Microsoft SDKs\NETFXSDK not found.");

				if (!(sdks.GetSubKeyNames().OrderByDescending(i => i).FirstOrDefault() is string latestSdkVersion))
					throw new Exception("Latest SDK version cannot be detected.");

				using (var latestSdk = sdks.OpenSubKey($@"{latestSdkVersion}\WinSDK-NetFx40Tools"))
				{
					if (!(latestSdk.GetValue("InstallationFolder") is string installationFolder))
						throw new Exception("SDK installation directory cannot be detected.");

					if (!Directory.Exists(installationFolder))
						throw new Exception("SDK installation directory does not exists.");

					var ildasm = Path.Combine(installationFolder, "ildasm.exe");

					if (!File.Exists(ildasm))
						throw new Exception("ildasm.exe not found.");

					return ildasm;
				}
			}
		}

		static string GetAssemblerPath() =>
			Path.Combine(RuntimeEnvironment.GetRuntimeDirectory().TrimEnd(Path.DirectorySeparatorChar), "ilasm.exe");

		string Export(string code)
		{
			var defs = GetExportDefinitions(code).ToArray();

			foreach (var i in defs.Reverse())
			{
				code = code.Insert(i.CustomDirectiveIndex, $".export [{i.Order}]{(i.Name == null ? null : $" as {i.Name}")}\r\n    ");
				code = code.Insert(i.NameStartIndex, $"modopt([mscorlib]{i.GetCallingConventionTypeName()})\r\n          ");
			}

			return code;
		}

		IEnumerable<ExportDefinition> GetExportDefinitions(string code)
		{
			var customRegex = new Regex($@"\.custom instance void (?<assembly>\[[^\]]+\])?{Regex.Escape(AttributeName)}::\.ctor\(", RegexOptions.Compiled);
			var argsRegex = new Regex(@"=\s*\{(?<args>[^\}]*)\}", RegexOptions.Compiled);
			var argRegex = new Regex(@"(?<valtype>.+?)\((?<value>.+?)\)", RegexOptions.Compiled);
			var methodRegex = new Regex(@"\.method\s+([A-za-z0-9_\.\s]+?)(?<marshal>marshal\(.+?\))?([A-za-z0-9_\.\s]+?)(?<name>[^\s]+)\(", RegexOptions.Compiled | RegexOptions.RightToLeft);
			var ordinal = 0;

			foreach (Match custom in customRegex.Matches(code))
			{
				var args = argRegex.Matches(argsRegex.Match(code, custom.Index).Groups["args"].Value).Cast<Match>().ToDictionary(i => i.Groups["valtype"].Value, i => i.Groups["value"].Value);
				var name = methodRegex.Match(code, custom.Index);
				var nameStart = name.Groups["marshal"].Success ? name.Groups["marshal"].Index : name.Groups["name"].Index;

				yield return new ExportDefinition(++ordinal, args.ContainsKey("string") ? args["string"].Trim('\'') : null, (CallingConvention)int.Parse(args["int32"]), nameStart, custom.Index);
			}
		}

		class ExportDefinition
		{
			public int Order { get; }
			public string Name { get; }
			public CallingConvention CallingConvention { get; }
			public int CustomDirectiveIndex { get; }
			public int NameStartIndex { get; set; }

			public ExportDefinition(int order, string name, CallingConvention callingConvention, int nameStartIndex, int customDirectiveIndex)
			{
				Order = order;
				Name = name;
				CallingConvention = callingConvention;
				NameStartIndex = nameStartIndex;
				CustomDirectiveIndex = customDirectiveIndex;
			}

			public string GetCallingConventionTypeName()
			{
				switch (CallingConvention)
				{
					case CallingConvention.Cdecl:
						return typeof(CallConvCdecl).FullName;
					case CallingConvention.Winapi:
					case CallingConvention.StdCall:
						return typeof(CallConvStdcall).FullName;
					case CallingConvention.ThisCall:
						return typeof(CallConvThiscall).FullName;
					case CallingConvention.FastCall:
						return typeof(CallConvFastcall).FullName;
					default:
						throw new InvalidOperationException();
				}
			}
		}
	}
}
