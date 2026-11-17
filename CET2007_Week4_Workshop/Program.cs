using System;

namespace CET2007_Week4_Workshop
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Exercise1();
			Exercise2();

			Console.ReadKey();
		}

		static void Exercise1()
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

			Console.WriteLine("\nProgram finished successfully.\n");
		}

		static void Exercise2()
		{
			Console.WriteLine("=== Exercise 2: Custom Exceptions for Faulty Sensors ===\n");

			Sensor[] sensors =
			{
				new Sensor("Temperature", -20, 50, true),
				new Sensor("Humidity", 0, 100, true),
				new Sensor("Motion", 0, 1, false)
			};
			double[] sampleReading = { 22.5, 120.0, 1.0 };

			Console.WriteLine("Configured Sensors:");
			for (int i = 0; i < sensors.Length; i++)
				Console.WriteLine(sensors[i].ToString());

			Console.WriteLine("\n--- Validating Sample Readings ---\n");

			for (int i = 0; i < sensors.Length; i++)
			{
				Console.WriteLine("Checking " + sensors[i].Name + " with value " + sampleReading[i] + " ...");
				try
				{
					sensors[i].ValidateReading(sampleReading[i]);
					Console.WriteLine(sensors[i].Name + " reading accepted.");
				}
				catch (SensorOfflineException)
				{
					Console.WriteLine(sensors[i].Name + " sensor offline - check connection.");
				}
				catch (ReadingOutOfRangeException)
				{
					Console.WriteLine(sensors[i].Name + " reading out of range (value " + sampleReading[i] + ").");
				}
				Console.WriteLine();
			}

			Console.WriteLine("Done.");
		}
	}
}