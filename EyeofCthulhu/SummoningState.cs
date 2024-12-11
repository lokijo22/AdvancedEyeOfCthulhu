using Terraria;
using Terraria.ModLoader;

public class SummoningState : EyeofCthuluState
{
    public SummoningState(EyeofCthulhuGlobalNPC parent) : base(parent) {}

    public override void AI(NPC npc)
    {
        Main.NewText("Summoning!");
        // logic goes here
    }

}