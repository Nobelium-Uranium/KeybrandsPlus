using KeybrandsPlus.Common.EntitySources;
using KeybrandsPlus.Content.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace KeybrandsPlus.Content.Items.Currency
{
    public class Munny : ModItem
    {
        private bool pickup;
        private bool loot;
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Can be exchanged for various goods");
            SacrificeTotal = 100;
        }
        public override void SetDefaults()
        {
            Item.Size = new Vector2(12);
            Item.rare = ItemRarityID.Quest;
            Item.maxStack = 9999;
        }
        public override void OnSpawn(IEntitySource source)
        {
            if (source is ProjectileSource_MunnyPickup)
                pickup = true;
            if (source is EntitySource_Loot)
                loot = true;
        }
        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            if (!pickup && Main.netMode != NetmodeID.MultiplayerClient)
            {
                int munny = Item.stack;
                for (int i = 0; i < Item.stack; i++)
                {
                    Vector2 velo;
                    if (loot)
                    {
                        velo = Main.rand.NextVector2CircularEdge(5f, 2.5f);
                        if (velo.Y > 0f)
                            velo.Y *= -1f;
                        velo *= Main.rand.NextFloat(.75f, 1f);
                        velo.Y -= 2.5f;
                    }
                    else
                    {
                        velo = Item.velocity;
                        velo.X *= Main.rand.NextFloat(.5f, .75f);
                    }
                    velo *= Main.rand.NextFloat(.75f, 1f);
                    if (munny >= 100)
                    {
                        Projectile.NewProjectile(Terraria.Entity.GetSource_None(), Item.Center, velo, ModContent.ProjectileType<MunnyBig>(), 0, 0f);
                        munny -= 100;
                    }
                    else if (munny >= 10)
                    {
                        Projectile.NewProjectile(Terraria.Entity.GetSource_None(), Item.Center, velo, ModContent.ProjectileType<MunnyMed>(), 0, 0f);
                        munny -= 10;
                    }
                    else
                    {
                        Projectile.NewProjectile(Terraria.Entity.GetSource_None(), Item.Center, velo, ModContent.ProjectileType<MunnySmall>(), 0, 0f);
                        munny--;
                    }
                    if (munny <= 0)
                        break;
                }
                Item.TurnToAir();
                Item.position = Vector2.Zero;
                Item.active = false;
            }
        }
        public override bool OnPickup(Player player)
        {
            pickup = false;
            loot = false;
            return true;
        }
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            return false;
        }
    }
}
