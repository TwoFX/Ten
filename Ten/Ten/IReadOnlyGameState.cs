/*
 * IReadOnlyGameState.cs
 * Copyright (c) 2016 Markus Himmel
 * This file is distributed under the terms of the MIT license.
 */

using System.Collections.ObjectModel;
using System.Windows.Media;

namespace Ten
{
	public interface IReadOnlyGameState
	{
		ReadOnlyCollection<Tile> NextMoves { get; }
		ReadOnlyArray2D<Color?> Field { get; }

		int Score { get; }
		bool IsGameRunning { get; }

		bool IsValidMove(Move move);
	}
}
