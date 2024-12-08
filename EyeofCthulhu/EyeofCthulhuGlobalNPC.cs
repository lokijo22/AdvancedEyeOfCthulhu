using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

/*
This class is a Mob behavior handling class.

It is called now to handle the behavior of mobs with the given ID.

*/
public class EyeofCthulhuGlobalNPC : GlobalNPC
{

    private static int ticksPassed;
    private static TargetingAI targetingai = new TargetingAI();
    private static PositioningAI positioningai = new PositioningAI();


    // potentially create a list to access active copies of this ai
    public override bool AppliesToEntity(NPC npc, bool lateInstantiation)
    {
        return npc.type == NPCID.EyeofCthulhu;
    }

    public override void SetDefaults(NPC npc)
    {
        npc.aiStyle = -1;
    }

    // runs this method every tick that an NPC with the loaded NPCID exists
    // does not ovverride default behavior
    public override bool PreAI(NPC npc)
    {
        return false;
    }

    // this should make it so only this function is called for every instance of npc
    public override void PostAI(NPC npc)
    {
        ticksPassed ++;

        bool hasValidTarget = targetingai.TargetClosestPlayer(npc);

        if (hasValidTarget)
        {   
            positioningai.Update(npc);
        }
        else
        {
            npc.rotation += .015f;
        }
        
    }

    public override void OnKill(NPC npc)
    {

        // Send a message to the console
        Main.NewText("Eye of Cthulhu has been Defeated", Color.Red);

        // call GlobalNPC class OnKill() - does nothing
        base.OnKill(npc);
    }

    public override void OnSpawn(NPC npc, IEntitySource source)
    {

        // Send a message to the console
        Main.NewText("The Eye of Cthulhu has Awoken!", Color.Red);

        // call GlobalNPC OnSpawn() - does nothing
        base.OnSpawn(npc, source);
    }
}