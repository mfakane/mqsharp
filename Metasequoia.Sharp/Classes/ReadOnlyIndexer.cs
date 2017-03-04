using System;
using System.Collections;
using System.Collections.Generic;

namespace Metasequoia
{
	public class ReadOnlyIndexer<T> : IReadOnlyList<T>
	{
		readonly Func<int, T> getValue;
		readonly Func<int> getCount;

		public T this[int index] => getValue(index);
		public int Count => getCount();

		public ReadOnlyIndexer(Func<int> getCount, Func<int, T> getValue)
		{
			this.getCount = getCount;
			this.getValue = getValue;
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
