using KeybrandsPlus.Helpers;
using Terraria;
using Terraria.ModLoader;

namespace KeybrandsPlus.Globals
{
    class KeyProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity { get { return true; } }
        public override bool CloneNewInstances { get { return true; } }
        public bool IsKeybrandProj;
        public bool Fire;
        public bool Blizzard;
        public bool Thunder;
        public bool Aero;
        public bool Water;
        public bool Dark;
        public int[] FireTypes = { 2, 15, 19, 34, 35, 41, 82, 85, 163, 187, 188, 258, 291, 292, 295, 296, 310, 321, 325, 326, 327, 328, 329, 376, 400, 401, 402, 424, 425, 426, 467, 485, 553, 611, 612, 636, 654, 664, 666, 668, 686, 687, 694, 695, 696, 706, 711 };
        public int[] BlizzardTypes = { 80, 109, 113, 118, 119, 120, 128, 166, 172, 174, 177, 253, 257, 263, 309, 337, 343, 344, 348, 349, 359, 464, 520 };
        public int[] ThunderTypes = { 254, 255, 435, 437, 442, 443, 450, 466, 580 };
        public int[] AeroTypes = { 38, 206, 221, 227, 316, 317, 368, 595, 656, 657, 660, 684, 704, 712 };
        public int[] WaterTypes = { 22, 26, 27, 69, 70, 239, 264, 384, 386, 405, 408, 409, 410, 511, 512, 513, 621, 669 };
        public int[] DarkTypes = { 4, 7, 8, 25, 44, 45, 46, 114, 115, 153, 154, 157, 245, 270, 274, 290, 293, 294, 297, 299, 304, 406, 307, 468, 495, 496, 497, 542, 543, 585, 593, 659, 661 };
        
        public override void SetDefaults(Projectile projectile)
        {
            if (projectile.modProjectile is KeybrandProj)
                IsKeybrandProj = true;
            foreach (int i in FireTypes)
                if (projectile.type == i)
                    Fire = true;
            foreach (int i in BlizzardTypes)
                if (projectile.type == i)
                    Blizzard = true;
            foreach (int i in ThunderTypes)
                if (projectile.type == i)
                    Thunder = true;
            foreach (int i in AeroTypes)
                if (projectile.type == i)
                    Aero = true;
            foreach (int i in WaterTypes)
                if (projectile.type == i)
                    Water = true;
            foreach (int i in DarkTypes)
                if (projectile.type == i)
                    Dark = true;
        }
    }
}
