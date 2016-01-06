using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class TestUpdateAnEntity
    {
        enum Direction { Left, Right, Up, Down }

        private sealed class Entity
        {
            // "hunger" is to simulate the case we need to do more than 1 operation per move(move & increase hungry)
            public int hunger; // hunger increase by 1 when move 1 unit

            public int x;
            public int y;

            public Entity()
            {
                x = 0; y = 0; hunger = 0;
            }
        }

        //
        // Compare different ways to update Entity in Move(), MoveWithSwitchCase(), MoveWithDelegate(), & MoveWithActionMap().
        //

        /// <summary>
        /// Update Entity by modifiying any field directly in the Class
        /// </summary>
        [TestMethod]
        public void Move()
        {
            Entity myEntity = new Entity();

            // (0,0) -> (5,-1)
            myEntity.x -= 5;       // Left   5
            myEntity.hunger += 5;
            myEntity.x += 10;      // Right 10
            myEntity.hunger += 10;
            myEntity.y += 1;       // Up     1
            myEntity.hunger += 1;
            myEntity.y -= 2;       // Down   2
            myEntity.hunger += 2;

            Assert.AreEqual(myEntity.x, 5);
            Assert.AreEqual(myEntity.y, -1);
            Assert.AreEqual(myEntity.hunger, 18);
        }

        /// <summary>
        /// Using Switch/Case to update Entity
        /// </summary>
        [TestMethod]
        public void MoveWithSwitchCase()
        {
            // Use Switch Case
            Entity myEntity = new Entity();

            PerfromMoveWithSwitchCase(Direction.Left,  myEntity, 5 );
            PerfromMoveWithSwitchCase(Direction.Right, myEntity, 10);
            PerfromMoveWithSwitchCase(Direction.Up,    myEntity, 1 );
            PerfromMoveWithSwitchCase(Direction.Down,  myEntity, 2 );

            Assert.AreEqual(myEntity.x, 5);
            Assert.AreEqual(myEntity.y, -1);
            Assert.AreEqual(myEntity.hunger, 18);
        }

        /// <summary>
        /// Switch/Case for Entity update
        /// </summary>
        private void PerfromMoveWithSwitchCase(Direction direction, Entity entity, int distance)
        {
            switch (direction)
            {
                case Direction.Left:
                    entity.x -= distance;
                    entity.hunger += distance;
                    break;
                case Direction.Right:
                    entity.x += distance;
                    entity.hunger += distance;
                    break;
                case Direction.Up:
                    entity.y += distance;
                    entity.hunger += distance;
                    break;
                case Direction.Down:
                    entity.y -= distance;
                    entity.hunger += distance;
                    break;
            }
        }

        /// <summary>
        /// Use Delegate (define Action/Func) to update position instead
        /// </summary>
        [TestMethod]
        public void MoveWithDelegate()
        {
            Entity myEntity = new Entity();

            // Setup Action to be reused. Both pos(x,y), and hungry updated in the Action
            Action<Entity, int> moveLeft  = (entity, distance) => { entity.x -= distance; entity.hunger += distance; };
            Action<Entity, int> moveRight = (entity, distance) => { entity.x += distance; entity.hunger += distance; };
            Action<Entity, int> moveUp    = (entity, distance) => { entity.y += distance; entity.hunger += distance; };
            Action<Entity, int> moveDown  = (entity, distance) => { entity.y -= distance; entity.hunger += distance; };

            /*
            // Or use Func version if we need return value
            Func<Entity, int, int> moveLeft  = (entity, distance) => { entity.x -= distance; entity.hunger += distance; return entity.hunger; };
            Func<Entity, int, int> moveRight = (entity, distance) => { entity.x += distance; entity.hunger += distance; return entity.hunger; };
            Func<Entity, int, int> moveUp    = (entity, distance) => { entity.y += distance; entity.hunger += distance; return entity.hunger; };
            Func<Entity, int, int> moveDown  = (entity, distance) => { entity.y -= distance; entity.hunger += distance; return entity.hunger; };
            */        

            // Update position with delegate. (Make the code cleaner in some way)
            moveLeft(myEntity, 5);
            moveRight(myEntity, 10);
            moveUp(myEntity, 1);
            moveDown(myEntity, 2);

            Assert.AreEqual(myEntity.x, 5);
            Assert.AreEqual(myEntity.y, -1);
            Assert.AreEqual(myEntity.hunger, 18);
        }

        [TestMethod]
        public void MoveWithActionMap()
        {
            // Use the Action/Func map
            Entity myEntity = new Entity();

            directionMap[Direction.Left](myEntity, 5);
            directionMap[Direction.Right](myEntity, 10);
            directionMap[Direction.Up](myEntity, 1);
            directionMap[Direction.Down](myEntity, 2);

            Assert.AreEqual(myEntity.x, 5);
            Assert.AreEqual(myEntity.y, -1);
            Assert.AreEqual(myEntity.hunger, 18);
        }

        /// <summary>
        /// Setup Action/Func map to update position
        /// </summary>
        private Dictionary<Direction, Action<Entity, int>> directionMap = new Dictionary<Direction, Action<Entity, int>>
        {
            { Direction.Left,  (position, distance) => { position.x -= distance; position.hunger += distance; } },
            { Direction.Right, (position, distance) => { position.x += distance; position.hunger += distance; } },
            { Direction.Up,    (position, distance) => { position.y += distance; position.hunger += distance; } },
            { Direction.Down,  (position, distance) => { position.y -= distance; position.hunger += distance; } },
        };

    }
}
