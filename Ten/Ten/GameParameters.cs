using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ten
{
	public class GameParameters
	{
		public int FieldSizeX
		{
			get;
			private set;
		}

		public int FieldSizeY
		{
			get;
			private set;
		}

		public int NumMoves
		{
			get;
			private set;
		}

		public GameParameters(int fieldSizeX, int fieldSizeY, int numMoves)
		{
			FieldSizeX = fieldSizeX;
			FieldSizeY = fieldSizeY;
			NumMoves = numMoves;
		}
	}
}
