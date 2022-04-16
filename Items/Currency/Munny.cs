using KeybrandsPlus.Globals;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KeybrandsPlus.Items.Currency
{
    class Munny : ModItem
    {
        public override bool CloneNewInstances => true;
        private bool PickupTimer;
        private int PickupSoundStage;
        private bool PickupSound;
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
            PickupSoundStage = 0;
            PickupSound = false;
            if (!PickupTimer)
                PickupTimer = true;
        }
        public override void PostUpdate()
        {
            if (PickupSoundStage > 0)
            {
                PickupSound = true;
            }
            else
                PickupSoundStage++;
        }
        public override void GrabRange(Player player, ref int grabRange)
        {
            if (!item.GetGlobalItem<KeyItem>().PlayerDropped && Main.myPlayer == player.whoAmI)
            {
                if (player.GetModPlayer<KeyPlayer>().MasterTreasureMagnet)
                    grabRange *= 24;
                else if (player.GetModPlayer<KeyPlayer>().TreasureMagnetPlus)
                    grabRange *= 12;
                else if (player.GetModPlayer<KeyPlayer>().TreasureMagnet)
                    grabRange *= 6;
            }
        }
        public override bool GrabStyle(Player player)
        {
            if (!item.GetGlobalItem<KeyItem>().PlayerDropped && Main.myPlayer == player.whoAmI)
            {
                Vector2 vectorItemToPlayer = player.Center - item.Center;
                Vector2 movement = vectorItemToPlayer.SafeNormalize(default);
                if (player.GetModPlayer<KeyPlayer>().MasterTreasureMagnet)
                {
                    movement *= 12f;
                    item.velocity += movement;
                    AdjustMagnitude(ref item.velocity, 36f);
                    return true;
                }
                else if (player.GetModPlayer<KeyPlayer>().TreasureMagnetPlus)
                {
                    movement *= 6f;
                    item.velocity += movement;
                    AdjustMagnitude(ref item.velocity, 18f);
                    return true;
                }
                else if (player.GetModPlayer<KeyPlayer>().TreasureMagnet)
                {
                    movement *= 3f;
                    item.velocity += movement;
                    AdjustMagnitude(ref item.velocity, 9f);
                    return true;
                }
            }
            return base.GrabStyle(player);
        }
        private void AdjustMagnitude(ref Vector2 vector, float max)
        {
            float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            if (magnitude > max)
            {
                vector *= max / magnitude;
            }
        }
        public override bool CanPickup(Player player)
        {
            return PickupTimer;
        }
        public override bool OnPickup(Player player)
        {
            if (PickupSound && Main.myPlayer == player.whoAmI)
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/MunnyPickup").WithVolume(0.8f), player.Center);
            return true;
        }
    }
}
