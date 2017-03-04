using System;
using System.Collections;
using System.Collections.Generic;

namespace Metasequoia
{
	public sealed class DocumentObjectCollection : IList<Object>, IReadOnlyList<Object>
	{
		public Document Document { get; }
		public int Count => NativeMethods.MQDoc_GetObjectCount(Document.Handle);
		bool ICollection<Object>.IsReadOnly => false;

		public Object this[int index]
		{
			get => Object.FromHandle(Document, NativeMethods.MQDoc_GetObject(Document.Handle, index));
			set => throw new NotSupportedException();
		}

		public DocumentObjectCollection(Document document) =>
			Document = document;

		public void Add(Object item)
		{
			NativeMethods.MQDoc_AddObject(Document.Handle, item.Handle);
			item.Document = Document;
		}

		public void Clear()
		{
			while (Count > 0)
				RemoveAt(0);
		}

		public bool Contains(Object item) =>
			IndexOf(item) != -1;

		void ICollection<Object>.CopyTo(Object[] array, int arrayIndex) =>
			throw new NotSupportedException();

		public int IndexOf(Object item) =>
			NativeMethods.MQDoc_GetObjectIndex(Document.Handle, item.Handle);

		public void Insert(int index, Object item)
		{
			NativeMethods.MQDoc_InsertObject(Document.Handle, item.Handle, this[index].Handle);
			item.Document = Document;
		}

		public bool Remove(Object item)
		{
			var idx = IndexOf(item);

			if (idx != -1)
			{
				NativeMethods.MQDoc_DeleteObject(Document.Handle, idx);

				return true;
			}

			return false;
		}

		public void RemoveAt(int index) =>
			NativeMethods.MQDoc_DeleteObject(Document.Handle, index);

		public IEnumerator<Object> GetEnumerator()
		{
			var count = Count;

			for (int i = 0; i < count; i++)
				yield return this[i];
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}
