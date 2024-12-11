using System;
using Terraria;

public abstract class EyeofCthuluState
{
    private EyeofCthulhuGlobalNPC parent;

    public EyeofCthuluState(EyeofCthulhuGlobalNPC globalNPC)
    {

    }

    public abstract void AI(NPC npc);

    public EyeofCthulhuGlobalNPC GetParent()
    {
        return parent;
    }
}