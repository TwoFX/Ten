/*
 * ExtensionMethods.cs
 * Copyright (c) 2016 Markus Himmel
 * This file is distributed under the terms of the MIT license.
 */

using System.Collections.Generic;

namespace Ten
{
	static class ExtensionMethods
	{
		public static IEnumerable<T> AsFlatEnumerable<T>(this T[,] source)
		{
			int sizeX = source.GetLength(0);
			int sizeY = source.GetLength(1);
			for (int i = 0; i < sizeX; i++)
			{
				for (int j = 0; j < sizeY; j++)
				{
					yield return source[i, j];
				}
			}
		}
	}
}
