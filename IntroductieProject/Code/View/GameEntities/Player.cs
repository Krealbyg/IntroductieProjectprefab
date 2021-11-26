using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace IntroductieProject
{
    /// <summary>
    /// This class represents the basic logic of an entity in the game, and is therefore very different from the GameEntities like buttons, sliders, or screens.
    /// This class should contain attributes that are needed for the player.
    /// That may be basic attributes such as HP, or movement animations.
    /// Be careful to not make this class "Too big". 
    /// For example suppose you want your player to contain some "Attacks". Then do not add those here directly.
    /// Rather make an Attacks base class, and give a player a list of these attacks!
    /// </summary>
    /// test3
    class Player : GameEntity
    {
        internal Player(Vector2 center, int width, int height, string assetName = "steampunkL1") : base(center, width, height, assetName)
        {
        }
    }
}
