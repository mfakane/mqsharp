using System;

namespace Metasequoia
{
	public interface IStationPlugin : IPlugin
    {
		string Caption { get; }
		IStationPluginCommand[] Commands { get; }
		bool OnEvent(Document doc, MQEvent eventType, IntPtr option);
    }
}
