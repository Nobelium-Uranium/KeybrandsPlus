using System.Collections.Generic;
using KeybrandsPlus.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace KeybrandsPlus.Globals
{
    class KeyItem : GlobalItem
    {
        public override bool InstancePerEntity { get { return true; } }
        public override bool CloneNewInstances { get { return true; } }
        public bool IsKeybrand;
        public int TimeLeft;
        public float Scale;

        public bool Fire;
        public bool Blizzard;
        public bool Thunder;
        public bool Aero;
        public bool Water;
        public bool Dark;
        public int[] FireTypes = { 121, 1826, 3823, 3827 };
        public int[] BlizzardTypes = { 686, 724, 1306, 1928 };
        public int[] ThunderTypes = { 198, 199, 200, 201, 202, 203, 2880, 3764, 3765, 3766, 3767, 3768, 3769 };
        public int[] AeroTypes = { 190, 1827, 2608, 3258 };
        public int[] WaterTypes = { 155 };
        public int[] DarkTypes = { 46, 273, 675, 795, 1327 };

        public override void SetDefaults(Item item)
        {
            if (item.modItem is Keybrand)
                IsKeybrand = true;
            if (item.type == ItemType<Items.Currency.Munny>())
            {
                TimeLeft = 600;
                Scale = 1f;
            }
            else if (item.type == ItemType<Items.Other.Blood>())
                TimeLeft = Main.rand.Next(60, 301);
            foreach (int i in FireTypes)
                if (item.type == i)
                    Fire = true;
            foreach (int i in BlizzardTypes)
                if (item.type == i)
                    Blizzard = true;
            foreach (int i in ThunderTypes)
                if (item.type == i)
                    Thunder = true;
            foreach (int i in AeroTypes)
                if (item.type == i)
                    Aero = true;
            foreach (int i in WaterTypes)
                if (item.type == i)
                    Water = true;
            foreach (int i in DarkTypes)
                if (item.type == i)
                    Dark = true;
        }
        public override void UpdateInventory(Item item, Player player)
        {
            if (item.type == ItemID.Keybrand)
                item.SetDefaults(ItemType<Items.Materials.RustedKeybrand>());
        }
        public override void Update(Item item, ref float gravity, ref float maxFallSpeed)
        {
            if (item.type == ItemID.Keybrand)
                item.SetDefaults(ItemType<Items.Materials.RustedKeybrand>());
            if (item.type == ItemType<Items.Currency.Munny>() || item.type == ItemType<Items.Other.Blood>())
            {
                if (TimeLeft > 0)
                {
                    TimeLeft--;
                    if (item.type == ItemType<Items.Other.Blood>() && item.wet)
                        TimeLeft--;
                }
                else if (item.type == ItemType<Items.Currency.Munny>())
                {
                    if (Scale <= 0.1)
                        item.active = false;
                    Scale -= 0.1f;
                }
                else
                    item.active = false;
            }
        }
        public override void PostUpdate(Item item)
        {
            if (item.type == ItemType<Items.Currency.Munny>())
                Lighting.AddLight(item.Center, Color.Gold.ToVector3() * 0.2f * Scale);
        }
        public override bool CanPickup(Item item, Player player)
        {
            if (item.type == ItemType<Items.Currency.Munny>())
            {
                return TimeLeft > 0;
            }
            return base.CanPickup(item, player);
        }
        public override bool PreDrawInWorld(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            if (item.type == ItemType<Items.Currency.Munny>())
            {
                Texture2D texture = mod.GetTexture("Textures/Munny");
                Rectangle sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
                Vector2 origin = sourceRectangle.Size() / 2f;
                Color drawColor = item.GetAlpha(lightColor);
                Main.spriteBatch.Draw(texture, item.Center - Main.screenPosition, sourceRectangle, drawColor, rotation, origin, Scale, SpriteEffects.None, 0f);
                return false;
            }
            return base.PreDrawInWorld(item, spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
        }
        public override bool UseItem(Item item, Player player)
        {
            if (player.GetModPlayer<KeyPlayer>().LeafBracer && item.healLife > 0)
            {
                CombatText.NewText(player.getRect(), CombatText.HealLife, "Leaf Bracer!", true);
                if (player.GetModPlayer<KeyPlayer>().LeafBracer && 90 - player.GetModPlayer<KeyPlayer>().LeafBracerTimer > 0)
                    player.GetModPlayer<KeyPlayer>().LeafBracerTimer += 90 - player.GetModPlayer<KeyPlayer>().LeafBracerTimer;
            }
            return base.UseItem(item, player);
        }
        public override void OnHitNPC(Item item, Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (IsKeybrand && player.lifeSteal > 0f && !target.boss && !target.friendly && !target.SpawnedFromStatue && target.lifeMax >= 50 && target.damage != 0 && target.type != NPCID.TargetDummy)
            {
                if (damage >= target.life)
                    Item.NewItem(target.getRect(), ItemID.Heart);
                else if (Main.rand.NextBool(50))
                    Item.NewItem(target.getRect(), ItemID.Heart);
            }
        }
        public override void ModifyWeaponDamage(Item item, Player player, ref float add, ref float mult, ref float flat)
        {
            if (IsKeybrand)
            {
                if (item.type == ItemType<Items.Weapons.Developer.Chimera>())
                    mult += (player.GetModPlayer<KeyPlayer>().KeybrandMelee + player.GetModPlayer<KeyPlayer>().KeybrandRanged + player.GetModPlayer<KeyPlayer>().KeybrandMagic) / 3;
                else if (item.melee)
                    mult += player.GetModPlayer<KeyPlayer>().KeybrandMelee;
                else if (item.ranged)
                    mult += player.GetModPlayer<KeyPlayer>().KeybrandRanged;
                else if (item.magic)
                    mult += player.GetModPlayer<KeyPlayer>().KeybrandMagic;
            }
            if (player.GetModPlayer<KeyPlayer>().AliveAndKicking && player.statLife == player.statLifeMax2)
                mult += .5f;
        }
    }
}
