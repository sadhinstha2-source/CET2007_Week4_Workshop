using System;

namespace CET2007_Week4_Workshop
{
	public class Sensor
	{
		public string Name { get; set; }
		public double Min { get; set; }
		public double Max { get; set; }
		public bool IsOnline { get; set; }

		public Sensor(string name, double min, double max, bool isOnline)
		{
			Name = name;
			Min = min;
			Max = max;
			IsOnline = isOnline;
		}

		public void ValidateReading(double value)
		{
			if (!IsOnline)
				throw new SensorOfflineException("Sensor " + Name + " is offline.");

			if (value < Min || value > Max)
				throw new ReadingOutOfRangeException(
					"Reading " + value + " is outside range [" + Min + " .. " + Max + "] for " + Name + ".",
					value, Min, Max);
		}

		public override string ToString()
		{
			return "Sensor: " + Name + ", Range: [" + Min + " .. " + Max + "], Online: " + IsOnline;
		}
	}
}
