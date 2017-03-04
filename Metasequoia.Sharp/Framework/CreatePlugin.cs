namespace Metasequoia
{
	public abstract class CreatePlugin : Plugin, ICreatePlugin
	{
		public abstract IPluginCommand[] Commands { get; }
	}
}
