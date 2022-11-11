using Interfaces;
using Signals;
using Type;
using UnityEngine;

namespace Controller
{
    public class ParticalPysicController : MonoBehaviour, IPushObject
    {
        [SerializeField]
        private ParticalTypeAsScore particalTypeAsScore;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PendulumPhysicController pendulumPhysicController))
            {
                PushToPool((PoolObjectType)particalTypeAsScore, transform.parent.gameObject);
            }
        }

        public void PushToPool(PoolObjectType poolObjectType, GameObject obj) => PoolSignals.Instance.onReleaseObjectFromPool?.Invoke(poolObjectType, obj);

        public ParticalTypeAsScore GetParticalType() => particalTypeAsScore;
    }
}