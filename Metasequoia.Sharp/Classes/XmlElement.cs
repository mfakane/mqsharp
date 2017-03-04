using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Metasequoia
{
	public class XmlElement : IEnumerable<XmlElement>
	{
		public IntPtr Handle { get; }

		public XmlElement ParentElement
		{
			get
			{
				var ptr = new IntPtr[1];

				NativeMethods.MQXmlElem_Value(Handle, MQXmlelem.GetParentElement, ptr);

				return FromHandle(ptr[0]);
			}
		}

		public string Name
		{
			get
			{
				var ptr = new IntPtr[1];

				NativeMethods.MQXmlElem_Value(Handle, MQXmlelem.GetName, ptr);

				return ReadUTF8String(ptr[0]);
			}
		}

		public string Text
		{
			get
			{
				var ptr = new IntPtr[1];

				NativeMethods.MQXmlElem_Value(Handle, MQXmlelem.GetText, ptr);

				return ReadUTF8String(ptr[0]);
			}
			set
			{
				using (var valuePtr = new UTF8String(value))
				{
					var ptr = new IntPtr[] { valuePtr };

					NativeMethods.MQXmlElem_Value(Handle, MQXmlelem.SetText, ptr);
				}
			}
		}

		XmlElement(IntPtr ptr) =>
			Handle = ptr;

		string ReadUTF8String(IntPtr ptr)
		{
			if (ptr == IntPtr.Zero) return null;

			var bytes = new List<byte>();

			for (var i = 0; ; i++)
			{
				var b = Marshal.ReadByte(ptr + i);

				if (b == 0)
					break;

				bytes.Add(b);
			}

			return Encoding.UTF8.GetString(bytes.ToArray());
		}

		public static XmlElement FromHandle(IntPtr ptr) => ptr == IntPtr.Zero ? null : new XmlElement(ptr);

		public XmlElement Add(string name)
		{
			using (var namePtr = new UTF8String(name))
			{
				var ptr = new IntPtr[] { namePtr, IntPtr.Zero };

				NativeMethods.MQXmlElem_Value(Handle, MQXmlelem.AddChildElement, ptr);

				return FromHandle(ptr[1]);
			}
		}

		public bool Remove(XmlElement child)
		{
			using (var result = new PinnedStructure<bool>())
			{
				var ptr = new IntPtr[] { child.Handle, result };

				NativeMethods.MQXmlElem_Value(Handle, MQXmlelem.RemoveChildElement, ptr);

				return result.Value;
			}
		}

		public XmlElement FirstChildElement()
		{
			var ptr = new IntPtr[2];

			NativeMethods.MQXmlElem_Value(Handle, MQXmlelem.FirstChildElement, ptr);

			return FromHandle(ptr[1]);
		}

		public XmlElement FirstChildElement(string name)
		{
			using (var namePtr = new UTF8String(name))
			{
				var ptr = new IntPtr[] { namePtr, IntPtr.Zero };

				NativeMethods.MQXmlElem_Value(Handle, MQXmlelem.FirstChildElement, ptr);

				return FromHandle(ptr[1]);
			}
		}

		public XmlElement NextChildElement(XmlElement child)
		{
			var ptr = new IntPtr[] { IntPtr.Zero, child.Handle, IntPtr.Zero };

			NativeMethods.MQXmlElem_Value(Handle, MQXmlelem.NextChildElement, ptr);

			return FromHandle(ptr[2]);
		}

		public XmlElement NextChildElement(XmlElement child, string name)
		{
			using (var namePtr = new UTF8String(name))
			{
				var ptr = new IntPtr[] { namePtr, child.Handle, IntPtr.Zero };

				NativeMethods.MQXmlElem_Value(Handle, MQXmlelem.NextChildElement, ptr);

				return FromHandle(ptr[2]);
			}
		}

		public string GetAttribute(string name)
		{
			using (var namePtr = new UTF8String(name))
			{
				var ptr = new IntPtr[] { namePtr, IntPtr.Zero };

				NativeMethods.MQXmlElem_Value(Handle, MQXmlelem.GetAttribute, ptr);

				return ReadUTF8String(ptr[1]);
			}
		}

		public void SetAttribute(string name, string value)
		{
			using (var namePtr = new UTF8String(name))
			using (var valuePtr = new UTF8String(value))
			{
				var ptr = new IntPtr[] { namePtr, valuePtr };

				NativeMethods.MQXmlElem_Value(Handle, MQXmlelem.SetAttribute, ptr);
			}
		}

		public IEnumerable<XmlElement> Elements(string name)
		{
			var elem = FirstChildElement(name);

			while (elem != null)
			{
				yield return elem;

				elem = NextChildElement(elem, name);
			}
		}

		public IEnumerator<XmlElement> GetEnumerator()
		{
			var elem = FirstChildElement();

			while (elem != null)
			{
				yield return elem;

				elem = NextChildElement(elem);
			}
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
		public override bool Equals(object obj) => obj is XmlElement elem && elem.Handle == Handle;
		public override int GetHashCode() => typeof(XmlElement).GetHashCode() ^ Handle.GetHashCode();
	}
}
