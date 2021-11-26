using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IntroductieProject
{
    /// <summary>
    /// This class represents the basics of a level.
    /// Currently, it contains a Player, and a list of other game entities.
    /// This class is responsible for collecting all assets that have to do with a level, and for making sure that all elements are drawn and updated correctly.
    /// Crucially, this class is NOT responsible for handling game logic itself. That is what controllers are for.
    /// </summary>
    class BaseLevel : GameObject
    {
        internal Player player;
        internal List<GameEntity> gameEntities = new List<GameEntity>();


        /// <summary>
        /// The constructor of the base level.
        /// It calls the constructor of the GameObject, so that that objects can do things such as setting the center and sprite of the level.
        /// Additionally, this constructor now creates a player.
        /// </summary>
        internal BaseLevel(Vector2 center, int width, int height, string assetName = "background") : base(center, width, height, assetName)
        {
            // A player and a bridge, and a lamp added, just to show how the code works.
            // You probably want to remove this code at some points
            this.player = new Player(new Vector2(200, 700), 30, 100);
            this.gameEntities.Add(player);

            // We want to get a lamp, that stands up.
            // However, if we look at our base sprite, it has an arrow that points downwards when the lamp is lying on the floor!
            // This is why we define a width and a height as if the lamp is lying down: more width than height.
            // We then rotate the lamp.
            // "beatiful code" would let the lamp change its own sprite. This should not be the responsibility of this level class, so go and add code in GameEntity that can get its own sprite!
            GameEntity lamp = new GameEntity(new Vector2(700, 700), 100, 30, "LampRightLooking");

            lamp.orientation = EntityOrientation.Right;
            // We set the bridge to start moving
            lamp.startMoving();
            this.gameEntities.Add(lamp);

            // Lastly, add all game entities to the set of children.
            // Note that this makes game entities children, but not all children are game entities!
            foreach (GameEntity entity in this.gameEntities)
                this.children.Add(entity);
        }




        /// <summary>
        /// We added an update that checks for collisions.
        /// It first updates checks for collisions, and then calls the base function that updates all children.
        /// </summary>
        internal override void update(GameTime time)
        {
            // This collision detection code is slow. It runs in O(n^2) time, where n is the number of gameEntities.
            // That is VERY slow :P
            // The update function is called many times per second. Eventually you need a more efficient solution that the one presented here.
            // the if-statement checks if the object is not colliding with itself.
            foreach (GameEntity entity in this.gameEntities)
                foreach (GameEntity other in this.gameEntities)
                    entity.fireCollisionEvent(other);

            base.update(time);
        }
    }
}
