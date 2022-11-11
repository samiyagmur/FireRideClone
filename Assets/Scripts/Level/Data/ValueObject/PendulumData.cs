using System;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class PendulumData
    {
        public Vector3 ResetYPos;

        [Space(order = 5)]
        public PendulumMovementData PendulumMovementData;
    }
}