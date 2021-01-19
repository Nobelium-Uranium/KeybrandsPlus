using Terraria;
using Terraria.ID;
using KeybrandsPlus.Globals;
using Terraria.ModLoader;
using System;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KeybrandsPlus.Items.Weapons.Developer
{
    class Chimera : Helpers.Keybrand
    {
        public override Texture2D GlowmaskTexture => mod.GetTexture("Textures/Glowmasks/Chimera");

        private int LifestealCooldown;
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("-10 Light Alignment\n" +
                "-10 Dark Alignment\n" +
                "Alt Attack: Chimera's Bite\n" +
                "Creates a magic blade beam that make enemies bleed healing droplets\n" +
                "The blade beam can hit most Heartless that are normally immune to magic\n" +
                "Abilities: MP Hasteza, Damage Control, Leaf Bracer\n" +
                "Restores mana on direct hits, also making the enemy bleed\n" +
                "Can only be used after Moon Lord is defeated\n" +
                "'Bound to the Stars'");
        }
        public override void SetDefaults()
        {
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.melee = false;
            item.crit = 13;
            item.Size = new Vector2(30);
            item.damage = 75;
            item.useTime = 10;
            item.useAnimation = 10;
            item.knockBack = 5f;
            item.rare = ItemRarityID.Cyan;
            item.UseSound = SoundID.Item116;
            item.autoReuse = true;
            item.useTurn = true;
            item.shootSpeed = 1f;
        }
        public override bool NewPreReforge()
        {
            return false;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse != 2)
            {
                item.damage = 75;
                item.crit = 13;
                item.magic = false;
                item.useTurn = true;
                item.mana = 0;
                item.shoot = 0;
                item.noMelee = false;
                item.UseSound = SoundID.Item116;
                item.knockBack = 5f;
            }
            else
            {
                item.damage = 145;
                item.crit = 3;
                item.magic = true;
                item.useTurn = false;
                item.mana = 5;
                item.shoot = ProjectileType<Projectiles.ChimeraBite>();
                item.noMelee = true;
                item.UseSound = SoundID.Item60;
                item.knockBack = 0f;
            }
            return NPC.downedMoonlord || player.name == "Chem" || player.name == "Aarazel" || player.name == "Araxlaez" || player.name == "Lazure";
        }
        public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
        {
            if (!target.friendly && target.type != NPCID.TargetDummy && target.lifeMax > 5 && LifestealCooldown <= 0)
            {
                player.statMana += damage / 5;
                CombatText.NewText(player.getRect(), CombatText.HealMana, damage / 5);
                if (player.lifeSteal > 0f)
                    for (int i = 0; i < Main.rand.Next(5, 21); i++)
                    {
                        int blood = Item.NewItem(target.getRect(), ItemType<Items.Other.Blood>());
                        Main.item[blood].velocity *= 1.5f;
                    }
                LifestealCooldown = 15;
            }
        }
        public override void HoldItem(Player player)
        {
            if (NPC.downedMoonlord)
            {
                player.GetModPlayer<KeyPlayer>().MPHasteza = true;
                player.GetModPlayer<KeyPlayer>().DamageControl = true;
                player.GetModPlayer<KeyPlayer>().LeafBracer = true;
            }
            if (LifestealCooldown > 0)
                LifestealCooldown -= 1;
        }
        public override void UpdateInventory(Player player)
        {
            if (NPC.downedMoonlord)
            {
                player.GetModPlayer<KeyPlayer>().LightAlignment -= 10;
                player.GetModPlayer<KeyPlayer>().DarkAlignment -= 10;
            }
        }
    }
}
