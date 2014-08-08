﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace WatchYourBackLibrary
{
    public class Line
    {
        private Vector2 p1;
        private Vector2 p2;
        private float rotation;

        public Line(Vector2 p1, Vector2 p2) 
        {
            this.p1 = p1;
            this.p2 = p2;
            rotation = 0;
        }

        public Line(Vector2 point1, Vector2 point2, float rotation)
        {
            this.rotation = rotation;
            p1 = point1;
            p2 = point2;
        }

        public Vector2 P1
        {
            get { return p1; }
            set { p1 = value; }
        }

        public Vector2 P2
        {
            get { return p2; }
            set { p2 = value; }
        }

        public float X1
        {
            get { return p1.X; }
            set { p1.X = value; }
        }

        public float X2
        {
            get { return p2.X; }
            set { p2.X = value; }
        }

        public float Y1
        {
            get { return p1.Y; }
            set { p1.Y = value; }
        }

        public float Y2
        {
            get { return p2.Y; }
            set { p2.Y = value; }
        }
    }
}
