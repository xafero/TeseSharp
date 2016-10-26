using System;

namespace TeseSharp
{
	public class TeseBuilder
	{
		private bool skipNull;

		public TeseBuilder SkipNull(bool skipNull)
		{
			this.skipNull = skipNull;
			return this;
		}

		public bool IsSkipNull { get { return skipNull; } }

		public Tese Create()
		{
			return new Tese();
		}
	}
}