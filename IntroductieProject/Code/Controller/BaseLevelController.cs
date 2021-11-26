using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IntroductieProject
{
    /// <summary>
    /// This class represents the controller that steers the levels of your game.
    /// This class will contain the specific logic of your game, that is present in ALL levels:
    /// e.g. the keeping of a game score, checking if the player is dead, handling collisions...
    /// </summary>
    abstract class BaseLevelController : BaseController
    {

        /// <summary>
        /// Currently, the constructor already sets a nice basic level as its view.
        /// This is something that you want to adjust yourself.
        /// </summary>
        internal BaseLevelController() : base(new BaseLevel(new Vector2(Game.ScreenSize.X / 2, Game.ScreenSize.Y / 2), Game.ScreenSize.X, Game.ScreenSize.Y))
        {

        }

        /// <summary>
        /// This function is used to initialize the event handlers of the level.
        /// E.g. responding to mouse movement.
        /// </summary>
        protected override void initializeViewAndEvents()
        {

        }

        /// <summary>
        ///  The BaseLevelController has a view.
        ///  Normally, we only know that the view is a GameObject. So we cannot access elements such as a Player, or GameEntities.
        ///  This function basically says: I am sure that the view of this controller is a BaseLevel, so give it to me as such.
        /// </summary>
        protected BaseLevel baseLevel => (BaseLevel)this.view;
    }
}
