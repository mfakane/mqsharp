using System.Runtime.InteropServices;

namespace Metasequoia
{
	/// <summary>
	/// MQUserDataInfo
	/// </summary>
	public struct UserDataInfo
    {
		public uint Size;
		public uint ProductId;
		public uint PluginId;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)] public string Identifier;
		public int UserDataType;
		public int BytesPerElement;
		public bool Create;
    }
}
