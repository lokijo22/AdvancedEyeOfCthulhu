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
    // potentially create a list to access active copies of this ai
    public override bool AppliesToEntity(NPC npc, bool lateInstantiation)
    {
        return npc.type == NPCID.EyeofCthulhu;
    }

    public override void SetDefaults(NPC npc)
    {
        npc.aiStyle = -1;
    }
    // runs this method every tick as that an NPC with the loaded NPCID exists
    // does not ovverride default behavior
    public override bool PreAI(NPC npc)
    {
        return false;
    }
    // this should make it so only this function is called for every instance of npc
    public override void PostAI(NPC npc)
    {
        npc.TargetClosest();

        if (npc.HasValidTarget)
        {   
            LookAtTarget(npc);
        }
        else
        {
            npc.rotation += .005f;
        }
        
    }

    private void LookAtTarget(NPC npc)
    {
        Player targetPlayer = Main.player[npc.target];

        Vector2 playerCenter = GetSpriteCenter(targetPlayer.position, targetPlayer.width, targetPlayer.height);

        Vector2 npcCenter = GetSpriteCenter(npc.position, npc.width, npc.height);
        
        Vector2 relativePosition = playerCenter - npcCenter;
        
        npc.rotation = (float)Math.Atan2(relativePosition.Y, relativePosition.X) - (float)(Math.PI/2);

        Main.NewText("Player center found at " + playerCenter.ToString(), Color.Blue);

        Main.NewText("Relative Position: " + relativePosition.ToString(), Color.Cyan);

        Main.NewText(npc.rotation);
        
    }

    private Vector2 GetSpriteCenter(Vector2 spritePosition, int spriteWidth, int spriteHeight)
    {
        float centerX = spritePosition.X + spriteWidth / 2f;
        float centerY = spritePosition.Y + spriteHeight / 2f;
        return new Vector2(centerX, centerY);
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