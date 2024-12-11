using Terraria;
using Terraria.ModLoader;

public class PersueState : EyeofCthuluState
{
    public PersueState(EyeofCthulhuGlobalNPC parent) : base(parent) {}

    public override void AI(NPC npc)
    {
        bool hasValidTarget = EyeofCthulhuGlobalNPC.targetingai.TargetClosestPlayer(npc);

        if (hasValidTarget)
        {   
            EyeofCthulhuGlobalNPC.positioningai.Update(npc);
        }
        else
        {
            npc.rotation += .015f;
        }

        // logic goes here
    }

}