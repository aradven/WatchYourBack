﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace WatchYourBackLibrary
{
    [Serializable()]
    public class NetworkArgs : EventArgs
    {
<<<<<<< HEAD
        private long sender;
=======
>>>>>>> origin/Networking
        private int xInput;
        private int yInput;
        private int mouseX;
        private int mouseY;
        private bool clicked;


<<<<<<< HEAD
        public NetworkArgs(long sender, int xInput, int yInput, Vector2 mouseLoc, bool clicked)
        {
            this.sender = sender;
=======
        public NetworkArgs(int xInput, int yInput, Vector2 mouseLoc, bool clicked)
        {
>>>>>>> origin/Networking
            this.xInput = xInput;
            this.yInput = yInput;
            mouseX = (int)mouseLoc.X;
            mouseY = (int)mouseLoc.Y;
            this.clicked = clicked;
        }

        public override string ToString()
        {
<<<<<<< HEAD
            return sender + ", " + xInput + ", " + yInput + ", (" + mouseX + ", " + mouseY+"), " + clicked;
        }

        public int XInput { get { return xInput; } }
        public int YInput { get { return yInput; } }
        public int MouseX { get { return mouseX; } }
        public int MouseY { get { return mouseY; } }
        public bool Clicked { get { return clicked; } }
=======
            return xInput + ", " + yInput + ", (" + mouseX + ", " + mouseY+"), " + clicked;
        }
>>>>>>> origin/Networking
        
    }
}
