using Terraria.Audio;

namespace KeybrandsPlus.Assets.Sounds
{
    public sealed class KeySoundStyle
    {
        #region NPCs
        #region TreasureChest
        public static readonly SoundStyle NPCChestOpen = new($"{nameof(KeybrandsPlus)}/Assets/Sounds/NPCs/TreasureChest/NPCChestOpen")
        {
            Volume = .5f
        };
        #endregion
        #endregion
        #region Other
        public static readonly SoundStyle AirDash = new($"{nameof(KeybrandsPlus)}/Assets/Sounds/Other/AirDash")
        {
            Volume = .5f
        };
        public static readonly SoundStyle MunnyBounceTiny = new($"{nameof(KeybrandsPlus)}/Assets/Sounds/Other/MunnyBounce")
        {
            Volume = .25f,
            PitchVariance = .1f
        };
        public static readonly SoundStyle MunnyBounceSmall = new($"{nameof(KeybrandsPlus)}/Assets/Sounds/Other/MunnyBounce")
        {
            Volume = .25f,
            Pitch = -.075f,
            PitchVariance = .1f
        };
        public static readonly SoundStyle MunnyBounceMed = new($"{nameof(KeybrandsPlus)}/Assets/Sounds/Other/MunnyBounce")
        {
            Volume = .25f,
            Pitch = -.15f,
            PitchVariance = .1f
        };
        public static readonly SoundStyle MunnyBounceBig = new($"{nameof(KeybrandsPlus)}/Assets/Sounds/Other/MunnyBounce")
        {
            Volume = .25f,
            Pitch = -.3f,
            PitchVariance = .1f
        };
        public static readonly SoundStyle MunnyPickup = new($"{nameof(KeybrandsPlus)}/Assets/Sounds/Other/MunnyPickup")
        {
            Volume = .1f
        };
        public static readonly SoundStyle MunnyCountUp = new($"{nameof(KeybrandsPlus)}/Assets/Sounds/Other/MunnyPickup")
        {
            Volume = .05f
        };
        public static readonly SoundStyle MunnyCountDown = new($"{nameof(KeybrandsPlus)}/Assets/Sounds/Other/MunnyCountDown")
        {
            Volume = .05f
        };
        public static readonly SoundStyle PurchaseItem = new($"{nameof(KeybrandsPlus)}/Assets/Sounds/Other/PurchaseItem")
        {
            Volume = .5f
        };
        #endregion
        #region Tiles
        #region Furniture
        public static readonly SoundStyle ChestOpen = new($"{nameof(KeybrandsPlus)}/Assets/Sounds/Tiles/Furniture/ChestOpen")
        {
            Volume = .25f
        };
        #endregion
        #endregion
    }
}
