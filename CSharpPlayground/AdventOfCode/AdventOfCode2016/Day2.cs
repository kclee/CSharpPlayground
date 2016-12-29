using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.AdventOfCode2016
{
    //TODO: different way to handle Move or store keypad data
    class Day2
    {
        // Part 1: 1985, Part 2:
        static readonly string[] input_1 = new string[] { "ULL",
                                                          "RRDDD",
                                                          "LURDL",
                                                          "UUUUD" };
        // Part 1: 33444, Part 2: 446A6
        static readonly string[] input_2 = new string[] { "RDLULDLDDRLLLRLRULDRLDDRRRRURLRLDLULDLDLDRULDDLLDRDRUDLLDDRDULLLULLDULRRLDURULDRUULLLUUDURURRDDLDLDRRDDLRURLLDRRRDULDRULURURURURLLRRLUDULDRULLDURRRLLDURDRRUUURDRLLDRURULRUDULRRRRRDLRLLDRRRDLDUUDDDUDLDRUURRLLUDUDDRRLRRDRUUDUUULDUUDLRDLDLLDLLLLRRURDLDUURRLLDLDLLRLLRULDDRLDLUDLDDLRDRRDLULRLLLRUDDURLDLLULRDUUDRRLDUDUDLUURDURRDDLLDRRRLUDULDULDDLLULDDDRRLLDURURURUUURRURRUUDUUURULDLRULRURDLDRDDULDDULLURDDUDDRDRRULRUURRDDRLLUURDRDDRUDLUUDURRRLLRR",
                                                          "RDRRLURDDDDLDUDLDRURRLDLLLDDLURLLRULLULUUURLDURURULDLURRLRULDDUULULLLRLLRDRRUUDLUUDDUDDDRDURLUDDRULRULDDDLULRDDURRUURLRRLRULLURRDURRRURLDULULURULRRLRLUURRRUDDLURRDDUUDRDLLDRLRURUDLDLLLLDLRURDLLRDDUDDLDLDRRDLRDRDLRRRRUDUUDDRDLULUDLUURLDUDRRRRRLUUUDRRDLULLRRLRLDDDLLDLLRDDUUUUDDULUDDDUULDDUUDURRDLURLLRUUUUDUDRLDDDURDRLDRLRDRULRRDDDRDRRRLRDULUUULDLDDDUURRURLDLDLLDLUDDLDLRUDRLRLDURUDDURLDRDDLLDDLDRURRULLURULUUUUDLRLUUUDLDRUDURLRULLRLLUUULURLLLDULLUDLLRULRRLURRRRLRDRRLLULLLDURDLLDLUDLDUDURLURDLUURRRLRLLDRLDLDRLRUUUDRLRUDUUUR",
                                                          "LLLLULRDUUDUUDRDUUURDLLRRLUDDDRLDUUDDURLDUDULDRRRDDLLLRDDUDDLLLRRLURDULRUUDDRRDLRLRUUULDDULDUUUDDLLDDDDDURLDRLDDDDRRDURRDRRRUUDUUDRLRRRUURUDURLRLDURDDDUDDUDDDUUDRUDULDDRDLULRURDUUDLRRDDRRDLRDLRDLULRLLRLRLDLRULDDDDRLDUURLUUDLLRRLLLUUULURUUDULRRRULURUURLDLLRURUUDUDLLUDLDRLLRRUUDDRLUDUDRDDRRDDDURDRUDLLDLUUDRURDLLULLLLUDLRRRUULLRRDDUDDDUDDRDRRULURRUUDLUDLDRLLLLDLUULLULLDDUDLULRDRLDRDLUDUDRRRRLRDLLLDURLULUDDRURRDRUDLLDRURRUUDDDRDUUULDURRULDLLDLDLRDUDURRRRDLDRRLUDURLUDRRLUDDLLDUULLDURRLRDRLURURLUUURRLUDRRLLULUULUDRUDRDLUL",
                                                          "LRUULRRUDUDDLRRDURRUURDURURLULRDUUDUDLDRRULURUDURURDRLDDLRUURLLRDLURRULRRRUDULRRULDLUULDULLULLDUDLLUUULDLRDRRLUURURLLUUUDDLLURDUDURULRDLDUULDDRULLUUUURDDRUURDDDRUUUDRUULDLLULDLURLRRLRULRLDLDURLRLDLRRRUURLUUDULLLRRURRRLRULLRLUUDULDULRDDRDRRURDDRRLULRDURDDDDDLLRRDLLUUURUULUDLLDDULDUDUUDDRURDDURDDRLURUDRDRRULLLURLUULRLUDUDDUUULDRRRRDLRLDLLDRRDUDUUURLRURDDDRURRUDRUURUUDLRDDDLUDLRUURULRRLDDULRULDRLRLLDRLURRUUDRRRLRDDRLDDLLURLLUDL",
                                                          "ULURLRDLRUDLLDUDDRUUULULUDDDDDRRDRULUDRRUDLRRRLUDLRUULRDDRRLRUDLUDULRULLUURLLRLLLLDRDUURDUUULLRULUUUDRDRDRUULURDULDLRRULUURURDULULDRRURDLRUDLULULULUDLLUURULDLLLRDUDDRRLULUDDRLLLRURDDLDLRLLLRDLDRRUUULRLRDDDDRUDRUULDDRRULLDRRLDDRRUDRLLDUDRRUDDRDLRUDDRDDDRLLRDUULRDRLDUDRLDDLLDDDUUDDRULLDLLDRDRRUDDUUURLLUURDLULUDRUUUDURURLRRDULLDRDDRLRDULRDRURRUDLDDRRRLUDRLRRRRLLDDLLRLDUDUDDRRRUULDRURDLLDLUULDLDLDUUDDULUDUDRRDRLDRDURDUULDURDRRDRRLLRLDLU" };
        
        public static void RunDay2()
        {
            string[] input = input_2;

            string buttonsPressed = "";
            KeyPadPresser keyPadPresser = new KeyPadPresser();

            foreach (var instructionString in input)
            {
                keyPadPresser.MoveByInstruction(instructionString);
                char buttonPressed = keyPadPresser.GetCurrentButton();
                buttonsPressed += buttonPressed;
            }
            System.Diagnostics.Debug.WriteLine("Buttons Pressed: " + buttonsPressed);
        }

        class KeyPadPresser
        {
            static readonly char[][] KeyPad_Part1 = new char[][] { new char[] { '1', '2', '3' },
                                                                   new char[] { '4', '5', '6' },
                                                                   new char[] { '7', '8', '9' } };

            static readonly char[][] KeyPad_Part2 = new char[][] { new char[] { '0', '0', '1', '0', '0' },
                                                                   new char[] { '0', '2', '3', '4', '0' },
                                                                   new char[] { '5', '6', '7', '8', '9' },
                                                                   new char[] { '0', 'A', 'B', 'C', '0' },
                                                                   new char[] { '0', '0', 'D', '0', '0' } };
            char[][] KeyPad;


            public int CurrentButtonX { get; private set; }
            public int CurrentButtonY { get; private set; }

            public KeyPadPresser()
            {
                // Part 1
                //CurrentButtonX = 1;
                //CurrentButtonY = 1;
                //KeyPad = KeyPad_Part1;

                // Part 2
                CurrentButtonX = 0;
                CurrentButtonY = 2;
                KeyPad = KeyPad_Part2;
            }

            public void MoveByInstruction(string instructionSequence)
            {
                foreach (char direction in instructionSequence)
                {
                    Move(direction);
                }
            }
            
            private void Move(char direction)
            {
                switch (direction)
                {
                    case 'U':
                        CurrentButtonY -= 1;
                        if (CurrentButtonY < 0 || KeyPad[CurrentButtonY][CurrentButtonX] == '0')
                        {
                            CurrentButtonY += 1;
                        }
                        break;
                    case 'D':
                        CurrentButtonY += 1;
                        if (CurrentButtonY > KeyPad[0].Length - 1 || KeyPad[CurrentButtonY][CurrentButtonX] == '0')
                        {
                            CurrentButtonY -= 1;
                        }
                        break;
                    case 'L':
                        CurrentButtonX -= 1;
                        if (CurrentButtonX < 0 || KeyPad[CurrentButtonY][CurrentButtonX] == '0')
                        {
                            CurrentButtonX += 1;
                        }
                        break;
                    case 'R':
                        CurrentButtonX += 1;
                        if (CurrentButtonX > KeyPad.Length - 1 || KeyPad[CurrentButtonY][CurrentButtonX] == '0')
                        {
                            CurrentButtonX -= 1;
                        }
                        break;
                }
            }

            public char GetCurrentButton()
            {
                return KeyPad[CurrentButtonY][CurrentButtonX];
            }

        }
    }
}
