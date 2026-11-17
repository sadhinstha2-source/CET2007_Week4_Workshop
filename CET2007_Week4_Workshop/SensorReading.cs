using System;

namespace CET2007_Week4_Workshop
{
	public class SensorReading
	{
		public string Name { get; set; }
		public double Value { get; set; }

		public SensorReading(string name, double value)
		{
			Name = name;
			Value = value;
		}

		public override string ToString()
		{
			return "Sensor: " + Name + " - Value: " + Value;
		}
	}
}