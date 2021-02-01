using KeybrandsPlus.Globals;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KeybrandsPlus.Items.Currency
{
    class Munny : ModItem
    {
        public override bool CloneNewInstances => true;
        private bool PickupTimer;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Munny");
            Tooltip.SetDefault("Can be exchanged for various goods");
        }
        public override void SetDefaults()
        {
            item.Size = new Vector2(12);
            item.rare = ItemRarityID.Quest;
            item.maxStack = 9999;
            item.value = 5000;
        }
        public override Color? GetAlpha(Color lightColor) => Color.White;
        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            if (!PickupTimer)
            {
                PickupTimer = true;
            }
        }
        public override void GrabRange(Player player, ref int grabRange)
        {
            if (!item.GetGlobalItem<KeyItem>().PlayerDropped)
            {
                if (player.GetModPlayer<KeyPlayer>().MasterTreasureMagnet)
                    grabRange *= 30;
                else if (player.GetModPlayer<KeyPlayer>().TreasureMagnetPlus)
                    grabRange *= 10;
                else if (player.GetModPlayer<KeyPlayer>().TreasureMagnet)
                    grabRange *= 5;
            }
        }
        public override bool GrabStyle(Player player)
        {
            if (!item.GetGlobalItem<KeyItem>().PlayerDropped)
            {
                Vector2 vectorItemToPlayer = player.Center - item.Center;
                Vector2 movement = vectorItemToPlayer.SafeNormalize(default);
                if (player.GetModPlayer<KeyPlayer>().MasterTreasureMagnet)
                {
                    movement *= 40f;
                    item.velocity = movement;
                    return true;
                }
                else if (player.GetModPlayer<KeyPlayer>().TreasureMagnetPlus)
                {
                    movement *= 20f;
                    item.velocity = movement;
                    return true;
                }
                else if (player.GetModPlayer<KeyPlayer>().TreasureMagnet)
                {
                    movement *= 10f;
                    item.velocity = movement;
                    return true;
                }
            }
            return base.GrabStyle(player);
        }
        public override bool CanPickup(Player player)
        {
            return PickupTimer;
        }
        public override bool OnPickup(Player player)
        {
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/MunnyPickup").WithVolume(0.8f), player.Center);
            return true;
        }
    }
}
