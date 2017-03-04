using System;

namespace Metasequoia
{
	/// <summary>
	/// MQSendMessageInfo
	/// </summary>
	public struct SendMessageInfo
    {
		public uint ProductId { get; set; }
		public uint PluginId { get; set; }
		public int Index { get; set; }
		public IntPtr Option { get; set; }
	}
}
