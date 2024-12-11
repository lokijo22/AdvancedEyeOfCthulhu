using System;
using System.Collections.Generic;
using System.Linq;
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
    private static List<NPC> npcs = [];
    private static List<EyeofCthuluState> states = [];
    public static Random random = new Random();
    private static EyeofCthuluState[] possibleStates;
    private static EyeofCthuluState state;
    private static int ticksPassed;
    public static TargetingAI targetingai = new TargetingAI();
    public static PositioningAI positioningai = new PositioningAI();

    public EyeofCthulhuGlobalNPC() : base() {
        possibleStates = new EyeofCthuluState[]
        {
            new DashingState(this),
            new GlaringState(this),
            new ObserveState(this),
            new PersueState(this),
            new SummoningState(this)
        };

        state = possibleStates[random.Next(0,4)];
        Main.NewText(state.ToString());
    }


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

        state.AI(npc);
        
    }

    public override void OnKill(NPC npc)
    {
        // forget this npc
        Main.NewText(npcs.Remove(npc));
        Main.NewText("There are " + npcs.Count.ToString() + " Eyes of Cthulhu");
    }

    public override void OnSpawn(NPC npc, IEntitySource source)
    {
        // new npc to keep track of
        npcs.Add(npc);
        Main.NewText("There are " + npcs.Count.ToString() + " Eyes of Cthulhu");
    }
}