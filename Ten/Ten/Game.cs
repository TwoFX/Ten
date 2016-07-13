/*
 * Game.cs
 * Copyright (c) 2016 Markus Himmel
 * This file is distributed under the terms of the MIT license.
 */

using System.Collections.Generic;
using System.Threading;

namespace Ten
{
	class Game
	{
		private List<IGameStateObserver> observers = new List<IGameStateObserver>();
		private GameState state;
		private IMoveProvider provider;

		public Game(GameParameters pars, IMoveProvider provider)
		{
			state = new GameState(pars.FieldSizeX, pars.FieldSizeY, pars.NumMoves);
			this.provider = provider;
		}

		public void Run(int moveDelay = 0)
		{
			notifyObservers();
			MoveResult res;
			do
			{
				do
				{
					res = state.TryApplyMove(provider.GetNextMove(state));
				} while (res == MoveResult.Invalid);
				notifyObservers();
				Thread.Sleep(moveDelay);
			} while (res != MoveResult.GameEnded);
		}

		public void AddObserver(IGameStateObserver observer)
		{
			observers.Add(observer);
		}

		public bool RemoveObserver(IGameStateObserver observer)
		{
			return observers.Remove(observer);
		} 

		private void notifyObservers()
		{
			foreach (var observer in observers)
			{
				observer.Notify(state);
			}
		}
	}
}
