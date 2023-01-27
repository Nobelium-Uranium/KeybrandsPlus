using KeybrandsPlus.Assets.Sounds;
using KeybrandsPlus.Common.EntitySources;
using KeybrandsPlus.Common.Helpers;
using KeybrandsPlus.Content.Items.Currency;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace KeybrandsPlus.Content.Projectiles
{
    public class MunnySmall : ModProjectile
    {
        public override string Texture => $"{nameof(KeybrandsPlus)}/Assets/Textures/MunnySmall";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Munny");
        }
        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(16);
            Projectile.aiStyle = ProjAIStyleID.Arrow;
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 1200;
        }
        public override bool? CanDamage()
        {
            return false;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X * .1f;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
            Projectile.velocity *= .5f;
            if (Projectile.velocity.Length() > 1f)
            {
                float volScale = Utils.Clamp(Projectile.velocity.Length() / 2.5f, .1f, 1f);
                SoundEngine.PlaySound(KeySoundStyle.MunnyBounce.WithVolumeScale(volScale), Projectile.Center);
            }
            return false;
        }
        public override void AI()
        {
            Projectile.rotation = 0;
            Projectile.spriteDirection = 1;
            if (Projectile.timeLeft <= 1140)
            {
                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    Player player = Main.player[i];
                    if (!player.active)
                        break;
                    if (!player.dead && player.Hitbox.Intersects(Projectile.Hitbox) && KeyUtils.HasSpaceForMunny(player))
                    {
                        if (Main.myPlayer == player.whoAmI)
                            SoundEngine.PlaySound(KeySoundStyle.MunnyPickup);
                        player.QuickSpawnItem(new ProjectileSource_MunnyPickup(), ModContent.ItemType<Munny>());
                        Projectile.Kill();
                    }
                }
            }
            Projectile.velocity.X *= .99f;
            Tile tile = Framing.GetTileSafely(Projectile.Bottom.ToTileCoordinates16());
            if (Collision.SolidCollision(Projectile.TopLeft, Projectile.width, Projectile.height) && tile.HasTile && !tile.IsActuated && !tile.IsHalfBlock && tile.Slope == SlopeType.Solid && Main.tileSolid[tile.TileType])
            {
                Projectile.position.Y -= 8;
                Projectile.velocity = Vector2.Zero;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
            Vector2 origin = texture.Size() * .5f;
            float scale = Utils.Clamp(Projectile.timeLeft / 20f, 0f, 1f);
            Main.EntitySpriteDraw(texture,
                Projectile.Center - Main.screenPosition,
                null, Color.White, 0f, origin, scale, SpriteEffects.None, 0
                );
            return false;
        }
        public override void PostDraw(Color lightColor)
        {
            Lighting.AddLight(Projectile.Center, Color.Goldenrod.ToVector3() * .2f);
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
    public class MunnyMed : ModProjectile
    {
        public override string Texture => $"{nameof(KeybrandsPlus)}/Assets/Textures/MunnyMed";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Munny");
        }
        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(20);
            Projectile.aiStyle = ProjAIStyleID.Arrow;
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 1200;
        }
        public override bool? CanDamage()
        {
            return false;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X * .1f;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
            Projectile.velocity *= .5f;
            if (Projectile.velocity.Length() > 1f)
            {
                float volScale = Utils.Clamp(Projectile.velocity.Length() / 2.5f, .1f, 1f);
                SoundEngine.PlaySound(KeySoundStyle.MunnyBounceMed.WithVolumeScale(volScale), Projectile.Center);
            }
            return false;
        }
        public override void AI()
        {
            Projectile.rotation = 0;
            Projectile.spriteDirection = 1;
            if (Projectile.timeLeft <= 1140)
            {
                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    Player player = Main.player[i];
                    if (!player.active)
                        break;
                    if (!player.dead && player.Hitbox.Intersects(Projectile.Hitbox) && KeyUtils.HasSpaceForMunny(player))
                    {
                        if (Main.myPlayer == player.whoAmI)
                            SoundEngine.PlaySound(KeySoundStyle.MunnyPickup);
                        player.QuickSpawnItem(new ProjectileSource_MunnyPickup(), ModContent.ItemType<Munny>(), 10);
                        Projectile.Kill();
                    }
                }
            }
            Projectile.velocity.X *= .99f;
            Tile tile = Framing.GetTileSafely(Projectile.Bottom.ToTileCoordinates16());
            if (Collision.SolidCollision(Projectile.TopLeft, Projectile.width, Projectile.height) && tile.HasTile && !tile.IsActuated && !tile.IsHalfBlock && tile.Slope == SlopeType.Solid && Main.tileSolid[tile.TileType])
            {
                Projectile.position.Y -= 8;
                Projectile.velocity = Vector2.Zero;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
            Vector2 origin = texture.Size() * .5f;
            float scale = Utils.Clamp(Projectile.timeLeft / 20f, 0f, 1f);
            Main.EntitySpriteDraw(texture,
                Projectile.Center - Main.screenPosition,
                null, Color.White, 0f, origin, scale, SpriteEffects.None, 0
                );
            return false;
        }
        public override void PostDraw(Color lightColor)
        {
            Lighting.AddLight(Projectile.Center, Color.Goldenrod.ToVector3() * .25f);
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
    public class MunnyBig : ModProjectile
    {
        public override string Texture => $"{nameof(KeybrandsPlus)}/Assets/Textures/MunnyBig";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Munny");
        }
        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(22);
            Projectile.aiStyle = ProjAIStyleID.Arrow;
            Projectile.extraUpdates = 1;
            Projectile.timeLeft = 1200;
        }
        public override bool? CanDamage()
        {
            return false;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X * .1f;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
            Projectile.velocity *= .5f;
            if (Projectile.velocity.Length() > 1f)
            {
                float volScale = Utils.Clamp(Projectile.velocity.Length() / 2.5f, .1f, 1f);
                SoundEngine.PlaySound(KeySoundStyle.MunnyBounceBig.WithVolumeScale(volScale), Projectile.Center);
            }
            return false;
        }
        public override void AI()
        {
            Projectile.rotation = 0;
            Projectile.spriteDirection = 1;
            if (Projectile.timeLeft <= 1140)
            {
                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    Player player = Main.player[i];
                    if (!player.active)
                        break;
                    if (!player.dead && player.Hitbox.Intersects(Projectile.Hitbox) && KeyUtils.HasSpaceForMunny(player))
                    {
                        if (Main.myPlayer == player.whoAmI)
                            SoundEngine.PlaySound(KeySoundStyle.MunnyPickup);
                        player.QuickSpawnItem(new ProjectileSource_MunnyPickup(), ModContent.ItemType<Munny>(), 100);
                        Projectile.Kill();
                    }
                }
            }
            Projectile.velocity.X *= .99f;
            Tile tile = Framing.GetTileSafely(Projectile.Bottom.ToTileCoordinates16());
            if (Collision.SolidCollision(Projectile.TopLeft, Projectile.width, Projectile.height) && tile.HasTile && !tile.IsActuated && !tile.IsHalfBlock && tile.Slope == SlopeType.Solid && Main.tileSolid[tile.TileType])
            {
                Projectile.position.Y -= 8;
                Projectile.velocity = Vector2.Zero;
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
            Vector2 origin = texture.Size() * .5f;
            float scale = Utils.Clamp(Projectile.timeLeft / 20f, 0f, 1f);
            Main.EntitySpriteDraw(texture,
                Projectile.Center - Main.screenPosition,
                null, Color.White, 0f, origin, scale, SpriteEffects.None, 0
                );
            return false;
        }
        public override void PostDraw(Color lightColor)
        {
            Lighting.AddLight(Projectile.Center, Color.Goldenrod.ToVector3() * .3f);
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
}
