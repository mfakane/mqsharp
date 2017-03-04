using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Metasequoia
{
	public sealed class ObjectFaceCollection : IList<Face>, IReadOnlyList<Face>
	{
		public Object Object { get; }
		public int Count => NativeMethods.MQObj_GetFaceCount(Object.Handle);
		bool ICollection<Face>.IsReadOnly => false;

		public Face this[int index]
		{
			get => Face.FromIndex(Object, index);
			set => throw new NotSupportedException();
		}

		public ObjectFaceCollection(Object obj) =>
			Object = obj;

		void ICollection<Face>.Add(Face item) =>
			throw new NotSupportedException();
		void ICollection<Face>.CopyTo(Face[] array, int arrayIndex) =>
			throw new NotSupportedException();
		void IList<Face>.Insert(int index, Face item) =>
			throw new NotSupportedException();
		bool ICollection<Face>.Remove(Face item) =>
			Remove(item);
		void IList<Face>.RemoveAt(int index) =>
			RemoveAt(index);

		public Face Add(int[] vertices)
		{
			var idx = NativeMethods.MQObj_AddFace(Object.Handle, vertices.Length, vertices);

			if (idx == -1)
				throw new OutOfMemoryException();

			return Face.FromIndex(Object, idx);
		}

		public Face Add(int[] vertices, Face copyAttributesFrom)
		{
			var rt = Add(vertices);

			rt.CopyAttributesFrom(copyAttributesFrom);

			return rt;
		}

		public Face Add(IEnumerable<Vertex> vertices) =>
			Add(vertices.Select(i => i.Index).ToArray());

		public Face Add(IEnumerable<Vertex> vertices, Face copyAttributesFrom) =>
			Add(vertices.Select(i => i.Index).ToArray(), copyAttributesFrom);

		public void Clear()
		{
			while (Count > 0)
				RemoveAt(0);
		}

		public bool Contains(Face item) =>
			IndexOf(item) != -1;

		public int IndexOf(Face item) =>
			NativeMethods.MQObj_GetFaceIndexFromUniqueID(Object.Handle, item.UniqueId);

		public bool Remove(Face item, bool deleteVertices = true)
		{
			var idx = IndexOf(item);

			if (idx != -1)
			{
				RemoveAt(idx, deleteVertices);

				return true;
			}

			return false;
		}

		public void RemoveAt(int index, bool deleteVertices = true) =>
			NativeMethods.MQObj_DeleteFace(Object.Handle, index, deleteVertices);

		public IEnumerator<Face> GetEnumerator()
		{
			var count = Count;

			for (int i = 0; i < count; i++)
				yield return this[i];
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}