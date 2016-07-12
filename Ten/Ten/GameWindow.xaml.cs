using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Ten
{
	/// <summary>
	/// Interaction logic for GameWindow.xaml
	/// </summary>
	public partial class GameWindow : Window, IGameStateObserver
	{
		private int fieldSizeX, fieldSizeY;
		private const int BLOCK_SIZE = 50;
		private const int OFFSET_LEFT = 10;
		private const int OFFSET_TOP = 100;
		private const int OFFSET_BOTTOM = 12;
		private const int OFFSET_RIGHT = 10;
		private const int BLOCK_RADIUS = 8;
		private const int BLOCK_DISTANCE = 2;
		private readonly Color empty = Color.FromRgb(235, 235, 235);
		private Rectangle[,] field;

		public GameWindow(int fieldSizeX, int fieldSizeY)
		{
			InitializeComponent();

			this.fieldSizeX = fieldSizeX;
			this.fieldSizeY = fieldSizeY;
			field = new Rectangle[fieldSizeX, fieldSizeY];

			for (byte i = 0; i < fieldSizeX; i++)
			{
				for (int j = 0; j < fieldSizeY; j++)
				{
					var h = new Rectangle();
					h.Fill = new SolidColorBrush(empty);
					h.Width = BLOCK_SIZE;
					h.Height = BLOCK_SIZE;
					h.HorizontalAlignment = HorizontalAlignment.Left;
					h.VerticalAlignment = VerticalAlignment.Top;
					h.Margin = new Thickness(OFFSET_LEFT + j * (BLOCK_SIZE + BLOCK_DISTANCE), OFFSET_TOP + i * (BLOCK_SIZE + BLOCK_DISTANCE), 0, 0);
					h.RadiusX = BLOCK_RADIUS;
					h.RadiusY = BLOCK_RADIUS;
					grid.Children.Add(h);
					field[i, j] = h;
				}
			}

			pts.HorizontalContentAlignment = HorizontalAlignment.Center;
			pts.Margin = new Thickness(OFFSET_LEFT, 10, 0, 0);
			pts.Width = fieldSizeY * (BLOCK_SIZE + BLOCK_DISTANCE) - BLOCK_DISTANCE;
			pts.Content = "0";

			SizeToContent = SizeToContent.WidthAndHeight;
			grid.Height = OFFSET_TOP + fieldSizeX * BLOCK_SIZE + (fieldSizeX - 1) * BLOCK_DISTANCE + OFFSET_BOTTOM;
			grid.Width = OFFSET_LEFT + fieldSizeY * BLOCK_SIZE + (fieldSizeY - 1) * BLOCK_DISTANCE + OFFSET_RIGHT;
		}

		public void Notify(IReadOnlyGameState state)
		{
			for (int i = 0; i < fieldSizeX; i++)
			{
				for (int j = 0; j < fieldSizeY; j++)
				{
					Dispatcher.Invoke(() => field[i, j].Fill = new SolidColorBrush(state.Field[i, j].HasValue ? state.Field[i, j].Value : empty));
				}
			}
			Dispatcher.Invoke(() => pts.Content = state.Score);
			Dispatcher.Invoke(() => pts.Foreground = new SolidColorBrush(state.IsGameRunning ? Color.FromRgb(92, 190, 228) : Color.FromRgb(220, 101, 85)));
		}
	}
}
