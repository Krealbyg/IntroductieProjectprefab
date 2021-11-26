using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace IntroductieProject
{
    
    /// <summary>
    /// This class is for the logic that only applies to the first level!
    /// Maybe you want to make a tutorial level in which you cannot die.
    /// Maybe you want the game speed to be slower at the start.
    /// The possibilities are endless!
    /// </summary>
    class Level1Controller : BaseLevelController
    {
        /// <summary>
        /// This function is used to initialize the event handlers of the level.
        /// E.g. here you can add unique level 1 events such as special buttons that you can only press in this level.
        /// As a concrete example, there is a function that allows you to drag the player around.
        /// </summary>
        protected override void initializeViewAndEvents()
        {
            // An example of how you can make a game entity draggable.
            this.baseLevel.player.draggable = true;
            this.baseLevel.player.onMouseDrag += dragThePlayer;

            // An example of how you can make a game entity respond to keys being pressed.
            this.baseLevel.player.onKeyChange += handleKeyChange; ;

            // For now, we attach our poorly constructed collision-handler onto each entity!
            // In the real game, you want to remove this and replace it with something sensible.
            foreach (GameEntity entity in this.baseLevel.gameEntities)
            {
                // An example of how you can distinguish between other entities and the player.
                // If you want only a specific set of entities to have a handler, you can separately store their IDs somewhere.
                if(entity.ID != this.baseLevel.player.ID)
                    entity.onCollisionDetected += handleCollisionsPoorly;
            }
        }

        /// <summary>
        /// This event handler specifies what happens to a game entity on a key change.
        /// We attached it to the player, but this code works for any game entity.
        /// Currently, it asks the entity to start moving left or right, and stop moving when either the left or right button is released.
        /// Notice that the code only triggers on a key CHANGE.
        /// If you keep the left key down, the entity will keep moving until the key is released.
        /// </summary>
        private void handleKeyChange(GameObject sender)
        {
            GameEntity entity = (GameEntity)sender;

            // Mostly, the code below shows an example of how to use the inputmanager.
            if (InputManager.isKeyJustPressed(Keys.Left))
            {
                entity.direction = new Vector2(-1, 0);
                if (entity.velocity == 0)
                    entity.startMoving();
            }
            if (InputManager.isKeyJustPressed(Keys.Right))
            {
                entity.direction = new Vector2(1, 0);
                if (entity.velocity == 0)
                    entity.startMoving();
            }
            if (  ( InputManager.isKeyJustReleased(Keys.Left) && !InputManager.isKeyDown(Keys.Right) )
                || (InputManager.isKeyJustReleased(Keys.Right) && !InputManager.isKeyDown(Keys.Left) ) )
            {
                entity.stopMoving();
            }
        }
        

        /// <summary>
        /// This event handler is for handling collusions (currently)
        /// However, at the moment is is doing something that works, but that is quire stupid:
        /// if an object Sender collides with Collider,
        /// Sender will simply turn around!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="collider"></param>
        private void handleCollisionsPoorly(GameObject sender, GameObject collider)
        {
            // If we attach this handler, only to entities, we can safely assume that the sender is a game entity
            GameEntity entity = (GameEntity)sender;
            entity.direction = new Vector2(-entity.direction.X, entity.direction.Y);
        }

        /// <summary>
        /// The event handler for dragging the player.
        /// We attached this handler to the player, so we KNOW that the sender is the player itself!
        /// But all we need to know for now, is that it is a GameObject!
        /// </summary>
        /// <param name="sender"> In this case, the player.</param>
        /// <param name="mouse"> The mouse that is dragging the player</param>
        private void dragThePlayer(GameObject sender, MouseState mouse)
        {
            sender.centerPosition = new Vector2(mouse.Position.X, mouse.Position.Y);
        }
    }
}
