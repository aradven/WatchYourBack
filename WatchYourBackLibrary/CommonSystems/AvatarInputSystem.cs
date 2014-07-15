﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using WatchYourBackLibrary;

namespace WatchYourBackLibrary
{

    /*
     * Adjusts aspects of the Avatars given various inputs by the players
     */
    public class AvatarInputSystem : ESystem
    {
        public AvatarInputSystem()
            : base(false, true, 3)
        {
            components += (int)Masks.PLAYER_INPUT;
            components += (int)Masks.VELOCITY;
            components += (int)Masks.TRANSFORM;
        }

        public override void update(TimeSpan gameTime)
        {
            foreach (Entity entity in activeEntities)
            {
                AvatarInputComponent input = (AvatarInputComponent)entity.Components[Masks.PLAYER_INPUT];
                VelocityComponent velocity = (VelocityComponent)entity.Components[Masks.VELOCITY];
                TransformComponent transform = (TransformComponent)entity.Components[Masks.TRANSFORM];
                AllegianceComponent allegiance = (AllegianceComponent)entity.Components[Masks.ALLEGIANCE];

                Vector2 rotationVector = HelperFunctions.AngleToVector(transform.Rotation);
                float relativeAngle = HelperFunctions.Angle(velocity.Velocity, rotationVector);
                float speedModifier = 1;
                if (relativeAngle > Math.PI / 2)
                    speedModifier = 1.0f / 2.0f;

                float xVel = 0;
                float yVel = 0;

                if (input.MoveY == 1)
                    yVel = 4 * speedModifier;
                else if (input.MoveY == -1)
                    yVel = -4 * speedModifier;
                else
                    yVel = 0;

                if (input.MoveX == 1)
                    xVel = 4 * speedModifier;
                else if (input.MoveX == -1)
                    xVel = -4 * speedModifier;
                else
                    xVel = 0;

                velocity.Y = yVel;
                velocity.X = xVel;

                float xDir = input.LookX - transform.Center.X;
                float yDir = input.LookY - transform.Center.Y;
                Vector2 dir = new Vector2(xDir, yDir);
                dir.Normalize();
                transform.LookDirection = dir;

               
                float angle = transform.LookAngle - transform.Rotation;
                angle = HelperFunctions.Normalize(angle);
                if (angle > Math.PI)
                   velocity.RotationSpeed = -5;
                if (angle < Math.PI)
                    velocity.RotationSpeed = 5;
                if (angle < 0.1f || angle > (float)Math.PI*2 - 0.1f)
                    velocity.RotationSpeed = 0;
                    
                




                if(entity.hasComponent(Masks.WIELDER))
                {
                    if (((WielderComponent)entity.Components[Masks.WIELDER]).hasWeapon)
                    {
                        Entity weapon = ((WielderComponent)entity.Components[Masks.WIELDER]).Weapon;
                        VelocityComponent weaponVelocityComponent = (VelocityComponent)weapon.Components[Masks.VELOCITY];
                        weaponVelocityComponent.Y = yVel;
                        weaponVelocityComponent.X = xVel;
                    }
                }
            }
        }
    }
}
