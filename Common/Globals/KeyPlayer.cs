using KeybrandsPlus.Common.Helpers;
using KeybrandsPlus.Content.Items.Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace KeybrandsPlus.Common.Globals
{
    public class KeyPlayer : ModPlayer
    {

        public long recentMunny;
        public int recentMunnyCounter;

        public bool MunnyMagnet;

        public override void ResetEffects()
        {
            MunnyMagnet = false;
        }

        public override void UpdateDead()
        {
            ResetEffects();
        }

        public override void PostUpdate()
        {
            if (recentMunnyCounter > 0)
                recentMunnyCounter--;
            else
                recentMunny = 0;
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (target.lifeMax > 5 && !target.CountsAsACritter && !target.friendly && !target.immortal && MunnyMagnet && crit && Main.rand.NextBool(3))
            {
                int amount;
                if (damage > target.lifeMax)
                    amount = (int)Math.Ceiling(target.lifeMax / 25f);
                else
                    amount = (int)Math.Ceiling(damage / 25f);
                if (target.boss || NPCID.Sets.ShouldBeCountedAsBoss[target.type] || NPCID.Sets.BossHeadTextures[target.type] != -1)
                    amount = (int)Math.Ceiling(amount * .75f);
                if (amount > 0)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        Item.NewItem(Player.GetSource_OnHit(target), target.getRect(), ModContent.ItemType<Munny>(), amount);
                }
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (target.lifeMax > 5 && !target.CountsAsACritter && !target.friendly && !target.immortal && MunnyMagnet && crit && Main.rand.NextBool(3))
            {
                int amount;
                if (damage > target.lifeMax)
                    amount = (int)Math.Ceiling(target.lifeMax / 10f);
                else
                    amount = (int)Math.Ceiling(damage / 10f);
                if (amount > 0)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        Item.NewItem(Player.GetSource_OnHit(target), target.getRect(), ModContent.ItemType<Munny>(), amount);
                }
            }
        }

        public void AddRecentMunny(int amount)
        {
            if (recentMunnyCounter <= 120)
                recentMunny = 0;
            recentMunny += amount;
            recentMunnyCounter = 300;
        }

        public bool HasMunnyPouch(out Item pouch)
        {
            pouch = null;
            for (int i = 0; i < 50; i++)
            {
                Item item = Player.inventory[i];
                if (item.ModItem is MunnyPouch)
                {
                    pouch = item;
                    return true;
                }
            }
            return false;
        }

        public void MunnySavings(int amount)
        {
            if (HasMunnyPouch(out Item item))
            {
                MunnyPouch pouch = item.ModItem as MunnyPouch;
                pouch.storedMunny += amount;
            }
        }

        public long CountMunny(bool countPouches = true, bool countLoose = false)
        {
            long amount = 0;
            MunnyPouch pouch;
            Item[] vault;
            if (Main.mouseItem.ModItem is MunnyPouch && countPouches)
            {
                pouch = Main.mouseItem.ModItem as MunnyPouch;
                amount += pouch.storedMunny;
            }
            else if (Main.mouseItem.type == ModContent.ItemType<Munny>() && countLoose)
                amount += Main.mouseItem.stack;
            for (int i = 0; i < 50; i++)
            {
                Item item = Player.inventory[i];
                if (item.ModItem is MunnyPouch && countPouches)
                {
                    pouch = item.ModItem as MunnyPouch;
                    amount += pouch.storedMunny;
                }
                else if (item.type == ModContent.ItemType<Munny>() && countLoose)
                    amount += item.stack;
            }
            vault = Player.bank.item;
            for (int i = 0; i < vault.Length; i++)
            {
                Item item = vault[i];
                if (item.ModItem is MunnyPouch && countPouches)
                {
                    pouch = item.ModItem as MunnyPouch;
                    amount += pouch.storedMunny;
                }
                else if (item.type == ModContent.ItemType<Munny>() && countLoose)
                    amount += item.stack;
            }
            vault = Player.bank2.item;
            for (int i = 0; i < vault.Length; i++)
            {
                Item item = vault[i];
                if (item.ModItem is MunnyPouch && countPouches)
                {
                    pouch = item.ModItem as MunnyPouch;
                    amount += pouch.storedMunny;
                }
                else if (item.type == ModContent.ItemType<Munny>() && countLoose)
                    amount += item.stack;
            }
            vault = Player.bank3.item;
            for (int i = 0; i < vault.Length; i++)
            {
                Item item = vault[i];
                if (item.ModItem is MunnyPouch && countPouches)
                {
                    pouch = item.ModItem as MunnyPouch;
                    amount += pouch.storedMunny;
                }
                else if (item.type == ModContent.ItemType<Munny>() && countLoose)
                    amount += item.stack;
            }
            if (Player.IsVoidVaultEnabled)
            {
                vault = Player.bank4.item;
                for (int i = 0; i < vault.Length; i++)
                {
                    Item item = vault[i];
                    if (item.ModItem is MunnyPouch && countPouches)
                    {
                        pouch = item.ModItem as MunnyPouch;
                        amount += pouch.storedMunny;
                    }
                    else if (item.type == ModContent.ItemType<Munny>() && countLoose)
                        amount += item.stack;
                }
            }
            return amount;
        }
    }
}
