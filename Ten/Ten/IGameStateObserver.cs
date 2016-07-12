namespace Ten
{
	interface IGameStateObserver
	{
		void Notify(IReadOnlyGameState state);
	}
}
