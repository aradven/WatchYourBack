﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WatchYourBackLibrary;

using Microsoft.Xna.Framework;

namespace WatchYourBackLibrary
{
    public class RectangleColliderComponent : EComponent
    {
        public override int BitMask { get { return (int)Masks.COLLIDER + (int)Masks.RECTANGLE_COLLIDER; } }
        public override Masks Mask { get { return Masks.RECTANGLE_COLLIDER; } }

        private Rectangle collider;
        private bool destructable;
       

        public RectangleColliderComponent()
        {
        }

        public RectangleColliderComponent(Rectangle r, bool destructable)
        {
            collider = r;
            this.destructable = destructable;
        }

        public int X
        {
            get { return collider.X; }
            set { collider.X = value; }
        }

        public int Y
        {
            get { return collider.Y; }
            set { collider.Y = value; }
        }

        public int Width
        {
            get { return collider.Width; }
            set { collider.Width = value; }
        }

        public int Height
        {
            get { return collider.Height; }
            set { collider.Height = value; }
        }

        public Rectangle Collider
        {
            get { return collider; }
            set { collider = value; }
        }

        public bool IsDestructable
        {
            get { return destructable; }
            set { destructable = value; }
        }

        


    }
}