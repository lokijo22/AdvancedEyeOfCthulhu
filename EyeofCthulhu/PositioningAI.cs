using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

public class PositioningAI
{
    private enum trackingMode{
        LAZY = 50,
        SlOW = 25,
        NORMAL = 15,
        QUICK = 10,
        LOCKED_ON = 5
    }
    public void Update(NPC npc, int mode = 0, bool debug = false)
    {
        // run methods for a specific mode to update values
        TrackTarget(npc);
        Vector2 playerVelocityVector = Main.player[npc.target].velocity;

        // calculate length of the hypotonuse to get accurate velocity
        float velocity = (float)Math.Sqrt(Math.Pow(playerVelocityVector.X, 2) + Math.Pow(playerVelocityVector.Y, 2));

        MoveForward(npc, Math.Abs(velocity) + 1.5f);
    }

    private void Rotate(float radians)
    {
        // rotate sprite by given radians
    }

    public void setMode()
    {
        // set or change mode
    }

    private void TrackTarget(NPC npc, bool debug = false)
    {
        // get the rotation that the eye must be in to face the player
        float targetRotation = GetRotationToTarget(npc);

        // normalize 
        NormalizeRotation(ref npc.rotation);

        // get the difference between current rotation and target rotation
        float difference = targetRotation - npc.rotation;

        NormalizeRotation(ref difference);

        float adjustment = difference;

        if (difference > (float)Math.PI)
        {
            adjustment -= (float)(Math.PI * 2);
        }

        if (debug)
        {
            Main.NewText("NPC Rotation: " + npc.rotation.ToString());
        }

        npc.rotation += adjustment/15;

        if (debug)
        {
            Main.NewText("Target Rotation: " + targetRotation.ToString());
            Main.NewText("Difference: " + difference.ToString());
            Main.NewText("Adjustment: " + adjustment.ToString());
        }
    }

    private float GetRotationToTarget(NPC npc, bool debug = false)
    {
        Player targetPlayer = Main.player[npc.target];
        
        Vector2 relativePosition = targetPlayer.Center - npc.Center;
        
        // find the angle using arctan. then add a 90 deg offest bc of the orientation of the sprite
        float rotation = (float)Math.Atan2(relativePosition.Y, relativePosition.X) - (float)(Math.PI/2);
        // ALERT!! rotation must be normalized after this

        NormalizeRotation(ref rotation);

        if (debug)
        {
            Main.NewText("Player center found at " + targetPlayer.Center.ToString(), Color.Cyan);

            Main.NewText("Relative Position: " + relativePosition.ToString(), Color.Cyan);

            Main.NewText("Eye Rotation: " + npc.rotation.ToString());
        }

        return rotation;
    }

    private void NormalizeRotation(ref float rotation)
    {
        // Ensure the rotation value is between -2π and 2π
        rotation %= (float)(2 * Math.PI);

        // Convert negative values to their positive equivalent
        if (rotation < 0)
        {
            rotation += (float)(2 * Math.PI);
        }
    }

    // gives the Eye velocity in the direction it is currently facing
    private void MoveForward(NPC npc, float speed)
    {   
        float xVelocity;
        // calculate x velocity
        if (npc.rotation < Math.PI)
        {
            // calculate negative x velocity
            xVelocity = -Linear_Gradient(0, npc.rotation, speed);
        }
        else if (npc.rotation > Math.PI)
        {
            // Calculate positive x velocity
            xVelocity = Linear_Gradient(2, npc.rotation, speed);
        }
        else
        {
            xVelocity = 0f;
        }

        float yVelocity;
        // calculate y velocity
        if (npc.rotation < Math.PI)
        {
            // calculate negative y velocity
            yVelocity = -Linear_Gradient(1, npc.rotation, speed);
        }
        else if (npc.rotation > Math.PI)
        {
            // Calculate positive y velocity
            yVelocity = Linear_Gradient(3, npc.rotation, speed);
        }
        else
        {
            yVelocity = 0f;
        }

        npc.velocity = new Vector2(xVelocity, yVelocity);
    }

    private float Linear_Gradient(int side, float rotation, float speed)
    {
        float pi = (float)Math.PI;

        // rotation converted relatively to base case 0 - pi
        rotation -= (pi / 2) * side;
        
        // add logic here to verify the selection size is exactly pi and side is 
        if (rotation > pi/2 && rotation < pi)
        {
            return speed * (pi - rotation) / (pi /2);
        }
        else
        {

            return speed * (rotation / (pi /2));
        }
    }
}