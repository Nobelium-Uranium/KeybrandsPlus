using KeybrandsPlus.Assets.Sounds;
using KeybrandsPlus.Common.Globals;
using KeybrandsPlus.Content.Items.Materials.Special;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace KeybrandsPlus.Common.Helpers
{
    public sealed class KeyUtils
    {
        public static bool ProbablyABoss(NPC npc) => npc.boss || NPCID.Sets.ShouldBeCountedAsBoss[npc.type] || NPCID.Sets.BossHeadTextures[npc.type] != -1;
        public static Rectangle GetFrame(Texture2D tex, int currFrame = 0, bool horizontal = false)
        {
            if (horizontal)
                return new Rectangle(tex.Width * currFrame, 0, tex.Width, tex.Height);
            return new Rectangle(0, tex.Height * currFrame, tex.Width, tex.Height);
        }
        public static Vector2 GetOrigin(Texture2D tex) => new Vector2(tex.Width / 2f, tex.Height / 2f);
        /// <summary>
        /// Makes an entity float on liquids
        /// Despite the name of the method, this will also work with honey and lava if enabled
        /// </summary>
        /// <param name="entity">The entity that will float</param>
        /// <param name="floating">Outputs whether or not the entity is floating on a liquid</param>
        /// <param name="buoyancy">Bigger numbers means the entity is slowed less from falling into liquids</param>
        /// <param name="acc">How quickly the entity will float to the surface</param>
        /// <param name="maxAcc">The max velocity a floating entity travels upward</param>
        /// <param name="water">Does the entity float in water?</param>
        /// <param name="honey">Does the entity float in honey?</param>
        /// <param name="lava">Does the entity float in lava?</param>
        public static void FloatOnWater(Entity entity, out bool floating, float buoyancy = .5f, float acc = .1f, float maxAcc = 8f, bool water = true, bool honey = true, bool lava = true)
        {
            if ((water && entity.wet && !entity.honeyWet && !entity.lavaWet) || (honey && entity.honeyWet) || (lava && entity.lavaWet))
            {
                floating = true;
                if (entity.velocity.Y > 0f)
                    entity.velocity.Y *= buoyancy;
                int posX = (int)(entity.Center.X / 16f);
                int posY = (int)(entity.Center.Y / 16f);
                float waterLine = GetWaterLine(entity, posX, posY);
                if (entity.Center.Y > waterLine)
                {
                    entity.velocity.Y -= acc;
                    if (entity.velocity.Y < -maxAcc)
                        entity.velocity.Y = -maxAcc;
                    if (entity.Center.Y + entity.velocity.Y < waterLine)
                        entity.velocity.Y = waterLine - entity.Center.Y;
                }
            }
            else
                floating = false;
        }
        public static float GetWaterLine(Entity entity, int x, int y)
        {
            float result = entity.position.Y + (float)entity.height;
            if (Main.tile[x, y - 1] != null && Main.tile[x, y - 1].LiquidAmount > 0)
            {
                result = (float)(y * 16f);
                result -= (float)(Main.tile[x, y - 1].LiquidAmount / 16f);
            }
            else if (Main.tile[x, y] != null && Main.tile[x, y].LiquidType == LiquidID.Water)
            {
                result = (float)((y + 1) * 16f);
                result -= (float)(Main.tile[x, y].LiquidAmount / 16f);
            }
            else if (Main.tile[x, y + 1] != null && Main.tile[x, y + 1].LiquidType == LiquidID.Water)
            {
                result = (float)((y + 2) * 16f);
                result -= (float)(Main.tile[x, y + 1].LiquidAmount / 16f);
            }
            return result;
        }
        public static int BuyPriceByRarity(int rarity)
        {
            int amount = 2000; // 20 Silver
            if (rarity > 0)
            {
                for (int i = 0; i < rarity; i++)
                    amount *= 2;
            }
            return amount;
        }
        public static string GetRankLetter(int score, bool intermediate = false)
        {
            if (score == 300) // P Rank
                return "P";
            if (score >= 280) // S Rank
            {
                if (intermediate)
                {
                    if (score >= 295)
                        return "S++";
                    else if (score >= 290)
                        return "S+";
                    return "S";
                }
                else
                    return "S";
            }
            else if (score >= 250) // A Rank
            {
                if (intermediate)
                {
                    if (score >= 275)
                        return "A+";
                    else if (score >= 265)
                        return "A";
                    return "A-";
                }
                else
                    return "A";
            }
            else if (score >= 200) // B Rank
            {
                if (intermediate)
                {
                    if (score >= 235)
                        return "B+";
                    else if (score >= 225)
                        return "B";
                    return "B-";
                }
                else
                    return "B";
            }
            else if (score >= 100) // C Rank
            {
                if (intermediate)
                {
                    if (score >= 175)
                        return "C+";
                    else if (score >= 150)
                        return "C";
                    return "C-";
                }
                else
                    return "C";
            }
            else // D Rank
            {
                if (intermediate)
                {
                    if (score >= 75)
                        return "D+";
                    else if (score >= 50)
                        return "D";
                    return "D-";
                }
                else 
                    return "D";
            }
        }
        public static string GetQualityName(int score)
        {
            if (score == 300) // P Rank
                return "Immaculate";
            if (score >= 280) // S Rank
                return "Legendary";
            else if (score >= 250) // A Rank
                return "Exceptional";
            else if (score >= 200) // B Rank
                return "Average";
            else if (score >= 100) // C Rank
                return "Flawed";
            else // D Rank
            {
                if (score > 0)
                    return "Inferior";
                else
                    return "Worthless";
            }
        }
        public static void DrawItemWorldTexture(SpriteBatch spriteBatch, string path, Vector2 position, int width, int height, float rotation, float scale, Color drawColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>(path).Value;
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    position.X - Main.screenPosition.X + width * 0.5f,
                    position.Y - Main.screenPosition.Y + height - texture.Height * 0.5f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                drawColor,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }
    }
}
