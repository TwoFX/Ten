/*
 * FlipArray2D.cs
 * Copyright (c) 2016 Markus Himmel
 * This file is distributed under the terms of the MIT license.
 */

namespace Ten
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
			return underlying.GetLength(flip ? 1 - dimension : dimension);
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
