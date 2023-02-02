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
        public static readonly SoundStyle MunnyBounce = new($"{nameof(KeybrandsPlus)}/Assets/Sounds/Other/MunnyBounce")
        {
            Volume = .25f,
            PitchVariance = .1f
        };
        public static readonly SoundStyle MunnyBounceMed = new($"{nameof(KeybrandsPlus)}/Assets/Sounds/Other/MunnyBounce")
        {
            Volume = .25f,
            Pitch = -.075f,
            PitchVariance = .1f
        };
        public static readonly SoundStyle MunnyBounceBig = new($"{nameof(KeybrandsPlus)}/Assets/Sounds/Other/MunnyBounce")
        {
            Volume = .25f,
            Pitch = -.15f,
            PitchVariance = .1f
        };
        public static readonly SoundStyle MunnyPickup = new($"{nameof(KeybrandsPlus)}/Assets/Sounds/Other/MunnyPickup")
        {
            Volume = .1f
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
