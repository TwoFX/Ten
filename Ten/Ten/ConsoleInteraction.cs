﻿/*
 * ConsoleInteraction.cs
 * Copyright (c) 2016 Markus Himmel
 * This file is distributed under the terms of the MIT license.
 */

using System;
using System.Linq;

namespace Ten
{
	[SingleInstance]
	[SelectorName("Console Interface")]
	class ConsoleInteraction : IGameStateObserver, IMoveProvider
	{
		private int fieldSizeX, fieldSizeY;

		public ConsoleInteraction(GameParameters pars)
		{
			fieldSizeX = pars.FieldSizeX;
			fieldSizeY = pars.FieldSizeY;
		}

		public void Notify(IReadOnlyGameState state)
		{
			Console.WriteLine($"Score: {state.Score}");
			for (int i = 0; i < fieldSizeX; i++)
			{
				for (int j = 0; j < fieldSizeY; j++)
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
			int[] inp = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
			return new Move(inp[0], inp[1], inp[2]);
		}
	}
}
