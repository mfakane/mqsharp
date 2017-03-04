namespace Metasequoia
{
	public abstract class SelectPlugin : Plugin, ISelectPlugin
	{
		public abstract IPluginCommand[] Commands { get; }
	}
}
