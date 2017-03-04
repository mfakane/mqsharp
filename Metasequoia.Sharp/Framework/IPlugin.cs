namespace Metasequoia
{
	public interface IPlugin
    {
		uint AuthorId { get; }
		uint PluginId { get; }
		string DisplayName { get; }
	}
}
