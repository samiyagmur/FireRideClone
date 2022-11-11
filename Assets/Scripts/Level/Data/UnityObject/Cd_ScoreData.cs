using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "Cd_ScoreData", menuName = "Data/ScoreData")]
    public class Cd_ScoreData : ScriptableObject
    {
        public ScoreData ScoreData;

        private const string Key = "_scoreData";

        public string GetKey()
        {
            return Key;
        }
    }
}