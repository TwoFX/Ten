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
