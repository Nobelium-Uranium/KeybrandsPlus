using System.Collections.Generic;
using System.IO;
using KeybrandsPlus.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using static Terraria.ModLoader.ModContent;

namespace KeybrandsPlus.Globals
{
    class KeyItem : GlobalItem
    {
        public override bool InstancePerEntity { get { return true; } }
        public override bool CloneNewInstances { get { return true; } }
        private bool InHotbar;
        public bool IsKeybrand;
        public int LimitPenalty;
        public bool ExemptFromLimit;
        private bool KeybrandLimitReached;
        public bool NoWarning;
        public bool NoKeybrandMaster;

        public bool IsSpecial;
        public bool IsBelt;
        public bool IsChain;
        public bool IsRing;

        public bool AliveNKicking;

        public bool PlayerDropped;
        public int TimeLeft;
        public float Scale;

        public bool Fire;
        public bool Blizzard;
        public bool Thunder;
        public bool Aero;
        public bool Water;
        public bool Light;
        public bool Dark;
        public bool Nil;
        public int[] FireTypes = { 121, 1826, 3823, 3827 };
        public int[] BlizzardTypes = { 686, 724, 1306, 1928 };
        public int[] ThunderTypes = { 198, 199, 200, 201, 202, 203, 2880, 3764, 3765, 3766, 3767, 3768, 3769 };
        public int[] AeroTypes = { 190, 1827, 2608, 3258 };
        public int[] WaterTypes = { 155 };
        public int[] LightTypes = { 368, 674, 723, 757, 989, 2880, 3063 };
        public int[] DarkTypes = { 46, 273, 675, 795, 1327 };
        public int[] NilTypes = { };

        private int OldDef;

        public override void SetDefaults(Item item)
        {
            if (item.modItem is Keybrand)
                IsKeybrand = true;
            if (item.type == ItemType<Items.Currency.Munny>())
            {
                TimeLeft = 900;
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
            foreach (int i in LightTypes)
                if (item.type == i)
                    Light = true;
            foreach (int i in DarkTypes)
                if (item.type == i)
                    Dark = true;
        }
        public override void UpdateInventory(Item item, Player player)
        {
            InHotbar = KeyUtils.InHotbar(player, item);
            KeybrandLimitReached = player.GetModPlayer<KeyPlayer>().KeybrandLimitReached;
            if (item.type == ItemID.Keybrand)
                item.SetDefaults(ItemType<Items.Materials.RustedKeybrand>());
            if (IsKeybrand && !ExemptFromLimit && InHotbar)
                player.GetModPlayer<KeyPlayer>().HeldKeybrands += 1 + LimitPenalty;
        }
        public override void HoldItem(Item item, Player player)
        {
            if (IsKeybrand)
                player.GetModPlayer<KeyPlayer>().KeybrandSelected = true;
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
        public override bool CanEquipAccessory(Item item, Player player, int slot)
        {
            if ((IsSpecial && player.GetModPlayer<KeyPlayer>().SpecialEquipped) || (IsBelt && player.GetModPlayer<KeyPlayer>().BeltEquipped) || (IsChain && player.GetModPlayer<KeyPlayer>().ChainEquipped) || (IsRing && player.GetModPlayer<KeyPlayer>().RingEquipped))
                return false;
            return base.CanEquipAccessory(item, player, slot);
        }
        public override void UpdateEquip(Item item, Player player)
        {
            if (IsSpecial)
                player.GetModPlayer<KeyPlayer>().SpecialEquipped = true;
            if (IsBelt)
                player.GetModPlayer<KeyPlayer>().BeltEquipped = true;
            if (IsChain)
                player.GetModPlayer<KeyPlayer>().ChainEquipped = true;
            if (IsRing)
                player.GetModPlayer<KeyPlayer>().RingEquipped = true;
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
                Main.spriteBatch.Draw(texture, item.Center - Main.screenPosition, sourceRectangle, drawColor * Main.essScale, rotation, origin, Scale, SpriteEffects.None, 0f);
                return false;
            }
            return base.PreDrawInWorld(item, spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
        }
        public override bool CanUseItem(Item item, Player player)
        {
            if (IsKeybrand && !ExemptFromLimit && KeybrandLimitReached)
                return false;
            return base.CanUseItem(item, player);
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
        public override void ModifyHitNPC(Item item, Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
        {
            if (target.GetGlobalNPC<KeyNPC>().OnlyKeybrand && !IsKeybrand)
                damage /= 10;
            if (Nil)
            {
                if (target.defense > 0)
                {
                    OldDef = target.defense;
                    target.defense = 0;
                }
                damage = (int)(damage * (1f - target.GetGlobalNPC<KeyNPC>().NilResist));
            }
            if (!Nil)
            {
                if (item.melee || item.ranged)
                    damage -= (int)(damage * target.GetGlobalNPC<KeyNPC>().PhysResist);
                if (item.magic || item.summon)
                    damage -= (int)(damage * target.GetGlobalNPC<KeyNPC>().MagicResist);
                if (Fire)
                    damage -= (int)(damage * target.GetGlobalNPC<KeyNPC>().FireResist);
                if (Blizzard)
                    damage -= (int)(damage * target.GetGlobalNPC<KeyNPC>().BlizzardResist);
                if (Thunder)
                    damage -= (int)(damage * target.GetGlobalNPC<KeyNPC>().ThunderResist);
                if (Aero)
                    damage -= (int)(damage * target.GetGlobalNPC<KeyNPC>().AeroResist);
                if (Water)
                    damage -= (int)(damage * target.GetGlobalNPC<KeyNPC>().WaterResist);
                if (Light)
                    damage -= (int)(damage * target.GetGlobalNPC<KeyNPC>().LightResist);
                if (Dark)
                    damage -= (int)(damage * target.GetGlobalNPC<KeyNPC>().DarkResist);
            }
        }
        public override void ModifyHitPvp(Item item, Player player, Player target, ref int damage, ref bool crit)
        {
            if (Nil)
            {
                damage = (int)(damage + target.GetModPlayer<KeyPlayer>().PlayerDefense * (1 + target.endurance));
                damage = (int)(damage * (1f - target.GetModPlayer<KeyPlayer>().ChainResistNil));
            }
            else
            {
                if (Fire)
                    damage -= (int)(damage * target.GetModPlayer<KeyPlayer>().ChainResistFire);
                if (Blizzard)
                    damage -= (int)(damage * target.GetModPlayer<KeyPlayer>().ChainResistBlizzard);
                if (Thunder)
                    damage -= (int)(damage * target.GetModPlayer<KeyPlayer>().ChainResistThunder);
                if (Aero)
                    damage -= (int)(damage * target.GetModPlayer<KeyPlayer>().ChainResistAero);
                if (Water)
                    damage -= (int)(damage * target.GetModPlayer<KeyPlayer>().ChainResistWater);
                if (Light)
                    damage -= (int)(damage * target.GetModPlayer<KeyPlayer>().ChainResistLight);
                if (Dark)
                    damage -= (int)(damage * target.GetModPlayer<KeyPlayer>().ChainResistDark);
                if (target.GetModPlayer<KeyPlayer>().DamageControlPlus && target.statLife <= target.statLifeMax2 / 2)
                    damage /= 2;
                else if (target.GetModPlayer<KeyPlayer>().DamageControl && target.statLife <= target.statLifeMax2 / 5)
                    damage /= 2;
            }
        }
        public override void OnHitNPC(Item item, Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (OldDef > 0)
            {
                target.defense = OldDef;
                OldDef = 0;
            }
            if (IsKeybrand && !player.moonLeech && player.lifeSteal > 0f && !target.boss && !target.friendly && !target.SpawnedFromStatue && target.lifeMax >= 50 && target.damage != 0 && target.type != NPCID.TargetDummy)
            {
                if (Main.rand.NextBool(3) && damage >= target.life)
                    Item.NewItem(target.getRect(), ItemID.Heart);
            }
        }
        public override void ModifyWeaponDamage(Item item, Player player, ref float add, ref float mult, ref float flat)
        {
            if (player.GetModPlayer<KeyPlayer>().BlossomWings && (Fire || Blizzard || Thunder || Aero || Water || Light || Dark))
                add += .2f;
            if (IsKeybrand)
            {
                if (item.type == ItemType<Items.Weapons.Developer.Chimera>())
                    mult += player.GetModPlayer<KeyPlayer>().KeybrandMelee + player.GetModPlayer<KeyPlayer>().KeybrandRanged + player.GetModPlayer<KeyPlayer>().KeybrandMagic;
                else if (item.melee)
                    mult += player.GetModPlayer<KeyPlayer>().KeybrandMelee;
                else if (item.ranged)
                    mult += player.GetModPlayer<KeyPlayer>().KeybrandRanged;
                else if (item.magic)
                    mult += player.GetModPlayer<KeyPlayer>().KeybrandMagic;
            }
            if (AliveNKicking)
            {
                if (player.statLife >= (float)player.statLifeMax2 * .75f)
                    mult += .5f;
                else if (player.statLife <= (float)player.statLifeMax2 * .5f)
                    mult -= .5f;
            }
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            int index;
            if (item.type == ItemID.Keybrand)
            {
                tooltips.Add(new TooltipLine(mod, "Rusted", "[KeybrandsPlus] This item is replaced by the Rusted Keybrand") { overrideColor = Color.Red });
            }
            if (IsKeybrand)
            {
                index = tooltips.FindIndex(tt => tt.mod.Equals("Terraria") && tt.Name.Equals("ItemName"));
                if (index != -1)
                {
                    if (!NoWarning)
                        tooltips.Insert(index + 1, new TooltipLine(mod, "KeybrandType", "-Keybrand-") { overrideColor = Color.Goldenrod });
                    else
                        tooltips.Insert(index + 1, new TooltipLine(mod, "LockbladeType", "-Lockblade-") { overrideColor = Color.Goldenrod });
                }
                if (!NoWarning)
                {
                    if (LimitPenalty >= 4)
                        tooltips.Add(new TooltipLine(mod, "ImmenselyPowerful", "This keybrand is immensely powerful, preventing you from using any other keybrand in the hotbar") { overrideColor = Color.Goldenrod });
                    else if (LimitPenalty == 3)
                        tooltips.Add(new TooltipLine(mod, "ExtremelyPowerful", "This keybrand is extremely powerful, limiting the amount of keybrands you can have in the hotbar by 4") { overrideColor = Color.Goldenrod });
                    else if (LimitPenalty == 2)
                        tooltips.Add(new TooltipLine(mod, "VeryPowerful", "This keybrand is very powerful, limiting the amount of keybrands you can have in the hotbar by 3") { overrideColor = Color.Goldenrod });
                    else if (LimitPenalty == 1)
                        tooltips.Add(new TooltipLine(mod, "Powerful", "This keybrand is powerful, limiting the amount of keybrands you can have in the hotbar by 2") { overrideColor = Color.Goldenrod });
                    if (KeybrandLimitReached && InHotbar && !ExemptFromLimit)
                        tooltips.Add(new TooltipLine(mod, "HeldKeybrandsWarning", "Warning: You have exceeded the maximum allotted keybrands in the hotbar") { overrideColor = Color.Red });
                    else if (LimitPenalty < 4)
                        tooltips.Add(new TooltipLine(mod, "Limit", "You may only have up to 5 total keybrands in the hotbar") { overrideColor = Color.Goldenrod });
                    if (ExemptFromLimit)
                        tooltips.Add(new TooltipLine(mod, "NoPenalty", "However, this keybrand is exempt from this limit") { overrideColor = Color.Goldenrod });
                }
            }
            if (IsSpecial)
            {
                index = tooltips.FindIndex(tt => tt.mod.Equals("Terraria") && tt.Name.Equals("ItemName"));
                if (index != -1)
                    tooltips.Insert(index + 1, new TooltipLine(mod, "SubArmorType", "-Special-") { overrideColor = Color.Goldenrod });
            }
            else if (IsBelt || IsChain || IsRing)
            {
                index = tooltips.FindIndex(tt => tt.mod.Equals("Terraria") && tt.Name.Equals("ItemName"));
                if (index != -1)
                {
                    string SubArmorType = "???";
                    if (IsBelt && IsChain && IsRing)
                        SubArmorType = "Regalia";
                    else if (IsChain && IsRing)
                        SubArmorType = "Amulet";
                    else if (IsBelt && IsRing)
                        SubArmorType = "Band";
                    else if (IsBelt && IsChain)
                        SubArmorType = "Ribbon";
                    else if (IsRing)
                        SubArmorType = "Ring";
                    else if (IsChain)
                        SubArmorType = "Chain";
                    else if (IsBelt)
                        SubArmorType = "Belt";
                    tooltips.Insert(index + 1, new TooltipLine(mod, "SubArmorType", "-" + SubArmorType + "-") { overrideColor = Color.Goldenrod });
                }
            }
        }
    }
}
