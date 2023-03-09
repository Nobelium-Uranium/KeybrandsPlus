using KeybrandsPlus.Assets.Sounds;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace KeybrandsPlus.Common.Globals
{
    public class KeyProjectile : GlobalProjectile
    {
        public override void AI(Projectile projectile)
        {
            Player player = Main.player[projectile.owner];
            KeyPlayer modPlayer = player.GetModPlayer<KeyPlayer>();
            if (projectile.aiStyle == ProjAIStyleID.Hook && modPlayer.AirDash)
            {
                if (modPlayer.DashCount > 0)
                {
                    Vector2 velo = projectile.velocity * 2;
                    int dashDir = velo.X >= 0 ? 1 : -1;
                    if (velo.Length() > 30f)
                        velo = Vector2.Normalize(velo) * 30f;
                    if (Main.myPlayer == projectile.owner)
                        SoundEngine.PlaySound(KeySoundStyle.AirDash);
                    player.velocity = velo;
                    player.ChangeDir(dashDir);
                    player.immuneTime = 5;
                    modPlayer.DashCount--;
                }
                projectile.Kill();
            }
        }
    }
}
