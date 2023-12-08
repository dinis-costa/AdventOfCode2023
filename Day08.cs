using System.Text.RegularExpressions;

namespace AdventOfCode2023
{
    internal class Day08
    {
        private readonly string _input;
        public Day08()
        {
            _input = "Day08.txt";
        }

        public void PartOne()
        {
            var lines = File.ReadLines(_input).ToList();
            string instructions = lines[0];

            List<(string Node, string Left, string Right)> nodes = new();
            for (int i = 2; i < lines.Count; i++)
            {
                string pattern = @"(\w+) = \((\w+), (\w+)\)";
                Match match = Regex.Match(lines[i], pattern);

                nodes.Add((match.Groups[1].Value, match.Groups[2].Value, match.Groups[3].Value));
            }

            (string Node, string Left, string Right) currentNode = nodes.First(n => n.Node == "AAA");
            int stepsNumber = 0;
            do
            {
                foreach (var x in instructions)
                {
                    if (x == 'L') currentNode = nodes.First(n => n.Node == currentNode.Left);
                    else currentNode = nodes.First(n => n.Node == currentNode.Right);

                    stepsNumber++;
                    if (currentNode.Node == "ZZZ") break;
                }
            } while (currentNode.Node != "ZZZ");

            Console.WriteLine(stepsNumber);
        }

        public void PartTwo()
        {
            var lines = File.ReadLines(_input).ToList();
            string instructions = lines[0];

            List<(string Node, string Left, string Right)> nodes = new();
            for (int i = 2; i < lines.Count; i++)
            {
                string pattern = @"(\w+) = \((\w+), (\w+)\)";
                Match match = Regex.Match(lines[i], pattern);

                nodes.Add((match.Groups[1].Value, match.Groups[2].Value, match.Groups[3].Value));
            }

            List<(string Node, string Left, string Right)> startingNodes = nodes.Where(n => n.Node[2] == 'A').ToList();
            List<(string Node, string Left, string Right)> endingNodes = startingNodes;
            var nodeDictionary = nodes.ToDictionary(n => n.Node, n => n);
            long stepsNumber = 0;

            do
            {
                foreach (var x in instructions)
                {
                    for (int i = 0; i < startingNodes.Count; i++)
                    {
                        var currentNode = startingNodes[i];
                        currentNode = x == 'L' ? nodeDictionary[currentNode.Left] : nodeDictionary[currentNode.Right];
                        endingNodes[i] = currentNode;
                    }
                    stepsNumber++;
                }
            } while (!endingNodes.All(n => n.Node[2] == 'Z'));

            Console.WriteLine(stepsNumber);
        }
    }
}