using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using Terraria;
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
            NPCID.Sets.DangerDetectRange[npc.type] = 100;
            NPCID.Sets.AttackType[npc.type] = 3;
            NPCID.Sets.AttackTime[npc.type] = 20;
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
            npc.lifeMax = 3000;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath44;
            npc.knockBackResist = 0.1f;
            animationType = NPCID.Stylist;
            npc.buffImmune[BuffID.Wet] = true;
        }

        public override void AI()
        {
            #region no
            if (npc.HasBuff(BuffID.Lovestruck) && !Main.dayTime)
            {
                npc.buffImmune[BuffID.Wet] = false;
                npc.AddBuff(BuffID.Wet, 2);
                npc.buffImmune[BuffID.Wet] = true;
            }
            #endregion
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            int num = npc.life > 0 ? 1 : 5;
            for (int k = 0; k < num; k++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 226);
            }
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
                Projectile.NewProjectileDirect(npc.position, npc.velocity, mod.ProjectileType("MasterExplosion"), 9999999, 15f, 0);
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
                return true;
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
            WeightedRandom<string> chat = new WeightedRandom<string>();
            int Dryad = NPC.FindFirstNPC(NPCID.Dryad);
            int Cyborg = NPC.FindFirstNPC(NPCID.Cyborg);
            int Angler = NPC.FindFirstNPC(NPCID.Angler);
            if (!npc.homeless && player.name != "Cheems")
            {
                if (Dryad >= 0 && !Main.bloodMoon)
                {
                    chat.Add("Is there a practical reason " + Main.npc[Dryad].GivenName + " dresses so skimply? I get that she's one with nature and all, but that doesn't mean she should be so... revealing.");
                }
                if (Cyborg >= 0)
                {
                    chat.Add(Main.npc[Cyborg].GivenName + " is cool I guess, but my tech is far more advanced.");
                    chat.Add(Main.npc[Cyborg].GivenName + " and I get along well, he lets me reverse engineer his tech too!", 0.5);
                    chat.Add(Main.npc[Cyborg].GivenName + " and I get along well, he lets me reverse engineer his tech too! ...No, not in that way, gross.", 0.5);
                }
                if (Angler >= 0)
                {
                    chat.Add("I heard from " + Main.npc[Angler].GivenName + " that the shells bear secrets... not sure what that means...");
                    chat.Add(Main.npc[Angler].GivenName + " may come off as a bit of a prick, but honestly I think he's just misunderstood.");
                }
                if (player.name == "Sora" || player.name == "Riku" || player.name == "Kairi" || player.name == "Roxas" || player.name == "Axel" || player.name == "Xion" || player.name == "Ventus" || player.name == "Aqua" || player.name == "Terra")
                    chat.Add("Your name, it sounds familiar, but I can't think of why...");
                chat.Add("May your heart be your guiding key.");
                chat.Add("May your key be your guiding heart.", 0.5);
                //if (Main.hardMode && !NPC.downedMechBossAny)
                    //chat.Add("The heartless here seem to be much more composed and dangerous.");
                //if (Main.hardMode && !Main.dayTime)
                    //chat.Add("Hey, if you see a big red lanky heartless wielding a shield with a face... well, just leave it alone alright? It won't hurt you if you don't hurt it, and believe me when I say it'll hurt you. A LOT.", 0.75);
                //if (NPC.downedMoonlord)
                    //chat.Add("I believe it's time to prepare for the worst, it seems the Lord of the Celestials was the heartless' main enemy, and now they've found access to the world's heart...");
                /*if (NPC.downedBoss2 && !Main.hardMode)
                    chat.Add("There seems to be a new evil looming about... Make sure you've got the tools to deal with them.");
                else if (!Main.hardMode)*/
                    chat.Add("Hmm, so darkness has already touched this world, but I see no signs of heartless... curious...");
                chat.Add("If you are ever in need of materials for keybrands, I will happily oblige, as long as you have the Munny for it.");
                chat.Add("It is I... " + npc.GivenName + ", the seeker of light!");
                chat.Add("For the last time, it's not a scythe! Oh, sorry... did you need something?", 0.5);
                chat.Add("Shoutouts to Dan Yami, he's pretty cool. Hey, you should try Shadows of Abaddon sometime.", 0.25);
                chat.Add("Shoutouts to Pronoespro, show him your support by checking out Terrahearts Kingdom. It may be small (and kinda buggy), but it reinvigorated my motivation.", 0.25);
                chat.Add("Have you tried eter... eta... etr... masomode?", 0.075);
                chat.Add("Have you tried eter... eta... etr... sempiternity mode?", 0.1);
                chat.Add("Your meme, it's useful, I'll take it.", 0.05);
                #region no
                if (npc.HasBuff(BuffID.Lovestruck) && !Main.dayTime)
                {
                    chat.Add("Ugh, is it hot in here or is it just me...?", 1.5);
                    chat.Add("Orchid mantises are cool.", 0.25);
                    chat.Add("Orchid mantises are hot.", 0.05);
                    chat.Add("Plant mantises are hot.", 0.025);
                    chat.Add("Plant mantis waifus are hot.", 0.01);
                }
                else
                    chat.Add("Orchid mantises are cool.", 0.01);
                #endregion
            }
            else if (!npc.homeless)
                chat.Add("No.");
            else
            {
                chat.Add("I'm pretty sure I need a place to stay, even for me it's dangerous to be out in the open.");
                chat.Add("Got any free real estate? That would be greatly appreciated, thanks.");
            }
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
            string Comment = "This message should not appear, you should definitely tell the mod developer if it does!";
            string ExtraComment = "";
            string Affinity = "ERROR";
            if (player.GetModPlayer<Globals.KeyPlayer>().LightAlignment > player.GetModPlayer<Globals.KeyPlayer>().DarkAlignment)
                Affinity = "light";
            else if (player.GetModPlayer<Globals.KeyPlayer>().DarkAlignment > player.GetModPlayer<Globals.KeyPlayer>().LightAlignment)
                Affinity = "darkness";
            else
                Affinity = "duality";
            if (player.GetModPlayer<Globals.KeyPlayer>().LightAlignment == player.GetModPlayer<Globals.KeyPlayer>().DarkAlignment && player.GetModPlayer<Globals.KeyPlayer>().TotalAlignment >= 50)
                Comment = "Your heart is in perfect harmony! I commend your aptitude, though do be sure to keep your affinity aligned.";
            else if (player.GetModPlayer<Globals.KeyPlayer>().LightAlignment >= 50 && player.GetModPlayer<Globals.KeyPlayer>().DarkAlignment == 0)
                Comment = "Your heart is pure, there's not a hint of darkness in you! Still, you must remain vigilant if you want to keep it that way.";
            else if (player.GetModPlayer<Globals.KeyPlayer>().DarkAlignment >= 50 && player.GetModPlayer<Globals.KeyPlayer>().LightAlignment == 0)
                Comment = "Your heart is pitch black with darkness... Are you even human anymore? Well, I suppose if you can keep that darkness in check, you'll be fine.";
            else if ((player.GetModPlayer<Globals.KeyPlayer>().LightAlignment >= player.GetModPlayer<Globals.KeyPlayer>().DarkAlignment + 25 && player.GetModPlayer<Globals.KeyPlayer>().DarkAlignment != 0 && player.GetModPlayer<Globals.KeyPlayer>().LightAlignment >= 25) || player.GetModPlayer<Globals.KeyPlayer>().LightAlignment >= 25 && player.GetModPlayer<Globals.KeyPlayer>().DarkAlignment != 0)
                Comment = "Your heart is pure, but not entirely. Even the smallest amount of darkness could mean terrible things...";
            else if ((player.GetModPlayer<Globals.KeyPlayer>().DarkAlignment >= player.GetModPlayer<Globals.KeyPlayer>().LightAlignment + 25 && player.GetModPlayer<Globals.KeyPlayer>().LightAlignment != 0 && player.GetModPlayer<Globals.KeyPlayer>().DarkAlignment >= 25) || player.GetModPlayer<Globals.KeyPlayer>().DarkAlignment >= 25 && player.GetModPlayer<Globals.KeyPlayer>().LightAlignment != 0)
                Comment = "Your heart is succumbing to the darkness... Still, there is hope for you yet, as long as you don't let what little light you have in you out.";
            else if (player.GetModPlayer<Globals.KeyPlayer>().TotalAlignment >= 25)
                Comment = "Your heart is neutral. You're neither strongly afflicted by light nor darkness. I suppose using both light and darkness is a good move.";
            else
                Comment = "Your heart is neutral. You're neither afflicted by light nor darkness.";
            /*if (player.GetModPlayer<Globals.KeyPlayer>().TotalAlignment >= 100)
                ExtraComment = " It seems that as a result of the strength of your " + Affinity + ", the heartless are getting stronger... I suggest you stay alert.";
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
            shop.item[nextSlot].SetDefaults(ItemType<Items.Materials.KeybrandMold>());
            shop.item[nextSlot].shopCustomPrice = new int?(15);
            shop.item[nextSlot].shopSpecialCurrency = KeybrandsPlus.MunnyCost;
            nextSlot++;
            /*
            if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
            {
                shop.item[nextSlot].SetDefaults(ItemType<Items.Materials.BrokenHeroKeybrand>());
                shop.item[nextSlot].shopCustomPrice = new int?(50);
                shop.item[nextSlot].shopSpecialCurrency = KeybrandsPlus.MunnyCost;
                nextSlot++;
            }*/
            if (NPC.downedPlantBoss)
            {
                if (KeybrandsPlus.SoALoaded)
                {
                    shop.item[nextSlot].SetDefaults(ItemType<Items.Weapons.Other.BleakMidnight>());
                    shop.item[nextSlot].shopCustomPrice = new int?(2000);
                    shop.item[nextSlot].shopSpecialCurrency = KeybrandsPlus.MunnyCost;
                    nextSlot++;
                }
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
                shop.item[nextSlot].SetDefaults(ItemType<Items.Weapons.Developer.Chimera>());
                shop.item[nextSlot].shopCustomPrice = new int?(5000);
                shop.item[nextSlot].shopSpecialCurrency = KeybrandsPlus.MunnyCost;
                nextSlot++;
            }
        }

        public override void ModifyHitByItem(Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            damage = 1;
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (projectile.friendly)
                damage = 1;
        }

        public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
        {
            if (npc.lastInteraction != 255)
            {
                switch (Main.rand.Next(3))
                {
                    case 1:
                        CombatText.NewText(npc.getRect(), Color.Cyan, "Cease at once!", true);
                        break;
                    case 2:
                        CombatText.NewText(npc.getRect(), Color.Cyan, "That's not very nice!", true);
                        break;
                    default:
                        CombatText.NewText(npc.getRect(), Color.Cyan, "You monster!", true);
                        break;
                }
                Main.PlaySound(SoundID.NPCDeath7, player.Center);
                for (int i = 0; i < 10; i++)
                {
                    int dust = Dust.NewDust(player.Center, 0, 0, DustID.AncientLight, Scale: 3f);
                    Main.dust[dust].velocity *= 7.5f;
                    Main.dust[dust].noGravity = true;
                }
                player.AddBuff(BuffType<Buffs.Stop>(), 180);
                player.AddBuff(BuffType<Buffs.ChimeraBleed>(), 900);
                Vector2 vectorToPlayer = Vector2.Normalize(player.Center - npc.Center);
                Main.PlaySound(SoundID.Item60, npc.Center);
                int Bite = Projectile.NewProjectile(npc.Center, vectorToPlayer, ProjectileType<Projectiles.ChimeraBite>(), 50, 0);
                Main.projectile[Bite].hostile = true;
                Main.projectile[Bite].friendly = false;
                Main.projectile[Bite].magic = false;
            }
        }

        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
            if (projectile.friendly && npc.lastInteraction != 255)
            {
                Player player = Main.player[npc.lastInteraction];
                switch (Main.rand.Next(3))
                {
                    case 1:
                        CombatText.NewText(npc.getRect(), Color.Cyan, "Cease at once!", true);
                        break;
                    case 2:
                        CombatText.NewText(npc.getRect(), Color.Cyan, "That's not very nice!", true);
                        break;
                    default:
                        CombatText.NewText(npc.getRect(), Color.Cyan, "You monster!", true);
                        break;
                }
                Main.PlaySound(SoundID.NPCDeath7, player.Center);
                for (int i = 0; i < 10; i++)
                {
                    int dust = Dust.NewDust(player.Center, 0, 0, DustID.AncientLight, Scale: 3f);
                    Main.dust[dust].velocity *= 7.5f;
                    Main.dust[dust].noGravity = true;
                }
                player.AddBuff(BuffType<Buffs.Stop>(), 180);
                player.AddBuff(BuffType<Buffs.ChimeraBleed>(), 900);
                Vector2 vectorToPlayer = Vector2.Normalize(player.Center - npc.Center);
                Main.PlaySound(SoundID.Item60, npc.Center);
                int Bite = Projectile.NewProjectile(npc.Center, vectorToPlayer, ProjectileType<Projectiles.ChimeraBite>(), 50, 0);
                Main.projectile[Bite].hostile = true;
                Main.projectile[Bite].friendly = false;
                Main.projectile[Bite].magic = false;
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
            knockback = 5f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 15;
            randExtraCooldown = 15;
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
