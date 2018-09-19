using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers {
    public class MoveControler {

        private class Piece {

            public int position { get; private set; }
            public int? leftNode { get; private set; }  // holds the number for the left node
            public int? rightNode { get; private set; } // holds the number for the right node 

            public char color { get; private set; } // need comment to see what kind of char goes here. Either (O or *)||(+ or @)

            public Piece(int position, char color) {
                this.position = position;
                this.color = color;
                calculateNodeValues();
            }

            private void calculateNodeValues() {

                int[] RightSideNums = { 1,5,13,21,29 };
                int[] LeftSideNums = { 4,8,12,20,28 };

                int thisNode = position;
                bool side = false;

                foreach(int i in RightSideNums) {
                    if(i == thisNode) {
                        leftNode = 4;
                        side = true;
                    }
                }
                foreach(int i in LeftSideNums) {
                    if(i == thisNode) {
                        rightNode = 4;
                        side = true;
                    }
                }
                if(!side) {
                    bool change = true;
                    int changeValue = 4;
                    for(int i = 2; i <= 32; i++) {
                        if(change) {
                            if(i == thisNode) {
                                leftNode = 4;
                                rightNode = 5;
                            }
                        } else {
                            if(i == thisNode) {
                                leftNode = 3;
                                rightNode = 4;
                            }
                        }
                        if(i == changeValue) {
                            change = false;
                            changeValue += 4;
                        }
                    }
                }

            }
        }
        
        public static int numOfBlackPieces { private set; get; } = 12;
        public static int numOfWhitePieces { private set; get; } = 12;
        public static bool gameOver { get {
                if((numOfBlackPieces < 1)||(numOfWhitePieces < 1))
                    return true;
                else
                    return false;
            }
            set { }
        }

        public static void ProcessMove(MoveRequest Request = null,CheckerBoard TheBoard = null) {

            // Build Test Data When Params are Null
            if(Request == null) {
                Request = new MoveRequest {
                    RawUserInput = "b,1,2,3,4",
                    CurrentUser = 'b'
                };
                for(int i = 1; i < 5; i++)
                    Request.Moves.Add(i);
                Debug.WriteLine(Request);
            }
            if(TheBoard == null) {
                TheBoard = new CheckerBoard();
                Debug.WriteLine(TheBoard);
                TheBoard.MovePiece('b',12,16);
                TheBoard.MovePiece('w',24,20);
                TheBoard.MovePiece('b',11,15);
                TheBoard.MovePiece('w',23,19);
                TheBoard.MovePiece('b',10,14);
                TheBoard.MovePiece('w',22,18);
                TheBoard.MovePiece('b',9,13);
                TheBoard.MovePiece('w',21,17);
                TheBoard.RemovePiece(32);
                TheBoard.RemovePiece(29);
                Debug.WriteLine(TheBoard);
            }

            var BoardArray = TheBoard.Board;          // Getting a copy of the array
            bool CurrentPlayer = TheBoard.whosTurn(); // Black == false;  White == True
            int PieceToMove = Request.Moves[0];       // refrence of whats being moved

            // Define what a side is
            int[] RightSideNums = { 1,5,13,21,29 };
            int[] LeftSideNums = { 4,8,12,20,28 };
            int[] TopSideNums = { 29,30,31,32 };
            int[] BottomSideNums = { 1,2,3,4 };

            // Vars to tell what side a piece is on
            bool RightSide = IsSide(PieceToMove, RightSideNums);
            bool LeftSide = IsSide(PieceToMove, LeftSideNums);
            bool TopSide = IsSide(PieceToMove, TopSideNums);
            bool BottomSide = IsSide(PieceToMove,BottomSideNums);

            // Group Data Into Lists 
            List<Piece> BlackPieces = GetSpaces(false,TheBoard);       
            List<Piece> WhitePieces = GetSpaces(true,TheBoard);
            List<Piece> AllPieces = new List<Piece>(BlackPieces); AllPieces.AddRange(WhitePieces);
            //List<Piece> MovesToMake = new List<Piece>(); // Currently Empty On Instatiation

            // Reset Global vars
            numOfBlackPieces = BlackPieces.Count;
            numOfWhitePieces = WhitePieces.Count;

            // Logic
            // Start itterating through the pieces and find all jumps for any given piece
            // Need to make a move object that holds all possible moves for said piece

            

        }
        
        // Need Method to check if piece is a king. Call should be in the method below.

        private void findAllMoves(CheckerBoard TheBoard, List<Piece> PieceList, int PieceToMove) { // this method will need to return something

            int numOfPiecesTaken = 0;
            int startPosition = PieceToMove;
            int endPosition;
            List<int> RemovePieceList = new List<int>();
            bool color = TheBoard.whosTurn();
            bool pieceFound = false;

            if(TheBoard.whosTurn() == false) { // if Blacks Move
                
                foreach(Piece i in PieceList) { // 
                    if(i.position == PieceToMove) {
                        pieceFound = true; // Piece Found in list
                    }// TODO: Need an else for when a piece is not found -- maybe throw an exception
                }
                if(pieceFound) {
                    // this is where the search is made from all pieces to find all possible moves from said piece 
                    foreach(Piece i in PieceList) {
                        if(i.leftNode != null) {
                            //if(TheBoard.findPiece(i.leftNode) == )
                        }
                    }


                } else {

                }
            }
        }

        private static bool IsSide(int space, int[] Side) {
                foreach(int i in Side)
                    if(i == space)
                        return true;
                return false;
            
        }

        private static List<Piece> GetSpaces(bool Color, CheckerBoard TheBoard) {

            char color = ' '; /// Your going to need something here for checking if this is a king or not
            if(Color)
                color = '+';
            else
                color = 'O';

            List<Piece> ReturnList = new List<Piece>();

            for(int i = 1; i < 33; i++) {

                var temp = TheBoard.findPiece(i);
                if(temp == color) {
                    ReturnList.Add(new Piece(i,color));
                }
            }
            return ReturnList;
        }
        //public bool areThereMoves()
        //{
        //    //Checks if a move exists for the current player
        //    //player1 = false Blacks turn
        //    for (int i = 0; i < 8; i++)
        //    {
        //        for (int j = 0; j < 8; j++)
        //        {
        //            if (Board[i, j].piece == null)
        //            {
        //                //Dont look at blank space
        //                continue;
        //            }
        //            //Shorten all the neighors to cleaner code
        //            int? tl = Board[i, j].piece.topleftNode;
        //            int? tr = Board[i, j].piece.toprightNode;
        //            int? bl = Board[i, j].piece.toprightNode;
        //            int? br = Board[i, j].piece.toprightNode;
        //            if ((Board[i, j].piece.face == 'O') & (player1 == false))
        //            {
        //                //Piece is black and its blacks turn
        //                //Edge cases for the sides of the board. 
        //                //=============Black Man Edge cases =========
        //                if ((tl == null) & (GetPiece(tr) != null))
        //                {
        //                    //if left is off the board and the right is full, skip
        //                    continue;
        //                }
        //                else if ((tr == null) & (GetPiece(tl) != null))
        //                {
        //                    //if right is off the board and left is full, skip
        //                    continue;
        //                }
        //                else if ((tr == null) & (GetPiece(tl) == null))
        //                {
        //                    //left is off the board and right is empty, return true
        //                    return true;
        //                }
        //                else if ((tl == null) & (GetPiece(tr) == null))
        //                {
        //                    //left is off the board and right is empty, return true
        //                    return true;
        //                }
        //                //============================================

        //                if ((GetPiece(tl) == null) | (GetPiece(tr) == null))
        //                {
        //                    return true;
        //                }
        //                // TODO check for jumps
        //                // if jump is found return true

        //            }
        //            else if ((Board[i, j].piece.face == '@') & (player1 == false))
        //            {
        //                // ==============KING Edge Cases ==============
        //                if ((tl == null) & (GetPiece(tr) != null))
        //                {
        //                    //if left is off the board and the right is full, skip
        //                    continue;
        //                }
        //                else if ((tr == null) & (GetPiece(tl) != null))
        //                {
        //                    //if right is off the board and left is full, skip
        //                    continue;
        //                }
        //                else if ((tr == null) & (GetPiece(tl) == null))
        //                {
        //                    //left is off the board and right is empty, return true
        //                    return true;
        //                }
        //                else if ((tl == null) & (GetPiece(tr) == null))
        //                {
        //                    //left is off the board and right is empty, return true
        //                    return true;
        //                }
        //                else if ((bl == null) & (GetPiece(br) != null))
        //                {
        //                    //if left is off the board and the right is full, skip
        //                    continue;
        //                }
        //                else if ((br == null) & (GetPiece(bl) != null))
        //                {
        //                    //if right is off the board and left is full, skip
        //                    continue;
        //                }
        //                else if ((br == null) & (GetPiece(bl) == null))
        //                {
        //                    //left is off the board and right is empty, return true
        //                    return true;
        //                }
        //                else if ((bl == null) & (GetPiece(br) == null))
        //                {
        //                    //left is off the board and right is empty, return true
        //                    return true;
        //                }
        //                // ==================================================

        //                if ((tl == null) | (tr == null) | (bl == null) | (br == null))
        //                {
        //                    return true;
        //                }
        //                // Piece is  black king and its blacks turn
        //                //TODO all direction

        //            }
        //            else if ((Board[i, j].piece.face == '+') & (player1 == true))
        //            {

        //                //==============White Man Edge cases===================
        //                if ((bl == null) & (GetPiece(br) != null))
        //                {
        //                    //if left is off the board and the right is full, skip
        //                    continue;
        //                }
        //                else if ((br == null) & (GetPiece(bl) != null))
        //                {
        //                    //if right is off the board and left is full, skip
        //                    continue;
        //                }
        //                else if ((br == null) & (GetPiece(bl) == null))
        //                {
        //                    //left is off the board and right is empty, return true
        //                    return true;
        //                }
        //                else if ((bl == null) & (GetPiece(br) == null))
        //                {
        //                    //left is off the board and right is empty, return true
        //                    return true;
        //                }
        //                // ======================================================

        //                if ((bl == null) | (br == null))
        //                {
        //                    return true;
        //                }
        //                // Piece is white and its white turn
        //                //TODO bottom left bottom right
        //            }
        //            else if ((Board[i, j].piece.face == '#') & (player1 == true))
        //            {
        //                // ==============KING Edge Cases ==============
        //                if ((tl == null) & (GetPiece(tr) != null))
        //                {
        //                    //if left is off the board and the right is full, skip
        //                    continue;
        //                }
        //                else if ((tr == null) & (GetPiece(tl) != null))
        //                {
        //                    //if right is off the board and left is full, skip
        //                    continue;
        //                }
        //                else if ((tr == null) & (GetPiece(tl) == null))
        //                {
        //                    //left is off the board and right is empty, return true
        //                    return true;
        //                }
        //                else if ((tl == null) & (GetPiece(tr) == null))
        //                {
        //                    //left is off the board and right is empty, return true
        //                    return true;
        //                }
        //                else if ((bl == null) & (GetPiece(br) != null))
        //                {
        //                    //if left is off the board and the right is full, skip
        //                    continue;
        //                }
        //                else if ((br == null) & (GetPiece(bl) != null))
        //                {
        //                    //if right is off the board and left is full, skip
        //                    continue;
        //                }
        //                else if ((br == null) & (GetPiece(bl) == null))
        //                {
        //                    //left is off the board and right is empty, return true
        //                    return true;
        //                }
        //                else if ((bl == null) & (GetPiece(br) == null))
        //                {
        //                    //left is off the board and right is empty, return true
        //                    return true;
        //                }
        //                // ==================================================

        //                if ((tl == null) | (tr == null) | (bl == null) | (br == null))
        //                {
        //                    return true;
        //                }
        //                // Piece is white king and its whites turn
        //                //TODO all directions
        //            }

        //        }
        //    }

        //    return false;

        }

        //}


        //}
  
}

