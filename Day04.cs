using System.Text.RegularExpressions;

namespace AdventOfCode2023
{
    internal class Day04
    {
        private readonly string _input;
        public Day04()
        {
            _input = "Day04.txt";
        }

        public void PartOne()
        {
            var lines = File.ReadLines(_input).ToList();

            int numberSum = 0;
            foreach (var line in lines)
            {
                var cardNumbers = line.Split(':')[1].Split("|");
                var winningNumbers = Regex.Matches(cardNumbers[0], @"\d+").Cast<Match>().Select(m => int.Parse(m.Value));
                var scratchNumbers = Regex.Matches(cardNumbers[1], @"\d+").Cast<Match>().Select(m => int.Parse(m.Value));

                int doubledValue = 0;
                var numberOfMatches = scratchNumbers.Intersect(winningNumbers).Count();

                for (int i = 1; i <= numberOfMatches; i++)
                {
                    if (i == 1) doubledValue = 1;
                    else doubledValue += doubledValue;
                }
                numberSum += doubledValue;
            }

            Console.WriteLine(numberSum);
        }

        public void PartTwo()
        {
            var lines = File.ReadLines(_input).ToList();
            var lineDictionary = lines.ToDictionary(
                line => int.Parse(Regex.Match(line.Split(':')[0], @"\d+").Value),
                line => line
            );

            for (int line = 0; line < lines.Count; line++)
            {
                var cardSplit = lines[line].Split(':');
                var cardNumber = int.Parse(Regex.Match(cardSplit[0], @"\d+").Value);

                var cardNumbers = cardSplit[1].Split("|");
                var winningNumbers = Regex.Matches(cardNumbers[0], @"\d+").Cast<Match>().Select(m => int.Parse(m.Value));
                var scratchNumbers = Regex.Matches(cardNumbers[1], @"\d+").Cast<Match>().Select(m => int.Parse(m.Value));

                var numberOfMatches = scratchNumbers.Intersect(winningNumbers).Count();

                if (numberOfMatches == 0)
                    continue;

                for (int l = 1; l <= numberOfMatches; l++)
                {
                    if (lineDictionary.TryGetValue(cardNumber + l, out var lineToCopy))
                        lines.Insert(line + l, lineToCopy);
                }
            }

            Console.WriteLine(lines.Count);
        }
    }
}