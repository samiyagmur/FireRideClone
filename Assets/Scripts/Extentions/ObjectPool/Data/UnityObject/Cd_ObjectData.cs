using Data.ValueObject;
using System.Collections.Generic;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "Cd_Pool", menuName = "Data/ObjectPool")]
    public class Cd_ObjectData : ScriptableObject
    {
        public List<ObjectData> ObjectData;
    }
}