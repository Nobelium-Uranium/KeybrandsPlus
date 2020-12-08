﻿using KeybrandsPlus.Globals;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace KeybrandsPlus.Buffs
{
    class HeartlessAngel : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Goner");
            Description.SetDefault("Descend, Heartless Angel\n<right> to cancel");
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<KeyPlayer>().HeartlessAngel = true;
            player.buffImmune[BuffID.SoulDrain] = true;
            if (player.buffTime[buffIndex] == 150)
            {
                CombatText.NewText(player.getRect(), Color.Goldenrod, "Descend...");
            }
            if (player.buffTime[buffIndex] == 75)
            {
                CombatText.NewText(player.getRect(), Color.Goldenrod, "Heartless Angel!");
            }
            if (player.buffTime[buffIndex] == 60)
            {
                Main.PlaySound(SoundID.Item123, player.position);
            }
            if (player.buffTime[buffIndex] == 2)
            {
                player.immuneTime = 0;
                Main.PlaySound(SoundID.Item67, player.position);
                Main.PlaySound(SoundID.Item119, player.position);
                CombatText.NewText(player.getRect(), CombatText.HealLife, -player.statLife + 1);
                player.statLife = 1;
                player.lifeRegenTime = 0;
                player.manaRegenDelay = 180;
                player.manaRegenCount = 0;
                CombatText.NewText(player.getRect(), CombatText.HealMana, -player.statMana);
                player.statMana = 0;
                player.ClearBuff(BuffID.Regeneration);
                player.ClearBuff(BuffID.ManaRegeneration);
                player.ClearBuff(BuffID.RapidHealing);
                player.ClearBuff(BuffID.Lifeforce);
                player.ClearBuff(BuffID.Heartreach);
                player.ClearBuff(BuffID.Honey);
                player.ClearBuff(BuffID.NebulaUpLife1);
                player.ClearBuff(BuffID.NebulaUpLife2);
                player.ClearBuff(BuffID.NebulaUpLife3);
                player.ClearBuff(BuffID.NebulaUpMana1);
                player.ClearBuff(BuffID.NebulaUpMana2);
                player.ClearBuff(BuffID.NebulaUpMana3);
                if (Main.expertMode)
                {
                    player.AddBuff(ModContent.BuffType<SecondChanceCooldown>(), 600);
                    player.AddBuff(BuffID.PotionSickness, 450);
                    player.AddBuff(ModContent.BuffType<ElixirSickness>(), 450);
                    player.AddBuff(ModContent.BuffType<PanaceaCD>(), 450);
                }
            }
        }
        public override bool ReApply(Player player, int time, int buffIndex)
        {
            return true;
        }
    }
}
