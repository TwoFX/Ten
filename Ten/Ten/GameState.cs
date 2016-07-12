using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace Ten
{
	class GameState : IReadOnlyGameState
	{
		private Random rng = new Random();

		public Color?[,] Field { get; private set; }
		public Tile[] NextMoves { get; private set; }
		public int Score { get; private set; }
		public bool IsGameRunning { get; private set; }
		public int FieldSizeX { get; private set; }
		public int FieldSizeY { get; private set; }
		
		public GameState(int fieldSizeX, int fieldSizeY, int numMoves)
		{
			this.FieldSizeX = fieldSizeX;
			this.FieldSizeY = fieldSizeY;
			Field = new Color?[fieldSizeX, fieldSizeY];
			NextMoves = new Tile[numMoves];
			Score = 0;
			IsGameRunning = true;
			generateTiles();
		}

		private void generateTiles()
		{
			for (int i = 0; i < NextMoves.Length; i++)
			{
				NextMoves[i] = Tile.All[rng.Next(0, Tile.All.Count)];
			}
		}

		private IEnumerable<int> checkRows(FlipArray2D<Color?> source)
		{
			int rows = source.GetLength(0);
			int cols = source.GetLength(1);
			return Enumerable.Range(0, rows).Where(i => Enumerable.Range(0, cols).All(j => source[i, j] != null));
		}

		private void clearRows(FlipArray2D<Color?> source, IEnumerable<int> rows)
		{
			int cols = source.GetLength(1);

			foreach (int i in rows)
			{
				for (int j = 0; j < cols; j++)
				{
					source[i, j] = null;
				}
			}
		}

		private bool isValidMove(Move move)
		{
			if (NextMoves[move.Choice] == null)
				return false;

			for (int x = 0; x < Tile.TILE_BOUNDS; x++)
			{
				for (int y = 0; y < Tile.TILE_BOUNDS; y++)
				{
					if (NextMoves[move.Choice].Contention[x, y] &&
						(move.X + x >= FieldSizeX || move.Y + y >= FieldSizeY ||
						Field[move.X + x, move.Y + y] != null))
						return false;
				}
			}
			return true;
		} 

		public MoveResult TryApplyMove(Move move)
		{
			// Invalid move: At least one block is alreay occupied
			if (!isValidMove(move))
				return MoveResult.Invalid;

			// Commit the move to the field
			foreach (var loc in Enumerable.Range(0, Tile.TILE_BOUNDS).SelectMany(x => Enumerable.Range(0, Tile.TILE_BOUNDS).Select(y => new { x, y }))
				.Where(p => NextMoves[move.Choice].Contention[p.x, p.y]))
			{
				Field[move.X + loc.x, move.Y + loc.y] = NextMoves[move.Choice].Color;
			}

			// One point is awarded for each filled tile
			Score += NextMoves[move.Choice].Contention.AsFlatEnumerable().Count(x => x);

			// Remove choice and generate new choices if needed
			NextMoves[move.Choice] = null;
			if (NextMoves.All(x => x == null))
				generateTiles();

			// Check for complete rows and columns and then clear them.
			// The reason why this is split into two methods is that
			// there are cases where a single block clears both rows and
			// columns. If the first checked, say rows and then immediately
			// deleted these rows, then the cleared columns would not be
			// recognized. As the row checks are lazy by default, we have
			// to be extra careful to eagerly evaluate the candidates.
			var rowArr = new FlipArray2D<Color?>(Field, false);
			var colArr = new FlipArray2D<Color?>(Field, true);

			var rows = checkRows(rowArr).ToList();
			var cols = checkRows(colArr).ToList();

			clearRows(rowArr, rows);
			clearRows(colArr, cols);

			// The scoring system for cleared tiles in the original 1010!
			// game assumes a square board. Here it how it awards points:
			// Let s be the the total number of cleared rows and columns.
			// Then the number of points awarded for a cleared row is
			// 5 * s * (s + 1). In other words, you get 10 points for the
			// first cleared row, 20 points for the second cleared row,
			// etc. If you clear 3 rows/columns, you get
			// 10 + 20 + 30 = 5 * 3 * (3 + 1) = 60 points in addition to
			// the points you get for placing the last tile.

			// This approach does not really work when the board is
			// non-square. The approach used at the moment is a kludge:
			// calculate the average of height and width of the board,
			// round that down and use it as the basis for the calculation
			// described above. The problem with finding a better approach
			// is that the ordering in which rows and columns are counted
			// starts mattering when the board is non-square.
			Score += ((FieldSizeX + FieldSizeY) / 4)
				* (rows.Count + cols.Count) * (rows.Count + cols.Count + 1);

			// Check if the game has ended
			for (int c = 0; c < NextMoves.Length; c++)
			{
				for (int x = 0; x < FieldSizeX; x++)
				{
					for (int y = 0; y < FieldSizeY; y++)
					{
						// There is at least one valid move left, so the game can continue
						if (isValidMove(new Move(c, x, y)))
							return MoveResult.OK;
					}
				}
			}

			return MoveResult.GameEnded;
		}

		ReadOnlyCollection<Tile> IReadOnlyGameState.NextMoves
		{
			get
			{
				return Array.AsReadOnly(NextMoves);
			}
		}

		ReadOnlyArray2D<Color?> IReadOnlyGameState.Field
		{
			get
			{
				return new ReadOnlyArray2D<Color?>(Field);
			}
		}
	}
}
