using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        int N = 1000; // Number of random variates
        int k = 20;   // Number of class intervals

        Random random = new Random();
        List<int> histogram = new List<int>();

        // Initialize histogram bins
        for (int i = 0; i < k; i++)
        {
            histogram.Add(0);
        }

        // Generate random variates and count them in the intervals
        for (int i = 0; i < N; i++)
        {
            double value = random.NextDouble(); // Generates a random number in [0, 1)
            int interval = (int)(value * k);   // Determine the interval index

            // Increment the count in the corresponding interval
            histogram[interval]++;
        }

        // Print the results
        for (int i = 0; i < k; i++)
        {
            double start = (double)i / k;
            double end = (double)(i + 1) / k;
            Console.WriteLine($"Interval [{start:F2}, {end:F2}): {histogram[i]}");
        }
    }
}
