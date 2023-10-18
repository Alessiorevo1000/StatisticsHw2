using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Exercise1
{
    static void Main()
    {
        List<string> columns = new List<string>
        {
            "Age",
            "Hard Worker (0-5)",
            "Dream Works"
        };

        foreach (var column in columns)
        {
            var data1 = getColumnData(column);
            Console.WriteLine($"Compute: {column}");
            Console.WriteLine("-----------------------------");
            Console.WriteLine("Absolute Frequency:");
            PrintData(CalculateAbsoluteFrequency(data1));
            Console.WriteLine("-----------------------------");
            Console.WriteLine("Relative Frequency:");
            PrintData(CalculateRelativeFrequency(data1));
            Console.WriteLine("-----------------------------");
            Console.WriteLine("Percentage Frequency:");
       
            var a = CalculatePercentageFrequency(data1);

            foreach (var kvp in a)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value:F2}%");
            }

            Console.WriteLine("\n\n\n");
        }

        Console.WriteLine("-----------------------------");
        Console.WriteLine("Joint Distribution:");
        Console.WriteLine("-----------------------------");

        List<List<string>> data = new List<List<string>>();
        foreach (var column in columns)
        {
            data.Add(getColumnData(column));
        }

        PrintData(CalculateJointFrequency(data[0], data[1]));
    }

    static Dictionary<T, int> CalculateAbsoluteFrequency<T>(IEnumerable<T> data)
    {
        return data.GroupBy(x => x)
                   .ToDictionary(group => group.Key, group => group.Count());
    }

    static Dictionary<T, double> CalculateRelativeFrequency<T>(IEnumerable<T> data)
    {
        int dataLength = data.Count();
        var absoluteFrequency = CalculateAbsoluteFrequency(data);
        return absoluteFrequency.ToDictionary(kvp => kvp.Key, kvp => Math.Round((double)kvp.Value / dataLength, 2));
    }

    static Dictionary<T, double> CalculatePercentageFrequency<T>(IEnumerable<T> data)
    {
        int dataLength = data.Count();
        var absoluteFrequency = CalculateAbsoluteFrequency(data);
        return absoluteFrequency.ToDictionary(kvp => kvp.Key, kvp => Math.Round((double)kvp.Value / dataLength * 100, 2));
    }

    static Dictionary<string, int> CalculateJointFrequency<T, E>(IEnumerable<T> data1, IEnumerable<E> data2)
    {
        var labels = data1.SelectMany(x => data2, (x, y) => $"x: {x}, y: {y}");
        return labels.GroupBy(x => x)
                     .ToDictionary(group => group.Key, group => group.Count());
    }

    static void PrintData<K, V>(Dictionary<K, V> data)
    {
        foreach (var item in data)
        {
            Console.WriteLine($"{item.Key}: {item.Value}");
        }
    }

    static List<string> getColumnData(string columnName)
    {
        string tsvFileName = "./sheet.tsv";
        int targetColumnIndex = -1;
        List<string> results = new List<string>();

        if (!File.Exists(tsvFileName))
        {
            Console.WriteLine("File not found.");
            return null;
        }

        bool isFirstTime = true;

        using (StreamReader reader = new StreamReader(tsvFileName))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                List<string> columns = line.Split('\t').ToList();

                if (isFirstTime)
                {
                    targetColumnIndex = columns.IndexOf(columnName);
                    isFirstTime = false;
                }
                else if (targetColumnIndex < columns.Count)
                {
                    results.Add(columns[targetColumnIndex]);
                }
            }
        }

        return results;
    }
}
