using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System.Collections.Generic;
using System.IO;

namespace KeybrandsPlus.Globals
{
    class KeyWorld : ModWorld
    {
        public static bool downedDemonTide;

        public override void Initialize()
        {
            downedDemonTide = false;
        }

        public override TagCompound Save()
        {
            var downed = new List<string>();
            if (downedDemonTide)
                downed.Add("DemonTide");
            return new TagCompound
            {
                ["downed"] = downed,
            };
        }

        public override void Load(TagCompound tag)
        {
            var downed = tag.GetList<string>("downed");
            downedDemonTide = downed.Contains("DemonTide");
        }

        public override void LoadLegacy(BinaryReader reader)
        {
            int loadVersion = reader.ReadInt32();
            if (loadVersion == 0)
            {
                BitsByte flags = reader.ReadByte();
                downedDemonTide = flags[0];
            }
            else
                mod.Logger.WarnFormat("KeybrandsPlus: Unknown loadVersion: {0}", loadVersion);
        }

        public override void NetSend(BinaryWriter writer)
        {
            #region tip
            /*
			Remember that Bytes/BitsByte only have 8 entries. If you have more than 8 flags you want to sync, use multiple BitsByte:

				This is wrong:
			flags[8] = downed9thBoss; // an index of 8 is nonsense. 

				This is correct:
			flags[7] = downed8thBoss;
			writer.Write(flags);

			BitsByte flags2 = new BitsByte(); // create another BitsByte
			flags2[0] = downed9thBoss; // start again from 0
			// up to 7 more flags here
			writer.Write(flags2); // write this byte
			*/
            #endregion
            var flags = new BitsByte();
            flags[0] = downedDemonTide;
            writer.Write(flags);
        }

        public override void NetReceive(BinaryReader reader)
        {
            #region tip
            // As mentioned in NetSend, BitBytes can contain 8 values. If you have more, be sure to read the additional data:
            // BitsByte flags2 = reader.ReadByte();
            // downed9thBoss = flags[0];
            #endregion
            BitsByte flags = reader.ReadByte();
            downedDemonTide = flags[0];
        }

        public override void PostWorldGen()
        {
            for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest != null && Main.tile[chest.x, chest.y].type == TileID.Containers)
                {
                    for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                    {
                        if (chest.item[inventoryIndex].type == 0)
                        {
                            if (Main.tile[chest.x, chest.y].frameX == 2 * 36)
                            {
                                /*if (Main.rand.NextBool(50))
                                    chest.item[inventoryIndex].SetDefaults(ModContent.ItemType<Items.Synthesis.Other.ZenithitePlus>());
                                else*/ if (Main.rand.NextBool(5))
                                    chest.item[inventoryIndex].SetDefaults(ModContent.ItemType<Items.Weapons.AbyssalTide>());
                            }
                            /*else if (Main.rand.NextBool(25))
                                chest.item[inventoryIndex].SetDefaults(ModContent.ItemType<Items.Synthesis.Other.ZenithitePlus>());*/
                            break;
                        }
                    }
                    
                }
            }
        }
    }
}
