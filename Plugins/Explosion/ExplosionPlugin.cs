using Metasequoia;

namespace Explosion
{
	[Plugin("Explosion", 0xAB86CB1E, 0xAC5C35F1)]
	public class ExplosionPlugin : SelectPlugin
	{
		public override IPluginCommand[] Commands { get; } = new[]
		{
			new ExplosionCommand(),
		};
	}
}
