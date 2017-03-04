using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Metasequoia
{
	class UTF8String : SafeHandle
	{
		public override bool IsInvalid =>
			handle == IntPtr.Zero;

		public UTF8String(string value)
			: base(IntPtr.Zero, true)
		{
			if (value != null)
			{
				var bytes = Encoding.UTF8.GetBytes(value);

				handle = Marshal.AllocCoTaskMem(bytes.Length + 1);
				Marshal.Copy(bytes, 0, handle, bytes.Length);
				Marshal.WriteByte(handle + bytes.Length, 0);
			}
		}

		protected override bool ReleaseHandle()
		{
			if (handle != IntPtr.Zero)
				Marshal.FreeCoTaskMem(handle);

			return true;
		}

		public static implicit operator IntPtr(UTF8String ptr) =>
			ptr.DangerousGetHandle();
	}
}
