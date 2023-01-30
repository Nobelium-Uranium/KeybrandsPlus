using KeybrandsPlus.Common.Helpers;
using KeybrandsPlus.Common.UI;
using KeybrandsPlus.Content.Items.Currency;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using ReLogic.Content;
using System.IO;
using Terraria;
using Terraria.GameContent.UI;
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

        public static int MunnyCost;

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
                MunnyCost = CustomCurrencyManager.RegisterCurrency(new MunnyData(ModContent.ItemType<Munny>(), 1000000L));
                MunnyUI.Load();
            }
        }

        public override void Unload()
        {
            SteamID = null;
            MunnyCost = 0;

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

        public Rectangle GetFrame(Texture2D tex)
        {
            return new Rectangle(0, 0, tex.Width, tex.Height);
        }
        public Vector2 GetOrigin(Texture2D tex)
        {
            return new Vector2(tex.Width / 2f, tex.Height / 2f);
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
            Rectangle frame = mod.GetFrame(texture);
            Vector2 origin = mod.GetOrigin(texture);
            spriteBatch.Draw(texture, drawPos, frame, Color.White, 0f, origin, 1f, SpriteEffects.None, 0f);

            texture = mod.Assets.Request<Texture2D>("Assets/Title/PlusTop").Value;
            frame = mod.GetFrame(texture);
            origin = mod.GetOrigin(texture); //177
            spriteBatch.Draw(texture, drawPos + new Vector2(150f, 4.5f), frame, Color.White, MathHelper.ToRadians(22.5f), origin, 1f, SpriteEffects.None, 0f);

            texture = mod.Assets.Request<Texture2D>("Assets/Title/Logo").Value;
            frame = mod.GetFrame(texture);
            origin = mod.GetOrigin(texture);
            spriteBatch.Draw(texture, drawPos + new Vector2(4.5f, 9f), frame, Color.White, 0f, origin, 1f, SpriteEffects.None, 0f);

            texture = mod.Assets.Request<Texture2D>("Assets/Title/PlusBottom").Value;
            frame = mod.GetFrame(texture);
            origin = mod.GetOrigin(texture);
            spriteBatch.Draw(texture, drawPos + new Vector2(150f, 4.5f), frame, Color.White, MathHelper.ToRadians(22.5f), origin, 1f, SpriteEffects.None, 0f);

            texture = mod.Assets.Request<Texture2D>("Assets/Title/Subtitle").Value;
            frame = mod.GetFrame(texture);
            origin = mod.GetOrigin(texture);
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