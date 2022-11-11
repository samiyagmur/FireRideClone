using Type;
using UnityEngine;

namespace Interfaces
{
    public interface IPushObject
    {
        void PushToPool(PoolObjectType poolObjectType, GameObject obj);
    }
}