﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace WatchYourBackLibrary
{
    /// <summary>
    /// An interface for the class which manages the systems in the game. Is responsible for initializing, updating, and removing systems as needed. Also contains a list of
    /// all the entities which has changed during the last update cycle; this allows for the server to send data to the client on what needs to be removed, added, or modified,
    /// without sending the actual entities.
    /// </summary>
    public interface IECSManager
    {
        /// <summary>
        /// True if playing, false otherwise.
        /// </summary>
        bool Playing { get; set; }

        void Initialize();

        /// <summary>
        /// Adds an entity to the manager.
        /// </summary>
        /// <param name="entity">The entity to add</param>
        void AddEntity(Entity entity);

        /// <summary>
        /// Removes an entity from the manager
        /// </summary>
        /// <param name="entity">The entity to remove</param>
        void RemoveEntity(Entity entity);

        /// <summary>
        /// Adds a system to the manager.
        /// </summary>
        /// <param name="system">The system to add</param>
        void AddSystem(ESystem system);

        /// <summary>
        /// Removes a system from the manager.
        /// </summary>
        /// <param name="system">The system to remove</param>
        void RemoveSystem(ESystem system);   
      
        /// <summary>
        /// A dictionary of the active entities, with their unique ID's as the keys.
        /// </summary>
        Dictionary<int, Entity> Entities
        {
            get;
        }

        /// <summary>
        /// A dictionary of the entities changed during the current update cycle, with their unique ID's as the keys.
        /// </summary>
        Dictionary<int, EntityCommands> ChangedEntities
        {
            get;
        }

        List<ESystem> Systems
        {
            get;
        }

        QuadTree<Entity> QuadTree
        {
            get;
        }   

        /// <summary>
        /// Add an entity to the changed entities list.
        /// </summary>
        /// <param name="e">The modified entity</param>
        /// <param name="c">How the entity changed (added, modified, or removed)</param>
        void AddChangedEntities(Entity e, EntityCommands c);

        /// <summary>
        /// Updates the entity lists of the manager.
        /// </summary>
        /// <remarks>
        /// Moves active/inactive entities to their proper lists. Any systems that run during the update loop are then updated.
        /// </remarks>
        /// <param name="gameTime">The time elapsed since the last update</param>
        void Update(TimeSpan gameTime);

        /// <summary>
        /// Removes all entities that are either inactive or in the removal list from the game.
        /// </summary>
        void RemoveAll();

        /// <summary>
        /// The component containing all the information about the game's levels.
        /// </summary>
        LevelInfo LevelInfo
        {
            get;
            set;
        }

        /// <summary>
        /// Checks if the manager has graphics or not (ie. client or server).
        /// </summary>
        /// <returns>True if the manager has graphics</returns>
        bool HasGraphics();            
    }
}
