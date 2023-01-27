using Terraria.Audio;

namespace KeybrandsPlus.Assets.Sounds
{
    public sealed class KeySoundStyle
    {
        #region Other
        public static readonly SoundStyle MunnyBounce = new($"{nameof(KeybrandsPlus)}/Assets/Sounds/Other/MunnyBounce")
        {
            Volume = .375f,
            PitchVariance = .1f
        };
        public static readonly SoundStyle MunnyBounceMed = new($"{nameof(KeybrandsPlus)}/Assets/Sounds/Other/MunnyBounce")
        {
            Volume = .375f,
            Pitch = -.1f,
            PitchVariance = .1f
        };
        public static readonly SoundStyle MunnyBounceBig = new($"{nameof(KeybrandsPlus)}/Assets/Sounds/Other/MunnyBounce")
        {
            Volume = .375f,
            Pitch = -.2f,
            PitchVariance = .1f
        };
        public static readonly SoundStyle MunnyPickup = new($"{nameof(KeybrandsPlus)}/Assets/Sounds/Other/MunnyPickup")
        {
            Volume = .125f
        };
        #endregion
    }
}
