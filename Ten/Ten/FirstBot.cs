/*
 * FirstBot.cs
 * Copyright (c) 2016 Markus Himmel
 * This file is distributed under the terms of the MIT license.
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace Ten
{
	[SelectorName("My First Bot")]
	class FirstBot : IMoveProvider
	{
		private int fieldSizeX, fieldSizeY;

		public FirstBot(GameParameters pars)
		{
			fieldSizeX = pars.FieldSizeX;
			fieldSizeY = pars.FieldSizeY;
		}

		private int scoreMove(IReadOnlyGameState state, Move m)
		{
			List<Tuple<int, int>> occ = Enumerable.Range(0, Tile.TILE_BOUNDS)
				.SelectMany(i => Enumerable.Range(0, Tile.TILE_BOUNDS).Select(j => new { i, j }))
				.Where(p => state.NextMoves[m.Choice].Contention[p.i, p.j])
				.Select(x => Tuple.Create(m.X + x.i, m.Y + x.j)).ToList();

			int score = 0;
			for (int i = 0; i < fieldSizeX; i++)
			{
				if (Enumerable.Range(0, fieldSizeY).All(j => state.Field[i, j] != null || occ.Contains(Tuple.Create(i, j))))
					score += 10;

				if (Enumerable.Range(0, fieldSizeY).All(j => state.Field[i, j] == null) && Enumerable.Range(0, fieldSizeY).Any(j => occ.Contains(Tuple.Create(i, j))))
					score += -1;
			}

			for (int j = 0; j < fieldSizeY; j++)
			{
				if (Enumerable.Range(0, fieldSizeX).All(i => state.Field[i, j] != null || occ.Contains(Tuple.Create(i, j))))
					score += 10;

				if (Enumerable.Range(0, fieldSizeX).All(i => state.Field[i, j] == null) && Enumerable.Range(0, fieldSizeX).Any(i => occ.Contains(Tuple.Create(i, j))))
					score += -2;
			}

			foreach (Tile cand in Tile.All)
			{
				for (int i = 0; i < fieldSizeX; i++)
				{
					for (int j = 0; j < fieldSizeY; j++)
					{
						if (Enumerable.Range(0, Tile.TILE_BOUNDS).SelectMany(x => Enumerable.Range(0, Tile.TILE_BOUNDS).Select(y => new { x, y }))
							.Where(p => cand.Contention[p.x, p.y]).All(p => i + p.x < fieldSizeX && j + p.y < fieldSizeY && state.Field[i + p.x, j + p.y] == null && !occ.Contains(Tuple.Create(i + p.x, j + p.y))))
							goto found;
					}
				}
				score -= (state.NextMoves.Count(x => x == cand) > ((cand == state.NextMoves[m.Choice]) ? 1 : 0)) ? 10 : 5;
				found:;
			}

			score += state.NextMoves[m.Choice].Contention.AsFlatEnumerable().Count(x => x);

			return score;
		}

		public Move GetNextMove(IReadOnlyGameState state)
		{
			List<Move> candidates = Enumerable.Range(0, state.NextMoves.Count)
				.SelectMany(c => Enumerable.Range(0, fieldSizeX)
				.SelectMany(x => Enumerable.Range(0, fieldSizeY)
				.Select(y => new Move(c, x, y))))
				.Where(m => state.IsValidMove(m)).ToList();

			var e = candidates.OrderByDescending(x => scoreMove(state, x)).First();
			Console.WriteLine($"Selecting {e.Choice} {e.X} {e.Y} with score {scoreMove(state, e)}.");
			return e;
		}
	}
}
