/*
 * IMoveProvider.cs
 * Copyright (c) 2016 Markus Himmel
 * This file is distributed under the terms of the MIT license.
 */

namespace Ten
{
	interface IMoveProvider
	{
		Move GetNextMove(IReadOnlyGameState state);
	}
}
