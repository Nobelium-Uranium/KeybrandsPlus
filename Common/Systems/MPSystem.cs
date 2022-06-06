using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.UI;
using static Terraria.ModLoader.ModContent;

namespace KeybrandsPlus.Common.Systems
{
    internal class MPSystem : ModSystem
    {
        private UserInterface _MPUserInterface;
        internal MPUI MPUI;
        public override void Load()
        {
            if (!Main.dedServ)
            {
                MPUI = new MPUI();
                MPUI.Activate();
                _MPUserInterface = new UserInterface();
                _MPUserInterface.SetState(MPUI);
            }
        }
        public override void UpdateUI(GameTime gameTime)
        {
            _MPUserInterface?.Update(gameTime);
        }
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int ResourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            if (ResourceBarIndex != -1)
            {
                layers.Insert(ResourceBarIndex, new LegacyGameInterfaceLayer(
                    "KeybrandsPlus: MP Bar",
                    delegate
                    {
                        _MPUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
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

        public float MPChargeTimer = 1200;
        public float MPChargeTimerMax = 1200;
        public float MPChargeRate = 1f;

        public static readonly Color HealMP = Color.DodgerBlue;

        public override void Initialize()
        {
            MaxMP = DefaultMaxMP;
            MaxMP2 = MaxMP;
            MaxDelta = DefaultMaxMP;
            MaxDelta2 = MaxDelta;
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
            else if (Main.playerInventory)
                MPBarVisible = false;
            else
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
                    for (int i = 0; i < (int)Math.Floor((double)(CurrentDelta / MaxDelta2)); i++)
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
                    CurrentMP = 0;
                    MPChargeTimerMax = MPChargeTimer = 1200;
                    MPCharge = true;
                }
            }
            else
            {
                CurrentMP = 0;
                if (CurrentDelta >= MaxDelta2)
                {
                    int RestoreMP = 0;
                    for (int i = 0; i < (int)Math.Floor((double)(CurrentDelta / MaxDelta2)); i++)
                    {
                        RestoreMP += 60;
                        CurrentDelta -= MaxDelta2;
                    }
                    MPChargeTimer -= RestoreMP;
                    if (Main.myPlayer == Player.whoAmI)
                        CombatText.NewText(Player.getRect(), HealMP, $"-{RestoreMP / 60}s");
                }
                if (MPChargeRate > 3f)
                    MPChargeRate = 3f;
                else if (MPChargeRate < 0f)
                    MPChargeRate = 0f;
                MPChargeTimer -= MPChargeRate;
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

    internal class MPUI : UIState
    {
        ClientConfig config = GetInstance<ClientConfig>();

        private Vector2 offset;
        public bool dragging;

        private UIImage MPBarBase;

        public override void OnInitialize()
        {
            Left.Pixels = config.MPBarPosX;
            Top.Pixels = config.MPBarPosY;
            Width.Set(252, 0f);
            Height.Set(26, 0f);

            MPBarBase = new UIImage(Request<Texture2D>("KeybrandsPlus/Assets/UI/MPBarBack"));
            MPBarBase.Left.Set(0, 0f);
            MPBarBase.Top.Set(0, 0f);
            MPBarBase.Width.Set(252, 0f);
            MPBarBase.Height.Set(26, 0f);

            Append(MPBarBase);
        }

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
            if (!Main.LocalPlayer.GetModPlayer<MPPlayer>().MPBarVisible)
                return;
            base.Draw(spriteBatch);

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
            if (modPlayer.MPCharge)
                MPPercent = (int)Math.Round(MathHelper.Lerp(0f, 1f, 1f - modPlayer.MPChargeTimer / modPlayer.MPChargeTimerMax) * 200f);
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
            Vector2 drawPos = hitbox.TopLeft() + new Vector2(6, 4);
            spriteBatch.Draw(MPBar, drawPos, MPBarRect, Color.White);
            for (int i = 0; i < MPPercent; i++)
            {
                spriteBatch.Draw(MPBarFill, drawPos + new Vector2(i, 0), MPFillRect, Color.White);
            }
            drawPos += new Vector2(204, -2);
            spriteBatch.Draw(MPBarText, drawPos, MPBarTextRect, Color.White);
            drawPos += new Vector2(-204, 18);
            spriteBatch.Draw(DeltaBar, drawPos, null, Color.White);
            for (int i = 0; i < DeltaPercent; i++)
            {
                spriteBatch.Draw(DeltaBarFill, drawPos + new Vector2(i, 0), null, Color.White);
            }
            drawPos += new Vector2(-2, -18);
            spriteBatch.Draw(MPBarSeperator, drawPos, null, Color.White);

            if (hitbox.Contains(new Point(Main.mouseX, Main.mouseY)))
            {
                if (modPlayer.MPCharge)
                    spriteBatch.DrawString(FontAssets.MouseText.Value, modPlayer.MPChargeRate > 0f ? $"MP Charge: {Math.Ceiling(modPlayer.MPChargeTimer / modPlayer.MPChargeRate / 60f)}s ({modPlayer.CurrentDelta}/{modPlayer.MaxDelta2})" : $"MP Charge halted! ({modPlayer.CurrentDelta}/{modPlayer.MaxDelta2})", new Vector2(Main.mouseX + 20, Main.mouseY + 8), Color.White);
                else
                    spriteBatch.DrawString(FontAssets.MouseText.Value, $"MP: {modPlayer.CurrentMP}/{modPlayer.MaxMP2} ({modPlayer.CurrentDelta}/{modPlayer.MaxDelta2})", new Vector2(Main.mouseX + 20, Main.mouseY + 8), Color.White);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (!Main.LocalPlayer.GetModPlayer<MPPlayer>().MPBarVisible)
                return;
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
                config.MPBarPosX = (int)Left.Pixels;
                config.MPBarPosY = (int)Top.Pixels;
            }
            else
            {
                if ((int)Left.Pixels != config.MPBarPosX)
                    Left.Set(config.MPBarPosX, 0f);
                if ((int)Top.Pixels != config.MPBarPosY)
                    Top.Set(config.MPBarPosY, 0f);
            }
            Rectangle hitbox = MPBarBase.GetInnerDimensions().ToRectangle();
            if (hitbox.X < 0)
            {
                Left.Pixels = 0;
                Recalculate();
            }
            if (hitbox.X > Main.screenWidth - hitbox.Width)
            {
                Left.Pixels = Main.screenWidth - hitbox.Width;
                Recalculate();
            }
            if (hitbox.Y < 0)
            {
                Top.Pixels = 0;
                Recalculate();
            }
            if (hitbox.Y > Main.screenHeight - hitbox.Height)
            {
                Top.Pixels = Main.screenHeight - hitbox.Height;
                Recalculate();
            }
        }
    }
}
