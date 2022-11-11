using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "Cd_PendulumData", menuName = "Data/PendulumData")]
    public class Cd_PendulumData : ScriptableObject
    {
        public PendulumData PendulumData;
    }
}