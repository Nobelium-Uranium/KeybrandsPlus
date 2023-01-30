using KeybrandsPlus.Content.Items.Currency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace KeybrandsPlus.Common.Globals
{
    public class KeyPlayer : ModPlayer
    {
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

        public int CountMunny()
        {
            int amount = 0;
            MunnyPouch pouch;
            Item[] vault;
            for (int i = 0; i < 50; i++)
            {
                Item item = Player.inventory[i];
                if (item.ModItem is MunnyPouch)
                {
                    pouch = item.ModItem as MunnyPouch;
                    amount += pouch.storedMunny;
                }
                else if (item.type == ModContent.ItemType<Munny>())
                    amount += item.stack;
            }
            vault = Player.bank.item;
            for (int i = 0; i < vault.Length; i++)
            {
                Item item = vault[i];
                if (item.ModItem is MunnyPouch)
                {
                    pouch = item.ModItem as MunnyPouch;
                    amount += pouch.storedMunny;
                }
                else if (item.type == ModContent.ItemType<Munny>())
                    amount += item.stack;
            }
            vault = Player.bank2.item;
            for (int i = 0; i < vault.Length; i++)
            {
                Item item = vault[i];
                if (item.ModItem is MunnyPouch)
                {
                    pouch = item.ModItem as MunnyPouch;
                    amount += pouch.storedMunny;
                }
                else if (item.type == ModContent.ItemType<Munny>())
                    amount += item.stack;
            }
            vault = Player.bank3.item;
            for (int i = 0; i < vault.Length; i++)
            {
                Item item = vault[i];
                if (item.ModItem is MunnyPouch)
                {
                    pouch = item.ModItem as MunnyPouch;
                    amount += pouch.storedMunny;
                }
                else if (item.type == ModContent.ItemType<Munny>())
                    amount += item.stack;
            }
            if (Player.IsVoidVaultEnabled)
            {
                vault = Player.bank4.item;
                for (int i = 0; i < vault.Length; i++)
                {
                    Item item = vault[i];
                    if (item.ModItem is MunnyPouch)
                    {
                        pouch = item.ModItem as MunnyPouch;
                        amount += pouch.storedMunny;
                    }
                    else if (item.type == ModContent.ItemType<Munny>())
                        amount += item.stack;
                }
            }
            return amount;
        }
    }
}
