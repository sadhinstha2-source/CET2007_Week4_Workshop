using System;

namespace CET2007_Week4_Workshop
{
	public class ReadingOutOfRangeException : Exception
	{
		public double AttemptedValue { get; }
		public double Min { get; }
		public double Max { get; }

		public ReadingOutOfRangeException(string message, double attemptedValue, double min, double max)
			: base(message)
		{
			AttemptedValue = attemptedValue;
			Min = min;
			Max = max;
		}
	}
}
