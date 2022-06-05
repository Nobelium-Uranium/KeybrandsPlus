using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace KeybrandsPlus.Common.Systems
{
    public class MPPlayer : ModPlayer
    {
        public bool MPBarVisible;
        public bool MPCharge;
        public bool MPRegen;

        public int ChargedCrystals;

        public int CurrentMP;
        public const int DefaultMaxMP = 100;
        public int MaxMP;
        public int MaxMP2;
        public int MPRegenCounter;
        public float MPRegenRate;
        public float MPRegenDelay;

        public int CurrentDelta;
        public int MaxDelta;
        public int MaxDelta2;
        public float DeltaDecayCounter;
        public float DeltaDecayDelay;

        public float MPChargeTimer = 1;
        public float MPChargeTimerMax = 1;
        public float MPChargeRate;

        public static readonly Color HealMP = Color.DodgerBlue;

        public override void Initialize()
        {
            MaxMP = DefaultMaxMP;
            MaxDelta = DefaultMaxMP;
        }

        public override void ResetEffects()
        {
            ResetVariables();
        }

        public override void UpdateDead()
        {
            ResetVariables();
        }

        private void ResetVariables()
        {

        }

        //TODO whatever the hell this needs

        public override void PostUpdateEquips()
        {
            if (Player.dead)
            {
                MPCharge = false;
                CurrentMP = MaxMP2;
                MPBarVisible = false;
            }
            else if (!MPBarVisible)
                MPBarVisible = true;
            #region Handle Delta
            if (CurrentDelta > 0)
            {
                if (DeltaDecayDelay > 300)
                {
                    if (DeltaDecayCounter >= 5f)
                    {
                        DeltaDecayCounter = 0;
                        CurrentDelta--;
                    }
                    else
                        DeltaDecayCounter += 1 * (MaxDelta2 / 100);
                }
                else
                    DeltaDecayDelay++;
            }
            else
            {
                if (CurrentDelta < 0)
                    CurrentDelta = 0;
                DeltaDecayDelay = 0;
                DeltaDecayCounter = 0;
            }
            #endregion
            #region Handle MP
            if (!MPCharge)
            {
                if (CurrentDelta >= MaxDelta2)
                {
                    int RestoreMP = 0;
                    while (CurrentDelta >= MaxDelta2)
                    {
                        RestoreMP += (int)Main.rand.NextFloat(3f, 7f) * (MaxMP2 / 100);
                        CurrentDelta -= MaxDelta2;
                    }
                    CurrentMP += RestoreMP;
                    if (Main.myPlayer == Player.whoAmI)
                        CombatText.NewText(Player.getRect(), HealMP, RestoreMP);
                }
                if (CurrentMP > MaxMP2)
                    CurrentMP = MaxMP2;
                else if (CurrentMP <= 0)
                {
                    CurrentMP = MaxMP2;
                    MPChargeTimerMax = MPChargeTimer = 1200;
                    MPCharge = true;
                }
            }
            else
            {
                if (CurrentDelta >= MaxDelta2)
                {
                    int RestoreMP = 0;
                    while (CurrentDelta >= MaxDelta2)
                    {
                        RestoreMP += 60;
                        CurrentDelta -= MaxDelta2;
                    }
                    MPChargeTimer -= RestoreMP;
                    if (Main.myPlayer == Player.whoAmI)
                        CombatText.NewText(Player.getRect(), HealMP, $"-{RestoreMP / 60}s");
                }
                MPChargeRate = 1f;
                if (MPChargeRate > 3f)
                    MPChargeRate = 3f;
                else if (MPChargeRate < 0f)
                    MPChargeRate = 0f;
                MPChargeTimer -= MPChargeRate;
                CurrentMP = (int)MathHelper.Lerp(MaxMP2, 0, 1f - MPChargeTimer / MPChargeTimerMax);
                if (MPChargeTimer <= 0)
                {
                    MPChargeTimer = 0;
                    CurrentMP = MaxMP2;
                    MPCharge = false;
                }
            }
            #endregion
            if (CurrentDelta > MaxDelta2)
                CurrentDelta = MaxDelta2;
            else if (CurrentDelta < 0)
                CurrentDelta = 0;
        }
    }

    internal class MPSystem : UIState
    {
        private Vector2 offset;
        public bool dragging;

        private UIImage MPBarBase;

        public override void MouseDown(UIMouseEvent evt)
        {
            base.MouseDown(evt);
            DragStart(evt);
        }

        public override void MouseUp(UIMouseEvent evt)
        {
            base.MouseUp(evt);
            DragEnd(evt);
        }

        private void DragStart(UIMouseEvent evt)
        {
            offset = new Vector2(evt.MousePosition.X - Left.Pixels, evt.MousePosition.Y - Top.Pixels);
            dragging = true;
        }

        public override void OnInitialize()
        {
            MPBarBase = new UIImage(Request<Texture2D>("KeybrandsPlus/Assets/UI/MPBarBack"));
            MPBarBase.Left.Set(50, 0f);
            MPBarBase.Top.Set(200, 0f);
            MPBarBase.Width.Set(254, 0f);
            MPBarBase.Height.Set(26, 0f);

            Append(MPBarBase);
        }

        private void DragEnd(UIMouseEvent evt)
        {
            Vector2 end = evt.MousePosition;
            dragging = false;

            Left.Set(end.X - offset.X, 0f);
            Top.Set(end.Y - offset.Y, 0f);

            Recalculate();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!Main.LocalPlayer.GetModPlayer<MPPlayer>().MPBarVisible || Main.playerInventory)
                return;
            base.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            if (!Main.LocalPlayer.GetModPlayer<MPPlayer>().MPBarVisible || Main.playerInventory)
                return;

            int MPBarFrame = 0;
            int FillFrame = 0;

            Texture2D MPBar = Request<Texture2D>("KeybrandsPlus/Assets/UI/MPBarEmpty").Value;
            Texture2D MPBarFill = Request<Texture2D>("KeybrandsPlus/Assets/UI/MPBarFill").Value;
            Texture2D MPBarSeperator = Request<Texture2D>("KeybrandsPlus/Assets/UI/MPBarSeperator").Value;
            Texture2D MPBarText = Request<Texture2D>("KeybrandsPlus/Assets/UI/MPBarText").Value;
            Texture2D DeltaBar = Request<Texture2D>("KeybrandsPlus/Assets/UI/DeltaBarEmpty").Value;
            Texture2D DeltaBarFill = Request<Texture2D>("KeybrandsPlus/Assets/UI/DeltaBarFill").Value;

            var modPlayer = Main.LocalPlayer.GetModPlayer<MPPlayer>();

            int MPPercent = (int)Math.Round(MathHelper.Lerp(0f, 1f, modPlayer.CurrentMP / modPlayer.MaxMP2) * 200f);
            MPPercent = (int)MathHelper.Clamp(MPPercent, 0, 200);
            int DeltaPercent = (int)Math.Round(MathHelper.Lerp(0f, 1f, modPlayer.CurrentDelta / modPlayer.MaxDelta2) * 200f);
            DeltaPercent = (int)MathHelper.Clamp(DeltaPercent, 0, 200);

            if (modPlayer.MPCharge)
            {
                MPBarFrame = 1;
                FillFrame = 1;
                if (Main.GameUpdateCount % 60 <= 30) FillFrame = 2;
            }
            Rectangle MPBarRect = new Rectangle(0, (int)(MPBar.Height / 2 * MPBarFrame), MPBar.Width, MPBar.Height / 2);
            Rectangle MPBarTextRect = new Rectangle(0, (int)(MPBarText.Height / 2 * MPBarFrame), MPBarText.Width, MPBarText.Height / 2);
            Rectangle MPFillRect = new Rectangle(0, (int)(MPBarFill.Height / 3 * FillFrame), MPBarFill.Width, MPBarFill.Height / 3);

            Rectangle hitbox = MPBarBase.GetInnerDimensions().ToRectangle();
            Vector2 drawPos = hitbox.TopLeft() + new Vector2(8, 4);
            Main.spriteBatch.Draw(MPBar, drawPos, MPBarRect, Color.White);
            for (int i = 0; i < MPPercent; i++)
            {
                Main.spriteBatch.Draw(MPBarFill, drawPos + new Vector2(i, 0), MPFillRect, Color.White);
            }
            drawPos += new Vector2(-2, -2);
            Main.spriteBatch.Draw(MPBarSeperator, drawPos, null, Color.White);
            drawPos += new Vector2(206, 0);
            Main.spriteBatch.Draw(MPBarText, drawPos, MPBarTextRect, Color.White);
            drawPos += new Vector2(-204, 18);
            Main.spriteBatch.Draw(DeltaBar, drawPos, null, Color.White);
            for (int i = 0; i < DeltaPercent; i++)
            {
                Main.spriteBatch.Draw(DeltaBarFill, drawPos + new Vector2(i, 0), null, Color.White);
            }

            if (hitbox.Contains(new Point(Main.mouseX, Main.mouseY)))
            {
                if (modPlayer.MPCharge)
                    Main.spriteBatch.DrawString(FontAssets.MouseText.Value, modPlayer.MPChargeRate > 0f ? $"MP Charge: {Math.Ceiling(modPlayer.MPChargeTimer / modPlayer.MPChargeRate / 60f)}s ({modPlayer.CurrentDelta}/{modPlayer.MaxDelta2})" : $"MP Charge halted! ({modPlayer.CurrentDelta}/{modPlayer.MaxDelta2})", new Vector2(Main.mouseX + 20, Main.mouseY + 8), Color.White);
                else
                    Main.spriteBatch.DrawString(FontAssets.MouseText.Value, $"{modPlayer.CurrentMP}/{modPlayer.MaxMP2} ({modPlayer.CurrentDelta}/{modPlayer.MaxDelta2})", new Vector2(Main.mouseX + 20, Main.mouseY + 8), Color.White);
            }
            base.Update(gameTime);
            if (ContainsPoint(Main.MouseScreen))
            {
                Main.LocalPlayer.mouseInterface = true;
            }
            if (dragging)
            {
                Left.Set(Main.mouseX - offset.X, 0f);
                Top.Set(Main.mouseY - offset.Y, 0f);
                Recalculate();
            }
            Rectangle bounds = GetDimensions().ToRectangle();
            if (hitbox.Left < bounds.Left || hitbox.Right > bounds.Right || hitbox.Top < bounds.Top || hitbox.Bottom > bounds.Bottom)
            {
                Left.Pixels = Utils.Clamp(Left.Pixels, 0, bounds.Right - Width.Pixels);
                Top.Pixels = Utils.Clamp(Top.Pixels, 0, bounds.Bottom - Height.Pixels);
                Recalculate();
            }
        }
    }
}
