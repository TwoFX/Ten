using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
