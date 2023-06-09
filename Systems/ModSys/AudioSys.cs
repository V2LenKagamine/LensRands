using Terraria.Audio;
using Terraria.ModLoader;

namespace LensRands.Systems.ModSys
{
    public class AudioSys : ModSystem
    {
        public static readonly SoundStyle RealSlash = new(LensRands.AssetsPath + "Sounds/RealSlash");

        public static readonly SoundStyle Opticor = new(LensRands.AssetsPath + "Sounds/Opticor");
    }
}
