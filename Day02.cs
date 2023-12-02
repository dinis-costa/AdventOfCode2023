using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2023
{
    internal class Day02
    {
        private readonly string _input;
        public Day02()
        {
            _input = "Day02.txt";
        }

        public void PartOne()
        {
            const int RedMax = 12, GreenMax = 13, BlueMax = 14;
            int gameNumberSum = 0;

            foreach (var line in File.ReadLines(_input))
            {
                string[] parts = line.Split(':');
                string gamePart = parts[0];
                string gameResults = parts[1];

                int possibleSets = 0;

                string[] setResults = gameResults.Split(';');
                foreach (var set in setResults)
                {
                    int redCount = 0, greenCount = 0, blueCount = 0;

                    string[] cubes = set.Split(',');
                    foreach (var cube in cubes)
                    {
                        int cubeNumber = Convert.ToInt32(Regex.Match(cube, @"\d+").Value);

                        if (cube.Contains("red"))
                            redCount += cubeNumber;
                        else if (cube.Contains("green"))
                            greenCount += cubeNumber;
                        else
                            blueCount += cubeNumber;
                    }

                    if (redCount <= RedMax && greenCount <= GreenMax && blueCount <= BlueMax)
                        possibleSets++;
                }

                if (setResults.Length == possibleSets)
                {
                    int gameNumber = Convert.ToInt32(Regex.Match(gamePart, @"\d+").Value);
                    gameNumberSum += gameNumber;
                }
            }

            Console.WriteLine(gameNumberSum);
        }

        public void PartTwo()
        {
            int gameSum = 0;

            foreach (var line in File.ReadLines(_input))
            {
                int redCount = 0, greenCount = 0, blueCount = 0;

                string[] parts = line.Split(':');
                string gameResults = parts[1];

                string[] setResults = gameResults.Split(';');
                foreach (var set in setResults)
                {
                    string[] cubes = set.Split(',');
                    foreach (var cube in cubes)
                    {
                        int cubeNumber = Convert.ToInt32(Regex.Match(cube, @"\d+").Value);

                        if (cube.Contains("red"))
                            redCount = Math.Max(redCount, cubeNumber);
                        else if (cube.Contains("green"))
                            greenCount = Math.Max(greenCount, cubeNumber);
                        else
                            blueCount = Math.Max(blueCount, cubeNumber);
                    }
                }
                gameSum += redCount * greenCount * blueCount;
            }

            Console.WriteLine(gameSum);
        }
    }
}