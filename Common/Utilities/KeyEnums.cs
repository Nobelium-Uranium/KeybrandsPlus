namespace KeybrandsPlus.Common.Utilities
{
    internal class KeyEnums
    {
        public enum Element
        {
            Mundane,
            Fire,
            Blizzard,
            Thunder,
            Aero,
            Water,
            Light,
            Dark,
            Nil
        }
        public enum KeychainType
        {
            None,
            KingdomKey,
            KingdomKeyD
        }
        public enum SwingType
        {
            Default,
            Light,
            Heavy
        }
        public enum SwingDirection
        {
            Down,
            Up,
            Swipe,
            Thrust
        }
        public enum GroundComboModifier
        {
            Default,
            Slapshot,
            SlidingDash,
            UpperSlash,
            DodgeSlash,
            FlashStep,
            VicinityBreak
        }
        public enum AerialComboModifier
        {
            Default,
            Vortex,
            AerialSweep,
            HorizontalSlash,
            AerialDive,
            AerialSpiral
        }
        public enum GroundComboFinisher
        {
            Default,
            Blitz,
            RippleDrive,
            StunImpact,
            GravityBreak,
            Zantetsuken,
            FinishingLeap,
            GuardBreak,
            Explosion
        }
        public enum AerialComboFinisher
        {
            Default,
            HurricaneBlast,
            AerialFinish,
            MagnetBurst
        }
        public enum AmbushHeartless
        {
            ShadowScout, // 3 Shadows
            ShadowSwarm, // 5 Shadows
            ShadowTroop, // 4 Shadows, 1 Soldier
            SoldierScout, // 3 Soldiers
            SoldierSwarm, // 5 Soldiers
            SoldierTroop, // 4 Soldiers, 1 Large Body
        }
    }
}
