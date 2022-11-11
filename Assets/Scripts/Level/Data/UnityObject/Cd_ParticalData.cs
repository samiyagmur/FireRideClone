using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "Cd_ParticalData", menuName = "Data/ParticalData")]
    public class Cd_ParticalData : ScriptableObject
    {
        public ParticalData ParticalData;
    }
}