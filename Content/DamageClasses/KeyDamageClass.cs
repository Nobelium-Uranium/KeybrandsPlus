﻿using Terraria.ModLoader;

namespace KeybrandsPlus.Content.DamageClasses
{
    public class UniversalDamageClass : DamageClass
    {
        internal static UniversalDamageClass Instance;
        public override void Load() => Instance = this;
        public override void Unload() => Instance = null;
        public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
        {
            if (damageClass == Generic)
                return StatInheritanceData.Full;
            if (damageClass == Melee || damageClass == Magic || damageClass == Ranged)
                return new StatInheritanceData(.75f, .75f, .75f, .75f, .75f);
            return StatInheritanceData.None;
        }
        public override bool GetEffectInheritance(DamageClass damageClass)
        {
            if (damageClass == Melee || damageClass == Magic || damageClass == Ranged)
                return true;
            return false;
        }
    }
}