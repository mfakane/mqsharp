namespace Metasequoia
{
	public abstract class ObjectPlugin : Plugin, IObjectPlugin
	{
		public abstract IPluginCommand[] Commands { get; }
	}
}
