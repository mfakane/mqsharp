using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DllExport.Tasks
{
	class Disassembler
	{
		/// <summary>
		/// Gets or sets the path of Ildasm.exe.
		/// </summary>
		public string Ildasm { get; set; }
		/// <summary>
		/// Gets or sets a value that indicates the output includes line numbers of the original source.
		/// </summary>
		public bool LineNumbers { get; set; } = true;
		/// <summary>
		/// Gets or sets a value that indicates the output uses verbal from for custom attribute blobs. The default is binary form.
		/// </summary>
		public bool CAVerbal { get; set; } = true;
		/// <summary>
		/// Gets or sets a value that indicates the output produces the full list of types, to preserve type ordering in a round trip.
		/// </summary>
		public bool TypeList { get; set; } = true;

		public Disassembler(string ildasm) =>
			Ildasm = ildasm;

		IEnumerable<string> CreateArguments(string inputPath, string outputPath)
		{
			yield return "/unicode";
			yield return "/nobar";

			if (LineNumbers) yield return "/linenum";
			if (CAVerbal) yield return "/caverbal";
			if (TypeList) yield return "/typelist";

			yield return $"\"/out={outputPath}\"";
			yield return $"\"{inputPath}\"";
		}

		public void Disassemble(string inputPath, string outputPath)
		{
			using (var p = new Process
			{
				StartInfo =
				{
					FileName = Ildasm,
					Arguments = string.Join(" ", CreateArguments(inputPath, outputPath)),
					RedirectStandardError = true,
					UseShellExecute = false,
					CreateNoWindow = true,
				},
			})
			{
				p.Start();
				p.WaitForExit();

				if (p.ExitCode != 0)
					throw new InvalidOperationException(p.StandardError.ReadToEnd());
			}
		}
	}
}
