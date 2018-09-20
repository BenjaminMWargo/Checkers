using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers {
    public class GameControl { // Singlton Class
                               // this class will run the overall game

        private static CheckerBoard TheCheckerBoard;
        private GameControl() { TheCheckerBoard = new CheckerBoard(); }
        private static GameControl Instance;
        public static GameControl GetInstance() {
            if(Instance == null)
                Instance = new GameControl();
            return Instance;
        }

        public void RUN() {

           
            while(true) {
                var MoveRequest = InputControl.GetUserInput(TheCheckerBoard);
                if(MoveRequest.CurrentUser == 'q'){
                    return;
                }
                MakeMove(MoveRequest);
               
                // If no moves or no jumps, game is over for current player
                if((!TheCheckerBoard.anyJumps()) & !(TheCheckerBoard.areThereMoves())){
                    bool turn = TheCheckerBoard.whosTurn();
                    //false = blacks turn
                    if (turn){
                        Console.WriteLine("Black wins!");
                    }
                    else{
                        Console.WriteLine("White wins!");
                    }
                    return;
                }
            }
        }
        private void MakeMove(MoveRequest request) {
            char player = request.CurrentUser;
            List<int> moves = new List<int>(request.Moves);
            List<int> savedMoves = new List<int>(request.Moves);

            bool isJump = false;
            while(moves.Count > 1){
                int a = moves[0];
                int b = moves[1];
                int c = 0;
                if(moves.Count >= 3)
                    c = moves[2];
               
                if (TheCheckerBoard.anyJumps()){
                  //if there is a jump, they have to take it
                  isJump = true;
                }
                //If a and b are neighbors and only two inputs, normal move
                if (TheCheckerBoard.areNeigbors(a,b) && (moves.Count == 2)){
                    //normal move 

                    if (isJump){
                        //throw error
                        Console.WriteLine("Required Jump! Re-Enter Your Move");
                        var RawInput = Console.ReadLine();
                        InputControl.UserInput = new MoveRequest();
                        var UsrInput = InputControl.ParseInput(RawInput);
                        MakeMove(UsrInput);
                        return;
                     }
                    break;
                }


                //See if a and b can jump

                if(TheCheckerBoard.isItAJump(a,b,c)) {
                    var x = moves[0];
                    moves.Remove(b);
                    moves.Remove(c);
                    if(moves.Count < 2) {
                        //ALL jumps correct
                        break;
                    }
                }else if(TheCheckerBoard.isItAJump(a,b)) {
                    var x = moves[0];
                    moves.Remove(b);
                    if(moves.Count < 2) {
                        //ALL jumps correct
                        break;
                    }
                } else{
                    //Invalid jump.Force new input
                    Console.WriteLine("Invalid Jump! Re-Enter Your Move");
                    var input = Console.ReadLine();
                    //request = null;
                    MakeMove(InputControl.ParseInput(input));
                    return;
                }

            }

            //Make the moves using savedMoves
            for(int i = 0; i < request.Moves.Count - 1; i++) {

                try {
                    TheCheckerBoard.MovePiece(player,request.Moves[i],request.Moves[i+1]);
                }catch(Exception e) {
                    Console.WriteLine("Invalid Input! Re-Enter Your Move");
                    var input = Console.ReadLine();
                    MakeMove(InputControl.ParseInput(input));
                    return;
                }

                if (isJump){
                //Find shared neigbor, delete it
                     List<int?> aList = new List<int?>();
                     List<int?> bList = new List<int?>();
                     aList = TheCheckerBoard.getNeighbors(request.Moves[i]);
                     bList = TheCheckerBoard.getNeighbors(request.Moves[i+1]);
                     foreach (int? k in aList){
                        foreach(int? j in bList){
                            if (k == j){
                                TheCheckerBoard.RemovePiece((int)k);
                            }

                        }
                     }
                }
            }
            //Clears out cached moves
            request.Moves.Clear();
            moves = null;
            savedMoves = null;
            request = null;
            TheCheckerBoard.player1 = !TheCheckerBoard.player1;
        }
    }
}