using Microsoft.Xna.Framework;

namespace Gemfruit.Mod.Monsters
{
    public readonly struct HutchSpawnData
    {
        public readonly Vector2 Position;

        public HutchSpawnData(Vector2 position)
        {
            Position = position;
        }
    }
}