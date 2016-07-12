using System.Collections.ObjectModel;
using System.Windows.Media;

namespace Ten
{
	interface IReadOnlyGameState
	{
		ReadOnlyCollection<Tile> NextMoves { get; }
		ReadOnlyArray2D<Color?> Field { get; }
	}
}
