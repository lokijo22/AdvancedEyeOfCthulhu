using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

// This class aims to control all aspects of Finding Targets 
public class TargetingAI
{
    // Takes in an EyeOfCthulu and returns whether or not it was able to find a valid target
    public bool TargetClosestPlayer(NPC npc, bool debug = false)
    {
        npc.TargetClosest();

        bool validTargetExists = npc.HasValidTarget;

        if (debug)
        {
            if (validTargetExists)
            {
                Main.NewText("Valid target found", Color.Green);
                Main.NewText("Targeting player at position: " + Main.player[npc.target].position.ToString(), Color.Blue);
                return true;
            }
            else
            {
                Main.NewText("No valid target found", Color.Red);
                return false;
            }
        }
        else
        {
            return validTargetExists;
        }
        
    }
}