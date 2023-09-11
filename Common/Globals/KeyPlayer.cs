﻿using KeybrandsPlus.Assets.Sounds;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader.IO;

namespace KeybrandsPlus.Common.Globals
{
    public class KeyPlayer : ModPlayer
    {
        public int MunnySavings;
        public int MunnyCount;
        private int MunnyCountTimer;

        public int recentMunny;
        public int recentMunnyCounter;

        public bool MunnyMagnet;

        public bool SecondChance;
        private bool SecondChanceSavingThrow;

        public override void ResetEffects()
        {
            MunnyMagnet = false;
            SecondChanceSavingThrow = false;
        }

        public override void UpdateDead()
        {
            ResetEffects();
            PostUpdateEvenWhileDead();
        }

        public override void PostUpdate()
        {
            PostUpdateEvenWhileDead();
        }

        public void PostUpdateEvenWhileDead()
        {
            if (recentMunnyCounter > 0)
                recentMunnyCounter--;
            else
                recentMunny = 0;
            if (MunnySavings > 999999999)
                MunnySavings = 999999999;
            else if (MunnySavings < 0)
                MunnySavings = 0;
            CountMunny();
        }

        public override void OnHurt(Player.HurtInfo info)
        {
            if (SecondChance && Player.statLife > (int)Math.Floor((float)Player.statLifeMax2 * .05))
                SecondChanceSavingThrow = true;
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (SecondChanceSavingThrow)
            {
                if (Main.myPlayer == Player.whoAmI)
                    SoundEngine.PlaySound(SoundID.Item67);
                Player.statLife = 1;
                return false;
            }
            return true;
        }

        public void AddMunny(int amount, bool instantCount = false)
        {
            MunnySavings += amount;
            if (instantCount && MunnyCountTimer < 60)
                MunnyCountTimer = 60;
            else if (MunnyCountTimer < 60)
                MunnyCountTimer = 0;
            if (Math.Abs(amount) > 0)
                SetRecentMunny(amount);
        }

        public void SetRecentMunny(int amount)
        {
            if (recentMunnyCounter <= 30)
                recentMunny = 0;
            recentMunny += amount;
            recentMunnyCounter = 210;
        }

        public void CountMunny()
        {
            if (MunnyCount != MunnySavings)
            {
                if (MunnyCountTimer++ >= 60)
                {
                    int difference = MunnySavings - MunnyCount;
                    if (difference > 0) // Count up
                    {
                        if (Main.myPlayer == Player.whoAmI && MunnyCountTimer % 3 == 1)
                            SoundEngine.PlaySound(KeySoundStyle.MunnyCountUp);
                        MunnyCount += GetFancyNumber(Math.Abs(difference));
                        if (MunnyCount > MunnySavings)
                            MunnyCount = MunnySavings;
                    }
                    else if (difference < 0) // Count down
                    {
                        if (Main.myPlayer == Player.whoAmI && MunnyCountTimer % 3 == 1)
                            SoundEngine.PlaySound(KeySoundStyle.MunnyCountDown);
                        MunnyCount -= GetFancyNumber(Math.Abs(difference));
                        if (MunnyCount < MunnySavings)
                            MunnyCount = MunnySavings;
                    }
                }
            }
            else
                MunnyCountTimer = 0;
        }

        private static int GetFancyNumber(int value)
        { // This is definitely not clean
            if (value >= 100000000)
            {
                return 34567892;
            }
            else if (value >= 10000000)
            {
                return 3456789;
            }
            else if (value >= 1000000)
            {
                return 345678;
            }
            else if (value >= 100000)
            {
                return 34567;
            }
            else if (value >= 10000)
            {
                return 3456;
            }
            else if (value >= 1000)
            {
                return 345;
            }
            else if (value >= 100)
            {
                return 34;
            }
            return 3;
        }

        public override void SaveData(TagCompound tag)
        {
            tag["MunnySavings"] = MunnySavings;
        }

        public override void LoadData(TagCompound tag)
        {
            MunnySavings = tag.Get<int>("MunnySavings");
            MunnyCount = MunnySavings;
        }
    }
}
