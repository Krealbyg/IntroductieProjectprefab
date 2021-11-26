using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace IntroductieProject
{
    class MultiPlayerController : BaseController
    {

        internal MultiPlayerController() : base(new MultiPlayerHostScreen(new Vector2(Game.ScreenSize.X / 2, Game.ScreenSize.Y / 2), Game.ScreenSize.X, Game.ScreenSize.Y))
        {

        }
        protected override void initializeViewAndEvents()
        {
            
        }


        /// <summary>
        ///  The BaseLevelController has a view.
        ///  Normally, we only know that the view is a GameObject. .
        ///  This function basically says: I am sure that the view of this controller is a MultiPlayerHostScreen, so give it to me as such.
   
        /// </summary>
        internal MultiPlayerHostScreen startScreen => (MultiPlayerHostScreen)this.view;
    }
}
