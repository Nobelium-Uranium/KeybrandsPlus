using Terraria.Audio;

namespace KeybrandsPlus.Assets.Sounds
{
    public sealed class KeySoundStyle
    {
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
    }
}
