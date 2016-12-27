using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.AdventOfCode2016
{
    class Day1
    {
        enum Directions{N, E, S, W};

        class Position
        {
            public int X { get; private set; }
            public int Y { get; private set; }
            public List<Tuple<int, int>> Visited { get; private set; }
            public Directions Facing { get; private set; }

            public Position()
            {
                X = 0;
                Y = 0;
                Visited = new List<Tuple<int, int>>();
                Visited.Add(new Tuple<int, int>(0, 0));
                Facing = Directions.N;
            }

            public void MoveWithCommand(string directionToTurn, int distance)
            {
                UpdateFacingDirection(directionToTurn);
                UpdateCoordinates(distance);
            }

            public bool MoveWithCommandUntilVisitTwice(string directionToTurn, int distance)
            {
                bool atLocationVisitTwice = false;

                UpdateFacingDirection(directionToTurn);
                UpdateCoordinates(distance);

                foreach (var coord in Visited)
                {
                    // TODO: maybe check if any coordinate visited twice
                }

                if (!atLocationVisitTwice)
                {
                    Visited.Add(new Tuple<int, int>(X, Y));
                }

                return atLocationVisitTwice;
            }

            public int GetTotalBlocksAwayFromWorldCenter()
            {
                return Math.Abs(X) + Math.Abs(Y);
            }

            private void UpdateFacingDirection(string directionToTurn)
            {
                int facing = (int)Facing;

                if (directionToTurn.Equals("L"))
                {
                    facing -= 1;
                    if (facing < 0) facing = (int)Directions.W;
                }
                else
                {
                    facing += 1;
                    if (facing > (int)Directions.W) facing = (int)Directions.N;
                }

                Facing = (Directions)facing;
            }

            private void UpdateCoordinates(int distanceToGo)
            {
                switch (Facing)
                {
                    case Directions.N:
                        Y += distanceToGo;
                        break;
                    case Directions.S:
                        Y -= distanceToGo;
                        break;
                    case Directions.E:
                        X += distanceToGo;
                        break;
                    case Directions.W:
                        X -= distanceToGo;
                        break;
                }
            }
        }

        static readonly string input_test_1 = "R2, L3";              //  5
        static readonly string input_test_2 = "R2, R2, R2";          //  2
        static readonly string input_test_3 = "R5, L5, R5, R3";      // 12
        static readonly string input_test_4 = "R1, R1, R3, R1, R1, L2, R5, L2, R5, R1, R4, L2, R3, L3, R4, L5, R4, R4, R1, L5, L4, R5, R3, L1, R4, R3, L2, L1, R3, L4, R3, L2, R5, R190, R3, R5, L5, L1, R54, L3, L4, L1, R4, R1, R3, L1, L1, R2, L2, R2, R5, L3, R4, R76, L3, R4, R191, R5, R5, L5, L4, L5, L3, R1, R3, R2, L2, L2, L4, L5, L4, R5, R4, R4, R2, R3, R4, L3, L2, R5, R3, L2, L1, R2, L3, R2, L1, L1, R1, L3, R5, L5, L1, L2, R5, R3, L3, R3, R5, R2, R5, R5, L5, L5, R2, L3, L5, L2, L1, R2, R2, L2, R2, L3, L2, R3, L5, R4, L4, L5, R3, L4, R1, R3, R2, R4, L2, L3, R2, L5, R5, R4, L2, R4, L1, L3, L1, L3, R1, R2, R1, L5, R5, R3, L3, L3, L2, R4, R2, L5, L1, L1, L5, L4, L1, L1, R1";

        static readonly string input_test_1_q2 = "R8, R4, R4, R8";  //4

        public static void RunDay1()
        {
            string input = input_test_1_q2;

            List<string> commands = new List<string>();
            foreach (var command in input.Split(','))
            {
                commands.Add(command.Trim());
            }

            //int distance = GetEasterBunnyHQDistanceFromCommands(commands.ToArray());
            int distance = GetFirstVisitTwiceLocationDistanceFromCommands(commands.ToArray());
            System.Diagnostics.Debug.WriteLine("Easter Bunny HQ is " + distance + " blocks away.");
        }

        private static int GetEasterBunnyHQDistanceFromCommands(string[] commands)
        {
            Position pos = new Position();
            string directionToTurn = "";
            int distanceToGo = 0;

            foreach (var command in commands)
            {
                directionToTurn = command.StartsWith("L") ? "L" : "R";
                distanceToGo = int.Parse(command.Substring(1));

                pos.MoveWithCommand(directionToTurn, distanceToGo);
            }

            return pos.GetTotalBlocksAwayFromWorldCenter();
        }

        private static int GetFirstVisitTwiceLocationDistanceFromCommands(string[] commands)
        {
            Position pos = new Position();
            string directionToTurn = "";
            int distanceToGo = 0;

            foreach (var command in commands)
            {
                directionToTurn = command.StartsWith("L") ? "L" : "R";
                distanceToGo = int.Parse(command.Substring(1));

                bool done = pos.MoveWithCommandUntilVisitTwice(directionToTurn, distanceToGo);

                if (done)
                {
                    break;
                }                
            }

            return pos.GetTotalBlocksAwayFromWorldCenter();
        }

    }
}
