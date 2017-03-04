namespace Metasequoia
{
	public interface IPluginCommand
	{
		string Caption { get; }
		bool Execute(Document doc);
	}
}