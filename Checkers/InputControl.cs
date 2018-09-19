using System;
using System.Collections.Generic;

namespace Checkers {

    public class MoveRequest { // Object type that has move request data -- only method is the constructor
        public String RawUserInput { get; set; } = ""; 
        public char CurrentUser { get; set; } = ' ';   
        public List<int> Moves { get; set; }          
        public MoveRequest() {
            Moves = new List<int>();
        }
        public override string ToString() {
            String ReturnValue = RawUserInput + "\n" +   CurrentUser  + "\n";
            foreach(int i in Moves) {
                ReturnValue += i + " ";
            }
            return ReturnValue;
        }
    }

    public static class InputControl {

        // Class Level Vars
        public static CheckerBoard _CheckerBoard;
        public static MoveRequest UserInput;

        // Class Level Methods (public)
        public static MoveRequest GetUserInput(CheckerBoard checkerBoard) {

            if(UserInput == null)
                UserInput = new MoveRequest();

            _CheckerBoard = checkerBoard;
            String userName = "";
 
            if(checkerBoard.whosTurn() == false) { // false == player1 (Black); true == player2 (White)
                userName = "Black,";
            } else {
                userName = "White,";
            }

            PrintLine(_CheckerBoard.ToString() + "\n\n");

            PrintLine(userName + " please enter your move: ");
            UserInput.RawUserInput = Console.ReadLine();
            return ParseInput();
        } 

        private static void PrintLine(String output) {
            // this method is depricated -- it's purpous was for dynamic change between the system console and the debug console
            Console.WriteLine(output);
        }

        public static MoveRequest ParseInput(String userInput = null) {

#if DEBUG
            Console.WriteLine(userInput); // Echos the input when in DEBUG mode
#endif 

            String localUserInput; 
            if(userInput != null){
                localUserInput = userInput.ToLower();
            } else {
                localUserInput = UserInput.RawUserInput.ToLower();
            }
           
            //String localUserInput = UserInput.RawUserInput.ToLower();
            List<int?> MoveList = new List<int?>();

            // Parsing Section
            String[] Commands = localUserInput.Split(',');
            foreach(String word in Commands) {
                int TempNum = -1;
                switch(word) {
                    case "q":
                        UserInput.CurrentUser = 'q';
                        break;
                    case "w":
                        UserInput.CurrentUser = 'w';
                        break;
                    case "b":
                        UserInput.CurrentUser = 'b';
                        break;
                    default:
                        if((int.TryParse(word, out TempNum)) == false) {
                            PrintLine("Bad Input Error. Please Re-Enter Your Selection: ");
                            UserInput = new MoveRequest {
                                RawUserInput = Console.ReadLine()
                            };
                            ParseInput();
                            return UserInput;
                        } else {
                            UserInput.Moves.Add(TempNum);
                        }
                        break;
                }// end switch
            }// end loop

            // Error Checks Below
            if(!((UserInput.CurrentUser == 'w') || // Syntax Check
                 (UserInput.CurrentUser == 'b') ||
                 (UserInput.CurrentUser == 'q'))){
                    PrintLine("Syntax Error: Please Re-Enter Your Selection");
                UserInput = new MoveRequest {
                    RawUserInput = Console.ReadLine()
                };
                ParseInput();
                    return UserInput;
            } else { // turn checks
                if(UserInput.CurrentUser == 'q') {
                    System.Environment.Exit(1);
                    return UserInput;
                }else if(UserInput.CurrentUser == 'w') {
                    if(_CheckerBoard.whosTurn() != true) {
                        PrintLine("Out Of Turn Error: Please Re-Enter Your Selection");
                        UserInput = new MoveRequest {
                            RawUserInput = Console.ReadLine()
                        };
                        ParseInput();
                        return UserInput;
                    }
                }else if(UserInput.CurrentUser == 'b') {
                    if(_CheckerBoard.whosTurn() != false) {
                        PrintLine("Out Of Turn Error: Please Re-Enter Your Selection");
                        UserInput = new MoveRequest {
                            RawUserInput = Console.ReadLine()
                        };
                        ParseInput();
                        return UserInput;
                    }
                }
            }
            foreach(int i in UserInput.Moves) { // check to make sure all the numbers are within range
                if(i < 0) {
                    PrintLine("Input Out of Bounds Error: Please Re-Enter Your Selection");
                    UserInput = new MoveRequest {
                        RawUserInput = Console.ReadLine()
                    };
                    ParseInput();
                    return UserInput;
                } else if(i > 32) {
                    PrintLine("Input Out of Bounds Error: Please Re-Enter Your Selection");
                    UserInput = new MoveRequest {
                        RawUserInput = Console.ReadLine()
                    };
                    ParseInput();
                    return UserInput;
                }
            }

            bool firstLoop = true;
            foreach(int i in UserInput.Moves) {

                int position = UserInput.Moves[0];
                char piece = _CheckerBoard.findPiece(position);

                if((piece == '#')||(piece == '@'))
                    break;

                if(firstLoop) {
                    firstLoop = false;
                    continue;
                }
                /*if(i == UserInput.Moves[0]) {
                    Console.WriteLine("Move Error: Cannot place a piece where it came from. Re-Enter Your Selection");
                    UserInput = new MoveRequest {
                        RawUserInput = Console.ReadLine()
                    };
                    ParseInput();
                    return UserInput;
                }*/
            }

            return UserInput;
        }// end ParseInput

    }// end class
}// end namespace