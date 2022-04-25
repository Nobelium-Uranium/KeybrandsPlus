using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace KeybrandsPlus.Items.Other
{
    public class HollowSigil : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Locks maximum life to 1 when favorited, causing any damage you take to kill you\n" +
                "The Crown Charm will prevent a fatal hit regardless of health, but will have a 30 second cooldown\n" +
                "Being saved by the Crown Charm still counts as a hit in the standard nohit ruleset");
        }
        public override void SetDefaults()
        {
            item.Size = new Vector2(10);
            item.rare = ItemRarityID.Blue;
            item.GetGlobalItem<Globals.KeyRarity>().ShadowRarity = true;
        }
        public override void UpdateInventory(Player player)
        {
            if (item.favorited)
            {
                player.GetModPlayer<Globals.KeyPlayer>().HollowSigil = true;
            }
        }
        public override void AddRecipes()
        {
            ModRecipe r = new ModRecipe(mod);
            r.AddTile(TileID.DemonAltar);
            r.SetResult(this);
            r.AddRecipe();
        }
    }
}
