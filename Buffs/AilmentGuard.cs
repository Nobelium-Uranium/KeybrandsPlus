using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using KeybrandsPlus.Globals;

namespace KeybrandsPlus.Buffs
{
    class AilmentGuard : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Ailment Guard");
            Description.SetDefault("Immune to most debuffs");
            Main.debuff[Type] = true;
            canBeCleared = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<KeyPlayer>().ElixirGuard = true;
            for (int i = 0; i < player.buffImmune.Length; i++)
            {
                if (i == BuffID.Bleeding || i == BuffID.Poisoned || i == BuffID.OnFire || i == BuffID.Venom || i == BuffID.Darkness || i == BuffID.Blackout || i == BuffID.Obstructed || i == BuffID.Cursed || i == BuffID.Frostburn || i == BuffID.Confused || i == BuffID.Slow || i == BuffID.Weak || i == BuffID.Silenced || i == BuffID.BrokenArmor || i == BuffID.CursedInferno || i == BuffID.Chilled || i == BuffID.Ichor || i == BuffID.ShadowFlame || i == BuffID.Electrified || i == BuffID.Rabies || i == BuffID.VortexDebuff || i == BuffID.WitheredArmor || i == BuffID.WitheredWeapon || i == BuffID.OgreSpit || i == BuffID.Frozen || i == BuffID.Stoned || i == BuffID.Webbed)
                    player.buffImmune[i] = true;
            }
        }
    }
}
