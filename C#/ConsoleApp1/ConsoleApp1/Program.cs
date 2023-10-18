class Exercise1
{
    public static Dictionary<T, int> CalculateAbsoluteFrequency<T>(IEnumerable<T> data)        // send in input a list of data about a specifich column
    {
        Dictionary<T, int> frequency = new Dictionary<T, int>();                        // create a dict

        foreach (T item in data)                                                     // for each item in data 
        {
            if (frequency.ContainsKey(item))
            {
                frequency[item]++;
            }
            else
            {
                frequency[item] = 1;
            }
        }

        return frequency;
    }

    public static Dictionary<T, double> CalculateRelativeFrequency<T>(IEnumerable<T> data)
    {
        Dictionary<T, int> absoluteFrequency = CalculateAbsoluteFrequency(data);
        Dictionary<T, double> frequency = new Dictionary<T, double>();

        int dataLength = data.Count();

        foreach (var item in absoluteFrequency)
        {
            frequency[item.Key] = (double)item.Value / dataLength;
        }

        return frequency;
    }

    public static Dictionary<T, double> CalculatePercentageFrequency<T>(IEnumerable<T> data)
    {
        Dictionary<T, int> absoluteFrequency = CalculateAbsoluteFrequency(data);
        Dictionary<T, double> frequency = new Dictionary<T, double>();

        int dataLength = data.Count();

        foreach (var item in absoluteFrequency)
        {
            frequency[item.Key] = (double)item.Value / dataLength * 100;
        }

        return frequency;
    }

    public static Dictionary<string, int> CalculateJointFrequency<T, E>(IEnumerable<T> data1, IEnumerable<E> data2)
    {
        Dictionary<string, int> frequency = new Dictionary<string, int>();

        foreach (var x in data1)
        {
            foreach (var y in data2)
            {
                string label = $"x: {x}, y: {y}";
                if (frequency.ContainsKey(label))
                {
                    frequency[label]++;
                }
                else
                {
                    frequency[label] = 1;
                }
            }
        }

        return frequency;
    }

    public static void PrintData<K, V>(Dictionary<K, V> data)
    {
        foreach (var item in data)
        {
            Console.WriteLine($"{item.Key}: {item.Value}");
        }
    }

    public static List<String> getColumnData(String columnName)   // send a string es: hard work
    {
        string tsvFileName = "./sheet.tsv";              
        int targetColumnIndex = -1;                        
        List<String> results = new List<String>();

        // Check if the file exists
        if (!File.Exists(tsvFileName))
        {
            Console.WriteLine("File not found.");
            return null;
        }

        bool isFirstTime = true;
        // Read the TSV file and extract the desired column
        using (StreamReader reader = new StreamReader(tsvFileName))            // method for extract data
        {
            string line;
            while ((line = reader.ReadLine()) != null)                      //read from tsv until there is a line
            {
                List<String> columns = new List<string>(line.Split('\t'));   //remove the \t from each line and insert my data into list 
               
                
                if (isFirstTime)                                                 // i want remopve thiis and seee if the algo run (is a first time)
                {
                    targetColumnIndex = columns.IndexOf(columnName);           //set index to number of column when the target is positioned                
                    isFirstTime = false;                                        //set first time = null 
                }
                else
                {
                    if (targetColumnIndex < columns.Count)                    //if my index is less to number of comuns do 
                    {
                        results.Add(columns[targetColumnIndex]);              // list of stings --> result insert the columns 
                    }
                }
            }
        }

        return results;
    }

    static void Main()
    {

        List<String> columns = new List<String>();
        columns.Add("Hard Worker (0-5)");                          // change if you want calculate different coloumns 
        columns.Add("Dream Works");
        columns.Add("Age");

        List<List<String>> data = new List<List<string>>();        // i create a matrix         

        foreach (var column in columns)
        {
            Console.WriteLine(column);
            
            
            var d = getColumnData(column);                   // i have a list for any data from my column
            data.Add(d);                                      // add my list to data 
            Console.WriteLine("========== " + column + " ==========");
            Console.WriteLine("---------- absolutee ----------");
            PrintData(CalculateAbsoluteFrequency(d));
            Console.WriteLine("---------- relative ----------");
            PrintData(CalculateRelativeFrequency(d));
            Console.WriteLine("---------- percentage ----------");
            PrintData(CalculatePercentageFrequency(d));
            Console.WriteLine("\n\n\n");
        }
       
        Console.WriteLine("---------- joint distribution ----------");
        var result4 = CalculateJointFrequency(data[0], data[1]);
        PrintData(result4);
    }
}