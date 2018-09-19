using Microsoft.VisualStudio.TestTools.UnitTesting;
using Checkers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Checkers.Tests {
    [TestClass()]
    public class CheckerBoardTests {

        [TestMethod()]
        public void findPieceTest() {

            // this unit test checks multipal parts of the board at once
            // checks all the pieces are in the right spot and that the appropreate char value is returned
            // checks to ensure the appropreate exceptions are thrown for this method
            // also checks to ensure that multipal instances of the checker board can be made

            int[] blackSpaces = { 1,2,3,4,5,6,7,8,9,10,11,12 };
            int[] whiteSpaces = { 21,22,23,24,25,26,27,28,29,30,31,32 };
            int[] nullSpaces = { 13,14,15,16,17,18,19,20 };

            foreach(int i in blackSpaces)
                Assert.AreEqual(new CheckerBoard().findPiece(i),'O');


            foreach(int i in whiteSpaces)
                Assert.AreEqual(new CheckerBoard().findPiece(i),'+');

            int count = 0;
            foreach(int i in nullSpaces) {
                try {
                    new CheckerBoard().findPiece(i);
                } catch(NullReferenceException e) {
                    count++;
                }
            }
            Assert.AreEqual(count,8);
        }


        [TestMethod()]
        public void anyJumpsTest() {
            var TheBoard = new CheckerBoard();
            Debug.WriteLine(TheBoard);
            Assert.IsFalse(TheBoard.anyJumps());
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
            //Assert.IsTrue(TheBoard.anyJumps());
            var board = new CheckerBoard();
            board.MovePiece('b',10,15);
            board.MovePiece('w',24,19);
            board.MovePiece('b',15,24);
            board.RemovePiece(19);
            board.MovePiece('w',27,20);
            board.MovePiece('b',12,16);
            Debug.WriteLine(TheBoard);
            Assert.IsFalse(TheBoard.anyJumps());



        }

        [TestMethod()]
        public void isItAJumpTest() {

            var TheBoard = new CheckerBoard();
            Debug.WriteLine(TheBoard);
            TheBoard.MovePiece('b',10,15);
            TheBoard.MovePiece('w',24,19);
            Debug.WriteLine(TheBoard);
            Assert.IsTrue(TheBoard.isItAJump(15,24));

        }


    }
}