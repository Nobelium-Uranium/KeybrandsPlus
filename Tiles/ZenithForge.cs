using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace KeybrandsPlus.Tiles
{
    public class ZenithForge : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileLighted[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.newTile.Origin = new Point16(1, 1);
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 18 };
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Zenitherium Crucible");
            AddMapEntry(new Color(0, 148, 255), name);
            dustType = DustID.Electric;
            disableSmartCursor = true;
            adjTiles = new int[] { TileID.AdamantiteForge };
            animationFrameHeight = 38;
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            Tile tile = Main.tile[i, j];
            if (tile.frameX >= 18 && tile.frameX < 36 && tile.frameY % 38 >= 18 && tile.frameY % 38 < 38)
            { // 0, 250, 190
                r = 0f;
                g = .98f * .75f;
                b = .75f * .75f;
            }
        }
        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            if (++frameCounter >= 8)
            {
                frameCounter = 0;
                frame = ++frame % 3;
            }
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }
            int height = tile.frameY % animationFrameHeight >= 16 ? 18 : 16;
            int animate = Main.tileFrame[Type] * animationFrameHeight;
            Main.spriteBatch.Draw(mod.GetTexture("Textures/Glowmasks/ZenithForge"), new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.frameX, tile.frameY + animate, 16, height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 16, ModContent.ItemType<Items.Placeable.ZenithForge>());
        }
    }
}
