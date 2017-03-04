namespace Metasequoia
{
	public interface IImportExportPlugin : IPlugin
    {
		FileFilter[] AvailableFileTypes { get; }
	}
}
