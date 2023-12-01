namespace AdventOfCode2023
{
    internal class Day01
    {
        private readonly string _input;
        public Day01()
        {
            _input = "Day01.txt";
        }

        public void PartOne()
        {
            int sumTotal = 0;

            foreach (var line in File.ReadLines(_input))
            {
                char fn = line.FirstOrDefault(c => char.IsDigit(c), '0');
                char ln = line.LastOrDefault(c => char.IsDigit(c), '0');

                sumTotal += Convert.ToInt32($"{fn - '0'}{ln - '0'}");
            }

            Console.WriteLine(sumTotal);
        }

        public void PartTwo()
        {
            Dictionary<string, char> keyValuePairs = new()
            {
                {"one", '1'},
                {"two", '2'},
                {"three", '3'},
                {"four", '4'},
                {"five", '5'},
                {"six", '6'},
                {"seven", '7'},
                {"eight", '8'},
                {"nine", '9'}
            };

            int sumTotal = 0;

            foreach (var line in File.ReadLines(_input))
            {
                char firstNumber, lastNumber;

                string firstDictionaryMatch = keyValuePairs.Keys
                    .Where(key => line.Contains(key))
                    .OrderBy(key => line.IndexOf(key))
                    .FirstOrDefault();

                string lastDictionaryMatch = keyValuePairs.Keys
                    .Where(key => line.Contains(key))
                    .OrderBy(key => line.LastIndexOf(key))
                    .LastOrDefault();

                if (line.Any(c => char.IsDigit(c)))
                {
                    char firstCharDigit = line.First(c => char.IsDigit(c));
                    char lastCharDigit = line.Last(c => char.IsDigit(c));

                    if (keyValuePairs.Keys.Any(key => line.Contains(key)))
                    {
                        firstNumber = line.IndexOf(firstDictionaryMatch) < line.IndexOf(firstCharDigit) ? keyValuePairs[firstDictionaryMatch] : firstCharDigit;
                        lastNumber = line.LastIndexOf(lastDictionaryMatch) > line.LastIndexOf(lastCharDigit) ? keyValuePairs[lastDictionaryMatch] : lastCharDigit;
                    }
                    else
                    {
                        firstNumber = firstCharDigit;
                        lastNumber = lastCharDigit;
                    }
                }
                else
                {
                    firstNumber = keyValuePairs[firstDictionaryMatch];
                    lastNumber = keyValuePairs[lastDictionaryMatch];
                }

                sumTotal += Convert.ToInt32($"{firstNumber - '0'}{lastNumber - '0'}");
            }

            Console.WriteLine(sumTotal);
        }
    }
}