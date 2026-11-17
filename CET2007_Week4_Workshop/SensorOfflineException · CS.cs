using System;

namespace CET2007_Week4_Workshop
{
	public class SensorOfflineException : Exception
	{
		public SensorOfflineException(string message) : base(message)
		{
		}
	}
}