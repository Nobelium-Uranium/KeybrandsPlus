using KeybrandsPlus.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using static Terraria.ModLoader.ModContent;

namespace KeybrandsPlus.NPCs.TownNPC
{
    [AutoloadHead]
    class KeybrandMaster : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Keybrand Master");
            Main.npcFrameCount[npc.type] = 23;
            NPCID.Sets.ExtraFramesCount[npc.type] = 9;
            NPCID.Sets.AttackFrameCount[npc.type] = 4;
            NPCID.Sets.DangerDetectRange[npc.type] = 80;
            NPCID.Sets.AttackType[npc.type] = 3;
            NPCID.Sets.AttackTime[npc.type] = 10;
            NPCID.Sets.AttackAverageChance[npc.type] = 10;
        }

        public override void SetDefaults()
        {
            npc.townNPC = true;
            npc.friendly = true;
            npc.width = 18;
            npc.height = 40;
            npc.aiStyle = 7;
            npc.damage = 100;
            npc.defense = 50;
            npc.lifeMax = 25000;
            npc.DeathSound = SoundID.NPCDeath44;
            npc.knockBackResist = 0.1f;
            animationType = NPCID.Stylist;
            npc.buffImmune[BuffID.Wet] = true;
        }

        public override bool PreAI()
        {
            if (npc.life < npc.lifeMax)
                npc.life += 25;
            if (npc.life > npc.lifeMax)
                npc.life = npc.lifeMax;
            return base.PreAI();
        }

        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            Main.PlaySound(SoundID.NPCHit4, npc.Center);
            if (damage < 5000)
                damage = 0;
            else
                damage -= 5000;
            if (damage > 5000)
            {
                damage -= 5000;
                damage /= 10;
                damage += 5000;
            }
            if (damage > 10000)
                damage = 10000;
            crit = false;
            return false;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                for (int k = 0; k < 20; k++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 226, 2.5f * (float)hitDirection, -2.5f, 0, default(Color));
                }
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/MasterArm"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/MasterArm"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/MasterLeg"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/MasterLeg"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/MasterBody"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/MasterHead"), 1f);
            }
        }

        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            for (int k = 0; k < 255; k++)
            {
                Player player = Main.player[k];
                if (!player.active)
                {
                    continue;
                }
                foreach (Item i in player.inventory)
                {
                    if (i.GetGlobalItem<KeyItem>().IsKeybrand && !i.GetGlobalItem<KeyItem>().NoKeybrandMaster)
                        return true;
                }
                return false;
            }
            return false;
        }

        public override string TownNPCName()
        {
            return "Araxlaez";
        }

        public override string GetChat()
        { // chat.Add("");
            Player player = Main.LocalPlayer;
            bool Broke = true;
            foreach (Item i in player.inventory)
            {
                if (i.type == ItemType<Items.Currency.Munny>())
                    Broke = false;
            }
            WeightedRandom<string> chat = new WeightedRandom<string>();
            int Cyborg = NPC.FindFirstNPC(NPCID.Cyborg);
            if (Broke)
                chat.Add("Unfortunately for you, my services do not take coins. If you want to purchase my wares, earn some Munny first. They can be looted from most foes you slay.");
            else
                chat.Add("If you are ever in need of materials for keybrands, I am willing to oblige, if you can afford them.");
            if (Cyborg >= 0)
            {
                chat.Add("I admit that " + Main.npc[Cyborg].GivenName + "'s technology is impressive, but my own is far more exquisite.");
            }
            if (player.name == "Sora" || player.name == "Riku" || player.name == "Kairi" || player.name == "Roxas" || player.name == "Axel" || player.name == "Xion" || player.name == "Ventus" || player.name == "Aqua" || player.name == "Terra")
                chat.Add("Your name, it sounds familiar, but I can't think of why...");
            chat.Add("May your heart be your guiding key.");
            //if (Main.hardMode && !NPC.downedMechBossAny)
            //chat.Add("Be careful, as a result of defeating that cursed horror you fought down there, the Heartless here seem to be much more composed and dangerous.");
            //if (Main.hardMode && !Main.dayTime)
            //chat.Add("I must warn you, if you see a large red Heartless wielding a shield, I suggest that you keep your distance. Attempt this fight knowing that it may be your last.", 0.75);
            //if (NPC.downedMoonlord)
            //chat.Add("So, you've defeated the Lord of the Celestials... it seems to have awakened yet another new threat, the Nobodies.");
            /*if (NPC.downedBoss2 && !Main.hardMode)
                chat.Add("There seems to be a new evil looming about... ensure that you have the means to deal with them.");
            else if (!Main.hardMode)*/
            chat.Add("It appears that darkness has already touched this world, but I see no signs of Heartless... curious...");
            if (npc.life < npc.lifeMax)
                chat.Add("If you wish to cause me harm, then I am sorry to disappoint you, for this is merely a replica.", 2);
            chat.Add("Hm? You want the armor that I'm wearing? Unfortunately, armor plating of this caliber is too heavy and impractical for a mortal like you to wear, you'd be a pile of super-compressed flesh in seconds if you tried.", 0.5);
            chat.Add("Also try Shadows of Abaddon!", 0.1);
            chat.Add("Also try Kingdom Terrahearts!", 0.1);
            #region no
            if (npc.HasBuff(BuffID.Lovestruck) && !Main.dayTime)
            {
                chat.Add("Orchid mantises are cool.", 0.25);
                chat.Add("Orchid mantises are hot.", 0.05);
                chat.Add("Plant mantises are hot.", 0.01);
                chat.Add("Plant mantis waifus are hot.", 0.0025);
            }
            else
                chat.Add("Orchid mantises are cool.", 0.01);
            #endregion
            return chat;
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = Language.GetTextValue("LegacyInterface.28");
            button2 = "Check Alignment";
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            Player player = Main.LocalPlayer;
            string Comment = "This message should not appear.";
            string ExtraComment = "";
            string Affinity = "ERROR";
            if (player.GetModPlayer<Globals.KeyPlayer>().LightAlignment > player.GetModPlayer<Globals.KeyPlayer>().DarkAlignment)
                Affinity = "light";
            else if (player.GetModPlayer<Globals.KeyPlayer>().DarkAlignment > player.GetModPlayer<Globals.KeyPlayer>().LightAlignment)
                Affinity = "darkness";
            else
                Affinity = "duality";
            if (player.GetModPlayer<Globals.KeyPlayer>().LightAlignment == player.GetModPlayer<Globals.KeyPlayer>().DarkAlignment && player.GetModPlayer<Globals.KeyPlayer>().TotalAlignment >= 50)
                Comment = "Your heart is in perfect harmony, I commend your aptitude.";
            else if (player.GetModPlayer<Globals.KeyPlayer>().LightAlignment >= 50 && player.GetModPlayer<Globals.KeyPlayer>().DarkAlignment == 0)
                Comment = "Your heart is pure, there is not a hint of darkness in you.";
            else if (player.GetModPlayer<Globals.KeyPlayer>().DarkAlignment >= 50 && player.GetModPlayer<Globals.KeyPlayer>().LightAlignment == 0)
                Comment = "Your heart is pitch black with darkness.";
            else if ((player.GetModPlayer<Globals.KeyPlayer>().LightAlignment >= player.GetModPlayer<Globals.KeyPlayer>().DarkAlignment + 25 && player.GetModPlayer<Globals.KeyPlayer>().DarkAlignment != 0 && player.GetModPlayer<Globals.KeyPlayer>().LightAlignment >= 25) || player.GetModPlayer<Globals.KeyPlayer>().LightAlignment >= 25 && player.GetModPlayer<Globals.KeyPlayer>().DarkAlignment != 0)
                Comment = "Your heart is pure, but not entirely.";
            else if ((player.GetModPlayer<Globals.KeyPlayer>().DarkAlignment >= player.GetModPlayer<Globals.KeyPlayer>().LightAlignment + 25 && player.GetModPlayer<Globals.KeyPlayer>().LightAlignment != 0 && player.GetModPlayer<Globals.KeyPlayer>().DarkAlignment >= 25) || player.GetModPlayer<Globals.KeyPlayer>().DarkAlignment >= 25 && player.GetModPlayer<Globals.KeyPlayer>().LightAlignment != 0)
                Comment = "Your heart is succumbing to the darkness.";
            else if (player.GetModPlayer<Globals.KeyPlayer>().TotalAlignment >= 25)
                Comment = "Your heart is neutral. You are neither strongly afflicted by light nor darkness..";
            else
                Comment = "Your heart is neutral. You are neither afflicted by light nor darkness.";
            /*if (player.GetModPlayer<Globals.KeyPlayer>().TotalAlignment >= 100)
                ExtraComment = " It seems that as a result of the strength of your " + Affinity + ", the Heartless are getting stronger... I suggest you stay alert.";
            else*/ if (player.GetModPlayer<Globals.KeyPlayer>().TotalAlignment >= 75)
                ExtraComment = " At this rate, your " + Affinity + " will become the mightiest of all...";
            else if (player.GetModPlayer<Globals.KeyPlayer>().TotalAlignment >= 50)
                ExtraComment = " Your " + Affinity + " is getting quite strong...";
            else if (player.GetModPlayer<Globals.KeyPlayer>().TotalAlignment >= 25)
                ExtraComment = " Your " + Affinity + " shows promise...";
            if (firstButton)
                    shop = true;
                else
                {
                    Main.npcChatText = "Your alignment is at " + player.GetModPlayer<Globals.KeyPlayer>().LightAlignment + " light and " + player.GetModPlayer<Globals.KeyPlayer>().DarkAlignment + " dark. " + Comment + ExtraComment;
                }
        }

        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            shop.item[nextSlot].SetDefaults(ItemID.LesserHealingPotion);
            shop.item[nextSlot].shopCustomPrice = new int?(5);
            shop.item[nextSlot].shopSpecialCurrency = KeybrandsPlus.MunnyCost;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.HealingPotion);
            shop.item[nextSlot].shopCustomPrice = new int?(20);
            shop.item[nextSlot].shopSpecialCurrency = KeybrandsPlus.MunnyCost;
            nextSlot++;
            if (Main.hardMode)
            {
                shop.item[nextSlot].SetDefaults(ItemID.GreaterHealingPotion);
                shop.item[nextSlot].shopCustomPrice = new int?(50);
                shop.item[nextSlot].shopSpecialCurrency = KeybrandsPlus.MunnyCost;
                nextSlot++;
            }
            if (NPC.downedPlantBoss)
            {
                shop.item[nextSlot].SetDefaults(ItemID.SuperHealingPotion);
                shop.item[nextSlot].shopCustomPrice = new int?(75);
                shop.item[nextSlot].shopSpecialCurrency = KeybrandsPlus.MunnyCost;
                nextSlot++;
            }
            shop.item[nextSlot].SetDefaults(ItemID.LesserManaPotion);
            shop.item[nextSlot].shopCustomPrice = new int?(10);
            shop.item[nextSlot].shopSpecialCurrency = KeybrandsPlus.MunnyCost;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.ManaPotion);
            shop.item[nextSlot].shopCustomPrice = new int?(25);
            shop.item[nextSlot].shopSpecialCurrency = KeybrandsPlus.MunnyCost;
            nextSlot++;
            if (Main.hardMode)
            {
                shop.item[nextSlot].SetDefaults(ItemID.GreaterManaPotion);
                shop.item[nextSlot].shopCustomPrice = new int?(60);
                shop.item[nextSlot].shopSpecialCurrency = KeybrandsPlus.MunnyCost;
                nextSlot++;
            }
            if (NPC.downedPlantBoss)
            {
                shop.item[nextSlot].SetDefaults(ItemID.SuperManaPotion);
                shop.item[nextSlot].shopCustomPrice = new int?(85);
                shop.item[nextSlot].shopSpecialCurrency = KeybrandsPlus.MunnyCost;
                nextSlot++;
            }
            shop.item[nextSlot].SetDefaults(ItemType<Items.Consumables.MP.Ether>());
            shop.item[nextSlot].shopCustomPrice = new int?(15);
            shop.item[nextSlot].shopSpecialCurrency = KeybrandsPlus.MunnyCost;
            nextSlot++;
            if (Main.hardMode)
            {
                shop.item[nextSlot].SetDefaults(ItemType<Items.Consumables.MP.HiEther>());
                shop.item[nextSlot].shopCustomPrice = new int?(35);
                shop.item[nextSlot].shopSpecialCurrency = KeybrandsPlus.MunnyCost;
                nextSlot++;
            }
            if (NPC.downedPlantBoss)
            {
                shop.item[nextSlot].SetDefaults(ItemType<Items.Consumables.MP.MegaEther>());
                shop.item[nextSlot].shopCustomPrice = new int?(90);
                shop.item[nextSlot].shopSpecialCurrency = KeybrandsPlus.MunnyCost;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemType<Items.Consumables.MP.TurboEther>());
                shop.item[nextSlot].shopCustomPrice = new int?(125);
                shop.item[nextSlot].shopSpecialCurrency = KeybrandsPlus.MunnyCost;
                nextSlot++;
            }
            if (NPC.downedMoonlord)
            {
                shop.item[nextSlot].SetDefaults(ItemType<Items.Consumables.Elixir>());
                shop.item[nextSlot].shopCustomPrice = new int?(250);
                shop.item[nextSlot].shopSpecialCurrency = KeybrandsPlus.MunnyCost;
                nextSlot++;
            }
            if (Main.hardMode)
            {
                shop.item[nextSlot].SetDefaults(ItemType<Items.Consumables.Panacea>());
                shop.item[nextSlot].shopCustomPrice = new int?(30);
                shop.item[nextSlot].shopSpecialCurrency = KeybrandsPlus.MunnyCost;
                nextSlot++;
            }
            if (NPC.downedBoss3)
            {
                shop.item[nextSlot].SetDefaults(ItemType<Items.Consumables.DivinityPotion>());
                shop.item[nextSlot].shopCustomPrice = new int?(40);
                shop.item[nextSlot].shopSpecialCurrency = KeybrandsPlus.MunnyCost;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemType<Items.Consumables.ZenithStim>());
                shop.item[nextSlot].shopCustomPrice = new int?(40);
                shop.item[nextSlot].shopSpecialCurrency = KeybrandsPlus.MunnyCost;
                nextSlot++;
            }
            if (NPC.downedBoss2)
            {
                shop.item[nextSlot].SetDefaults(ItemType<Items.Accessories.Special.TreasureMagnet>());
                shop.item[nextSlot].shopCustomPrice = new int?(100);
                shop.item[nextSlot].shopSpecialCurrency = KeybrandsPlus.MunnyCost;
                nextSlot++;
            }
            if (NPC.downedMechBoss1 || NPC.downedMechBoss2 || NPC.downedMechBoss3)
            {
                shop.item[nextSlot].SetDefaults(ItemType<Items.Accessories.Special.TreasureMagnetPlus>());
                shop.item[nextSlot].shopCustomPrice = new int?(250);
                shop.item[nextSlot].shopSpecialCurrency = KeybrandsPlus.MunnyCost;
                nextSlot++;
            }
            if (NPC.downedMoonlord)
            {
                shop.item[nextSlot].SetDefaults(ItemType<Items.Accessories.Special.MasterTreasureMagnet>());
                shop.item[nextSlot].shopCustomPrice = new int?(1000);
                shop.item[nextSlot].shopSpecialCurrency = KeybrandsPlus.MunnyCost;
                nextSlot++;
            }
            shop.item[nextSlot].SetDefaults(ItemType<Items.Materials.UnchargedCrystal>());
            shop.item[nextSlot].shopCustomPrice = new int?(500);
            shop.item[nextSlot].shopSpecialCurrency = KeybrandsPlus.MunnyCost;
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemType<Items.Materials.KeybrandMold>());
            shop.item[nextSlot].shopCustomPrice = new int?(15);
            shop.item[nextSlot].shopSpecialCurrency = KeybrandsPlus.MunnyCost;
            nextSlot++;
            if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
            {
                shop.item[nextSlot].SetDefaults(ItemType<Items.Materials.BrokenHeroKeybrand>());
                shop.item[nextSlot].shopCustomPrice = new int?(175);
                shop.item[nextSlot].shopSpecialCurrency = KeybrandsPlus.MunnyCost;
                nextSlot++;
            }
            if (NPC.downedPlantBoss)
            {
                shop.item[nextSlot].SetDefaults(ItemType<Items.Materials.WarriorFragment>());
                shop.item[nextSlot].shopCustomPrice = new int?(10);
                shop.item[nextSlot].shopSpecialCurrency = KeybrandsPlus.MunnyCost;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemType<Items.Materials.GuardianFragment>());
                shop.item[nextSlot].shopCustomPrice = new int?(10);
                shop.item[nextSlot].shopSpecialCurrency = KeybrandsPlus.MunnyCost;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemType<Items.Materials.MysticFragment>());
                shop.item[nextSlot].shopCustomPrice = new int?(10);
                shop.item[nextSlot].shopSpecialCurrency = KeybrandsPlus.MunnyCost;
                nextSlot++;
            }
            if (NPC.downedMoonlord)
            {
                shop.item[nextSlot].SetDefaults(ItemType<Items.Materials.ZenithFragment>());
                shop.item[nextSlot].shopCustomPrice = new int?(120);
                shop.item[nextSlot].shopSpecialCurrency = KeybrandsPlus.MunnyCost;
                nextSlot++;/*
                shop.item[nextSlot].SetDefaults(ItemType<Items.Materials.UltimaBlueprint>());
                shop.item[nextSlot].shopCustomPrice = new int?(1000);
                shop.item[nextSlot].shopSpecialCurrency = KeybrandsPlus.MunnyCost;
                nextSlot++;*/
                if (KeybrandsPlus.SoALoaded)
                {
                    shop.item[nextSlot].SetDefaults(ItemType<Items.Weapons.Other.BleakMidnight>());
                    shop.item[nextSlot].shopCustomPrice = new int?(2000);
                    shop.item[nextSlot].shopSpecialCurrency = KeybrandsPlus.MunnyCost;
                    nextSlot++;
                }
                shop.item[nextSlot].SetDefaults(ItemType<Items.Armor.Developer.AvaliHelmet>());
                shop.item[nextSlot].shopCustomPrice = new int?(1500);
                shop.item[nextSlot].shopSpecialCurrency = KeybrandsPlus.MunnyCost;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemType<Items.Armor.Developer.AvaliShirt>());
                shop.item[nextSlot].shopCustomPrice = new int?(1500);
                shop.item[nextSlot].shopSpecialCurrency = KeybrandsPlus.MunnyCost;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemType<Items.Armor.Developer.AvaliPants>());
                shop.item[nextSlot].shopCustomPrice = new int?(1500);
                shop.item[nextSlot].shopSpecialCurrency = KeybrandsPlus.MunnyCost;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemType<Items.Accessories.Wings.AvaliGlider>());
                shop.item[nextSlot].shopCustomPrice = new int?(3000);
                shop.item[nextSlot].shopSpecialCurrency = KeybrandsPlus.MunnyCost;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemType<Items.Other.FullbrightDye>());
                shop.item[nextSlot].shopCustomPrice = new int?(1000);
                shop.item[nextSlot].shopSpecialCurrency = KeybrandsPlus.MunnyCost;
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemType<Items.Weapons.Developer.Chimera>());
                shop.item[nextSlot].shopCustomPrice = new int?(5000);
                shop.item[nextSlot].shopSpecialCurrency = KeybrandsPlus.MunnyCost;
                nextSlot++;
            }
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            if (Main.hardMode)
            {
                damage = 75;
                if (NPC.downedPlantBoss)
                {
                    damage = 100;
                    if (NPC.downedMoonlord)
                    {
                        damage = 500;
                    }
                }
            }
            else
            {
                damage = 50;
            }
            knockback = 3f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 7;
            randExtraCooldown = 7;
        }

        public override void DrawTownAttackSwing(ref Texture2D item, ref int itemSize, ref float scale, ref Vector2 offset)
        {
            item = Main.itemTexture[ItemType<Items.Weapons.Developer.Chimera>()];
            itemSize = 64;
            scale = 0.7f;
        }

        public override void TownNPCAttackSwing(ref int itemWidth, ref int itemHeight)
        {
            itemWidth = 64;
            itemHeight = 64;
        }
    }
}
