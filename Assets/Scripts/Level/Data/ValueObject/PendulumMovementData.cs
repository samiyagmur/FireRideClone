using System;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class PendulumMovementData
    {
        public Vector3 HitAngle;
        public float SpringJointSpring;
        public float SpringJointDamper;
        public float SpringJointMassScale;
        public float RateOfMaxDistance;
        public float RateOfMinDistancef;
        public Vector3 SelfAncorPoint;
    }
}