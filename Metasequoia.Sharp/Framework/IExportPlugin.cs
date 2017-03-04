namespace Metasequoia
{
	public interface IExportPlugin : IImportExportPlugin
	{
		bool Export(FileFilter type, string path, Document doc);
	}
}
