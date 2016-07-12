using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Ten
{
	class Program
	{
		static int sizeX = 10;
		static int sizeY = 10;
		static int moves = 3;

		[STAThread]
		static void Main(string[] args)
		{
			var app = new Application();
			var win = new GameWindow(sizeX, sizeY);
			Thread gameThread = new Thread(RunGame);
			gameThread.Start(win);
			app.Run(win);
		}

		static void RunGame(object gameWindow)
		{
			GameWindow win = gameWindow as GameWindow;
			var ci = new ConsoleInteraction();
			var bot = new FirstBot();
			var game = new Game(sizeX, sizeY, moves, bot);
			if (win != null)
				game.AddObserver(win);
			game.AddObserver(ci);
			game.Run(100);
		}
	}
}
