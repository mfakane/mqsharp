using System;
using System.Runtime.InteropServices;

namespace Metasequoia
{
	class PinnedObject : SafeHandle
	{
		readonly GCHandle gcHandle;

		public override bool IsInvalid =>
				handle == IntPtr.Zero;

		public PinnedObject(object value)
			: base(IntPtr.Zero, true)
		{
			gcHandle = GCHandle.Alloc(value, GCHandleType.Pinned);
			handle = gcHandle.AddrOfPinnedObject();
		}

		protected override bool ReleaseHandle()
		{
			gcHandle.Free();

			return true;
		}

		public static implicit operator IntPtr(PinnedObject ptr) =>
			ptr.DangerousGetHandle();
	}
}
