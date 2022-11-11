using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "Cd_LevelData", menuName = "Data/LevelData")]
    public class Cd_LevelData : ScriptableObject
    {
        public LevelData LevelData;
    }
}