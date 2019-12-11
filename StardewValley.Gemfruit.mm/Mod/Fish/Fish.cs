using System;
using System.Collections.Generic;

namespace Gemfruit.Mod.Fish
{
    public class Fish
    {
        public string Name { get; }
        public int Difficulty { get; }
        public FishAi Behavior { get; }
        public int MinSize { get; }
        public int MaxSize { get; }
        public List<Tuple<int, int>> TimeRanges { get; }
        //public Season Season { get; }
        public FishWeather Weather { get; }
        public int MinDepth { get; }
        public float SpawnMultiplier { get; }
        public float DepthMultiplier { get; }
        public int MinLevel { get; }
    }
}