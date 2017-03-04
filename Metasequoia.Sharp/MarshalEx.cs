using System;
using System.Runtime.InteropServices;

namespace Metasequoia
{
	static class MarshalEx
	{
		public static int ReadInt32(IntPtr ptr, int defaultValue) =>
			ptr == IntPtr.Zero ? defaultValue : Marshal.ReadInt32(ptr);
	}
}
