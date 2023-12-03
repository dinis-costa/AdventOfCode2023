using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2023
{
    internal class Day03
    {
        private readonly string _input;
        public Day03()
        {
            _input = "Day03.txt";
        }

        public void PartOne()
        {
            string engineSchematic = File.ReadAllText(_input);
            string[] lines = engineSchematic.Split('\n');

            int numberSum = 0;
            for (int line = 0; line < lines.Length; line++)
            {
                var lineNumbers = Regex.Matches(lines[line], @"\d+");
                foreach (Match number in lineNumbers)
                {
                    int numberStartIndex = number.Index;
                    int numberEndIndex = number.Length;

                    string previousLine = string.Empty;
                    if (line > 0)
                    {
                        if (numberStartIndex == 0)
                            previousLine = lines[line - 1].Substring(numberStartIndex, numberEndIndex + 1);
                        else if (numberStartIndex + numberEndIndex == lines[line - 1].Length - 1)
                            previousLine = lines[line - 1].Substring(numberStartIndex - 1, numberEndIndex);
                        else
                            previousLine = lines[line - 1].Substring(numberStartIndex - 1, numberEndIndex + 2);
                    }

                    string currentLine = string.Empty;
                    if (numberStartIndex == 0)
                        currentLine = lines[line].Substring(numberStartIndex, numberEndIndex + 1);
                    else if (numberStartIndex + numberEndIndex == lines[line].Length - 1)
                        currentLine = lines[line].Substring(numberStartIndex - 1, numberEndIndex);
                    else
                        currentLine = lines[line].Substring(numberStartIndex - 1, numberEndIndex + 2);

                    string nextLine = string.Empty;
                    if (line < lines.Length - 2)
                    {
                        if (numberStartIndex == 0)
                            nextLine = lines[line + 1].Substring(numberStartIndex, numberEndIndex + 1);
                        else if (numberStartIndex + numberEndIndex == lines[line + 1].Length - 1)
                            nextLine = lines[line + 1].Substring(numberStartIndex - 1, numberEndIndex);
                        else
                            nextLine = lines[line + 1].Substring(numberStartIndex - 1, numberEndIndex + 2);
                    }

                    bool pl = previousLine.Any(x => !char.IsDigit(x) && x != '.');
                    bool cl = currentLine.Any(x => !char.IsDigit(x) && x != '.');
                    bool nl = nextLine.Any(x => !char.IsDigit(x) && x != '.');

                    if (pl || cl || nl)
                        numberSum += Convert.ToInt32(number.Value);
                }
            }

            Console.WriteLine(numberSum);
        }

        public void PartTwo()
        {
            string engineSchematic = File.ReadAllText(_input);
            string[] lines = engineSchematic.Split('\n');

            int gearPower = 0;
            for (int line = 0; line < lines.Length; line++)
            {
                for (int column = 0; column < lines[line].Length; column++)
                {
                    if (lines[line][column] == '*')
                    {
                        char[,] gearNeighbours = {
                            {'.', '.', '.'},
                            {'.', 'c', '.'},
                            {'.', '.', '.'}
                        };

                        if (line - 1 >= 0 && line - 1 < lines.Length && column - 1 >= 0 && column - 1 < lines[line - 1].Length)
                            gearNeighbours[0, 0] = lines[line - 1][column - 1];

                        if (line - 1 >= 0 && line - 1 < lines.Length && column >= 0 && column < lines[line - 1].Length)
                            gearNeighbours[0, 1] = lines[line - 1][column];

                        if (line - 1 >= 0 && line - 1 < lines.Length && column + 1 >= 0 && column + 1 < lines[line - 1].Length)
                            gearNeighbours[0, 2] = lines[line - 1][column + 1];

                        if (line >= 0 && line < lines.Length && column - 1 >= 0 && column - 1 < lines[line].Length)
                            gearNeighbours[1, 0] = lines[line][column - 1];

                        if (line >= 0 && line < lines.Length && column + 1 >= 0 && column + 1 < lines[line].Length)
                            gearNeighbours[1, 2] = lines[line][column + 1];

                        if (line + 1 >= 0 && line + 1 < lines.Length && column - 1 >= 0 && column - 1 < lines[line + 1].Length)
                            gearNeighbours[2, 0] = lines[line + 1][column - 1];

                        if (line + 1 >= 0 && line + 1 < lines.Length && column >= 0 && column < lines[line + 1].Length)
                            gearNeighbours[2, 1] = lines[line + 1][column];

                        if (line + 1 >= 0 && line + 1 < lines.Length && column + 1 >= 0 && column + 1 < lines[line + 1].Length)
                            gearNeighbours[2, 2] = lines[line + 1][column + 1];

                        List<(int, int)> digitPositions = new();
                        for (int row = 0; row < gearNeighbours.GetLength(0); row++)
                        {
                            for (int col = 0; col < gearNeighbours.GetLength(1); col++)
                            {
                                if (char.IsDigit(gearNeighbours[row, col]))
                                    digitPositions.Add((row, col));
                            }
                        }

                        List<int> validNumbers = new();
                        if (digitPositions.Count > 1)
                        {
                            foreach (var digit in digitPositions)
                            {
                                int searchRow = 0;
                                if (digit.Item1 == 0) searchRow = -1;
                                else if (digit.Item1 == 1) searchRow = 0;
                                else if (digit.Item1 == 2) searchRow = 1;

                                int searchCol = 0;
                                if (digit.Item2 == 0) searchCol = column - 1;
                                else if (digit.Item2 == 1) searchCol = column;
                                else if (digit.Item2 == 2) searchCol = column + 1;

                                StringBuilder fullNumber = new();
                                var stringInput = lines[line + searchRow];

                                for (int i = searchCol; i >= 0 && char.IsDigit(stringInput[i]); i--)
                                    fullNumber.Insert(0, stringInput[i]);

                                for (int i = searchCol + 1; i < stringInput.Length && char.IsDigit(stringInput[i]); i++)
                                    fullNumber.Append(stringInput[i]);

                                if (!validNumbers.Contains(Convert.ToInt32(fullNumber.ToString())))
                                    validNumbers.Add(Convert.ToInt32(fullNumber.ToString()));
                            }
                        }

                        if (validNumbers.Count == 2)
                            gearPower += validNumbers[0] * validNumbers[1];
                    }
                }
            }
            Console.WriteLine(gearPower);
        }
    }
}