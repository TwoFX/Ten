using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ten
{
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	public sealed class HiddenFromSelectorAttribute : Attribute
	{
	}
}
