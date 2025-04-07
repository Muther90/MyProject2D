public static class AudioData
{
    public static class Params
    {
        public const string MasterVolume = "MasterVolume";
        public const string BackgroundVolume = "BackgroundVolume";
        public const string EffectsVolume = "EffectsVolume";
        public const float MinVolume = -80f;
        public const float DBMultiplier = 20f;
    }

    public static bool IsAudioEnabled { get; private set; } = true;

    public static void EnableAudio()
    {
        IsAudioEnabled = true;
    }

    public static void DisableAudio()
    {
        IsAudioEnabled = false;
    }
}
