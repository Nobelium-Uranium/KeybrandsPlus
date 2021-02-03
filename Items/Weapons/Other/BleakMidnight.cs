using Terraria;
using Terraria.ID;
using KeybrandsPlus.Globals;
using Terraria.ModLoader;

namespace KeybrandsPlus.Items.Weapons.Other
{
    class BleakMidnight : Helpers.Keybrand
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+50 Dark Alignment\n" +
                "Alt Attack: Draconic Flare\n" +
                "MP Cost: 36\n" +
                "Fires a lingering flare bolt that explodes into debris when enemies are near\n" +
                "You can only have up to 3 flare bolts active at a time\n" +
                "Abilities: Dark Affinity, MP Rage, Critical MP Hasteza\n" +
                "Dark Affinity boosts MP recharge speed when under the effects of a damaging debuff\n" +
                "The rate is dependant on your Dark Alignment\n" +
                "'A replica of the keybrand once wielded by a legendary death seraph'");
            Item.staff[item.type] = true;
        }
        public override void SetDefaults()
        {
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.melee = true;
            item.width = 82;
            item.height = 86;
            item.damage = 120;
            item.useTime = 15;
            item.useAnimation = 15;
            item.knockBack = 7f;
            item.rare = ItemRarityID.Red;
            item.UseSound = SoundID.Item71;
            item.autoReuse = true;
            item.useTurn = true;
            item.shootSpeed = 30f;
            item.value = 5000000;
            item.GetGlobalItem<KeyItem>().Dark = true;
            item.GetGlobalItem<KeyItem>().LimitPenalty = 2;
            item.GetGlobalItem<KeyRarity>().ContributorRarity = true;
            item.GetGlobalItem<KeyRarity>().ContributorName = "Dan Yami";
            item.GetGlobalItem<KeyRarity>().Midnight = true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse != 2)
            {
                item.damage = 120;
                item.useStyle = ItemUseStyleID.SwingThrow;
                item.melee = true;
                item.magic = false;
                item.useTurn = true;
                item.useTime = 15;
                item.useAnimation = 15;
                item.damage = 75;
                item.shoot = 0;
                item.noMelee = false;
                item.UseSound = SoundID.Item71;
            }
            else
            {
                item.damage = 240;
                item.useStyle = ItemUseStyleID.HoldingOut;
                item.melee = false;
                item.magic = true;
                item.useTurn = false;
                item.useTime = 45;
                item.useAnimation = 45;
                item.damage = 160;
                item.shoot = ModContent.ProjectileType<Projectiles.DraconicFlareBolt>();
                item.noMelee = true;
                item.UseSound = SoundID.Item73;
                if (!player.GetModPlayer<KeyPlayer>().KeybrandLimitReached && !player.GetModPlayer<KeyPlayer>().rechargeMP && player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.DraconicFlareBolt>()] < 3) player.GetModPlayer<KeyPlayer>().currentMP -= 36;
                return !player.GetModPlayer<KeyPlayer>().rechargeMP && player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.DraconicFlareBolt>()] < 3;
            }
            return base.CanUseItem(player);
        }
        public override void HoldItem(Player player)
        {
            player.GetModPlayer<KeyPlayer>().DarkAffinity = true;
            player.GetModPlayer<KeyPlayer>().MPRage = true;
            player.GetModPlayer<KeyPlayer>().CritMPHasteza = true;
        }
        public override void UpdateInventory(Player player)
        {
            player.GetModPlayer<KeyPlayer>().DarkAlignment += 50;
        }
    }
}
