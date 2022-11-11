using Data.UnityObject;
using Signals;
using System;
using System.Collections.Generic;
using Type;
using UnityEngine;
using UnityEngine.Rendering;

namespace Extantions
{
    public class Pool : MonoBehaviour
    {
        [SerializeField]
        private Cd_ObjectData _cd_ObjectDatas;//gidecek

        [SerializeField]
        private SerializedDictionary<PoolObjectType, Queue<GameObject>> objectPool;

        private GameObject _outGoingObject;

        private readonly int _loadPoolCount = Enum.GetNames(typeof(PoolObjectType)).Length;

        private int poolCount = 0;

        private void Awake()
        {
            objectPool = new SerializedDictionary<PoolObjectType, Queue<GameObject>>();

            for (; poolCount < _loadPoolCount; poolCount++)
            {
                objectPool.Add(_cd_ObjectDatas.ObjectData[poolCount].poolObjectType, new Queue<GameObject>());

                for (int j = 0; j < _cd_ObjectDatas.ObjectData[poolCount].PoolCount; j++)
                {
                    var poolObj = Instantiate(_cd_ObjectDatas.ObjectData[poolCount].PoolObject);

                    poolObj.SetActive(false);

                    objectPool[_cd_ObjectDatas.ObjectData[poolCount].poolObjectType].Enqueue(poolObj);
                }
            }
        }

        #region EventSubscribtion

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            PoolSignals.Instance.onGetObjectFromPool += OnGetObjcetFromPool;
            PoolSignals.Instance.onReleaseObjectFromPool += OnReleaseObjectFromPool;
        }

        private void UnsubscribeEvents()
        {
            PoolSignals.Instance.onGetObjectFromPool -= OnGetObjcetFromPool;
            PoolSignals.Instance.onReleaseObjectFromPool -= OnReleaseObjectFromPool;
        }

        private void OnDisable() => UnsubscribeEvents();

        #endregion EventSubscribtion

        private GameObject OnGetObjcetFromPool(PoolObjectType type)
        {
            if (objectPool[type].Count == 0 && _cd_ObjectDatas.ObjectData[(int)type].poolType == PoolType.Dynamic)
            {
                _outGoingObject = Instantiate(_cd_ObjectDatas.ObjectData[(int)type].PoolObject);
            }
            else
            {
                if (objectPool[type].Count != 0)
                {
                    _outGoingObject = objectPool[type].Peek();

                    _outGoingObject.SetActive(true);

                    objectPool[type].Dequeue();
                }
            }

            return _outGoingObject;
        }

        private void OnReleaseObjectFromPool(PoolObjectType type, GameObject obj)
        {
            obj.SetActive(false);

            objectPool[type].Enqueue(obj);
        }
    }
}