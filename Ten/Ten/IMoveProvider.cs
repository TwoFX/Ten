namespace Ten
{
	interface IMoveProvider
	{
		Move GetNextMove(IReadOnlyGameState state);
	}
}
