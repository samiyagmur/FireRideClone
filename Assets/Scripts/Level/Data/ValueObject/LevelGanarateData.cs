using System;

namespace Data.ValueObject
{
    [Serializable]
    public class LevelGanarateData
    {
        public int XOffsetofWall;

        public float YOffsetofWall;

        public float _distanceVerticalOfWall;

        public int NumberOfSpawnWalLineCountOnStart;

        public int Posibility;

        public float AmountOfIncreaseRatePerNextWall;

        public float MinVerticalDistanceLimit;

        public float MaxVerticalDistanceLimit;
    }
}