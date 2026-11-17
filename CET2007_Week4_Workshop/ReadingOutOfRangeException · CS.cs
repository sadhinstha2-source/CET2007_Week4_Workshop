using System;

namespace CET2007_Week4_Workshop
{
	public class ReadingOutOfRangeException : Exception
	{
		public ReadingOutOfRangeException(string message) : base(message)
		{
		}
	}
}