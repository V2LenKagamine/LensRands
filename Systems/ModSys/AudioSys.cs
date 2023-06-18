using Terraria.Audio;
using Terraria.ModLoader;

namespace LensRands.Systems.ModSys
{
    public class AudioSys : ModSystem
    {
        public static readonly SoundStyle RealSlash = new(LensRands.AssetsPath + "Sounds/RealSlash");

        public static readonly SoundStyle Opticor = new(LensRands.AssetsPath + "Sounds/Opticor");

        public static readonly SoundStyle Monika = new(LensRands.AssetsPath + "Sounds/MonikaSummon");

        public static readonly SoundStyle BEES = new(LensRands.AssetsPath + "Sounds/breefcase") { MaxInstances = 1 };

        public static readonly SoundStyle BADING = new(LensRands.AssetsPath + "Sounds/BADING");

        public static readonly SoundStyle GetBentLoser = new(LensRands.AssetsPath + "Sounds/Explodeurdickandsmallurass");

        public static readonly SoundStyle Speen = new(LensRands.AssetsPath + "Sounds/speen");
    }
}
