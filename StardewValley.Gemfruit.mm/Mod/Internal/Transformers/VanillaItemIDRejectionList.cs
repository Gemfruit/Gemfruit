using System.Collections.Generic;

namespace Gemfruit.Mod.Internal.Transformers
{
    public class VanillaItemIDRejectionList
    {
        private static List<int> RejectionList = new List<int>()
        {
            0,
            2,
            4,
            30,
            75,
            76,
            77,
            94,
            290,
            294,
            295,
            313,
            314,
            315,
            316,
            317,
            318,
            319,
            320,
            321,
            // TODO: 341 is furniture and should probably be added to a seperate list

        };
    }
}