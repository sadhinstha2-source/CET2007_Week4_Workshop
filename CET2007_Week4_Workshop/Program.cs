using System;

namespace CET2007_Week4_Workshop
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("=== Exercise 1: Handling Invalid Sensor Readings ===\n");

			string[] sensorNames = { "Temperature", "Humidity", "Motion" };
			string[] rawReadings = { "22.5", "bad_data", "-5.0" };

			SensorReading[] readings = new SensorReading[sensorNames.Length];

			for (int i = 0; i < sensorNames.Length; i++)
			{
				double value = 0.0;
				try
				{
					value = Convert.ToDouble(rawReadings[i]);

					if (value < 0)
						throw new ArgumentOutOfRangeException("value", "Sensor reading cannot be negative.");
				}
				catch (FormatException)
				{
					Console.WriteLine("Invalid number format for " + sensorNames[i] + ". Setting value to 0.0.");
					value = 0.0;
				}
				catch (ArgumentOutOfRangeException ex)
				{
					Console.WriteLine("Error for " + sensorNames[i] + ": " + ex.Message + " Setting value to 0.0.");
					value = 0.0;
				}

				readings[i] = new SensorReading(sensorNames[i], value);
			}

			Console.WriteLine("\n--- Final Sensor Readings ---");
			for (int i = 0; i < readings.Length; i++)
				Console.WriteLine(readings[i].ToString());

			Console.WriteLine("\nProgram finished successfully.");
			Console.ReadKey();
		}
	}
}