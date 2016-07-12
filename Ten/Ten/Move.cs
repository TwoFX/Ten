namespace Ten
{
	class Move
	{
		public Move(int Choice, int X, int Y)
		{
			this.Choice = Choice;
			this.X = X;
			this.Y = Y;
		}

		public int Choice
		{
			get;
			private set;
		}

		public int X
		{
			get;
			private set;
		}

		public int Y
		{
			get;
			private set;
		}
	}

	enum MoveResult
	{
		OK,
		Invalid,
		GameEnded
	}
	
}
