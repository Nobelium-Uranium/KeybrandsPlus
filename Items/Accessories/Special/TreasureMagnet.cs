﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using KeybrandsPlus.Globals;
using Microsoft.Xna.Framework;

namespace KeybrandsPlus.Items.Accessories.Special
{
    class TreasureMagnet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Increases Munny pickup range\nDoes not attract Munny dropped by players");
        }
        public override void SetDefaults()
        {
            item.Size = new Vector2(28);
            item.rare = ItemRarityID.Green;
            item.accessory = true;
        }
        public override bool CanEquipAccessory(Player player, int slot)
        {
            return !player.GetModPlayer<KeyPlayer>().TreasureMagnetPlus && !player.GetModPlayer<KeyPlayer>().MasterTreasureMagnet;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<KeyPlayer>().TreasureMagnet = true;
        }
    }
    class TreasureMagnetPlus : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Magnet+");
            Tooltip.SetDefault("Greatly increases Munny pickup range\nDoes not attract Munny dropped by players");
        }
        public override void SetDefaults()
        {
            item.Size = new Vector2(28);
            item.rare = ItemRarityID.Lime;
            item.accessory = true;
        }
        public override bool CanEquipAccessory(Player player, int slot)
        {
            return !player.GetModPlayer<KeyPlayer>().TreasureMagnet && !player.GetModPlayer<KeyPlayer>().MasterTreasureMagnet;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<KeyPlayer>().TreasureMagnetPlus = true;
        }
    }
    class MasterTreasureMagnet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Vastly increases Munny pickup range\nDoes not attract Munny dropped by players");
        }
        public override void SetDefaults()
        {
            item.Size = new Vector2(28);
            item.rare = ItemRarityID.Red;
            item.accessory = true;
        }
        public override bool CanEquipAccessory(Player player, int slot)
        {
            return !player.GetModPlayer<KeyPlayer>().TreasureMagnet && !player.GetModPlayer<KeyPlayer>().TreasureMagnetPlus;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<KeyPlayer>().MasterTreasureMagnet = true;
        }
    }
}
