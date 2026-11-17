using System;

namespace CET2007_Week4_Workshop
{
	internal class Program
	{
		static void Main(string[] args)
		{
			// Exercise1();
			// Exercise2();
			// Exercise3();
			// Exercise4();
			Exercise5();

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

		static void Exercise3()
		{
			Console.WriteLine("=== Exercise 3: Exception Hierarchy Demo ===\n");

			Sensor[] sensors =
			{
				new Sensor("Temperature", -20, 50, true),
				new Sensor("Humidity", 0, 100, true),
				new Sensor("Motion", 0, 1, true)
			};

			Console.WriteLine("Configured Sensors:");
			for (int i = 0; i < sensors.Length; i++)
				Console.WriteLine(sensors[i].ToString());

			Console.WriteLine("\n--- Demonstrating Exception Handling Order ---\n");

			try
			{
				// (a) ReadingOutOfRangeException
				Console.WriteLine("Testing extreme reading for Temperature...");
				sensors[0].ValidateReading(999);

				// (b) SensorOfflineException
				Console.WriteLine("Turning off Humidity sensor...");
				sensors[1].IsOnline = false;
				sensors[1].ValidateReading(50);

				// (c) IndexOutOfRangeException (array has only 3 items: index 0..2)
				Console.WriteLine("Accessing invalid sensor index...");
				sensors[5].ValidateReading(10);
			}
			catch (ReadingOutOfRangeException ex)
			{
				Console.WriteLine("[Caught] " + ex.GetType().Name + ": " + ex.Message);
			}
			catch (SensorOfflineException ex)
			{
				Console.WriteLine("[Caught] " + ex.GetType().Name + ": " + ex.Message);
			}
			catch (IndexOutOfRangeException ex)
			{
				Console.WriteLine("[Caught] " + ex.GetType().Name + ": " + ex.Message);
			}
			catch (Exception ex)
			{
				Console.WriteLine("[Caught] " + ex.GetType().Name + ": " + ex.Message);
			}

			Console.WriteLine("\nExecution continues normally after handling exceptions.");
		}

		static void Exercise4()
		{
			Console.WriteLine("=== Exercise 4: Context-Rich Custom Exceptions ===\n");

			Sensor[] sensors =
			{
				new Sensor("Humidity", 0, 100, true),
				new Sensor("Motion", 0, 1, false)
			};

			Console.WriteLine("Configured Sensors:");
			for (int i = 0; i < sensors.Length; i++)
				Console.WriteLine(sensors[i].ToString());

			Console.WriteLine("\n--- Simulating Exceptions with Context ---\n");

			Console.WriteLine("Checking Humidity with value 120 ...");
			try
			{
				sensors[0].ValidateReading(120);
			}
			catch (ReadingOutOfRangeException ex)
			{
				Console.WriteLine("[Caught] " + ex.GetType().Name + ": " + ex.Message);
				Console.WriteLine("Details -> Attempted: " + ex.AttemptedValue +
								  ", Min: " + ex.Min + ", Max: " + ex.Max);
			}
		}

		static void Exercise5()
		{
			Console.WriteLine("=== Exercise 5: Monitoring Cycle ===\n");

			Sensor[] sensors =
			{
				new Sensor("Temperature", -20, 50, true),
				new Sensor("Humidity", 0, 100, true),
				new Sensor("Motion", 0, 1, false),
				new Sensor("Pressure", 950, 1050, true),
				new Sensor("Light", 0, 1000, true)
			};
			double[] readings = { 25, 120, 1, 900, 500 };

			for (int i = 0; i < sensors.Length; i++)
			{
				Console.WriteLine(sensors[i].ToString());
				try
				{
					sensors[i].ValidateReading(readings[i]);
					Console.WriteLine("OK: " + sensors[i].Name + " reading accepted: " + readings[i]);
				}
				// Domain exceptions
				catch (SensorOfflineException ex)
				{
					Console.WriteLine("[SensorOfflineException] " + ex.Message);
				}
				catch (ReadingOutOfRangeException ex)
				{
					Console.WriteLine("[ReadingOutOfRangeException] " + ex.Message);
					Console.WriteLine("Attempted: " + ex.AttemptedValue + ", Min: " + ex.Min + ", Max: " + ex.Max);
				}
				// System exceptions
				catch (ArgumentOutOfRangeException ex)
				{
					Console.WriteLine("[ArgumentOutOfRangeException] " + ex.Message);
				}
				catch (IndexOutOfRangeException ex)
				{
					Console.WriteLine("[IndexOutOfRangeException] " + ex.Message);
				}
				// General fallback
				catch (Exception ex)
				{
					Console.WriteLine("[General Exception] " + ex.Message);
				}
				finally
				{
					Console.WriteLine();
					Console.WriteLine(sensors[i].Name + " Sensor Monitoring complete.");
					Console.WriteLine();
				}
			}
		}
	}
}