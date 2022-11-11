using System;
using System.Collections.Generic;

namespace Data.ValueObject
{
    [Serializable]
    public class ScoreData
    {
        public List<int> RankedScore;

        public int LastScore;
    }
}