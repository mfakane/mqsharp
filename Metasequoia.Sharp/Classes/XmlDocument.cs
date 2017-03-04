using System;

namespace Metasequoia
{
	public sealed class XmlDocument : IDisposable
	{
		public IntPtr Handle { get; }

		public XmlElement Root
		{
			get
			{
				var ptr = new IntPtr[1];

				NativeMethods.MQXmlDoc_Value(Handle, MQXmldoc.GetRootElement, ptr);

				return XmlElement.FromHandle(ptr[0]);
			}
		}

		public XmlDocument()
		{
			var ptr = new IntPtr[1];

			NativeMethods.MQXmlDoc_Value(Handle, MQXmldoc.Create, ptr);
			Handle = ptr[0];
		}

		XmlDocument(IntPtr ptr) =>
			Handle = ptr;

		public static XmlDocument FromHandle(IntPtr ptr) => ptr == IntPtr.Zero ? null : new XmlDocument(ptr);

		public XmlElement CreateRootElement(string name)
		{
			using (var namePtr = new UTF8String(name))
			{
				var ptr = new IntPtr[] { namePtr, IntPtr.Zero };

				NativeMethods.MQXmlDoc_Value(Handle, MQXmldoc.CreateRootElement, ptr);

				return XmlElement.FromHandle(ptr[1]);
			}
		}

		public bool Load(string filename)
		{
			using (var filenamePtr = new UnicodeString(filename))
			using (var result = new PinnedStructure<bool>())
			{
				var ptr = new IntPtr[] { filenamePtr, result };

				NativeMethods.MQXmlDoc_Value(Handle, MQXmldoc.Load, ptr);

				return result.Value;
			}
		}

		public bool Save(string filename)
		{
			using (var filenamePtr = new UnicodeString(filename))
			using (var result = new PinnedStructure<bool>())
			{
				var ptr = new IntPtr[] { filenamePtr, result };

				NativeMethods.MQXmlDoc_Value(Handle, MQXmldoc.Save, ptr);

				return result.Value;
			}
		}

		public void Dispose() =>
			NativeMethods.MQXmlDoc_Value(Handle, MQXmldoc.Delete, null);

		public override bool Equals(object obj) => obj is XmlDocument doc && doc.Handle == Handle;
		public override int GetHashCode() => typeof(XmlDocument).GetHashCode() ^ Handle.GetHashCode();
	}
}
