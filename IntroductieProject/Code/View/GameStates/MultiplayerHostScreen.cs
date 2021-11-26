using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace IntroductieProject
{
    class MultiPlayerHostScreen : GameObject
    {
        internal MultiPlayerHostScreen(Vector2 center, int width, int height, string assetName = "multiplayerbackground") : base(center, width, height, assetName)
        {

        }
    }
}
