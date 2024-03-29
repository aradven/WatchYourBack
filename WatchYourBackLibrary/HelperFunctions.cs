﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WatchYourBackLibrary
{
    /// <summary>
    /// A list of functions that can be helpful in the program, such as trigonometric functions that are not included in the standard
    /// XNA or C# codebase, or collision detection functions.
    /// </summary>
    public static class HelperFunctions
    {             
        /// <summary>
        /// Returns the diagonal length of a rectangle
        /// </summary>
        /// <param name="rect">The rectangle to measure</param>
        /// <returns>The length from opposite corners</returns>
        public static float Diagonal (Rectangle rect)
        {
           return (float)Math.Sqrt(Math.Pow(rect.Width, 2) + Math.Pow(rect.Height, 2)) / 2;
        }

        /// <summary>
        /// Converts an angle to a unit vector, based on the XNA coordinate system.
        /// </summary>
        /// <param name="angle">The angle, clockwise from the vertical</param>
        /// <returns>A unit vector in the direction of the angle</returns>
        public static Vector2 AngleToVector(float angle)
        {
            return new Vector2((float)Math.Sin(angle), -(float)Math.Cos(angle));
        }

        /// <summary>
        /// Converts a vector to an angle, based on the XNA coordinate system.
        /// </summary>
        /// <param name="vector">A vector</param>
        /// <returns>An angle clockwise from the vetical</returns>
        public static float VectorToAngle(Vector2 vector)
        {
            return Normalize((float)Math.Atan2(vector.X, -vector.Y));
        }

        /// <summary>
        /// Finds the angle between two vectors
        /// </summary>
        /// <param name="v1">A vector</param>
        /// <param name="v2">A vector</param>
        /// <returns>The angle between the two vectors</returns>
        public static float Angle(Vector2 v1, Vector2 v2)
        {
            float dotProduct = Vector2.Dot(v1, v2);
            float mag1 = (float)Math.Sqrt(Math.Pow(v1.X, 2) + Math.Pow(v1.Y, 2));
            float mag2 = (float)Math.Sqrt(Math.Pow(v2.X, 2) + Math.Pow(v2.Y, 2));
            float cosAngle = dotProduct / (Math.Abs(mag1) * Math.Abs(mag2));
            return (float)Math.Acos(cosAngle);
        }

        public static float Angle(float angle1, float angle2)
        {
            return (float)Math.Atan2(Math.Sin(angle2 - angle1), -Math.Cos(angle2 - angle1));
        }

        /// <summary>
        /// Takes an angle, and converts it to an angle between 0 and 2pi.
        /// </summary>
        /// <param name="angle">An angle</param>
        /// <returns>An angle between 0 and 2pi</returns>
        public static float Normalize (float angle)
        {
            if(angle > Math.PI * 2)
                angle -= (float)Math.PI * 2;
            if(angle < 0)
                angle += (float)Math.PI * 2;
            return angle;
        }

        

        /// <summary>
        /// Draws a string scaled and wrapped inside a box
        /// </summary>
        /// <param name="spriteBatch">Draws the text</param>
        /// <param name="font">The font</param>
        /// <param name="text">The text</param>
        /// <param name="textBox">The bounds of the text</param>
        /// <param name="scale">Rescales the text if it's too big</param>
        public static void DrawString(SpriteBatch spriteBatch, SpriteFont font, Color fontColor, string text, Rectangle textBox, float scale = 1)
        {
            if (text == "")
                return;

            string currentLine = string.Empty;
            string[] words = text.Split(' ');
            List<string> lines = new List<string>();
            int index = 0;

            Vector2 stringSize = font.MeasureString(text) * scale;
            int maxIndex = (int)(textBox.Height / (stringSize.Y));

            foreach (string word in words)
            {
                if (font.MeasureString(currentLine + word).Length() * scale > textBox.Width)
                {
                    currentLine = currentLine.Trim();
                    lines.Add(currentLine);
                    currentLine = string.Empty;
                }
                currentLine += word + ' ';
            }
            currentLine = currentLine.Trim();
            lines.Add(currentLine);

            if (lines.Count > maxIndex)
                DrawString(spriteBatch, font, fontColor, text, textBox, scale * 0.98f);
            else
            {
                float yPos = textBox.Height / (lines.Count);
                float offset = stringSize.Y / 2;

                foreach (string line in lines)
                {
                    float lineLength = font.MeasureString(line).Length() * scale; //The amount of space the line takes
                    float xPos = textBox.X + ((textBox.Width - lineLength) / 2); //Take the remaining space, cut it in half, and add it to the xPos to center the text on that line
                    spriteBatch.DrawString(font, line, new Vector2(xPos + 0.5f, textBox.Y + (yPos * (index)) + offset), fontColor, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0);
                    index++;
                }
            }
        }

        public static List<Vector2> SortVertices(List<Vector2> vertices, Vector2 center, Vector2 referencePoint)
        {           
            vertices = vertices.OrderBy(o => HelperFunctions.Angle(o - center, referencePoint - center)).ToList();           
            return vertices;
        }

        public static float Cross(Vector2 v, Vector2 w)
        {
            return (v.X * w.Y) - (v.Y * w.X);
        }

        public static Vector2 GetClosestPoint(Vector2 point, List<Vector2> points)
        {
            
            if (points == null || points.Count == 0)
                return Vector2.Zero;
            else
            {
                Vector2 closest = points[0];
                for(int i = 0; i < points.Count; i++)
                    if (Vector2.Distance(point, points[i]) < Vector2.Distance(point, closest))
                        closest = points[i];
                return closest;
            }                   
        }

        public static Vector2 GetFurthestPoint(Vector2 point, List<Vector2> points)
        {

            if (points == null || points.Count == 0)
                return Vector2.Zero;
            else
            {
                Vector2 closest = points[0];
                for (int i = 0; i < points.Count; i++)
                    if (Vector2.Distance(point, points[i]) > Vector2.Distance(point, closest))
                        closest = points[i];
                return closest;
            }
        }

        public static List<Vector2> BresenhamLine(int x1, int y1, int x2, int y2)
        {
            // Optimization: it would be preferable to calculate in  
            // advance the size of "result" and to use a fixed-size array  
            // instead of a list.  
            List<Vector2> result = new List<Vector2>();
            result.Clear();

            bool steep = Math.Abs(y2 - y1) > Math.Abs(x2 - x1);
            if (steep)
            {
                Swap(ref x1, ref y1);
                Swap(ref x2, ref y2);
            }
            if (x1 > x2)
            {
                Swap(ref x1, ref x2);
                Swap(ref y1, ref y2);
            }

            int deltax = x2 - x1;
            int deltay = Math.Abs(y2 - y1);
            int error = 0;
            int ystep;
            int y = y1;
            if (y1 < y2) ystep = 1; else ystep = -1;
            for (int x = x1; x <= x2; x++)
            {
                if (steep) result.Add(new Vector2(y, x));
                else result.Add(new Vector2(x, y));
                error += deltay;
                if (2 * error >= deltax)
                {
                    y += ystep;
                    error -= deltax;
                }
            }

            return result;
        }

        public static void Swap<T>(ref T a, ref T b)
        {
            T c = a;
            a = b;
            b = c;
        }

        
    }
}
