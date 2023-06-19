using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
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
    }
}
