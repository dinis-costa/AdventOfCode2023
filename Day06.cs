using System.Text.RegularExpressions;

namespace AdventOfCode2023
{
    internal class Day06
    {
        private readonly string _input;
        public Day06()
        {
            _input = "Day06.txt";
        }

        public void PartOneAndTwo()
        {
            string input = File.ReadAllText(_input);
            string[] lines = input.Split('\n');

            var times = Regex.Matches(lines[0], @"\d+").Select(match => long.Parse(match.Value));
            var distances = Regex.Matches(lines[1], @"\d+").Select(match => long.Parse(match.Value));

            var timesDistances = times.Zip(distances, (time, distance) => (time, distance)).ToList();

            var recordList = timesDistances.Select(set =>
            {
                return Enumerable.Range(0, (int)(set.time + 1))
                    .Count(time => time * (set.time - time) > set.distance);
            }).ToList();

            var mult = recordList.Aggregate((x, y) => x * y);
            Console.WriteLine(mult);
        }
    }
}