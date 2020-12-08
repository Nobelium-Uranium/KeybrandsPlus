﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace KeybrandsPlus.Helpers
{
    internal class GlowmaskHelper : GlobalItem
    {
        public Texture2D glowTexture = null;
        public int glowOffsetY = 0;
        public int glowOffsetX = 0;

        public override bool InstancePerEntity => true;

        public override bool CloneNewInstances => true;
    }

    internal class GlowmaskHelperPlayer : ModPlayer
    {
        public static readonly PlayerLayer Glowmask = new PlayerLayer("KeybrandsPlus", "Glowmask", PlayerLayer.HeldItem, delegate (PlayerDrawInfo drawInfo)
        {
            if (drawInfo.shadow != 0f)
                return;

            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = ModContent.GetInstance<KeybrandsPlus>();

            if (!drawPlayer.HeldItem.IsAir)
            {
                Item item = drawPlayer.HeldItem;
                Texture2D texture = item.GetGlobalItem<GlowmaskHelper>().glowTexture;
                Vector2 vecZero = Vector2.Zero;

                if (drawPlayer.itemAnimation <= 0 || texture == null)
                    return;

                Vector2 position = drawInfo.itemLocation;

                if (item.useStyle == 5)
                {
                    bool staff = Item.staff[item.type];
                    if (staff)
                    {
                        float num104 = drawPlayer.itemRotation + 0.785f * drawPlayer.direction; //TODO rename
                        int textureWidth = 0; //TODO rename
                        int num = 0; //TODO rename
                        Vector2 zero3 = new Vector2(0f, Main.itemTexture[item.type].Height); //TODO rename

                        if (drawPlayer.gravDir == -1f)
                        {
                            //TODO explain this
                            if (drawPlayer.direction == -1)
                            {
                                num104 += 1.57f; //TODO comment meaning
                                zero3 = new Vector2(Main.itemTexture[item.type].Width, 0f);
                                textureWidth -= Main.itemTexture[item.type].Width;
                            }
                            else
                            {
                                num104 -= 1.57f;
                                zero3 = Vector2.Zero;
                            }
                        }
                        //TODO explain
                        else if (drawPlayer.direction == -1)
                        {
                            zero3 = new Vector2(Main.itemTexture[item.type].Width, Main.itemTexture[item.type].Height);
                            textureWidth -= Main.itemTexture[item.type].Width;
                        }

                        DrawData value = new DrawData
                        (
                            texture,
                            new Vector2((int)(position.X - Main.screenPosition.X + zero3.X + textureWidth), (int)(position.Y - Main.screenPosition.Y + num)),
                            new Rectangle?(new Rectangle(0, 0, Main.itemTexture[item.type].Width, Main.itemTexture[item.type].Height)),
                            Color.White,
                            num104,
                            zero3,
                            item.scale,
                            drawInfo.spriteEffects,
                            0
                        );

                        Main.playerDrawData.Add(value);
                    }
                    else
                    {
                        Vector2 halfTexture = new Vector2(Main.itemTexture[item.type].Width / 2, Main.itemTexture[item.type].Height / 2); //TODO rename

                        Vector2 vector11 = new Vector2(10, texture.Height / 2); //TODO rename

                        GlowmaskHelper glowmaskHelper = item.GetGlobalItem<GlowmaskHelper>();

                        //TODO explain below
                        if (glowmaskHelper.glowOffsetX != 0)
                            vector11.X = glowmaskHelper.glowOffsetX;

                        vector11.Y += glowmaskHelper.glowOffsetY * drawPlayer.gravDir;
                        int num107 = (int)vector11.X;
                        halfTexture.Y = vector11.Y;
                        Vector2 origin5 = new Vector2(-num107, Main.itemTexture[item.type].Height / 2);

                        if (drawPlayer.direction == -1)
                            origin5 = new Vector2(Main.itemTexture[item.type].Width + num107, Main.itemTexture[item.type].Height / 2);

                        DrawData value = new DrawData(texture, new Vector2((int)(position.X - Main.screenPosition.X + halfTexture.X), (int)(position.Y - Main.screenPosition.Y + halfTexture.Y)), new Microsoft.Xna.Framework.Rectangle?(new Rectangle(0, 0, Main.itemTexture[item.type].Width, Main.itemTexture[item.type].Height)), Color.White, drawPlayer.itemRotation, origin5, item.scale, drawInfo.spriteEffects, 0);
                        Main.playerDrawData.Add(value);
                    }
                }
                else
                {
                    DrawData value = new DrawData
                    (
                        texture,
                        new Vector2((int)(position.X - Main.screenPosition.X),
                        (int)(position.Y - Main.screenPosition.Y)), new Rectangle?(new Rectangle(0, 0, texture.Width, texture.Height)),
                        Color.White,
                        drawPlayer.itemRotation,
                         new Vector2(texture.Width * 0.5f - texture.Width * 0.5f * drawPlayer.direction, drawPlayer.gravDir == -1 ? 0f : texture.Height),
                        item.scale,
                        drawInfo.spriteEffects,
                        0
                    );

                    Main.playerDrawData.Add(value);
                }
            }
        });

        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
            int itemLayer = layers.FindIndex(PlayerLayer => PlayerLayer.Name.Equals("HeldItem"));

            if (itemLayer != -1)
            {
                Glowmask.visible = true;
                layers.Insert(itemLayer + 1, Glowmask);
            }
        }
    }
}