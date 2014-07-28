﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WatchYourBackLibrary;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace WatchYourBackLibrary
{
      
    /// <summary>
    /// A factory to create all the entities of the game. Each method in the factory is a template for an entity in the game; the entities can be
    /// created using these methods by specifying their parameters. Also contains a method for creating a graphical representation of an entity,
    /// without creating the entity itself.
    /// </summary>
    public static class EFactory
    {
        public static ContentManager content;

        /// <summary>
        /// Creates a simple text display for showing values such as scores or times.
        /// </summary>
        /// <param name="rect">The location and size of the display</param>
        /// <returns>A display entity</returns>
        public static Entity createDisplay(Rectangle rect)
        {
            SpriteFont font = content.Load<SpriteFont>("TestFont");
            Entity e = new Entity(false,
                new TransformComponent(rect),
                new GraphicsComponent(rect, "", font, Color.Black, 0, "Display"));
            return e;                                     
        }

        /// <summary>
        /// Creates an avatar for the player, which also contains the player's info, such as their score.
        /// </summary>
        /// <param name="info">The information for the player</param>
        /// <param name="rect">The location and size of the avatar</param>
        /// <param name="player">The player's allegiance, used to determine what should collide with what</param>
        /// <param name="weaponType">The identity of the avatar's primary weapon</param>
        /// <param name="hasGraphics">A flag identifying whether the enttiy should have graphics or not</param>
        /// <returns>An avatar entity</returns>
        public static Entity createAvatar(PlayerInfoComponent info, Rectangle rect, Allegiance player, Weapons weaponType, bool hasGraphics)
        {
            Vector2 offset = new Vector2(rect.Width / 2, rect.Height / 2);
            Entity e = new Entity(false,
            info,
            new AllegianceComponent(player),
            new WielderComponent(weaponType),
            new TransformComponent(rect),
            new RectangleColliderComponent(rect),
            new PlayerHitboxComponent(rect, 10, -Vector2.UnitY),
            new CircleColliderComponent(new Vector2(rect.Center.X, rect.Center.Y), rect.Width/2, false),
            new VelocityComponent(0, 0),
            new StatusComponent(),
            new AvatarInputComponent());
            if (hasGraphics)
            {
                Texture2D myTexture = content.Load<Texture2D>("PlayerTexture");
                e.addComponent(new GraphicsComponent(rect, myTexture, myTexture.Bounds, 0, new Vector2(myTexture.Width / 2, myTexture.Height / 2), offset, 0.9f, "Avatar"));
            }
            e.Type = ENTITIES.AVATAR;
            return e;
        }

        /// <summary>
        /// Creates a graphical representation of an entity
        /// </summary>
        /// <param name="rect">The location and size of the entity</param>
        /// <param name="rotation">The rotation of the entity</param>
        /// <param name="rotationOrigin">The point the entity rotates around</param>
        /// <param name="rotationOffset">Changes the origin of the entity</param>
        /// <param name="ID">The unique ID of the entity</param>
        /// <param name="sourceRectangle">The area of the texture to be drawn</param>
        /// <param name="type">The type of entity</param>
        /// <param name="layer">The layer to be drawn on</param>
        /// <returns>An entity containing only a graphics component</returns>
        public static Entity createGraphics(Rectangle rect, float rotation, Vector2 rotationOrigin, Vector2 rotationOffset, int ID, Rectangle sourceRectangle, ENTITIES type, float layer)
        {
            Texture2D texture;
            switch (type)
            {
                case ENTITIES.AVATAR:
                    texture = content.Load<Texture2D>("PlayerTexture");
                    layer = 1;
                    rotationOrigin = new Vector2(texture.Width/2, texture.Height/2);
                    rotationOffset = new Vector2(rect.Width / 2, rect.Height / 2);
                    sourceRectangle = texture.Bounds;
                    break;
                case ENTITIES.SWORD:
                    texture = content.Load<Texture2D>("SwordTexture"); 
                    rotationOrigin = new Vector2(texture.Width/2, texture.Height);
                    rotationOffset = Vector2.Zero;
                    sourceRectangle = texture.Bounds;
                    break;
                case ENTITIES.THROWN:
                    texture = content.Load<Texture2D>("ThrownTexture");
                    layer = 0;
                    rotationOrigin = Vector2.Zero;
                    rotationOffset = Vector2.Zero;
                    sourceRectangle = texture.Bounds;
                    break;               
                default:
                    texture = null;  
                    texture = null;
                    layer = 1;
                    rotationOrigin = Vector2.Zero;
                    rotationOffset = Vector2.Zero;
                    sourceRectangle = Rectangle.Empty;
                    break;
            }
            Entity e = new Entity(
            new GraphicsComponent(rect, texture, sourceRectangle, rotation, rotationOrigin, rotationOffset, layer, type.ToString()));
            e.ID = ID;
            e.Type = type;
            return e;
        }

        /// <summary>
        /// Creates a graphical representation of a wall
        /// </summary>
        /// <param name="rect">The location and size of the entity</param>
        /// <param name="rotation">The rotation of the entity</param>
        /// <param name="rotationOrigin">The point the entity rotates around</param>
        /// <param name="rotationOffset">Changes the origin of the entity</param>
        /// <param name="ID">The unique ID of the entity</param>
        /// <param name="textureIndex">The segment of the texture atlas to be drawn</param>
        /// <param name="type">The type of entity</param>
        /// <param name="layer">The layer to be drawn on</param>
        /// <returns>A graphical entity of a wall</returns>
        public static Entity createGraphics(Rectangle rect, float rotation, Vector2 rotationOrigin, Vector2 rotationOffset, int ID, int[,] textureIndex, ENTITIES type, float layer)
        {
            Texture2D texture = content.Load<Texture2D>("TileTextures/WallTextureAtlas");
            Entity e = new Entity();
            GraphicsComponent g = new GraphicsComponent();
            g.Sprites.Add("TopLeft", new GraphicsInfo(new Rectangle(rect.X, rect.Y, rect.Width / 2, rect.Height / 2), texture, new Rectangle((int)LevelDimensions.X_SCALE / 2 * textureIndex[0, 0], 0, (int)LevelDimensions.X_SCALE / 2, (int)LevelDimensions.Y_SCALE / 2), 1));
                g.Sprites.Add("TopRight", new GraphicsInfo(new Rectangle(rect.X + rect.Width / 2, rect.Y, rect.Width / 2, rect.Height / 2), texture, new Rectangle((int)LevelDimensions.X_SCALE / 2 * textureIndex[0, 1], 0, (int)LevelDimensions.X_SCALE / 2, (int)LevelDimensions.Y_SCALE / 2), 1));
                g.Sprites.Add("BottomLeft", new GraphicsInfo(new Rectangle(rect.X, rect.Y + rect.Height / 2, rect.Width / 2, rect.Height / 2), texture, new Rectangle((int)LevelDimensions.X_SCALE / 2 * textureIndex[1, 0], 0, (int)LevelDimensions.X_SCALE / 2, (int)LevelDimensions.Y_SCALE / 2), 1));
                g.Sprites.Add("BottomRight", new GraphicsInfo(new Rectangle(rect.X + rect.Width / 2, rect.Y + rect.Height / 2, rect.Width / 2, rect.Height / 2), texture, new Rectangle((int)LevelDimensions.X_SCALE / 2 * textureIndex[1, 1], 0, (int)LevelDimensions.X_SCALE / 2, (int)LevelDimensions.Y_SCALE / 2), 1));
                e.addComponent(g);
            e.ID = ID;
            e.Type = type;
            return e;
        }

        
        /// <summary>
        /// Creates a sword. Takes various parameters of the wielder that allows it to accurately move with the wielder.
        /// </summary>
        /// <param name="wielder">The entity which is wielding the sword</param>
        /// <param name="wielderAllegiance">The allegiance of the wielder</param>
        /// <param name="anchorTransform">The position of the wielder.</param>
        /// <param name="rotationAngle">The initial rotation of the sword</param>
        /// <param name="anchorMovement">The velocity of the wielder.</param>
        /// <param name="hasGraphics">A flag identifying whether the enttiy should have graphics or not</param>
        /// <returns>A sword entity anchored at the wielder</returns>
        public static Entity createSword(Entity wielder, Allegiance wielderAllegiance, TransformComponent anchorTransform, float rotationAngle, VelocityComponent anchorMovement, bool hasGraphics)
        {
            Vector2 point = HelperFunctions.pointOnCircle(anchorTransform.Radius, rotationAngle, anchorTransform.Center);
            Vector2 rotation = Vector2.Transform(point - anchorTransform.Center, Matrix.CreateRotationZ((float)(Math.PI/4))) + anchorTransform.Center;
            point = rotation;
            Entity e = new Entity(true,
            new AllegianceComponent(wielderAllegiance),
            new TransformComponent(point, (int)SWORD.WIDTH, (int)SWORD.RANGE, rotationAngle),
            new WeaponComponent(wielder, MathHelper.ToRadians((float)SWORD.ARC), true),
            new VelocityComponent(anchorMovement.X, anchorMovement.Y, -(float)SWORD.SPEED),
            new LineColliderComponent(point, new Vector2(point.X, point.Y - (float)SWORD.RANGE), rotationAngle));

            if (hasGraphics)
            {
                Texture2D myTexture = content.Load<Texture2D>("SwordTexture");
                e.addComponent(new GraphicsComponent(new Rectangle((int)point.X, (int)point.Y, (int)SWORD.WIDTH, (int)SWORD.RANGE), myTexture, rotationAngle, new Vector2(myTexture.Width / 2, myTexture.Height), 0.8f, "Sword"));
            }
            e.Type = ENTITIES.SWORD;
            return e;
            
        }

        /// <summary>
        /// Creates a thrown weapon.
        /// </summary>
        /// <param name="wielderAllegiance">The allegiance of the wielder</param>
        /// <param name="xOrigin">The x-coordinate of the weapon</param>
        /// <param name="yOrigin">The y-coordinate of the weapon</param>
        /// <param name="rotationVector">A unit vector representing the direction of the weapon's velocity</param>
        /// <param name="rotationAngle">The angle of the weapon</param>
        /// <param name="hasGraphics">A flag identifying whether the enttiy should have graphics or not</param>
        /// <returns>A thrown weapon entity</returns>
        public static Entity createThrown(Allegiance wielderAllegiance, float xOrigin, float yOrigin, Vector2 rotationVector, float rotationAngle, bool hasGraphics)
        {

            Entity e = new Entity(true,
            new AllegianceComponent(wielderAllegiance),
            new TransformComponent(xOrigin, yOrigin, (int)THROWN.RADIUS, (int)THROWN.RADIUS, rotationAngle),
            new WeaponComponent(),
            new VelocityComponent(rotationVector.X * (float)THROWN.SPEED, rotationVector.Y * (float)THROWN.SPEED),
            new RectangleColliderComponent(new Rectangle((int)xOrigin, (int)yOrigin, (int)THROWN.RADIUS, (int)THROWN.RADIUS)));
            if (hasGraphics)
            {
                Texture2D myTexture = content.Load<Texture2D>("ThrownTexture");
                e.addComponent(new GraphicsComponent(new Rectangle((int)xOrigin, (int)yOrigin, (int)THROWN.RADIUS, (int)THROWN.RADIUS), myTexture, rotationAngle, new Vector2(myTexture.Width / 2, myTexture.Height), 1, "Thrown"));
            }
            e.Type = ENTITIES.THROWN;
            return e;

        }

        /// <summary>
        /// Creates a button for a menu, containing both it's graphical representation as well as it's effect when clicked.
        /// </summary>
        /// <param name="x">The x-coordinate of the button</param>
        /// <param name="y">The y-coordinate of the button</param>
        /// <param name="width">The width of the button</param>
        /// <param name="height">The height of the button</param>
        /// <param name="type">The role of the button</param>
        /// <param name="text">The text of the button</param>
        /// <returns>A button entity</returns>
        public static Entity createButton(int x, int y, int width, int height, Inputs type, string text)
        {
            Texture2D frame = content.Load<Texture2D>("Buttons/ButtonFrame");
            Texture2D clickedFrame = content.Load<Texture2D>("Buttons/ButtonFrameClicked");
            x -= frame.Width / 2;
            y -= frame.Height / 2;
            GraphicsComponent g = new GraphicsComponent(new Rectangle(x, y, width, height), frame, 0.99f, "Frame");
            g.Sprites.Add("ClickedFrame", new GraphicsInfo(g.Body, clickedFrame, 1f));
            g.Sprites.Add("Text", new GraphicsInfo(g.Body, content.Load<Texture2D>("Buttons/" + text + "Text"), 0.98f));

            return new Entity(false,
            new ButtonComponent(type),
            new TransformComponent(x, y, width, height),
            new RectangleColliderComponent(new Rectangle(x, y, width, height)),
            g);

        }

        /// <summary>
        /// Creates a wall segment.
        /// </summary>
        /// <param name="x">The x-coordinate of the wall</param>
        /// <param name="y">The y-coordinate of the wall</param>
        /// <param name="width">The width of the wall</param>
        /// <param name="height">The height of the wall</param>
        /// <param name="atlasIndex">The index of the texture atlas to be drawn</param>
        /// /// <param name="hasGraphics">A flag identifying whether the enttiy should have graphics or not</param>
        /// <returns>A wall segment entity</returns>
        public static Entity createWall(int x, int y, int width, int height, int[,] atlasIndex, bool hasGraphics)
        {
            Entity e = new Entity(false,
                new TileComponent(TileType.WALL, atlasIndex),
                new TransformComponent(x, y, width, height),
                new RectangleColliderComponent(new Rectangle(x, y, width, height)));
            if (hasGraphics)
            {
                Texture2D myTexture = content.Load<Texture2D>("TileTextures/WallTextureAtlas");
                GraphicsComponent g = new GraphicsComponent();
                g.Sprites.Add("TopLeft", new GraphicsInfo(new Rectangle(x, y, width / 2, height / 2), myTexture, new Rectangle((int)LevelDimensions.X_SCALE / 2 * atlasIndex[0, 0], 0, (int)LevelDimensions.X_SCALE / 2, (int)LevelDimensions.Y_SCALE / 2), 1));
                g.Sprites.Add("TopRight", new GraphicsInfo(new Rectangle(x + width / 2, y, width / 2, height / 2), myTexture, new Rectangle((int)LevelDimensions.X_SCALE / 2 * atlasIndex[0, 1], 0, (int)LevelDimensions.X_SCALE / 2, (int)LevelDimensions.Y_SCALE / 2), 1));
                g.Sprites.Add("BottomLeft", new GraphicsInfo(new Rectangle(x, y + height / 2, width / 2, height / 2), myTexture, new Rectangle((int)LevelDimensions.X_SCALE / 2 * atlasIndex[1, 0], 0, (int)LevelDimensions.X_SCALE / 2, (int)LevelDimensions.Y_SCALE / 2), 1));
                g.Sprites.Add("BottomRight", new GraphicsInfo(new Rectangle(x + width / 2, y + height / 2, width / 2, height / 2), myTexture, new Rectangle((int)LevelDimensions.X_SCALE / 2 * atlasIndex[1, 1], 0, (int)LevelDimensions.X_SCALE / 2, (int)LevelDimensions.Y_SCALE / 2), 1));
                e.addComponent(g);
            }
            e.Type = ENTITIES.WALL;
            return e;
        }

        /// <summary>
        /// Creates a spawn point. Has no graphical component.
        /// </summary>
        /// <param name="x">The x-coordinate of the spawn</param>
        /// <param name="y">The y-coordinate of the spawn</param>
        /// <param name="width">The width of the spawn</param>
        /// <param name="height">The height of the spawn</param>
        /// <returns>A spawn entity</returns>
        public static Entity createSpawn(int x, int y, int width, int height)
        {
            Entity e = new Entity(
                new TileComponent(TileType.SPAWN),
                new TransformComponent(x, y, width, height));
            e.Drawable = false;
            return e;
        }

        

        
    }
}
