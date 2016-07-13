/*
 * ReadOnlyArray2D.cs
 * Copyright (c) 2016 Markus Himmel
 * This file is distributed under the terms of the MIT license.
 */

using System;

namespace Ten
{
	public class ReadOnlyArray2D<T>
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
