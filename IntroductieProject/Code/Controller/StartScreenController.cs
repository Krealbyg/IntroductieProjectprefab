using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace IntroductieProject
{
    class StartScreenController : BaseController
    {

        internal StartScreenController() : base(new StartScreen(new Vector2(Game.ScreenSize.X / 2, Game.ScreenSize.Y / 2), Game.ScreenSize.X, Game.ScreenSize.Y))
        {

        }

        protected override void initializeViewAndEvents()
        {
            // This function wants to add an event to the start button of the startScreen.
            // We know that the view that we talk to has a startButton, because we ensured that the view is a StartScreen.
            // We look at the button, and select its "onMouseUp" event. 
            // We then "attach"  our clickButtonHandler function, that processes the gamelogic for when the button is pressed.
            this.startScreen.startButton.onMouseUp += clickStartButtonHandler;
            this.startScreen.multiPlayerButton.onMouseUp += clickMultiHandler;
        }

        private void clickMultiHandler(GameObject sender, MouseState mouse)
        {
            Game.GameInstance.startMultiPlayer();
        }


        /// <summary>
        /// This function contains our logic for when we click the start button.
        /// Currently, this asks the game to start the Level. 
        /// </summary>
        /// <param name="sender"> The sender object, is in this case the button. Not our view. </param>
        /// <param name="mouse"> The event has access to the mouse, in case we need specific Mouse logic (e.g. the location of the mouse). </param>
        private void clickStartButtonHandler(GameObject sender, MouseState mouse)
        {
            Game.GameInstance.startLevel();
        }


        /// <summary>
        ///  The BaseLevelController has a view.
        ///  Normally, we only know that the view is a GameObject. So we cannot access elements such as a Player, or GameEntities.
        ///  This function basically says: I am sure that the view of this controller is a StartScreen, so give it to me as such.
        ///  Look at initializeViewAndEvents() to see why this is so usefull.
        /// </summary>
        internal StartScreen startScreen => (StartScreen)this.view;
    }
}
