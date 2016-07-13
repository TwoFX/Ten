/*
 * SelectorNameAttribute.cs
 * Copyright (c) 2016 Markus Himmel
 * This file is distributed under the terms of the MIT license.
 */

using System;

namespace Ten
{
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	public sealed class SelectorNameAttribute : Attribute
	{
		public string SelectorName
		{
			get;
			private set;
		}

		public SelectorNameAttribute(string selectorName)
		{
			SelectorName = selectorName;
		}
	}
}
