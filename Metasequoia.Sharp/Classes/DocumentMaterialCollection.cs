using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Metasequoia
{
	public sealed class DocumentMaterialCollection : IList<Material>, IReadOnlyList<Material>
	{
		public Document Document { get; }
		public int Count => NativeMethods.MQDoc_GetMaterialCount(Document.Handle);
		bool ICollection<Material>.IsReadOnly => false;

		public Material this[int index]
		{
			get => Material.FromHandle(Document, NativeMethods.MQDoc_GetMaterial(Document.Handle, index));
			set => throw new NotSupportedException();
		}

		public DocumentMaterialCollection(Document document) =>
			Document = document;

		public void Add(Material item) =>
			NativeMethods.MQDoc_AddMaterial(Document.Handle, item.Handle);

		public void Clear()
		{
			while (Count > 0)
				RemoveAt(0);
		}

		public bool Contains(Material item) =>
			IndexOf(item) != -1;

		void ICollection<Material>.CopyTo(Material[] array, int arrayIndex) =>
			throw new NotSupportedException();

		public int IndexOf(Material item) =>
			this.Any(i => i.Handle == item.Handle) ? this.TakeWhile(i => i.Handle != item.Handle).Count() : -1;

		void IList<Material>.Insert(int index, Material item) =>
			throw new NotSupportedException();

		public bool Remove(Material item)
		{
			var idx = IndexOf(item);

			if (idx != -1)
			{
				NativeMethods.MQDoc_DeleteMaterial(Document.Handle, idx);

				return true;
			}

			return false;
		}

		public void RemoveAt(int index) =>
			NativeMethods.MQDoc_DeleteMaterial(Document.Handle, index);

		public IEnumerator<Material> GetEnumerator()
		{
			var count = Count;

			for (int i = 0; i < count; i++)
				yield return this[i];
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}
