using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ten
{
	class RandomBot : IMoveProvider
	{
		private Random rng = new Random();

		public Move GetNextMove(IReadOnlyGameState state)
		{
			Move m;
			do
			{
				m = new Move(rng.Next(0, 3), rng.Next(0, 10), rng.Next(0, 10));
			} while (!state.IsValidMove(m));
			return m;
		}
	}
}
