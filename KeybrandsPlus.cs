using KeybrandsPlus.Common.Systems;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace KeybrandsPlus
{
	public class KeybrandsPlus : Mod
    {
        internal static string SteamID;

        //Dev SteamIDs for reference
        //Chem: 76561198079106803
        //Dan: 76561198178272217

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
        }

        public override void Unload()
        {
            SteamID = null;
        }
    }
    #region commands
    public class ConsumeMPCommand : ModCommand
    {
        public override CommandType Type => CommandType.Chat;
        public override string Command => "kplus_consumemp";
        public override string Description => "Self explanatory";
        public override string Usage => "/kplus_consumemp amount";
        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (KeybrandsPlus.SteamID == "76561198079106803" || KeybrandsPlus.SteamID == "76561198178272217")
            {
                caller.Reply("Soon:tm:");
            }
            else
                caller.Reply("Insufficient permissions");
        }
    }
    public class GainDeltaCommand : ModCommand
    {
        public override CommandType Type => CommandType.Chat;
        public override string Command => "kplus_gaindelta";
        public override string Description => "Self explanatory";
        public override string Usage => "/kplus_gaindelta amount";
        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (KeybrandsPlus.SteamID == "76561198079106803" || KeybrandsPlus.SteamID == "76561198178272217")
            {
                caller.Reply("Soon:tm:");
            }
            else
                caller.Reply("Insufficient permissions");
        }
    }
    #endregion
}