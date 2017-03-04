using System;
using System.Collections;
using System.Collections.Generic;

namespace Metasequoia
{
	public sealed class ObjectVertexCollection : IList<Vertex>, IReadOnlyList<Vertex>
	{
		public Object Object { get; }
		public int Count => NativeMethods.MQObj_GetVertexCount(Object.Handle);
		bool ICollection<Vertex>.IsReadOnly => false;

		public Vertex this[int index]
		{
			get => Vertex.FromIndex(Object, index);
			set
			{
				var v = this[index];

				v.CopyAttributesFrom(value);
				v.Position = value.Position;
			}
		}

		public ObjectVertexCollection(Object obj) =>
			Object = obj;

		void ICollection<Vertex>.Add(Vertex item) =>
			throw new NotSupportedException();
		void ICollection<Vertex>.CopyTo(Vertex[] array, int arrayIndex) =>
			throw new NotSupportedException();
		void IList<Vertex>.Insert(int index, Vertex item) =>
			throw new NotSupportedException();

		public void Optimize(float distance) =>
			Optimize(distance, null);

		public void Optimize(float distance, bool[] apply) =>
			NativeMethods.MQObj_OptimizeVertex(Object.Handle, distance, apply);

		public Vertex Add(Point item) =>
			this[NativeMethods.MQObj_AddVertex(Object.Handle, ref item)];

		public Vertex Add(Point item, Vertex copyAttributesFrom)
		{
			var rt = Add(item);

			rt.CopyAttributesFrom(copyAttributesFrom);

			return rt;
		}

		public void Clear()
		{
			while (Count > 0)
				RemoveAt(0);
		}

		public bool Contains(Vertex item) =>
			IndexOf(item) != -1;

		public int IndexOf(Vertex item) =>
			NativeMethods.MQObj_GetVertexIndexFromUniqueID(Object.Handle, item.UniqueId);

		public bool Remove(Vertex item)
		{
			var idx = IndexOf(item);

			if (idx != -1)
			{
				RemoveAt(idx);

				return true;
			}

			return false;
		}

		public void RemoveAt(int index) =>
			NativeMethods.MQObj_DeleteVertex(Object.Handle, index, true);

		public IEnumerator<Vertex> GetEnumerator()
		{
			var count = Count;

			for (int i = 0; i < count; i++)
				yield return this[i];
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}