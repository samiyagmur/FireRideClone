using System;
using Type;
using UnityEngine;

namespace Data.ValueObject
{
    [Serializable]
    public class ObjectData
    {
        public PoolObjectType poolObjectType;
        public PoolType poolType;
        public GameObject PoolObject;

        //public string PoolName;
        public int PoolCount;
    }
}