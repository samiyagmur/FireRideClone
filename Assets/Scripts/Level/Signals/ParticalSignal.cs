using Extantions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class ParticalSignal : MonoSingleton<ParticalSignal>
    {
        public UnityAction<Vector3> onSpawnPartical = delegate { };
    }
}