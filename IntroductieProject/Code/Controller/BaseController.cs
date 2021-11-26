using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntroductieProject
{
    /// <summary>
    /// Controllers are the place "where the magic happens".
    /// Each controller, has as its main responsibility "controlling" the actions of its particular view.
    /// This base class contains the basic elements that you need in order to properly control a view:
    /// * It contains the view itself.
    /// * It contains a function that initializes its own view, and attaches all needed events.
    /// * It propagates all input, updates and drawing instructions to the view.
    /// -- This class is abstract, because a controller always needs to uniquely implement the initialization of its view.
    /// </summary>
    abstract class BaseController
    {
        /// <summary>
        /// The view managed by this controller.
        /// </summary>
        protected GameObject view;

        /// <summary>
        /// The constructor of BaseController.
        /// This constructor sets the view that the controller is controlling and then initializes this view and all the events.
        /// </summary>
        /// <param name="view">The view managed by this controller.</param>
        internal BaseController(GameObject view)
        {
            this.view = view;
            this.initializeViewAndEvents();
        }

        /// <summary>
        /// Abstract method that initializes all the event handlers.
        /// This method sets initializes all events of its own view, but also possibly of their children!
        /// E.g: the screen controller can set the events of the screen itself, but also the events on the buttons on the screen.
        /// This is because buttons are so small, that they do not need their own controller.
        /// </summary>
        protected abstract void initializeViewAndEvents();

        /// <summary>
        /// Update method for the game.
        /// It always first collects the user input, and then gives it to the view.
        /// Note that several controllers could be asking for input, and propagating this input at the same time. This is why the handleInput function in GameObject checks if it hasn't already seen this input.
        /// </summary>
        /// <param name="time">The current time in the game. </param>
        internal virtual void update(GameTime time)
        {
            this.handleInput();
            this.view.update(time);
        }

        /// <summary>
        /// The method which draws the view onto a spritebatch.
        /// </summary>
        /// <param name="batch">The spritebatch in which the view is drawn.</param>
        internal virtual void draw(SpriteBatch batch)
        {
            this.view.draw(batch);
        }

        /// <summary>
        /// Responds to all touches and keyinputs pertaining to this game state.
        /// </summary>
        internal virtual void handleInput()
        {
            InputManager.update();
            this.view.handleInput();
        }
    }
}
