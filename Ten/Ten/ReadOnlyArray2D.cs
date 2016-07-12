using System;

namespace Ten
{
	class ReadOnlyArray2D<T>
	{
		private T[,] underlying;

		public ReadOnlyArray2D(T[,] source)
		{
			if (source == null)
				throw new ArgumentNullException(nameof(source));

			underlying = source;
		}

		public T this[int i, int j]
		{
			get
			{
				return underlying[i, j];
			}
		}
	}
}
