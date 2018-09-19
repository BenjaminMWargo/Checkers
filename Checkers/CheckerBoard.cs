using System;
using System.Collections.Generic;

namespace Checkers {

    public class CheckerBoard {

        // inner classes
        public class Piece { // this class defines what a piece is

            //class level vars
            private bool white; // White == false; Black == true;
            public char face;
            public int position { get; set; }
            public int? topleftNode { get; private set; }  // holds the number for the top left node
            public int? toprightNode { get; private set; } // holds the number for the top right node 
            public int? bottomleftNode { get; private set; }  // holds the number for the bottom left node
            public int? bottomrightNode { get; private set; } // holds the number for the bottom right node 

            public char color { get; private set; } // need comment to see what kind of char goes here. Either (O or *)||(+ or @)

            // constructor and methods
            public Piece(bool white,int position) {
                this.white = white;
                if(this.white == false)// if White then -- creates the GUI for the checker in string format
                    face = '+';
                else
                    face = 'O';

                this.position = position;
                this.color = color;
                calculateNodeValues();
            }// end piece constuctor

            public override String ToString() {
                return face.ToString();
            }

            public void KingMe() {
                if(white) // if white
                    face = '@';
                else
                    face = '#';
            }

            public bool getCheckerType() {
                return white; // White == false; Black == true;
            }

            public char getCheckerFaceValue() {
                return face;
            }

            public void calculateNodeValues() {

                int[] RightSideNums = { 5,13,21,29 };
                int[] LeftSideNums = { 4,12,20,28 };

                int thisNode = position;
                bool side = false;

                foreach(int i in RightSideNums) {
                    if(i == thisNode) {
                        topleftNode = thisNode + 4;
                        bottomleftNode = thisNode - 4;
                        side = true;
                    }
                }
                foreach(int i in LeftSideNums) {
                    if(i == thisNode) {
                        toprightNode = thisNode + 4;
                        bottomleftNode = thisNode - 4;
                        side = true;
                    }
                }
                if(!side) {
                    //Odd number lines, topleft increases by 5, even by 4
                    //Top right is always top left - 1
                    //bottoms are always 8 less than the top
                    bool change = true; // odd line
                    int counter = 1; //Counts squares on line
                    int changeValue = 4;
                    for(int i = 1; i <= 32; i++) {
                        if(change) {
                            if(i == thisNode) {
                                topleftNode = thisNode + 5;
                                toprightNode = topleftNode - 1;
                                bottomleftNode = topleftNode - 8;
                                bottomrightNode = toprightNode - 8;
                                break;
                            }
                        } else {
                            if(i == thisNode) {
                                topleftNode = thisNode + 4;
                                toprightNode = topleftNode - 1;
                                bottomleftNode = topleftNode - 8;
                                bottomrightNode = toprightNode - 8;
                                break;
                            }
                        }

                        if(counter == 4) {
                            change = !change;
                            counter = 0;
                        }
                        counter++;
                    }
                }
                if(topleftNode > 32) {
                    topleftNode = null;
                }
                if(toprightNode > 32) {
                    toprightNode = null;
                }
                if(bottomleftNode < 1) {
                    bottomleftNode = null;
                }
                if(bottomrightNode < 1) {
                    bottomrightNode = null;
                }
            }
        }

        public class Space { // this class defines what a space on the checker board is and what is in each space

            // class level vars
            public bool BlackSpace; // Black space == true; White Space == false
            public Piece piece;     // holds a players checker
            public int? number;     // holds the number of the specific space being refrenced. If NULL the space is white

            // constructor and methods
            public Space(bool BlackSpace,int? number = null,Piece piece = null) {
                this.BlackSpace = BlackSpace;
                this.piece = piece;
                this.number = number;
            }// end space constructor

            public override string ToString() {
                if(piece == null)
                    return "EMPTY";
                return piece.ToString();
            }
        }

        // class level vars
        public Space[,] Board { get; private set; } // Instance of the checker board -- initilized to [8,8] in the constructor
        public Space[,] BoardCopy {
            get {
                var temp = new Space[8,8];
                for(int x = 0; x < 8; x++) {
                    for(int y = 0; y < 8; y++) {
                        temp[x,y] = Board[x,y];
                    }
                }
                return temp;
            }
        }
        public bool player1 = false; // false == player1's move; true == player2's move 

        // methods and constructor
        public CheckerBoard() { // Might not be a good idea to make this class a singleton. Need to be able to run more than one game

            Board = new Space[8,8];

            // * in the comment means that the row has pieces in it

            // initilize bottom row (7)*
            Board[7,7] = new Space(false);
            Board[7,6] = new Space(false,1,new Piece(true,1));
            Board[7,5] = new Space(false);
            Board[7,4] = new Space(false,2,new Piece(true,2));
            Board[7,3] = new Space(false);
            Board[7,2] = new Space(false,3,new Piece(true,3));
            Board[7,1] = new Space(false);
            Board[7,0] = new Space(false,4,new Piece(true,4));

            // initilize row (6)*
            Board[6,7] = new Space(false,5,new Piece(true,5));
            Board[6,6] = new Space(false);
            Board[6,5] = new Space(false,6,new Piece(true,6));
            Board[6,4] = new Space(false);
            Board[6,3] = new Space(false,7,new Piece(true,7));
            Board[6,2] = new Space(false);
            Board[6,1] = new Space(false,8,new Piece(true,8));
            Board[6,0] = new Space(false);

            // initilize row (5)*
            Board[5,7] = new Space(false);
            Board[5,6] = new Space(false,9,new Piece(true,9));
            Board[5,5] = new Space(false);
            Board[5,4] = new Space(false,10,new Piece(true,10));
            Board[5,3] = new Space(false);
            Board[5,2] = new Space(false,11,new Piece(true,11));
            Board[5,1] = new Space(false);
            Board[5,0] = new Space(false,12,new Piece(true,12));

            // initilize row (4)
            Board[4,7] = new Space(false,13);
            Board[4,6] = new Space(false);
            Board[4,5] = new Space(false,14);
            Board[4,4] = new Space(false);
            Board[4,3] = new Space(false,15);
            Board[4,2] = new Space(false);
            Board[4,1] = new Space(false,16);
            Board[4,0] = new Space(false);

            // initilize row (3)
            Board[3,7] = new Space(false);
            Board[3,6] = new Space(false,17);
            Board[3,5] = new Space(false);
            Board[3,4] = new Space(false,18);
            Board[3,3] = new Space(false);
            Board[3,2] = new Space(false,19);
            Board[3,1] = new Space(false);
            Board[3,0] = new Space(false,20);

            // initilize row (2)*
            Board[2,7] = new Space(false,21,new Piece(false,21));
            Board[2,6] = new Space(false);
            Board[2,5] = new Space(false,22,new Piece(false,22));
            Board[2,4] = new Space(false);
            Board[2,3] = new Space(false,23,new Piece(false,23));
            Board[2,2] = new Space(false);
            Board[2,1] = new Space(false,24,new Piece(false,24));
            Board[2,0] = new Space(false);

            // initilize row (1)*
            Board[1,7] = new Space(false);
            Board[1,6] = new Space(false,25,new Piece(false,25));
            Board[1,5] = new Space(false);
            Board[1,4] = new Space(false,26,new Piece(false,26));
            Board[1,3] = new Space(false);
            Board[1,2] = new Space(false,27,new Piece(false,27));
            Board[1,1] = new Space(false);
            Board[1,0] = new Space(false,28,new Piece(false,28));

            // initilize top row (0)*
            Board[0,7] = new Space(false,29,new Piece(false,29));
            Board[0,6] = new Space(false);
            Board[0,5] = new Space(false,30,new Piece(false,30));
            Board[0,4] = new Space(false);
            Board[0,3] = new Space(false,31,new Piece(false,31));
            Board[0,2] = new Space(false);
            Board[0,1] = new Space(false,32,new Piece(false,32));
            Board[0,0] = new Space(false);

        }// End Board Constructor

        public override string ToString() { // Prints the checker board
            string output = "";
            for(int i = 0; i < 8; i++) {
                output += "|";
                for(int j = 0; j < 8; j++) {
                    if(Board[i,j].piece == null)
                        output += " |";
                    else
                        output += Board[i,j].piece + "|";
                    if(j == 7)
                        output += "\n";
                }
            }
            return output;
        }

        public void MovePiece(char player,int currentSpace,int newSpace) {


            player = player.ToLower(); // ensures the players can type in uppercase and/or lowercase 

            #region Exceptions

            // Basic Error Checks 
            if((player != 'b') && (player != 'w'))
                throw new InvalidOperationException("Invalid Player Parameter");

            if(player != 'w' && player1)
                throw new InvalidOperationException("Out Of Turn Exception");
            else if(player != 'b' && !player1)
                throw new InvalidOperationException("Out Of Turn Exception");

            if(currentSpace > 32)
                throw new InvalidOperationException("Invalid Paramater: Integer value cannot be greater than 32");

            if(currentSpace < 1)
                throw new InvalidOperationException("Invalid Paramater: Integer value cannot be less than 1");

            if(newSpace > 32)
                throw new InvalidOperationException("Invalid Paramater: Integer value cannot be greater than 32");

            if(newSpace < 1)
                throw new InvalidOperationException("Invalid Paramater: Integer value cannot be less than 1");

            if(newSpace == currentSpace)
                throw new InvalidOperationException("Invalid Move: Cannot move checker to the same point it started");

            #endregion

            // major local vars
            Piece piece = null;
            int newSpaceRow = 0;
            int newSpaceColoumn = 0;
            int currentRow = 0;
            int currentColumn = 0;
            bool newSpaceFound = false;
            char checkerFace = ' ';

            if(player == 'w')
                checkerFace = '+';
            else if(player == 'b')
                checkerFace = 'O';

            for(int i = 0; i < 8; i++) { // find piece to move
                for(int j = 0; j < 8; j++) {

                    if(newSpace == Board[i,j].number) { // if new space found
                        if(Board[i,j].piece != null)
                            throw new InvalidOperationException("Move Error: Checker already exists at specified location");
                        newSpaceRow = i;
                        newSpaceColoumn = j;
                        newSpaceFound = true;
                    }// end if

                    if(Board[i,j].number == currentSpace) { // if selected space found

                        if(Board[i,j].piece != null) {
                            piece = Board[i,j].piece;
                            Board[i,j].piece = null;
                            currentRow = i;
                            currentColumn = j;
                        } else {    // Error when player to move a checker from an empty space (checker does not exist at specified location)
                            throw new InvalidOperationException("Move Error: Checker does not exist at specified location");
                        }// end inner if
                    }// end outter if
                }// end inner loop
            }// end outter loop

            if(newSpaceFound) {
                piece.position = newSpace;
                Board[newSpaceRow,newSpaceColoumn].piece = piece;
                Board[newSpaceRow,newSpaceColoumn].piece.position = (int)Board[newSpaceRow,newSpaceColoumn].number;
                Board[newSpaceRow,newSpaceColoumn].piece.calculateNodeValues();
            } else {
                for(int i = currentRow; i < 8; i++) { // if new piece not found then itterate from last position
                    for(int j = currentColumn; j < 8; j++) {
                        if(Board[i,j].number == newSpace) { // if the correct space is found
                            if(Board[i,j].piece != null)
                                throw new InvalidOperationException("Move Error: Checker already exists at specified location");
                            if(Board[i,j].piece.getCheckerFaceValue() != checkerFace)
                                throw new InvalidOperationException("Move Error: Cannot move other players checker");
                            Board[i,j].piece = piece; // then put piece into its new place
                            Board[i,j].piece.position = (int)Board[i,j].number;
                            Board[i,j].piece.calculateNodeValues();

                            return;
                        }// end if
                    }// end inner loop
                } // end outter loop
            } // end if

            if(KingMeCheck(newSpace,checkerFace))
                Board[newSpaceRow,newSpaceColoumn].piece.KingMe();

            //player1 = !player1; // change player flag to the denote the next players move
        }// end MovePiece

        public char findPiece(int spaceNumber) {

            if(spaceNumber > 32)
                throw new InvalidOperationException("Spaces grater than 32 do not exist!");
            if(spaceNumber < 1)
                throw new InvalidOperationException("Spaces less than 1 do not exist!");

            foreach(Space space in Board) {
                if(space.number == spaceNumber)

                    return space.piece.getCheckerFaceValue(); // NOTE: This will throw a null refrence error if no piece is in the space
            }

            // This exception should never happen!
            throw new InvalidOperationException("Critical Error: Space Not Found!");

        }

        public void RemovePiece(int spaceNumber) {


            if(spaceNumber > 32)
                throw new InvalidOperationException("Spaces grater than 32 do not exist!");
            if(spaceNumber < 1)
                throw new InvalidOperationException("Spaces less than 1 do not exist!");

            foreach(Space space in Board) {
                if(space.number == spaceNumber) {
                    space.piece = null;
                    return;
                }
            }

            // This exception should never happen!
            throw new InvalidOperationException("Critical Error: Space Not Found!");


        }

        public bool whosTurn() { // false == player1's move; true == player2's move 
            return player1;
        }

        private bool KingMeCheck(int currentSpace,char checkerFace) {
            int[] BlacksBlocks = { 29,30,31,32 };
            int[] WhitesBlocks = { 1,2,3,4 };

            if(checkerFace == 'O') {
                foreach(int num in BlacksBlocks) {
                    if(currentSpace == num) {
                        return true;
                    }
                }
                return false;
            } else if(checkerFace == '+') {
                foreach(int num in WhitesBlocks) {
                    if(currentSpace == num)
                        return true;
                }
                return false;
            }
            return false;
        }// end KingMeCheck

        public Piece GetPiece(int? spaceNumber, Space[,] boardCopy = null) {

           if(boardCopy == null) {
                if(spaceNumber == null) {
                    return null;
                };

                foreach(Space i in Board) {
                    if(i.number == spaceNumber) {
                        return i.piece;
                    }
                }
            } else {
                if(spaceNumber == null) {
                    return null;
                };

                foreach(Space i in boardCopy) {
                    if(i.number == spaceNumber) {
                        return i.piece;
                    }
                }


            }
            return null;
        }

        public bool anyJumps() {
            //if player1 = false, blacks turn
            //Loop through list, if there are any jumps return true 
            for(int i = 0; i < 8; i++) {
                for(int j = 0; j < 8; j++) {
                    if(Board[i,j].number == null) {
                        continue;
                    }

                    Piece[] Nodes = new Piece[5]; // Nodes[1] and Nodes[2] are to the front the others are the back

                    // Getting all the values above
                    try {
                        Nodes[0] = GetPiece(Board[i,j].piece.position);
                    } catch(Exception e) { }
                    try {
                        Nodes[1] = GetPiece((int)Nodes[0].topleftNode);
                    } catch(Exception e) { }
                    try {
                        Nodes[2] = GetPiece((int)Nodes[0].toprightNode);
                    } catch(Exception e) { }
                    try {
                        Nodes[3] = GetPiece((int)Nodes[0].bottomleftNode);
                    } catch(Exception e) { }
                    try {
                        Nodes[4] = GetPiece((int)Nodes[0].bottomrightNode);
                    } catch(Exception e) { }

                    if(Board[i,j].piece == null) {
                        continue;
                    }


                    if((Board[i,j].piece.face == 'O') & (player1 == false)) { // reg piece
                        //Piece is black and its blacks turn
                        //TODO any jump for topleft and topright
                        // if jump is found return true

                        // Only Look To The Front
                        if((Nodes[1] != null)||(Nodes[2] != null)) {

                            if(Nodes[1] != null) { // left node
                                // If the top left node is the opposite of the current node  
                                if(Nodes[1].getCheckerType() == !Nodes[0].getCheckerType()) {
                                    try {
                                        var TempPiece = GetPiece((int)Nodes[1].topleftNode);
                                        if(TempPiece == null)
                                            return true;
                                    } catch(NullReferenceException e) { return true; }
                                }
                            }
                            if(Nodes[2] != null) {
                                // If the top right node is the opposite of the current node  
                                if(Nodes[2].getCheckerType() == !Nodes[0].getCheckerType()) {
                                    try {
                                        var TempPiece = GetPiece((int)Nodes[2].toprightNode);
                                        if(TempPiece == null)
                                            return true;
                                    } catch(NullReferenceException e) { return true; }
                                }
                            }
                        }
                        // need to check colors 
                        // this needs to be looped though to find all possible paths
                        // check for null spaces
                        // encapsulate the code above into a method???
                    } else if((Board[i,j].piece.face == '@') & (player1 == false)) // King
                      {
                        // Checking Front Nodes
                        if((Nodes[1] != null)||(Nodes[2] != null)) {

                            if(Nodes[1] != null) { // left node
                                // If the top left node is the opposite of the current node  
                                if(Nodes[1].getCheckerType() == !Nodes[0].getCheckerType()) {
                                    try {
                                        var TempPiece = GetPiece((int)Nodes[1].topleftNode);
                                        if(TempPiece == null)
                                            return true;
                                    } catch(NullReferenceException e) { return true; }
                                }
                            }
                            if(Nodes[2] != null) {
                                // If the top right node is the opposite of the current node  
                                if(Nodes[2].getCheckerType() == !Nodes[0].getCheckerType()) {
                                    try {
                                        var TempPiece = GetPiece((int)Nodes[1].toprightNode);
                                        if(TempPiece == null)
                                            return true;
                                    } catch(NullReferenceException e) { return true; }
                                }
                            }
                        }
                        //Checking Back Nodes 
                        if((Nodes[3] != null)||(Nodes[4] != null)) {

                            if(Nodes[3] != null) { // left node
                                                   // If the top left node is the opposite of the current node  
                                if(Nodes[3].getCheckerType() == !Nodes[0].getCheckerType()) {
                                    try {
                                        var TempPiece = GetPiece((int)Nodes[3].topleftNode);
                                        if(TempPiece == null)
                                            return true;
                                    } catch(NullReferenceException e) { return true; }
                                }
                            }
                            if(Nodes[4] != null) {
                                // If the top right node is the opposite of the current node  
                                if(Nodes[4].getCheckerType() == !Nodes[0].getCheckerType()) {
                                    try {
                                        var TempPiece = GetPiece((int)Nodes[4].toprightNode);
                                        if(TempPiece == null)
                                            return true;
                                    } catch(NullReferenceException e) { return true; }
                                }
                            }
                        }

                    } else if((Board[i,j].piece.face == '+') & (player1 == true)) { // reg piece
                        // Piece is white and its white turn
                        // Only Look To The Front
                        if((Nodes[3] != null)||(Nodes[4] != null)) {

                            if(Nodes[3] != null) { // left node
                                // If the top left node is the opposite of the current node  
                                if(Nodes[3].getCheckerType() == !Nodes[0].getCheckerType()) {
                                    try {
                                        var TempPiece = GetPiece((int)Nodes[3].bottomleftNode);
                                        if(TempPiece == null)
                                            return true;
                                    } catch(NullReferenceException e) { return true; }
                                }
                            }
                            if(Nodes[4] != null) {
                                // If the top right node is the opposite of the current node  
                                if(Nodes[4].getCheckerType() == !Nodes[0].getCheckerType()) {
                                    try {
                                        var TempPiece = GetPiece((int)Nodes[4].bottomrightNode);
                                    } catch(NullReferenceException e) { return true; }
                                }
                            }
                        }
                    } else if((Board[i,j].piece.face == '#') & (player1 == true)) // King
                      {
                        // Piece is white king and its whites turn
                        // Checking Front Nodes
                        // Checking Front Nodes
                        if((Nodes[1] != null)||(Nodes[2] != null)) {

                            if(Nodes[1] != null) { // left node
                                // If the top left node is the opposite of the current node  
                                if(Nodes[1].getCheckerType() == !Nodes[0].getCheckerType()) {
                                    try {
                                        var TempPiece = GetPiece((int)Nodes[1].topleftNode);
                                        if(TempPiece == null)
                                            return true;
                                    } catch(NullReferenceException e) { return true; }
                                }
                            }
                            if(Nodes[2] != null) {
                                // If the top right node is the opposite of the current node  
                                if(Nodes[2].getCheckerType() == !Nodes[0].getCheckerType()) {
                                    try {
                                        var TempPiece = GetPiece((int)Nodes[1].toprightNode);
                                        if(TempPiece == null)
                                            return true;
                                    } catch(NullReferenceException e) { return true; }
                                }
                            }
                        }
                        //Checking Back Nodes 
                        if((Nodes[3] != null)||(Nodes[4] != null)) {

                            if(Nodes[3] != null) { // left node
                                                   // If the top left node is the opposite of the current node  
                                if(Nodes[3].getCheckerType() == !Nodes[0].getCheckerType()) {
                                    try {
                                        var TempPiece = GetPiece((int)Nodes[3].bottomleftNode);
                                        if(TempPiece == null)
                                            return true;
                                    } catch(NullReferenceException e) { return true; }
                                }
                            }
                            if(Nodes[4] != null) {
                                // If the top right node is the opposite of the current node  
                                if(Nodes[4].getCheckerType() == !Nodes[0].getCheckerType()) {
                                    try {
                                        var TempPiece = GetPiece((int)Nodes[4].bottomrightNode);
                                        if(TempPiece == null)
                                            return true;
                                    } catch(NullReferenceException e) { return true; }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }// end anyJumps

        public bool areThereMoves() {
            //Checks if a move exists for the current player
            //player1 = false Blacks turn
            for(int i = 0; i < 8; i++) {
                for(int j = 0; j < 8; j++) {
                    if(Board[i,j].piece == null) {
                        //Dont look at blank space
                        continue;
                    }
                    //Shorten all the neighors to cleaner code
                    int? tl = Board[i,j].piece.topleftNode;
                    int? tr = Board[i,j].piece.toprightNode;
                    int? bl = Board[i,j].piece.bottomleftNode;
                    int? br = Board[i,j].piece.bottomrightNode;
                    if((Board[i,j].piece.face == 'O') & (player1 == false)) {
                        //Piece is black and its blacks turn
                        //Edge cases for the sides of the board. 
                        //=============Black Man Edge cases =========
                        if((tl == null) & (GetPiece(tr) != null)) {
                            //if left is off the board and the right is full, skip
                            continue;
                        } else if((tr == null) & (GetPiece(tl) != null)) {
                            //if right is off the board and left is full, skip
                            continue;
                        } else if((tr == null) & (GetPiece(tl) == null)) {
                            //left is off the board and right is empty, return true
                            return true;
                        } else if((tl == null) & (GetPiece(tr) == null)) {
                            //left is off the board and right is empty, return true
                            return true;
                        }
                        //============================================

                        if((GetPiece(tl) == null) | (GetPiece(tr) == null)) {
                            return true;
                        }
                        // TODO check for jumps
                        // if jump is found return true

                    } else if((Board[i,j].piece.face == '@') & (player1 == false)) {
                        // ==============KING Edge Cases ==============
                        if((tl == null) & (GetPiece(tr) != null)) {
                            //if left is off the board and the right is full, skip
                            continue;
                        } else if((tr == null) & (GetPiece(tl) != null)) {
                            //if right is off the board and left is full, skip
                            continue;
                        } else if((tr == null) & (GetPiece(tl) == null)) {
                            //left is off the board and right is empty, return true
                            return true;
                        } else if((tl == null) & (GetPiece(tr) == null)) {
                            //left is off the board and right is empty, return true
                            return true;
                        } else if((bl == null) & (GetPiece(br) != null)) {
                            //if left is off the board and the right is full, skip
                            continue;
                        } else if((br == null) & (GetPiece(bl) != null)) {
                            //if right is off the board and left is full, skip
                            continue;
                        } else if((br == null) & (GetPiece(bl) == null)) {
                            //left is off the board and right is empty, return true
                            return true;
                        } else if((bl == null) & (GetPiece(br) == null)) {
                            //left is off the board and right is empty, return true
                            return true;
                        }
                        // ==================================================

                        if((tl == null) | (tr == null) | (bl == null) | (br == null)) {
                            return true;
                        }
                        // Piece is  black king and its blacks turn
                        //TODO all direction

                    } else if((Board[i,j].piece.face == '+') & (player1 == true)) {

                        //==============White Man Edge cases===================
                        if((bl == null) & (GetPiece(br) != null)) {
                            //if left is off the board and the right is full, skip
                            continue;
                        } else if((br == null) & (GetPiece(bl) != null)) {
                            //if right is off the board and left is full, skip
                            continue;
                        } else if((br == null) & (GetPiece(bl) == null)) {
                            //left is off the board and right is empty, return true
                            return true;
                        } else if((bl == null) & (GetPiece(br) == null)) {
                            //left is off the board and right is empty, return true
                            return true;
                        }
                        // ======================================================

                        if((bl == null) | (br == null)) {
                            return true;
                        }
                        // Piece is white and its white turn
                        //TODO bottom left bottom right
                    } else if((Board[i,j].piece.face == '#') & (player1 == true)) {
                        // ==============KING Edge Cases ==============
                        if((tl == null) & (GetPiece(tr) != null)) {
                            //if left is off the board and the right is full, skip
                            continue;
                        } else if((tr == null) & (GetPiece(tl) != null)) {
                            //if right is off the board and left is full, skip
                            continue;
                        } else if((tr == null) & (GetPiece(tl) == null)) {
                            //left is off the board and right is empty, return true
                            return true;
                        } else if((tl == null) & (GetPiece(tr) == null)) {
                            //left is off the board and right is empty, return true
                            return true;
                        } else if((bl == null) & (GetPiece(br) != null)) {
                            //if left is off the board and the right is full, skip
                            continue;
                        } else if((br == null) & (GetPiece(bl) != null)) {
                            //if right is off the board and left is full, skip
                            continue;
                        } else if((br == null) & (GetPiece(bl) == null)) {
                            //left is off the board and right is empty, return true
                            return true;
                        } else if((bl == null) & (GetPiece(br) == null)) {
                            //left is off the board and right is empty, return true
                            return true;
                        }
                        // ==================================================

                        if((tl == null) | (tr == null) | (bl == null) | (br == null)) {
                            return true;
                        }
                        // Piece is white king and its whites turn
                        //TODO all directions
                    }

                }
            }

            return false;

        }

        public bool isItAJump(int a,int b, int c = -1) {
            // See if A is a neighbor to a neighbor of b. B needs to be empty and the neighbor should be an enemy

            if(c > 0) {
                //If A and B pass, check B and C assuming B is correct.
                var boardCopy = this.BoardCopy;

                if((a == 0)||(b == 0))
                    return false;

                var aPiece = GetPiece(a);
                var bPiece = GetPiece(b);
                var cPiece = GetPiece(c);
                List<int?> aList = new List<int?>();
                List<int?> bList = new List<int?>();
                List<int?> cList = new List<int?>();
                if((aPiece.face == 'O')||(aPiece.face == '@')||(aPiece.face == '#')) {
                    //Add top neighbors
                    aList.Add(aPiece.topleftNode);
                    aList.Add(aPiece.toprightNode);                   
                }
                if((aPiece.face == '+')||(aPiece.face == '@')||(aPiece.face == '#')) {
                    //Add bottom neighbors
                    aList.Add(aPiece.bottomleftNode);
                    aList.Add(aPiece.bottomrightNode);
                }
                bList = getNeighbors(b);
                //Compare the lists to find if there is a common neighbor
                foreach(int? i in aList) {
                    foreach(int? j in bList) {
                        if(i == j) {
                            //common neighbor, see if it is an enemy peice
                            var iPiece = GetPiece(i);
                            if((((aPiece.face == 'O')||(aPiece.face == '@'))&&((iPiece.face=='+')||(iPiece.face=='#')))||((aPiece.face == '+')||(aPiece.face == '#')&&((iPiece.face=='+')||(iPiece.face=='#')))) {
                                //if b is empty
                                if(bPiece == null) {
                                    //A and B are a jump, now check B and C.
                                    cList =getNeighbors(c);
                                    bList.Clear();
                                    //Clear and readd neighbors to B based on the face value of A
                                    if((aPiece.face == 'O')||(aPiece.face == '@')||(aPiece.face == '#')) {
                                         //Add top neighbors
                                           bList.Add(bPiece.topleftNode);
                                           bList.Add(bPiece.toprightNode);                   
                                      }
                                    if((aPiece.face == '+')||(aPiece.face == '@')||(aPiece.face == '#')) {
                                           //Add bottom neighbors
                                          bList.Add(aPiece.bottomleftNode);
                                          bList.Add(aPiece.bottomrightNode);
                                     }
                                    //Find a common neighbor
                                    foreach(int? k in bList) {
                                        foreach(int? l in cList) {
                                            if(k == l){
                                                //neighbor found, store it as a piece.
                                                var lPiece = GetPiece(l);
                                                //check if middle peice is an enemy. Use a to check face, as b is techincailly empty
                                                if((((aPiece.face == 'O')||(aPiece.face == '@'))&&((lPiece.face=='+')||(lPiece.face=='#')))||((aPiece.face == '+')||(aPiece.face == '#')&&((lPiece.face=='+')||(lPiece.face=='#')))) {
                                                    //check if end space is empty
                                                    if(cPiece == null) {
                                                        return true;
                                                    }
                                                }
                                            }
                                        
                                        }
                                    }
                                }
                            }

                        }

                    }
                }
                return false;


            } else {

                if((a == 0)||(b == 0))
                    return false;

                var aPiece = GetPiece(a);
                var bPiece = GetPiece(b);

                List<int?> aList = new List<int?>();
                List<int?> bList = new List<int?>();
                if((aPiece.face == 'O')||(aPiece.face == '@')||(aPiece.face == '#')) {
                    //Add top neighbors
                    aList.Add(aPiece.topleftNode);
                    aList.Add(aPiece.toprightNode);                    // TODO: IF NULL DO SOMETHING HERE
                }
                if((aPiece.face == '+')||(aPiece.face == '@')||(aPiece.face == '#')) {
                    //Add bottom neighbors
                    aList.Add(aPiece.bottomleftNode);
                    aList.Add(aPiece.bottomrightNode);
                }
                bList = getNeighbors(b);
                //Compare the lists to find if there is a common neighbor
                foreach(int? i in aList) {
                    foreach(int? j in bList) {
                        if(i == j) {
                            //common neighbor, see if it is an enemy peice
                            var iPiece = GetPiece(i);
                            if((((aPiece.face == 'O')||(aPiece.face == '@'))&&((iPiece.face=='+')||(iPiece.face=='#')))||((aPiece.face == '+')||(aPiece.face == '#')&&((iPiece.face=='+')||(iPiece.face=='#')))) {
                                //if b is empty
                                if(bPiece == null) {
                                    return true;
                                }
                            }

                        }

                    }
                }
            }
            return false;
        }

        public bool areNeigbors(int a,int b) {
            var aPiece = GetPiece(a);
            var bPiece = GetPiece(b);
            List<int?> aList = new List<int?>();
            if((aPiece.face == 'O')||(aPiece.face == '@')||(aPiece.face == '#')) {
                //Add top neighbors
                aList.Add(aPiece.topleftNode);
                aList.Add(aPiece.toprightNode);
            }
            if((aPiece.face == '+')||(aPiece.face == '@')||(aPiece.face == '#')) {
                //Add bottom neighbors
                aList.Add(aPiece.bottomleftNode);
                aList.Add(aPiece.bottomrightNode);
            }
            foreach(int? i in aList) {
                if(i == b) {
                    return true;
                }

            }
            return false;

        }

        public List<int?> getNeighbors(int i){
          List<int?> z = new List<int?> { 5,6,7,8,13,14,15,16,21,22,23,24,29,30,31,32 };
          List<int?> x = new List<int?>();
          if (z.Contains(i)){
            x.Add(i + 4); // topleft
            x.Add(i + 3); // topright
            x.Add(i - 4); // bottomleft
            x.Add(i - 5); //bottomright
          }else{
            x.Add(i + 5); // topleft
            x.Add(i + 4); // topright
            x.Add(i - 3); // bottomleft
            x.Add(i - 4); //bottomright
           }
          return x;

        }

    }// end CheckerBoard class
}// end NameSpace