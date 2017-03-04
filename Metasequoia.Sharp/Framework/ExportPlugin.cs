namespace Metasequoia
{
	public abstract class ExportPlugin : Plugin, IExportPlugin
	{
		public abstract FileFilter[] AvailableFileTypes { get; }
		public abstract bool Export(FileFilter type, string path, Document doc);
	}
}
