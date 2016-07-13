using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
