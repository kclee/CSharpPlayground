using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        int myPositionX = 0;
        enum Direction { Left, Right }


        [TestMethod]
        public void SimpleMove()
        {
            // A very simple way to update position
            myPositionX = 0;

            myPositionX -= 5; // Imagine we need to do many other stuff here

            myPositionX += 10;// Imagine we need to do many other stuff here as well

            Assert.AreEqual(myPositionX, 5);

            //////////////////////////////////////////

            // Define Func, and we can reuse it later
            myPositionX = 0;

            // These Func can be reused
            Func<int, int, int> moveLeft  = 
                (position, distance) => 
                {
                    // Imagine we need to do many other stuff here (just do it once inside Func)
                    return position -= distance;                    
                };

            Func<int, int, int> moveRight = 
                (position, distance) => 
                {
                    // Imagine we need to do many other stuff here (just do it once inside Func)
                    return position += distance;
                };

            myPositionX = moveLeft(myPositionX, 5);
            myPositionX = moveRight(myPositionX, 10);

            Assert.AreEqual(myPositionX, 5);

            // Func can be reused, and reduce some code, e.g.
            /*
            moveLeft();
            moveLeft();
            moveRight();
            moveRight();
            */
        }

        /// <summary>
        /// Using Func for refactoring Switch statements
        /// </summary>
        public void MoreAdvancedMove()
        {
            // Use Switch Case
            myPositionX = 0;

            myPositionX = MoveWithSwitchCase(Direction.Left, myPositionX, 5);
            myPositionX = MoveWithSwitchCase(Direction.Right, myPositionX, 10);

            Assert.AreEqual(myPositionX, 5);

            //////////////////////////////////////////
            
            // Use the Func map
            myPositionX = 0;

            myPositionX = directionMap[Direction.Left](myPositionX, 5);
            myPositionX = directionMap[Direction.Right](myPositionX, 10);

            Assert.AreEqual(myPositionX, 5);
        }

        /// <summary>
        /// Use SwitchCase to update position
        /// </summary>
        private int MoveWithSwitchCase(Direction dir, int currentPos, int distance)
        {
            switch (dir)
            {
                case Direction.Left:
                    return currentPos - distance;
                case Direction.Right:
                    return currentPos + distance;
                default:
                    return currentPos;
            }
        }

        /// <summary>
        /// Setup a Func map
        /// </summary>
        private Dictionary<Direction, Func<int, int, int>> directionMap = new Dictionary<Direction, Func<int, int, int>>
        {
            { Direction.Left,  (positionX, distance) => positionX - distance},
            { Direction.Right, (positionX, distance) => positionX + distance},
        };

    }
}
