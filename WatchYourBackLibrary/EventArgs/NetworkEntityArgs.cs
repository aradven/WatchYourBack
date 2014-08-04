﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatchYourBackLibrary
{
    [Serializable()]
    public enum EntityCommands
    {
        Add,
        Remove,
        Modify
    }

    /// <summary>
    /// Arguments sent by the server to the client, containing information about entities to draw and how to draw them.
    /// </summary>
    [Serializable()]
    public class NetworkEntityArgs : EventArgs
    {
        private EntityCommands command;
        private Entities type;
        private int id;
        private float xPos;
        private float yPos;
        private int width;
        private int height;
        private float rotation;
        private int textureIndex;
        private int[,] subIndex;

        public NetworkEntityArgs(Entities type, EntityCommands command, int id, float xPos, float yPos, int width, int height, float rotation)
        {
            this.command = command;
            this.type = type;
            this.id = id;
            this.xPos = xPos;
            this.yPos = yPos;
            this.width = width;
            this.height = height;
            this.rotation = rotation;
        }

        public NetworkEntityArgs(Entities type, EntityCommands command, int id, float xPos, float yPos, int width, int height, float rotation, int textureIndex)
            : this(type, command, id, xPos, yPos, width, height, rotation)
        {
            this.textureIndex = textureIndex;
            this.subIndex = null;
        }

        public NetworkEntityArgs(Entities type, EntityCommands command, int id, float xPos, float yPos, int width, int height, float rotation, int[,] textureIndex)
            : this(type, command, id, xPos, yPos, width, height, rotation)
        {
            this.subIndex = textureIndex;
        }

        public EntityCommands Command { get { return command; } }
        public Entities Type { get { return type; } }
        public int ID { get { return id; } }
        public float XPos { get { return xPos; } }
        public float YPos { get { return yPos; } }
        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public float Rotation { get { return rotation; } }
        public int TextureIndex { get { return textureIndex; } }
        public int[,] SubIndex { get { return subIndex; } }

    }
}
