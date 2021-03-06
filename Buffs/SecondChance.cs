﻿using Terraria;
using Terraria.ModLoader;
using KeybrandsPlus.Globals;

namespace KeybrandsPlus.Buffs
{
    class SecondChance : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Second Chance");
            Description.SetDefault("You will survive a fatal blow");
            Main.debuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            canBeCleared = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<KeyPlayer>().SecondChance = true;
        }
    }
}
