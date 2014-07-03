﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WatchYourBackLibrary
{
    /* 
     * Container for all the components that make up game objects. Contains a list of all the components contained, and methods to modify said components. Each entity
     * can have only one of each component. Each component has a component-specific bit; combining these into a bitmask for the entity allows for quick lookup and comparison
     * using bitwise operators.
     */

    

    public class Entity
    {
        private int mask;
        private bool isActive;
        private Dictionary<Masks, EComponent> components;

       

        public Entity()
        {
            isActive = false;
            components = new Dictionary<Masks, EComponent>();
            
        }

        public Entity(params EComponent[] args)
        {
            isActive = false;
            components = new Dictionary<Masks, EComponent>();
            foreach(EComponent arg in args)
            {
                this.addComponent(arg);
            }

        }

        //Checks if the entity has a component of this type already
        public bool hasComponent(Masks bitMask)
        {
            if ((this.mask & (int)bitMask) != 0)
                return true;
            return false;
            
            
        }

        //Add a component to the entity
        public void addComponent(EComponent component)
        {
            if (!hasComponent(component.Mask))
            {
                components.Add(component.Mask, component);
                component.setEntity(this);
                mask += (int)component.Mask;
            }
        }

        //Remove a component from the entity
        public void removeComponent(EComponent component)
        {
            if (hasComponent(component.Mask))
            {
                components.Remove(component.Mask);
                mask -= (int)component.Mask;
            }
        }

        //Initialize the entity. This sets the entity to active, and initializes all of it's components as well
        public void initialize()
        {
            isActive = true;
        }

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        public Dictionary<Masks, EComponent> Components
        {
            get { return components; }
        }

        public EComponent getGraphics
        {
            get { return components[Masks.GRAPHICS]; }
        }

        public int Mask { get { return mask; } }
    }
}
