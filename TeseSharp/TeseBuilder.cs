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

		public Tese Create()
		{
			return new Tese();
		}
	}
}