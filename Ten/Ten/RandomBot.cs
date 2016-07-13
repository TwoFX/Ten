/*
 * RandomBot.cs
 * Copyright (c) 2016 Markus Himmel
 * This file is distributed under the terms of the MIT license.
 */

using System;

namespace Ten
{
	[SelectorName("Random Bot")]
	class RandomBot : IMoveProvider
	{
		private Random rng = new Random();
		private int fieldSizeX, fieldSizeY;

		public RandomBot(GameParameters pars)
		{
			fieldSizeX = pars.FieldSizeX;
			fieldSizeY = pars.FieldSizeY;
		}

		public Move GetNextMove(IReadOnlyGameState state)
		{
			Move m;
			do
			{
				m = new Move(rng.Next(0, state.NextMoves.Count), rng.Next(0, fieldSizeX), rng.Next(0, fieldSizeY));
			} while (!state.IsValidMove(m));
			return m;
		}
	}
}
