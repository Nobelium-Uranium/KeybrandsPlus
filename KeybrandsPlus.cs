using KeybrandsPlus.Common.Helpers;
using KeybrandsPlus.Common.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using ReLogic.Content;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace KeybrandsPlus
{
    public class KeybrandsPlus : Mod
    {
        internal static KeybrandsPlus Instance;

        internal static readonly string ModConfigPath = Path.Combine(Main.SavePath, "ModConfigs");

        internal static string SteamID;

        //Dev SteamIDs for reference
        //Chem: 76561198079106803
        //Dan: 76561198178272217

        public static Dictionary<int, string> keyRarities = new Dictionary<int, string>();

        public override void Load()
        {
            SteamID = string.Empty;
            try
            {
                SteamID = Steamworks.SteamUser.GetSteamID().ToString();
            }
            catch
            {
                Logger.WarnFormat("[KeybrandsPlus] Unable to grab SteamID, Steam servers are likely offline or otherwise unavailable.");
            }

            if (!Main.dedServ)
            {
                MunnyUI.Load();
            }
        }

        public override void PostSetupContent()
        {
            foreach (ModRarity rarity in ModContent.GetContent<ModRarity>())
            {
                if (rarity.Mod == this)
                    keyRarities.Add(rarity.Type, rarity.FullName);
            }
        }

        public override void Unload()
        {
            SteamID = null;

            keyRarities.Clear();

            MunnyUI.Unload();
        }

        internal static void SaveConfig(ModConfig config)
        {
            Directory.CreateDirectory(ModConfigPath);
            string filename = config.Mod.Name + "_" + config.Name + ".json";
            string path = Path.Combine(ModConfigPath, filename);
            string json = JsonConvert.SerializeObject(config/*, serializerSettings*/);
            File.WriteAllText(path, json);
        }
    }
    public class DearlyBelovedMenu : ModMenu
    {
        KeybrandsPlus mod = ModContent.GetInstance<KeybrandsPlus>();

        public override Asset<Texture2D> Logo => mod.Assets.Request<Texture2D>("Assets/Title/Logo");

        public override bool PreDrawLogo(SpriteBatch spriteBatch, ref Vector2 logoDrawCenter, ref float logoRotation, ref float logoScale, ref Color drawColor)
        {
            logoScale = 1f;
            logoRotation = 0;

            Vector2 drawPos = logoDrawCenter;

            Texture2D texture = mod.Assets.Request<Texture2D>("Assets/Title/Heart").Value;
            Rectangle frame = KeyUtils.GetFrame(texture);
            Vector2 origin = KeyUtils.GetOrigin(texture);
            spriteBatch.Draw(texture, drawPos, frame, Color.White, 0f, origin, 1f, SpriteEffects.None, 0f);

            texture = mod.Assets.Request<Texture2D>("Assets/Title/PlusTop").Value;
            frame = KeyUtils.GetFrame(texture);
            origin = KeyUtils.GetOrigin(texture); //177
            spriteBatch.Draw(texture, drawPos + new Vector2(150f, 4.5f), frame, Color.White, MathHelper.ToRadians(22.5f), origin, 1f, SpriteEffects.None, 0f);

            texture = mod.Assets.Request<Texture2D>("Assets/Title/Logo").Value;
            frame = KeyUtils.GetFrame(texture);
            origin = KeyUtils.GetOrigin(texture);
            spriteBatch.Draw(texture, drawPos + new Vector2(4.5f, 9f), frame, Color.White, 0f, origin, 1f, SpriteEffects.None, 0f);

            texture = mod.Assets.Request<Texture2D>("Assets/Title/PlusBottom").Value;
            frame = KeyUtils.GetFrame(texture);
            origin = KeyUtils.GetOrigin(texture);
            spriteBatch.Draw(texture, drawPos + new Vector2(150f, 4.5f), frame, Color.White, MathHelper.ToRadians(22.5f), origin, 1f, SpriteEffects.None, 0f);

            texture = mod.Assets.Request<Texture2D>("Assets/Title/Subtitle").Value;
            frame = KeyUtils.GetFrame(texture);
            origin = KeyUtils.GetOrigin(texture);
            spriteBatch.Draw(texture, drawPos + new Vector2(4.5f, 61.5f), frame, Color.White, 0f, origin, 1f, SpriteEffects.None, 0f);

            return false;
        }

        public override void Update(bool isOnTitleScreen)
        {
            if (Main.gameMenu)
            {
                Main.dayTime = true;
                Main.dayRate = 0;
                Main.time = 27000f;
                Main.sunModY = 0;
            }
        }

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/DearlyBeloved");

        public override string DisplayName => "Keybrands+";
    }
}