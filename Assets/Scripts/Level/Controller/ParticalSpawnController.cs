using Data.ValueObject;
using Interfaces;
using Signals;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using Type;
using UnityEngine;

namespace Controller
{
    public class ParticalSpawnController : MonoBehaviour, IPullObject, IPushObject
    {
        private ParticalData _particalData;

        private GameObject _chosenPartical;

        private int _particalSpawnOrder;

        private PoolObjectType _poolObjectType;

        [ShowInInspector]
        private LinkedList<GameObject> particalStack = new LinkedList<GameObject>();

        [ShowInInspector]
        private LinkedList<PoolObjectType> particalTpye = new LinkedList<PoolObjectType>();

        internal void SetData(ParticalData particalData) => _particalData = particalData;

        internal void SpawnPartical(Vector3 wallPosition)
        {
            if (_particalSpawnOrder >= 1)
            {
                _particalData.VerticalSpawnOffset = new Vector3(0, 10, 0);

                _poolObjectType = PoolObjectType.BeamOrenge;

                _particalSpawnOrder = 0;
            }
            else
            {
                int chosenParticalNumber = CalculateWhichPartical();

                _poolObjectType = (PoolObjectType)chosenParticalNumber;

                _particalSpawnOrder++;
            }

            _chosenPartical = PullFromPool(_poolObjectType);

            _chosenPartical.transform.position = wallPosition + _particalData.VerticalSpawnOffset;

            if (particalStack.Count > 1)
            {
                PushToPool(particalTpye.First.Value, particalStack.First.Value);

                particalTpye.Remove(particalTpye.Last.Previous.Value);
                particalStack.Remove(particalStack.Last.Previous.Value);
            }

            particalTpye.AddLast(_poolObjectType);
            particalStack.AddLast(_chosenPartical);
        }

        private int CalculateWhichPartical() => Random.Range(4, 6);

        public GameObject PullFromPool(PoolObjectType poolObjectType)
        {
            return PoolSignals.Instance.onGetObjectFromPool?.Invoke(poolObjectType);
        }

        public void PushToPool(PoolObjectType poolObjectType, GameObject obj)
        {
             PoolSignals.Instance.onReleaseObjectFromPool?.Invoke(poolObjectType, obj);
        }
    }
}