namespace Metasequoia
{
	public class FileFilter
    {
		public string DisplayName { get; set; }
		public string Extension { get; set; }

		public FileFilter(string displayName, string extension)
		{
			DisplayName = displayName;
			Extension = extension;
		}
	}
}
