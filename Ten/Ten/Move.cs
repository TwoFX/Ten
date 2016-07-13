/*
 * Move.cs
 * Copyright (c) 2016 Markus Himmel
 * This file is distributed under the terms of the MIT license.
 */

namespace Ten
{
	public class Move
	{
		public Move(int Choice, int X, int Y)
		{
			this.Choice = Choice;
			this.X = X;
			this.Y = Y;
		}

		public int Choice
		{
			get;
			private set;
		}

		public int X
		{
			get;
			private set;
		}

		public int Y
		{
			get;
			private set;
		}
	}

	enum MoveResult
	{
		OK,
		Invalid,
		GameEnded
	}
	
}
