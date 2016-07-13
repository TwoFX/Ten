/*
 * GameParameters.cs
 * Copyright (c) 2016 Markus Himmel
 * This file is distributed under the terms of the MIT license.
 */

namespace Ten
{
	public class GameParameters
	{
		public int FieldSizeX
		{
			get;
			private set;
		}

		public int FieldSizeY
		{
			get;
			private set;
		}

		public int NumMoves
		{
			get;
			private set;
		}

		public GameParameters(int fieldSizeX, int fieldSizeY, int numMoves)
		{
			FieldSizeX = fieldSizeX;
			FieldSizeY = fieldSizeY;
			NumMoves = numMoves;
		}
	}
}
