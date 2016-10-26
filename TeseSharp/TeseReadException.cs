using System;

namespace TeseSharp
{
	[Serializable]
	public class TeseReadException : Exception
	{
		public TeseReadException(Exception t)
			: base("", t)
		{
		}
	}
}