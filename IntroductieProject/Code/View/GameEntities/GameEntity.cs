using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IntroductieProject
{
    /// <summary>
    /// This class represents the basic logic of an entity in the game, and is therefore very different from the GameEntities like buttons, sliders, or screens.
    /// This entity contains some additional attributes that are usefull for whichever game entity you might want to add.
    /// </summary>
    class GameEntity : GameObject
    {
        /// <summary>
        /// This float represents the Orientation of the object, standard objects are oriented downwards, so they look at you!
        /// </summary>
        internal EntityOrientation orientation = EntityOrientation.Down;

        /// <summary>
        /// This float represents the direction the object can mode in.
        /// Currently it is set to (-1,0).
        /// </summary>
        internal Vector2 direction = new Vector2(-1, 0);


        /// <summary>
        /// This float represents the velocity of the object.
        /// For stationary game entities, this can always remain zero.
        /// </summary>
        internal float velocity = 0;

        /// <summary>
        /// This is an example of an actual "attribute" of a game entity.
        /// In this case, this float represents the speed at which it moves.
        /// We made sure that only an entity can acccess its base speed.
        /// </summary>
        protected float baseSpeed = 3;


        /// <summary>
        /// This constant makes sure that we can express "veclocity" in terms of pixel per second.
        /// This conversion factor is based on the current updatespeed. So if you change the update speed, you need to change this also.
        /// </summary>
        protected readonly float velocityScale = 16.6667f;

        internal GameEntity(Vector2 center, int width, int height, string assetName = "bridge") : base(center, width, height, assetName)
        {
        }


        /// <summary>
        /// This function overrides the basic update function.
        /// First it calls the base function. So that all its children are updated and whatnot.
        /// Then it specifically updates the entity, to include its speed.
        /// </summary>
        /// <param name="time"> The game time, handed down by the controller. </param>
        internal override void update(GameTime time)
        {
            base.update(time);

            double elapsed = time.ElapsedGameTime.TotalMilliseconds;

            // Invoke the logic for making an object move based on its velocity and direction.
            this.moveEntity(time);
        }

        /// <summary>
        /// This function is the base code for moving an entity.
        /// It normalizes the direction vector (to ensure that the vector has length one) so that it can be multiplied by a scaling factor divided by the elapsed time.
        /// </summary>
        /// <param name="time">The game time, handed down by the controller. </param>
        protected virtual void moveEntity(GameTime time)
        {
            if (this.velocity > 0)
            {
                float xOffset = (this.velocity * this.direction.X) / this.direction.Length() * this.velocityScale / (float)time.ElapsedGameTime.TotalMilliseconds;
                float yOffset = (this.velocity * this.direction.Y) / this.direction.Length() * this.velocityScale / (float)time.ElapsedGameTime.TotalMilliseconds;

                this.centerPosition = this.centerPosition + new Vector2(xOffset, yOffset);
            }
        }

        /// <summary>
        /// This function shows how other classes can safely ask a game entity to perform an action with its private properties.
        /// In this specific case, they can ask the entity to start moving. But the entity itself can decide for itself what that actually means!
        /// This is useful, because within this function the GameEntity can add additional logic it might need for moving. Such as turning its sprite or starting an animation!
        /// </summary>
        internal virtual void startMoving()
        {
            this.velocity = this.baseSpeed;
        }

        /// <summary>
        /// This function shows how other classes can ask the entity to stop moving.
        /// Again it can then decide for itself what that means.
        /// </summary>
        internal virtual void stopMoving()
        {
            this.velocity = 0;
        }

        /// <summary>
        /// This function returns the bounding box of the GameEntity. 
        /// This function exists to show what might happen when you have objects with another orientation.
        /// </summary>
        internal override Rectangle getBoundingBox()
        {
            // If we are oriented left/right instead of up/down, our width and height swaps!
            if (this.orientation == EntityOrientation.Right || this.orientation == EntityOrientation.Left)
                return new Rectangle((int)this.centerPosition.X - height / 2, (int)this.centerPosition.Y - width / 2, height, width);
            else
                return new Rectangle((int)this.centerPosition.X - width / 2, (int)this.centerPosition.Y - height / 2, width, height);
            
        }
    }

    /// <summary>
    /// Used to provide a reason for the canPlaceTile(...) result. Instead of just a boolean saying
    /// yes or no we can indicate more intricate results if we like.
    /// </summary>
    enum EntityOrientation
    {
        Left,
        Right,
        Up,
        Down
    }

}
