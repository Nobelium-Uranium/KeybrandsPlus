using Terraria;
using Terraria.ID;
using KeybrandsPlus.Globals;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using KeybrandsPlus.Helpers;
using Terraria.ModLoader;

namespace KeybrandsPlus.Items.Weapons.Developer
{
    class Chimera : Helpers.Keybrand
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("-10 Light Alignment\n" +
                "-10 Dark Alignment\n" +
                "Alt Attack: Chimera's Bite\n" +
                "MP Cost: 25\n" +
                "Creates a piercing magic blade beam that inflicts a powerful bleeding debuff\n" +
                "The blade beam can hit most Heartless that are normally immune to magic\n" +
                "Also throws a fan of knives that ignore defense at critical moments\n" +
                "Abilities: MP Hasteza, Damage Control, Leaf Bracer\n" +
                "Direct melee hits fill twice as much Delta than usual\n" +
                "Can only be used after Moon Lord is defeated\n" +
                "'Bound to the Stars'");
        }
        public override void SetDefaults()
        {
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.melee = false;
            item.crit = 13;
            item.Size = new Vector2(30);
            if (KeybrandsPlus.SoALoaded)
                item.damage = 395;
            else
                item.damage = 225;
            item.useTime = 10;
            item.useAnimation = 10;
            item.knockBack = 5f;
            item.rare = ItemRarityID.Cyan;
            item.UseSound = SoundID.Item116;
            item.autoReuse = true;
            item.useTurn = true;
            item.shootSpeed = 2.5f;
            item.GetGlobalItem<KeyItem>().Nil = true;
            item.GetGlobalItem<KeyItem>().LimitPenalty = 4;
            item.GetGlobalItem<KeyRarity>().DeveloperRarity = true;
            item.GetGlobalItem<KeyRarity>().DeveloperName = "ChemAtDark";
        }
        public override bool NewPreReforge()
        {
            return false;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse != 2)
            {
                if (KeybrandsPlus.SoALoaded)
                    item.damage = 395;
                else
                    item.damage = 225;
                item.useTime = 10;
                item.useAnimation = 10;
                item.crit = 13;
                item.magic = false;
                item.useTurn = true;
                item.shoot = 0;
                item.noMelee = false;
                item.UseSound = SoundID.Item116;
                item.knockBack = 5f;
            }
            else
            {
                if (KeybrandsPlus.SoALoaded)
                    item.damage = 830;
                else
                    item.damage = 475;
                item.useTime = 14;
                item.useAnimation = 14;
                item.crit = 3;
                item.magic = true;
                item.useTurn = false;
                item.shoot = ProjectileType<Projectiles.ChimeraBite>();
                item.noMelee = true;
                item.UseSound = SoundID.Item60;
                item.knockBack = 0f;
                if (!player.GetModPlayer<KeyPlayer>().KeybrandLimitReached && !player.GetModPlayer<KeyPlayer>().rechargeMP) player.GetModPlayer<KeyPlayer>().currentMP -= 25;
                return (NPC.downedMoonlord || player.name == "Chem" || player.name == "Aarazel" || player.name == "Araxlaez" || player.name == "Lazure") && !player.GetModPlayer<KeyPlayer>().rechargeMP;
            }
            return NPC.downedMoonlord || player.name == "Chem" || player.name == "Aarazel" || player.name == "Araxlaez" || player.name == "Lazure";
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2 && player.statLife <= player.statLifeMax2 / 5)
            {
                int ProjAmount = Main.rand.Next(7, 14);
                for (int i = 0; i < ProjAmount; i++)
                    Projectile.NewProjectile(position, Vector2.Normalize(new Vector2(speedX, speedY)) * 15, ModContent.ProjectileType<Projectiles.ChimeraTooth>(), damage / 4, 0, player.whoAmI, Main.rand.Next(0, 3), 45);
            }
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }
        public override void HoldItem(Player player)
        {
            if (KeyUtils.InHotbar(player, item) && !player.GetModPlayer<KeyPlayer>().KeybrandLimitReached && (NPC.downedMoonlord || player.name == "Chem" || player.name == "Aarazel" || player.name == "Araxlaez" || player.name == "Lazure"))
            {
                player.GetModPlayer<KeyPlayer>().MPHasteza = true;
                player.GetModPlayer<KeyPlayer>().DamageControl = true;
                player.GetModPlayer<KeyPlayer>().LeafBracer = true;
            }
        }
        public override void UpdateInventory(Player player)
        {
            if (NPC.downedMoonlord || player.name == "Chem" || player.name == "Aarazel" || player.name == "Araxlaez" || player.name == "Lazure")
            {
                player.GetModPlayer<KeyPlayer>().LightAlignment -= 10;
                player.GetModPlayer<KeyPlayer>().DarkAlignment -= 10;
            }
        }
    }
}
