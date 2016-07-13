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
