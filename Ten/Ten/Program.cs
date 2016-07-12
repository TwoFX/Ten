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

		[STAThread]
		static void Main(string[] args)
		{
			var app = new Application();
			var win = new GameWindow(10, 10);
			Thread gameThread = new Thread(RunGame);
			gameThread.Start(win);
			app.Run(win);
		}

		static void RunGame(object gameWindow)
		{
			GameWindow win = gameWindow as GameWindow;
			var ci = new ConsoleInteraction();
			var bot = new RandomBot();
			var game = new Game(10, 10, 3, bot);
			if (win != null)
				game.AddObserver(win);
			game.AddObserver(ci);
			game.Run(500);
		}
	}
}
