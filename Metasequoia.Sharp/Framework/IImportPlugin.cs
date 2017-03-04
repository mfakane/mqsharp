using System;

namespace Metasequoia
{
	public interface IImportPlugin : IImportExportPlugin
    {
		bool IsBackground { get; set; }
		bool SupportsBackgroundLoading { get; }
		IntPtr ImportOptions { get; set; }
		bool Import(FileFilter type, string path, Document doc);
    }
}
