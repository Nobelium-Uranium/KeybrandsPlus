using KeybrandsPlus.Assets.Sounds;
using KeybrandsPlus.Common.Globals;
using KeybrandsPlus.Common.Helpers;
using KeybrandsPlus.Common.Rarities;
using KeybrandsPlus.Content.Items.Materials.Special;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace KeybrandsPlus.Content.Items.Other
{
    public abstract class TreasureBox : ModItem
    {
        public override string Texture => "KeybrandsPlus/Assets/placeholder";
        protected int score;
        public override void SetStaticDefaults() => Item.ResearchUnlockCount = 0;
        public override void SetDefaults() => Item.Size = new Vector2(20);
        public override void UpdateInventory(Player player)
        {
            if (score > 300)
                score = 300;
            else if (score < 0)
                score = 0;
        }
        public override bool CanRightClick() => true;
        public void SetScore(int desiredScore) => score = desiredScore;
        public override void SaveData(TagCompound tag) => tag["TreasureBoxScore"] = score;
        public override void LoadData(TagCompound tag) => score = tag.Get<int>("TreasureBoxScore");
        public override void ModifyTooltips(List<TooltipLine> tooltips) => tooltips.Add(new TooltipLine(Mod, "KPlus: TreasureBox",
                $"Score: {score}pts\n" +
                $"Quality: {KeyUtils.GetQualityName(score)}"));
    }
    #region Basic
    public class TreasureBox1 : TreasureBox
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Blue;
        }
        public override void RightClick(Player player)
        {
            if (score == 300) // P Rank
                player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<GradeMaterial5>());
            if (score >= 280) // S Rank
                player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<GradeMaterial3>());
            else if (score >= 250) // A Rank
                player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<GradeMaterial2>(), 10);
            else if (score >= 200) // B Rank
                player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<GradeMaterial2>());
            else if (score >= 100) // C Rank
                player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<GradeMaterial1>(), 10);
            else // D Rank
                player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<GradeMaterial1>());
            if (score > 0)
            {
                if (Main.myPlayer == player.whoAmI)
                    SoundEngine.PlaySound(KeySoundStyle.MunnyPickup);
                player.GetModPlayer<KeyPlayer>().AddMunny(score * 5);
            }
        }
    }
    public class TreasureBox1Repeat : TreasureBox
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Blue;
        }
        public override void RightClick(Player player)
        {
            if (score == 300) // P Rank
                player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<GradeMaterial3>());
            else if (score >= 280) // S Rank
                player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<GradeMaterial2>(), 50);
            else if (score >= 250) // A Rank
                player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<GradeMaterial2>(), 5);
            else if (score >= 200) // B Rank
                player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<GradeMaterial1>(), 50);
            else if (score >= 100) // C Rank
                player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<GradeMaterial1>(), 5);
            else // D Rank
                player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<GradeMaterial1>());
            if (score > 0)
            {
                if (Main.myPlayer == player.whoAmI)
                    SoundEngine.PlaySound(KeySoundStyle.MunnyPickup);
                player.GetModPlayer<KeyPlayer>().AddMunny(score * 5);
            }
        }
    }
    #endregion
    #region Hardened
    public class TreasureBox2 : TreasureBox
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.LightRed;
        }
        public override void RightClick(Player player)
        {
            if (score == 300) // P Rank
                player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<GradeMaterial5>());
            if (score >= 280) // S Rank
                player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<GradeMaterial3>(), 10);
            else if (score >= 250) // A Rank
                player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<GradeMaterial3>());
            else if (score >= 200) // B Rank
                player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<GradeMaterial2>(), 10);
            else if (score >= 100) // C Rank
                player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<GradeMaterial2>());
            else // D Rank
                player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<GradeMaterial1>(), 10);
            if (score > 0)
            {
                if (Main.myPlayer == player.whoAmI)
                    SoundEngine.PlaySound(KeySoundStyle.MunnyPickup);
                player.GetModPlayer<KeyPlayer>().AddMunny(score * 25);
            }
        }
    }
    public class TreasureBox2Repeat : TreasureBox
    {
        public override void SetDefaults()
        {
            Item.rare = ItemRarityID.LightRed;
        }
        public override void RightClick(Player player)
        {
            if (score == 300) // P Rank
                player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<GradeMaterial3>(), 10);
            else if (score >= 280) // S Rank
                player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<GradeMaterial3>(), 5);
            else if (score >= 250) // A Rank
                player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<GradeMaterial2>(), 50);
            else if (score >= 200) // B Rank
                player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<GradeMaterial2>(), 5);
            else if (score >= 100) // C Rank
                player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<GradeMaterial1>(), 50);
            else // D Rank
                player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<GradeMaterial1>(), 5);
            if (score > 0)
            {
                if (Main.myPlayer == player.whoAmI)
                    SoundEngine.PlaySound(KeySoundStyle.MunnyPickup);
                player.GetModPlayer<KeyPlayer>().AddMunny(score * 25);
            }
        }
    }
    #endregion
    #region Lunar
    public class TreasureBox3 : TreasureBox
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Cyan;
        }
        public override void RightClick(Player player)
        {
            if (score == 300) // P Rank
                player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<GradeMaterial5>());
            if (score >= 280) // S Rank
                player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<GradeMaterial4>());
            else if (score >= 250) // A Rank
                player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<GradeMaterial3>(), 10);
            else if (score >= 200) // B Rank
                player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<GradeMaterial3>());
            else if (score >= 100) // C Rank
                player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<GradeMaterial2>(), 10);
            else // D Rank
                player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<GradeMaterial2>());
            if (score > 0)
            {
                if (Main.myPlayer == player.whoAmI)
                    SoundEngine.PlaySound(KeySoundStyle.MunnyPickup);
                player.GetModPlayer<KeyPlayer>().AddMunny(score * 50);
            }
        }
    }
    public class TreasureBox3Repeat : TreasureBox
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ItemRarityID.Cyan;
        }
        public override void RightClick(Player player)
        {
            if (score == 300) // P Rank
                player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<GradeMaterial4>());
            else if (score >= 280) // S Rank
                player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<GradeMaterial3>(), 50);
            else if (score >= 250) // A Rank
                player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<GradeMaterial3>(), 5);
            else if (score >= 200) // B Rank
                player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<GradeMaterial2>(), 50);
            else if (score >= 100) // C Rank
                player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<GradeMaterial2>(), 5);
            else // D Rank
                player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<GradeMaterial1>(), 50);
            if (score > 0)
            {
                if (Main.myPlayer == player.whoAmI)
                    SoundEngine.PlaySound(KeySoundStyle.MunnyPickup);
                player.GetModPlayer<KeyPlayer>().AddMunny(score * 50);
            }
        }
    }
    public class TreasureBox3Special : TreasureBox
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 0;
            ItemID.Sets.IsLavaImmuneRegardlessOfRarity[Type] = true;
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ModContent.RarityType<ElectrumRarity>();
        }
        public override void RightClick(Player player)
        {
            if (score == 300) // P Rank
                player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<GradeMaterial5>());
            if (score >= 280) // S Rank
                player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<GradeMaterial4>());
            else if (score >= 250) // A Rank
                player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<GradeMaterial3>(), 10);
            else if (score >= 200) // B Rank
                player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<GradeMaterial3>());
            else if (score >= 100) // C Rank
                player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<GradeMaterial2>(), 10);
            else // D Rank
                player.QuickSpawnItem(player.GetSource_OpenItem(Type), ModContent.ItemType<GradeMaterial2>());
            if (score > 0)
            {
                if (Main.myPlayer == player.whoAmI)
                    SoundEngine.PlaySound(KeySoundStyle.MunnyPickup);
                player.GetModPlayer<KeyPlayer>().AddMunny(score * 50);
            }
        }
    }
    #endregion
}
