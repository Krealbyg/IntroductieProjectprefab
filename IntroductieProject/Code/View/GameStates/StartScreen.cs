using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace IntroductieProject
{  
    /// <summary>
    /// This object represents the start screen.
    /// The start screen is itself a game object, with a sprite as its background.
    /// The children of the start screen are all UI elements, such as buttons and sliders.
    /// </summary>
    class StartScreen : GameObject
    {
        /// <summary>
        /// The button that starts the game.
        /// </summary>
        public GameObject startButton;

        /// <summary>
        /// The button that gives the multiplayer screen.
        /// </summary>
        public GameObject multiPlayerButton;

        /// <summary>
        /// The constructor of the base level.
        /// It calls the constructor of the GameObject, so that that objects can do things such as setting the center and sprite of the level.
        /// Additionally, this constructor now creates a player.
        /// </summary>
        internal StartScreen(Vector2 center, int width, int height, string assetName = "background") : base(center, width, height, assetName)
        {
            startButton = new GameObject(new Vector2(Game.ScreenSize.X / 2, Game.ScreenSize.Y / 4), 200, 200, "playbutton");
            this.children.Add(startButton);

            multiPlayerButton = new GameObject(new Vector2(Game.ScreenSize.X / 2, Game.ScreenSize.Y*3 / 4), 200, 200, "multiplayer");
            this.children.Add(multiPlayerButton);
        }
    }
}
