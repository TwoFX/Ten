using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ten
{
	class FirstBot : IMoveProvider
	{
		private int scoreMove(IReadOnlyGameState state, Move m)
		{
			List<Tuple<int, int>> occ = Enumerable.Range(0, Tile.TILE_BOUNDS)
				.SelectMany(i => Enumerable.Range(0, Tile.TILE_BOUNDS).Select(j => new { i, j }))
				.Where(p => state.NextMoves[m.Choice].Contention[p.i, p.j])
				.Select(x => Tuple.Create(m.X + x.i, m.Y + x.j)).ToList();

			int score = 0;
			for (int i = 0; i < state.FieldSizeX; i++)
			{
				if (Enumerable.Range(0, state.FieldSizeY).All(j => state.Field[i, j] != null || occ.Contains(Tuple.Create(i, j))))
					score += 10;

				if (Enumerable.Range(0, state.FieldSizeY).All(j => state.Field[i, j] == null) && Enumerable.Range(0, state.FieldSizeY).Any(j => occ.Contains(Tuple.Create(i, j))))
					score += -2;
			}

			for (int j = 0; j < state.FieldSizeY; j++)
			{
				if (Enumerable.Range(0, state.FieldSizeX).All(i => state.Field[i, j] != null || occ.Contains(Tuple.Create(i, j))))
					score += 10;

				if (Enumerable.Range(0, state.FieldSizeX).All(i => state.Field[i, j] == null) && Enumerable.Range(0, state.FieldSizeX).Any(i => occ.Contains(Tuple.Create(i, j))))
					score += -5;
			}

			foreach (Tile cand in Tile.All)
			{
				for (int i = 0; i < state.FieldSizeX; i++)
				{
					for (int j = 0; j < state.FieldSizeY; j++)
					{
						if (Enumerable.Range(0, Tile.TILE_BOUNDS).SelectMany(x => Enumerable.Range(0, Tile.TILE_BOUNDS).Select(y => new { x, y }))
							.Where(p => cand.Contention[p.x, p.y]).All(p => i + p.x < state.FieldSizeX && j + p.y < state.FieldSizeY && state.Field[i + p.x, j + p.y] == null && !occ.Contains(Tuple.Create(i + p.x, j + p.y))))
							goto found;
					}
				}
				score -= 4;
				found:;
			}

			return score;
		}

		public Move GetNextMove(IReadOnlyGameState state)
		{
			List<Move> candidates = Enumerable.Range(0, state.NextMoves.Count)
				.SelectMany(c => Enumerable.Range(0, state.FieldSizeX)
				.SelectMany(x => Enumerable.Range(0, state.FieldSizeY)
				.Select(y => new Move(c, x, y))))
				.Where(m => state.IsValidMove(m)).ToList();

			return candidates.OrderByDescending(x => scoreMove(state, x)).ThenBy(x => x.X).First();
		}
	}
}
