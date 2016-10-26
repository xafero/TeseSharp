using System;
using System.Reflection;

namespace TeseSharp
{
	[Serializable]
	public class TeseWriteException : Exception
	{
		public TeseWriteException(Exception t)
			: base("", t)
		{
		}

		public TeseWriteException(FieldInfo field, Exception e)
			: base(field + "", e)
		{
		}
	}
}