using Type;
using UnityEngine;

namespace Interfaces
{
    public interface IPullObject
    {
        GameObject PullFromPool(PoolObjectType poolObjectType);
    }
}