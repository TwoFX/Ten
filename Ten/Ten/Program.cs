using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ten
{
	class Program
	{
		static void Main(string[] args)
		{
			var ci = new ConsoleInteraction();
			var game = new Game(10, 10, 3, ci);
			game.AddObserver(ci);
			game.Run();
			Console.WriteLine("Done");
			Console.ReadKey();
		}
	}
}
