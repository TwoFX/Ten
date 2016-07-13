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
			new Application().Run(new Selector());
		}
	}
}
