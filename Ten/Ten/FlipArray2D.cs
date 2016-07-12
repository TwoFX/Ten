﻿namespace Ten
{
	class FlipArray2D<T>
	{
		private T[,] underlying;
		private bool flip;

		public FlipArray2D(T[,] source, bool flip = true)
		{
			underlying = source;
			this.flip = flip;
		}

		public int GetLength(int dimension)
		{
			return underlying.GetLength(1 - dimension);
		}

		public T this[int i, int j]
		{
			get
			{
				return flip ? underlying[j, i] : underlying[i, j];
			}

			set
			{
				if (flip)
					underlying[j, i] = value;
				else
					underlying[i, j] = value;
			}
		}
	}
}