using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ten
{
	class Game
	{
		private List<IGameStateObserver> observers = new List<IGameStateObserver>();
		private GameState state;
		private IMoveProvider provider;

		public Game(int fieldSizeX, int fieldSizeY, int numMoves, IMoveProvider provider)
		{
			state = new GameState(fieldSizeX, fieldSizeY, numMoves);
			this.provider = provider;
		}

		public void Run()
		{
			MoveResult res;
			do
			{
				do
				{
					res = state.TryApplyMove(provider.GetNextMove(state));
				} while (res == MoveResult.Invalid);
				notifyObservers();
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
