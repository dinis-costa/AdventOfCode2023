namespace AdventOfCode2023
{
    internal class Day07
    {
        private readonly string _input;
        public Day07()
        {
            _input = "Day07.txt";
        }

        public void PartOne()
        {
            var lines = File.ReadLines(_input).ToList();
            char[] cards = ['A', 'K', 'Q', 'J', 'T', '9', '8', '7', '6', '5', '4', '3', '2'];

            List<(List<int>, int, int)> cardsInfo = new List<(List<int>, int, int)>();

            foreach (var line in lines)
            {
                (List<int>, int, int) entry = new();

                var lineInfo = line.Split(' ');
                var hand = lineInfo[0];

                List<int> handNumbs = new();
                char[] inputArray = hand.ToCharArray();
                for (int i = 0; i < inputArray.Length; i++)
                {
                    int index = Array.IndexOf(cards, inputArray[i]);
                    handNumbs.Add(index);
                }

                int handType = 0;
                if (hand.GroupBy(card => card).Any(group => group.Count() == 5))
                    handType = 0;
                else if (hand.GroupBy(card => card).Any(group => group.Count() == 4))
                    handType = 1;
                else if (hand.GroupBy(card => card).Any(group => group.Count() == 3) &&
                         hand.GroupBy(card => card).Any(group => group.Count() == 2))
                    handType = 2;
                else if (hand.GroupBy(card => card).Any(group => group.Count() == 3) &&
                         hand.GroupBy(card => card).All(group => group.Count() != 2))
                    handType = 3;
                else if (hand.GroupBy(card => card).Count(group => group.Count() == 2) == 2)
                    handType = 4;
                else if (hand.GroupBy(card => card).Any(group => group.Count() == 2) &&
                         hand.GroupBy(card => card).All(group => group.Count() != 3))
                    handType = 5;
                else
                    handType = 6;

                entry.Item1 = handNumbs;
                entry.Item2 = handType;
                entry.Item3 = Convert.ToInt32(lineInfo[1]);
                cardsInfo.Add(entry);
            }

            cardsInfo = cardsInfo.OrderByDescending(item => item.Item2)
                     .ThenByDescending(item => item.Item1[0])
                     .ThenByDescending(item => item.Item1[1])
                     .ThenByDescending(item => item.Item1[2])
                     .ThenByDescending(item => item.Item1[3])
                     .ThenByDescending(item => item.Item1[4])
                     .ToList();

            for (int i = 0; i < cardsInfo.Count; i++)
            {
                var currentTuple = cardsInfo[i];
                cardsInfo[i] = (currentTuple.Item1, currentTuple.Item2, currentTuple.Item3 * (i + 1));
            }

            int sumOfBids = cardsInfo.Sum(tuple => tuple.Item3);

            Console.WriteLine(sumOfBids);
        }

        public void PartTwo()
        {
            var lines = File.ReadLines(_input).ToList();
            char[] cards = ['A', 'K', 'Q', 'T', '9', '8', '7', '6', '5', '4', '3', '2', 'J'];

            List<(List<int>, int, int)> cardsInfo = new();

            foreach (var line in lines)
            {
                (List<int>, int, int) entry = new();

                var lineInfo = line.Split(' ');
                var hand = lineInfo[0];

                var mostPresentChar = hand.GroupBy(c => c).OrderByDescending(g => g.Key == 'J' ? 0 : g.Count()).First().Key;
                var newHand = hand.Replace('J', mostPresentChar);

                List<int> handNumbs = new();
                char[] inputArray = hand.ToCharArray();
                for (int i = 0; i < inputArray.Length; i++)
                {
                    int index = Array.IndexOf(cards, inputArray[i]);
                    handNumbs.Add(index);
                }

                int handType = 0;
                if (newHand.GroupBy(card => card).Any(group => group.Count() == 5))
                    handType = 0;
                else if (newHand.GroupBy(card => card).Any(group => group.Count() == 4))
                    handType = 1;
                else if (newHand.GroupBy(card => card).Any(group => group.Count() == 3) &&
                         newHand.GroupBy(card => card).Any(group => group.Count() == 2))
                    handType = 2;
                else if (newHand.GroupBy(card => card).Any(group => group.Count() == 3) &&
                         newHand.GroupBy(card => card).All(group => group.Count() != 2))
                    handType = 3;
                else if (newHand.GroupBy(card => card).Count(group => group.Count() == 2) == 2)
                    handType = 4;
                else if (newHand.GroupBy(card => card).Any(group => group.Count() == 2) &&
                         newHand.GroupBy(card => card).All(group => group.Count() != 3))
                    handType = 5;
                else
                    handType = 6;

                entry.Item1 = handNumbs;
                entry.Item2 = handType;
                entry.Item3 = Convert.ToInt32(lineInfo[1]);
                cardsInfo.Add(entry);
            }

            cardsInfo = cardsInfo.OrderByDescending(item => item.Item2)
                     .ThenByDescending(item => item.Item1[0])
                     .ThenByDescending(item => item.Item1[1])
                     .ThenByDescending(item => item.Item1[2])
                     .ThenByDescending(item => item.Item1[3])
                     .ThenByDescending(item => item.Item1[4])
                     .ToList();

            for (int i = 0; i < cardsInfo.Count; i++)
            {
                var currentTuple = cardsInfo[i];
                cardsInfo[i] = (currentTuple.Item1, currentTuple.Item2, currentTuple.Item3 * (i + 1));
            }

            int sumOfBids = cardsInfo.Sum(tuple => tuple.Item3);

            Console.WriteLine(sumOfBids);
        }
    }
}