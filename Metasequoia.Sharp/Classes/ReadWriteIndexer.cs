using System;
using System.Collections;
using System.Collections.Generic;

namespace Metasequoia
{
	public class ReadWriteIndexer<T> : IReadOnlyList<T>
	{
		readonly Func<int, T> getValue;
		readonly Action<int, T> setValue;
		readonly Func<int> getCount;

		public T this[int index]
		{
			get => getValue(index);
			set => setValue(index, value);
		}

		public int Count => getCount();

		public ReadWriteIndexer(Func<int> getCount, Func<int, T> getValue, Action<int, T> setValue)
		{
			this.getCount = getCount;
			this.getValue = getValue;
			this.setValue = setValue;
		}

		public IEnumerator<T> GetEnumerator()
		{
			var count = Count;

			for (int i = 0; i < count; i++)
				yield return this[i];
		}

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}
