/*
 * Program.cs
 * Copyright (c) 2016 Markus Himmel
 * This file is distributed under the terms of the MIT license.
 */

using System;
using System.Windows;

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
