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
                if (amount >= 0)
                    pouch.storedMunny += (ulong)amount;
                else
                    pouch.storedMunny -= (ulong)-amount;
            }
        }
    }
}
