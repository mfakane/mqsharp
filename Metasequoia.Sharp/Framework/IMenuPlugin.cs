namespace Metasequoia
{
	public interface IMenuPlugin : IPlugin
    {
		IPluginCommand[] Commands { get; }
    }
}
