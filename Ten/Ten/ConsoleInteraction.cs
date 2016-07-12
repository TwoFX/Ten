using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ten
{
	class ConsoleInteraction : IGameStateObserver, IMoveProvider
	{
		public void Notify(IReadOnlyGameState state)
		{
			Console.WriteLine($"Score: {state.Score}");
			for (int i = 0; i < state.FieldSizeX; i++)
			{
				for (int j = 0; j < state.FieldSizeY; j++)
				{
					Console.Write(state.Field[i, j] != null ? '#' : '.');
				}
				Console.WriteLine();
			}

			foreach (Tile tile in state.NextMoves)
			{
				if (tile != null)
				{
					for (int i = 0; i < Tile.TILE_BOUNDS; i++)
					{
						for (int j = 0; j < Tile.TILE_BOUNDS; j++)
						{
							Console.Write(tile.Contention[i, j] ? '#' : ' ');
						}
						Console.WriteLine();
					}
				}
				else
				{
					Console.WriteLine("(used)");
				}
			}
		}

		public Move GetNextMove(IReadOnlyGameState state)
		{
			Console.Write("Please enter your move (c x y): ");
			int[] inp = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
			return new Move(inp[0], inp[1], inp[2]);
		}
	}
}
