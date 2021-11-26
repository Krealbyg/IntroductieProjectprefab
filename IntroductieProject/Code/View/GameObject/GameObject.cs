using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace IntroductieProject
{
    /// <summary>
    /// This class is the main class of this project.
    /// It serves as the view of all objects in our game: which includes objects such as the start screen and the background.
    /// This class contains many "template" methods to help you and your group get started.
    /// </summary>
    class GameObject
    {
        /// <summary>
        /// This is the most important attribute of the GameObject. 
        /// Every GameObject has a list of children that receive drawing instructions, and update instructions from this GameObject.
        /// Good examples of this children relation are:
        /// The start screen that has buttons as children.
        /// The level screen that has the player itself as a child running around.
        /// (Possibly) the player that has an HP bar as children.
        /// </summary>
        internal List<GameObject> children = new List<GameObject>();

        /// <summary>
        /// This point represents the center of this object. Normally, this is the center of the (rectangular) bounding box of the object.
        /// </summary>
        internal Vector2 centerPosition;

        /// <summary>
        /// This is the width and the height of the object. Currently, they (together with the center position) define an axis-aligned rectangular bounding box.
        /// </summary>
        internal int width, height;

        /// <summary>
        /// Every GameObject is required to have a sprite: if it does not have a sprite, are you sure that it should be a game object?
        /// </summary>
        protected Texture2D sprite;

        /// <summary>
        /// Every GameObject has a unique ID, that is handed down from the game.
        /// This is later used to handle events with this object: such as dragging this object around.
        /// Do not change this ID yourself: the constructor will set it correctly.
        /// </summary>
        internal int ID = 0;

        /// <summary>
        /// Handling clicks can be very annoying: many API's tend to "fire off" events several times for no reason.
        /// This is why we gave every click event a unique ID, this list stores the unique ID of every click that we are currently dealing with.
        /// This is later used to make sure we never process the same click twice.
        /// </summary>
        protected List<int> associatedClicks = new List<int>();

        /// <summary>
        /// A bool that determines if the object should register that it can be dragged.
        /// </summary>
        internal bool draggable = false;

        /// <summary>
        /// This is the standard constructor of the GameObject.
        /// Currently, every GameObject that is created, uses this constructor to get created.
        /// This function sets the basic atrributes of the GameObject and assigns it a unique ID.
        /// </summary>
        /// <param name="center"> A point that sets the center of this object. This center can be used for movement of the object. </param>
        /// <param name="width"> An integer that specifies the width of the object. </param>
        /// <param name="height"> An integer that specifies the height of the object </param>
        /// <param name="assetName"> The name of the sprite asset that this GameObject will use. </param>
        /// <param name="draggable"> A boolean that specifies if this Game Object can be dragged. </param>
        internal GameObject(Vector2 center, int width, int height, string assetName, bool draggable = false)
        {
            // GameObjects are responsible for their own data, not the data stored in the entire game.
            // This is why the GameObject asks the current Game Instance if it could please get a sprite.
            this.sprite = Game.GameInstance.getSprite(assetName);
            this.centerPosition = center;
            this.width = width;
            this.height = height;
            this.ID = Game.GameInstance.getUniqueGameObjectID();
            this.draggable = draggable;
        }

        /// <summary>
        /// This function returns the bounding box of the GameObject. 
        /// This function is "virtual" which means that subclasses of GameObject (classes that "inherit" GameObject) can override it to use their own game logic.
        /// Currently this bounding box is used to determine if the object is being clicked. 
        /// Later, this bounding box could be used to check for collision detection.
        /// It is likely that you at some point need to change this function: what must you do when you want to get a circular bounding box? Or maybe a rotated bounding box?
        /// </summary>
        internal virtual Rectangle getBoundingBox()
        {
            return new Rectangle((int)this.centerPosition.X - width / 2, (int)this.centerPosition.Y - height / 2, width, height);
        }

        /// <summary>
        /// The update function.
        /// This function is "virtual" which means that subclasses of GameObject (classes that "inherit" GameObject) can override it to use their own game logic.
        /// Currently, this basic function only suggests that the children of the GameObject should also be updated.
        /// IMPORTANT: This function is called around 60 times per second. So it is very important that do DO NOT do very computationally expensive things in this function, or in methods called within this function.
        /// E.g. suppose you want to do collision detection. Then as yourself: is it really needed to sort and detect for collision EVERY 1/60'th of a second?
        /// Or can you maybe think of something smarter?
        /// </summary>
        /// <param name="time">The GameTime object can be used to compute the time that ellapsed since the previous time that update was called.
        /// This is for example useful when you want the object to move with a certain speed. </param>
        internal virtual void update(GameTime time)
        {
            foreach (GameObject child in this.children)
            {
                child.update(time);
            }
        }


        /// <summary>
        /// This event is invoked when the left mouse goes down for the first time, WITHIN the bounding box of this object.
        /// Example: clicking this object triggers this event.
        /// Dragging the mouse over this object does not trigger this event.
        /// </summary>
        internal event MouseEvent onMouseDown;

        /// <summary>
        /// This event is invoked whenever the mouse moves. 
        /// This can for example be used whenever you want the player to "look" at the mouse at all times.
        /// </summary>
        internal event MouseEvent onMouseMoved;

        /// <summary>
        /// This event is invoked when the left mouse goes up for the first time, whilst it went down WITHIN the bounding box of this object.
        /// </summary>
        internal event MouseEvent onMouseUp;

        /// <summary>
        /// This event is invoked when the left mouse is "dragging" this object.
        /// Currently, only one object can be dragged at a time.
        /// </summary>
        internal event MouseEvent onMouseDrag;

        /// <summary>
        /// This event is invoked whenever the set of keys that is pressed changes.
        /// You can use the previous keys, and current keys that are pressed to decide if keys are being released, pressed, or both.
        /// </summary>
        internal event KeyEvent onKeyChange;


        /// <summary>
        /// This event can be invoked when a collision is detected.
        /// </summary>
        internal event collisionEvent onCollisionDetected;

        /// <summary>
        /// This function is "virtual" which means that subclasses of GameObject (classes that "inherit" GameObject) can override it to use their own game logic.
        /// Currently, this function checks the InputManager if there is any input requires responding to. if so, it calls a function that fires an event.
        /// Note that it FIRST handles all the input of the children:
        /// That means that this current object is LAST.
        /// E.g. suppose that the Player can be dragged, and that the player stores a "hat" object as a child that can also be dragged.
        /// Currently, this function first selects the "hat" as the object to be dragged, because children first,
        /// but it then (almost immediately) overrides that information and decides that the Player should be dragged instead.
        /// </summary>
        internal virtual void handleInput()
        {
            // Handle input in all the children from the template method.
            foreach (GameObject child in this.children)
            {
                child.handleInput();
            }

            if (InputManager.didAKeyChange)
                this.onKeyChange?.Invoke(this);

            if (InputManager.didTheMouseClick)
                this.mouseDown(InputManager.MouseState, InputManager.getClickID());

            if (InputManager.didTheMouseMove)
                this.mouseMoved(InputManager.MouseState);

            if (InputManager.doesTheMouseDrag)
                this.mouseDrag(InputManager.MouseState);

            if (InputManager.didTheMouseRelease)
                this.mouseUp(InputManager.MouseState, InputManager.getClickID());
        }


        /// <summary>
        /// This function is "virtual" which means that subclasses of GameObject (classes that "inherit" GameObject) can override it to use their own game logic.
        /// Currently, this function checks the InputManager if there is any input requires responding to. if so, it calls a function that fires an event.
        /// Note that it FIRST handles all the input of the children:
        /// That means that this current object is FIRST.
        /// E.g. suppose that a player object stores a "hat" object.
        /// Currently, this function first selects the "player" as the object to be painted. 
        /// Then it draws all the children (the hat) OVER the player.
        /// </summary>
        internal virtual void draw(SpriteBatch batch)
        {
            //First draw your own sprite
            this.drawOwnSprite(batch);

            // Then draw your children
            foreach (GameObject child in this.children)
            {
                child.draw(batch);
            }
        }

        /// <summary>
        /// The game logic of drawing your own sprite.
        /// </summary>
        internal virtual void drawOwnSprite(SpriteBatch batch)
        {
            batch.Draw(this.sprite, this.getBoundingBox(), Color.White);
        }


        /// <summary>
        /// This function is called when the left mouse button goes down.
        /// It checks if the Mouse is in the bounding box of this object, if so, if fires an onMouseDown event.
        /// </summary>
        /// <param name="mouse">An object that stores the current state of the mouse.</param>
        /// <param name="clickID">The unque ID that is associated with this click. </param>
        protected void mouseDown(MouseState mouse, int clickID)
        {
            // Check if the touch is inside the bounding box.
            if (this.getBoundingBox().Contains(mouse.Position))
            {
                // Invoke the event
                this.associatedClicks.Add(clickID);
                this.onMouseDown?.Invoke(this, mouse);

                // Mark this as the object that is being dragged
                if (this.draggable)
                    InputManager.dragObjects.Add(this.ID);
            }
        }

        /// <summary>
        /// Called when a mouse is moved. This move is not necessarily related to the object.
        /// </summary>
        /// <param name="mouse">An object that stores the current state of the mouse.</param>
        protected void mouseMoved(MouseState mouse)
        {
            this.onMouseMoved?.Invoke(this, mouse);
        }

        /// <summary>
        /// Called when a mouse drags. We check if the mouse could drag the object. 
        /// </summary>
        /// <param name="mouse">An object that stores the current state of the mouse.</param>
        protected void mouseDrag(MouseState mouse)
        {
            if (InputManager.dragObjects.Contains(this.ID))
                this.onMouseDrag?.Invoke(this, mouse);
        }

        /// <summary>
        /// This function is called when the left mouse button goes up.
        /// It checks if the Mouse was in the bounding box of this object, when it went up. If so, if fires an onMouseDown event.
        /// </summary>
        /// <param name="mouse">An object that stores the current state of the mouse.</param>
        /// <param name="clickID">The unque ID that is associated with the original click of the mouse. </param>
        protected void mouseUp(MouseState mouse, int clickID)
        {
            if (this.associatedClicks.Contains(clickID))
            {
                this.associatedClicks.Remove(clickID);
                this.onMouseUp?.Invoke(this, mouse);
            }
        }

        /// <summary>
        /// This function can be called to check for collision and to fire off the collisionDetected event if needed.
        /// </summary>
        /// <param name="other">The other GameObject that we might be colliding with.</param>
        internal void fireCollisionEvent(GameObject other)
        {
            if (other.ID != this.ID && this.getBoundingBox().Intersects(other.getBoundingBox()))
                this.onCollisionDetected?.Invoke(this, other);
        }


        /// <summary>
        /// This is a delegate handler for collision events.
        /// With this function, you can define your own events that should trigger when two entities collide with one another. 
        /// </summary>
        /// <param name="sender"> The object that is triggering the event.
        /// </param>
        /// <param name="collider"> The object the sender is colliding with. </param>
        internal delegate void collisionEvent(GameObject sender, GameObject collider);


        /// <summary>
        /// This is a delegate handler for mouse events. With this function, you can define your own mouse events.
        /// This class contains four such events, that can use the same delegate:
        /// onMouseDown
        /// onMouseMoved
        /// onMouseDrag
        /// onMouseUp
        /// </summary>
        /// <param name="sender">The object that fires the Key event. Set standard to this object.</param>
        /// <param name="mouse">An object that stores the current state of the mouse.</param>
        internal delegate void MouseEvent(GameObject sender, MouseState mouse);


        /// <summary>
        /// This is a delegate handler for key press events. With this function, you can define key press events.
        /// This class contains one such event:
        /// KeyChangedEvent
        /// </summary>
        /// <param name="sender"> The object that fires the Key event. Set standard to this object. </param>
        internal delegate void KeyEvent(GameObject sender);
    }
}
