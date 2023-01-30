using KeybrandsPlus.Assets.Sounds;
using KeybrandsPlus.Common.EntitySources;
using KeybrandsPlus.Common.Helpers;
using KeybrandsPlus.Content.Items.Currency;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace KeybrandsPlus.Content.Projectiles
{
    public class MunnySmall : ModProjectile
    {
        private bool pickup;
        private float randTimeOffset;
        private bool floating;
        private bool stuck;
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
            if (Projectile.velocity.Length() > 1f && !floating && !Projectile.lavaWet)
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

            if (pickup)
            {
                Player owner = Main.player[Projectile.owner];
                if (!owner.active || owner.dead)
                    Projectile.Kill();
                Projectile.aiStyle = 0;
                Projectile.extraUpdates = 0;
                Projectile.ignoreWater = true;
                Projectile.tileCollide = false;
                Projectile.Center = owner.Center + new Vector2((float)Math.Sin((Main.GameUpdateCount + randTimeOffset) * .25f) * 64f * (Projectile.timeLeft / 60f), 0f);
            }
            else
            {
                if (Projectile.lavaWet)
                    Projectile.Kill();

                if (!stuck)
                {
                    if (Projectile.timeLeft <= 1140)
                    {
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (!player.active)
                                break;
                            if (!player.dead && player.Hitbox.Intersects(Projectile.Hitbox) && KeyUtils.HasSpaceForMunny(player, out bool intoVault))
                            {
                                Projectile.owner = player.whoAmI;
                                pickup = true;
                                if (Main.myPlayer == player.whoAmI)
                                    SoundEngine.PlaySound(KeySoundStyle.MunnyPickup);
                                Item newItem = new Item(ModContent.ItemType<Munny>());
                                player.GetItem(player.whoAmI, newItem, new GetItemSettings(false, true, true));
                                Projectile.timeLeft = 60;
                                randTimeOffset = MathHelper.ToRadians(Main.rand.NextFloat(0f, 360f));
                                Projectile.netUpdate = true;
                            }
                        }
                    }
                    KeyUtils.FloatOnWater(Projectile, out floating, .95f, .025f, 4f, true, false, false);
                    if (!floating)
                        KeyUtils.FloatOnWater(Projectile, out floating, .5f, .0125f, 2f, false, true, false);
                    if (floating)
                    {
                        Projectile.aiStyle = 0;
                        if (Projectile.honeyWet)
                            Projectile.velocity.X *= .95f;
                        else
                            Projectile.velocity.X *= .98f;
                    }
                    else
                    {
                        Projectile.aiStyle = ProjAIStyleID.Arrow;
                        Projectile.velocity.X *= .99f;
                    }
                }
                else
                    Projectile.aiStyle = ProjAIStyleID.Arrow;

                Tile tile = Framing.GetTileSafely(Projectile.Bottom.ToTileCoordinates16());
                if (Collision.SolidCollision(Projectile.TopLeft, Projectile.width, Projectile.height) && tile.HasTile && !tile.IsActuated && !tile.IsHalfBlock && tile.Slope == SlopeType.Solid && Main.tileSolid[tile.TileType])
                {
                    Projectile.position.Y -= 8;
                    Projectile.velocity = Vector2.Zero;
                    stuck = true;
                }
                else if (stuck)
                {
                    stuck = false;
                    Vector2 velo = Main.rand.NextVector2CircularEdge(5f, 2.5f);
                    if (velo.Y > 0f)
                        velo.Y *= -1f;
                    velo *= Main.rand.NextFloat(.75f, 1f);
                    velo.Y -= 2.5f;
                    Projectile.velocity = velo;
                }
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
            Vector2 origin = texture.Size() * .5f;
            float scale = Utils.Clamp(Projectile.timeLeft / (pickup ? 60f : 20f), 0f, 1f);
            if (!stuck)
            {
                Main.EntitySpriteDraw(texture,
                    Projectile.Center - Main.screenPosition,
                    null, Color.White, 0f, origin, scale, SpriteEffects.None, 0
                    );
            }
            return false;
        }
        public override void PostDraw(Color lightColor)
        {
            if (!stuck)
                Lighting.AddLight(Projectile.Center, Color.Goldenrod.ToVector3() * .2f);
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
    public class MunnyMed : ModProjectile
    {
        private bool pickup;
        private float randTimeOffset;
        private bool floating;
        private bool stuck;
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
            Projectile.timeLeft = 1800;
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
            if (Projectile.velocity.Length() > 1f && !floating && !Projectile.lavaWet)
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

            if (pickup)
            {
                Player owner = Main.player[Projectile.owner];
                if (!owner.active || owner.dead)
                    Projectile.Kill();
                Projectile.aiStyle = 0;
                Projectile.extraUpdates = 0;
                Projectile.ignoreWater = true;
                Projectile.tileCollide = false;
                Projectile.Center = owner.Center + new Vector2((float)Math.Sin((Main.GameUpdateCount + randTimeOffset) * .25f) * 64f * (Projectile.timeLeft / 60f), 0f);
            }
            else
            {
                if (Projectile.lavaWet)
                    Projectile.Kill();

                if (!stuck)
                {
                    if (Projectile.timeLeft <= 1740)
                    {
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (!player.active)
                                break;
                            if (!player.dead && player.Hitbox.Intersects(Projectile.Hitbox) && KeyUtils.HasSpaceForMunny(player, out bool intoVault))
                            {
                                Projectile.owner = player.whoAmI;
                                pickup = true;
                                if (Main.myPlayer == player.whoAmI)
                                    SoundEngine.PlaySound(KeySoundStyle.MunnyPickup);
                                Item newItem = new Item(ModContent.ItemType<Munny>(), 10);
                                player.GetItem(player.whoAmI, newItem, new GetItemSettings(false, true, true));
                                Projectile.timeLeft = 60;
                                randTimeOffset = MathHelper.ToRadians(Main.rand.NextFloat(0f, 360f));
                                Projectile.netUpdate = true;
                            }
                        }
                    }
                    KeyUtils.FloatOnWater(Projectile, out floating, .95f, .025f, 4f, true, false, false);
                    if (!floating)
                        KeyUtils.FloatOnWater(Projectile, out floating, .5f, .0125f, 2f, false, true, false);
                    if (floating)
                    {
                        Projectile.aiStyle = 0;
                        if (Projectile.honeyWet)
                            Projectile.velocity.X *= .95f;
                        else
                            Projectile.velocity.X *= .98f;
                    }
                    else
                    {
                        Projectile.aiStyle = ProjAIStyleID.Arrow;
                        Projectile.velocity.X *= .99f;
                    }
                }
                else
                    Projectile.aiStyle = ProjAIStyleID.Arrow;

                Tile tile = Framing.GetTileSafely(Projectile.Bottom.ToTileCoordinates16());
                if (Collision.SolidCollision(Projectile.TopLeft, Projectile.width, Projectile.height) && tile.HasTile && !tile.IsActuated && !tile.IsHalfBlock && tile.Slope == SlopeType.Solid && Main.tileSolid[tile.TileType])
                {
                    Projectile.position.Y -= 8;
                    Projectile.velocity = Vector2.Zero;
                    stuck = true;
                }
                else if (stuck)
                {
                    stuck = false;
                    Vector2 velo = Main.rand.NextVector2CircularEdge(5f, 2.5f);
                    if (velo.Y > 0f)
                        velo.Y *= -1f;
                    velo *= Main.rand.NextFloat(.75f, 1f);
                    velo.Y -= 2.5f;
                    Projectile.velocity = velo;
                }
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
            Vector2 origin = texture.Size() * .5f;
            float scale = Utils.Clamp(Projectile.timeLeft / 20f, 0f, 1f);
            if (!stuck)
            {
                Main.EntitySpriteDraw(texture,
                    Projectile.Center - Main.screenPosition,
                    null, Color.White, 0f, origin, scale, SpriteEffects.None, 0
                    );
            }
            return false;
        }
        public override void PostDraw(Color lightColor)
        {
            if (!stuck)
                Lighting.AddLight(Projectile.Center, Color.Goldenrod.ToVector3() * .2f);
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
    public class MunnyBig : ModProjectile
    {
        private bool pickup;
        private float randTimeOffset;
        private bool floating;
        private bool stuck;
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
            Projectile.timeLeft = 2400;
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
            if (Projectile.velocity.Length() > 1f && !floating && !Projectile.lavaWet)
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

            if (pickup)
            {
                Player owner = Main.player[Projectile.owner];
                if (!owner.active || owner.dead)
                    Projectile.Kill();
                Projectile.aiStyle = 0;
                Projectile.extraUpdates = 0;
                Projectile.ignoreWater = true;
                Projectile.tileCollide = false;
                Projectile.Center = owner.Center + new Vector2((float)Math.Sin((Main.GameUpdateCount + randTimeOffset) * .25f) * 64f * (Projectile.timeLeft / 60f), 0f);
            }
            else
            {
                if (Projectile.lavaWet)
                    Projectile.Kill();

                if (!stuck)
                {
                    if (Projectile.timeLeft <= 2340)
                    {
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player player = Main.player[i];
                            if (!player.active)
                                break;
                            if (!player.dead && player.Hitbox.Intersects(Projectile.Hitbox) && KeyUtils.HasSpaceForMunny(player, out bool intoVault))
                            {
                                Projectile.owner = player.whoAmI;
                                pickup = true;
                                if (Main.myPlayer == player.whoAmI)
                                    SoundEngine.PlaySound(KeySoundStyle.MunnyPickup);
                                Item newItem = new Item(ModContent.ItemType<Munny>(), 100);
                                player.GetItem(player.whoAmI, newItem, new GetItemSettings(false, true, true));
                                Projectile.timeLeft = 60;
                                randTimeOffset = MathHelper.ToRadians(Main.rand.NextFloat(0f, 360f));
                                Projectile.netUpdate = true;
                            }
                        }
                    }
                    KeyUtils.FloatOnWater(Projectile, out floating, .95f, .025f, 4f, true, false, false);
                    if (!floating)
                        KeyUtils.FloatOnWater(Projectile, out floating, .5f, .0125f, 2f, false, true, false);
                    if (floating)
                    {
                        Projectile.aiStyle = 0;
                        if (Projectile.honeyWet)
                            Projectile.velocity.X *= .95f;
                        else
                            Projectile.velocity.X *= .98f;
                    }
                    else
                    {
                        Projectile.aiStyle = ProjAIStyleID.Arrow;
                        Projectile.velocity.X *= .99f;
                    }
                }
                else
                    Projectile.aiStyle = ProjAIStyleID.Arrow;

                Tile tile = Framing.GetTileSafely(Projectile.Bottom.ToTileCoordinates16());
                if (Collision.SolidCollision(Projectile.TopLeft, Projectile.width, Projectile.height) && tile.HasTile && !tile.IsActuated && !tile.IsHalfBlock && tile.Slope == SlopeType.Solid && Main.tileSolid[tile.TileType])
                {
                    Projectile.position.Y -= 8;
                    Projectile.velocity = Vector2.Zero;
                    stuck = true;
                }
                else if (stuck)
                {
                    stuck = false;
                    Vector2 velo = Main.rand.NextVector2CircularEdge(5f, 2.5f);
                    if (velo.Y > 0f)
                        velo.Y *= -1f;
                    velo *= Main.rand.NextFloat(.75f, 1f);
                    velo.Y -= 2.5f;
                    Projectile.velocity = velo;
                }
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value;
            Vector2 origin = texture.Size() * .5f;
            float scale = Utils.Clamp(Projectile.timeLeft / 20f, 0f, 1f);
            if (!stuck)
            {
                Main.EntitySpriteDraw(texture,
                    Projectile.Center - Main.screenPosition,
                    null, Color.White, 0f, origin, scale, SpriteEffects.None, 0
                    );
            }
            return false;
        }
        public override void PostDraw(Color lightColor)
        {
            if (!stuck)
                Lighting.AddLight(Projectile.Center, Color.Goldenrod.ToVector3() * .2f);
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
}
