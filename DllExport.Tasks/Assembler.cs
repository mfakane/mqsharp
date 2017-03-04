using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DllExport.Tasks
{
	class Assembler
	{
		public string Ilasm { get; set; }
		public AssemblerTarget Target { get; set; }
		public AssemblerPlatform Platform { get; set; }
		public bool Debug { get; set; }

		public Assembler(string ilasm) =>
			Ilasm = ilasm;

		IEnumerable<string> CreateArguments(string inputPath, string outputPath)
		{
			yield return "/nologo";
			yield return "/quiet";

			switch (Target)
			{
				case AssemblerTarget.Executable:
					yield return "/exe";
					break;
				case AssemblerTarget.Library:
					yield return "/dll";
					break;
			}

			switch (Platform)
			{
				case AssemblerPlatform.AnyCPU:
					yield return "/flags=1";
					break;
				case AssemblerPlatform.x86:
					yield return "/flags=2";
					yield return "/32bitpreferred";
					break;
				case AssemblerPlatform.x64:
					yield return "/flags=8";
					yield return "/x64";
					break;
				case AssemblerPlatform.Itanium:
					yield return "/flags=8";
					yield return "/itanium";
					break;
				case AssemblerPlatform.Arm:
					yield return "/flags=8";
					yield return "/arm";
					break;
			}

			if (Debug)
				yield return "/debug";
			else
				yield return "/debug=opt";

			yield return $"\"/output={outputPath}\"";
			yield return $"\"{inputPath}\"";
		}

		public void Assemble(string inputPath, string outputPath)
		{
			using (var p = new Process
			{
				StartInfo =
				{
					FileName = Ilasm,
					Arguments = string.Join(" ", CreateArguments(inputPath, outputPath)),
					RedirectStandardOutput = true,
					RedirectStandardError = true,
					UseShellExecute = false,
					CreateNoWindow = true,
				},
			})
			{
				p.Start();
				p.WaitForExit();

				if (p.ExitCode != 0)
					throw new InvalidOperationException(p.StandardOutput.ReadToEnd() + p.StandardError.ReadToEnd());
			}
		}
	}

	enum AssemblerTarget
	{
		Executable,
		Library,
	}

	enum AssemblerPlatform
	{
		AnyCPU,
		x86,
		x64,
		Itanium,
		Arm,
	}
}
