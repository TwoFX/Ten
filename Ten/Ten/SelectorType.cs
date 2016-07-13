/*
 * SelectorType.cs
 * Copyright (c) 2016 Markus Himmel
 * This file is distributed under the terms of the MIT license.
 */

using System;
using System.Reflection;

namespace Ten
{
	class SelectorType
	{
		public Type Type
		{
			get;
			private set;
		}

		private string displayName;

		public SelectorType(Type type)
		{
			Type = type;
			displayName = Type
				.GetCustomAttribute<SelectorNameAttribute>()
				?.SelectorName
				?? type.ToString();
		}

		public override string ToString()
		{
			return displayName;
		}
	}
}
