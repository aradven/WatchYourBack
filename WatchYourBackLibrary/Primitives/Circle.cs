﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace WatchYourBackLibrary
{
    public class Circle
    {
        private float radius;
        private Vector2 center;
       
        public Circle(Vector2 center, float radius)
        {
            this.center = center;
            this.radius = radius;
        }

        public float X
        {
            get { return center.X; }
            set { center.X = value; }
        }

        public float Y
        {
            get { return center.Y; }
            set { center.Y = value; }
        }

        public Vector2 Center
        {
            get { return center; }
            set { center = value; }
        }

        public float Radius
        {
            get { return radius; }
            set { radius = value; }
        }       
      
        public Vector2 PointOnCircle (Vector2 vector)
        {
            float angle = HelperFunctions.VectorToAngle(vector);
            return HelperFunctions.pointOnCircle(this.radius, angle, this.center);
        }

        public Vector2 PointOnCircle (float angle)
        {
            return HelperFunctions.pointOnCircle(this.radius, angle, this.center);
        }   
    }
}