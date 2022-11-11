using Extantions;
using System;
using Type;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class PoolSignals : MonoSingleton<PoolSignals>
    {
        public Func<PoolObjectType, GameObject> onGetObjectFromPool = delegate { return null; };
        public UnityAction<PoolObjectType, GameObject> onReleaseObjectFromPool = delegate { };
    }
}