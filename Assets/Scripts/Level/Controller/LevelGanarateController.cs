using Data.ValueObject;
using Interfaces;
using Managers;
using Signals;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Threading.Tasks;
using Type;
using UnityEngine;

namespace Controller
{
    public class LevelGanarateController : MonoBehaviour, IPullObject, IPushObject, ICanSee
    {
        [ShowInInspector]
        private LinkedList<GameObject> _stackLowerWall = new LinkedList<GameObject>();

        [ShowInInspector]
        private LinkedList<GameObject> _stackUpperWall = new LinkedList<GameObject>();

        [SerializeField]
        private LevelManager levelManager;

        private Camera _camera;

        private GameObject _lowerWall;

        private GameObject _upperWall;

        private LevelGanarateData _levelGanarateData;

        private bool _isDeadPendulum;

        private int _particalSpawnPointAsWall;

        private int count = 0;

        private int conunt2 = 1;

        public void SetData(LevelGanarateData levelGanarateData)
        {
            _levelGanarateData = levelGanarateData;
        }

        private void Awake() => Init();

        private void Init() => _camera = Camera.main;

        private void Start() => SpawnStartWall();

        private void SpawnStartWall()
        {
            _lowerWall = PullFromPool(PoolObjectType.LowerFirstWall);

            _upperWall = PullFromPool(PoolObjectType.UpperFirstWall);

            _upperWall.transform.position = _lowerWall.transform.position + new Vector3(0, _levelGanarateData._distanceVerticalOfWall, 0);

            _stackLowerWall.AddFirst(_lowerWall);

            _stackUpperWall.AddFirst(_upperWall);

            for (int i = 0; i < _levelGanarateData.NumberOfSpawnWalLineCountOnStart; i++)
            {
                AddNextWall();
            }

            ManageRendering();
        }

        private async void ManageRendering()
        {
            while (true)
            {
                await Task.Delay(1);

                QuitTheCamara();
            }
        }

        private void QuitTheCamara()
        {
            if (_stackLowerWall.First.Value == null) return;

            if (!CanSeeOnCamera(_stackLowerWall.First.Value))
            {
                int poolOrderLowerWall = count % 2;

                int poolOrderUpperWall = poolOrderLowerWall + 2;

                PushToPool((PoolObjectType)poolOrderLowerWall, _stackLowerWall.First.Value);

                PushToPool((PoolObjectType)poolOrderUpperWall, _stackUpperWall.First.Value);

                _stackLowerWall.RemoveFirst();

                _stackUpperWall.RemoveFirst();

                AddNextWall();

                UpgradeScore();
            }
        }

        private void AddNextWall()
        {
            int poolOrderLowerWall = count % 2;

            int poolOrderUpperWall = poolOrderLowerWall + 2;

            GameObject _nextLowerWall = PullFromPool((PoolObjectType)poolOrderLowerWall);

            GameObject _nextUpperWall = PullFromPool((PoolObjectType)poolOrderUpperWall);

            count++;

            CalculateDistanceOfUpperToLoverWall();

            _nextLowerWall.transform.position = _stackLowerWall.Last.Value.transform.position +
                new Vector3(_levelGanarateData.XOffsetofWall, _levelGanarateData.YOffsetofWall, 0);

            if (_particalSpawnPointAsWall >= 25)
            {
                ParticalSignal.Instance.onSpawnPartical?.Invoke(_stackLowerWall.Last.Value.transform.position);

                _particalSpawnPointAsWall = 0;
            }
            else
            {
                _particalSpawnPointAsWall++;
            }

            _nextUpperWall.transform.position = _nextLowerWall.transform.position + new Vector3(0, _levelGanarateData._distanceVerticalOfWall, 0);

            _stackLowerWall.AddAfter(_stackLowerWall.Last, _nextLowerWall);

            _stackUpperWall.AddAfter(_stackUpperWall.Last, _nextUpperWall);
        }

        private void UpgradeScore()
        {
            levelManager.UpdateScorePerProgress();
        }

        private void CalculateDistanceOfUpperToLoverWall()
        {
            int randomNumberPercent = Random.Range(0, 100);

            if (randomNumberPercent <= _levelGanarateData.Posibility)
            {
                _levelGanarateData.YOffsetofWall = _levelGanarateData.AmountOfIncreaseRatePerNextWall;

                if (_levelGanarateData._distanceVerticalOfWall >= _levelGanarateData.MinVerticalDistanceLimit)
                    _levelGanarateData._distanceVerticalOfWall--;
            }
            else
            {
                _levelGanarateData.YOffsetofWall = -_levelGanarateData.AmountOfIncreaseRatePerNextWall;

                if (_levelGanarateData._distanceVerticalOfWall <= _levelGanarateData.MaxVerticalDistanceLimit)
                    _levelGanarateData._distanceVerticalOfWall++;
            }
        }

        public bool CanSeeOnCamera(GameObject wallGameObject)
        {
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_camera);

            return GeometryUtility.TestPlanesAABB(planes, wallGameObject.GetComponent<Collider>().bounds);
        }

        public GameObject PullFromPool(PoolObjectType poolObjectType)
        {
            return PoolSignals.Instance.onGetObjectFromPool?.Invoke(poolObjectType);
        }

        public void PushToPool(PoolObjectType poolObjectType, GameObject obj)
        {
            PoolSignals.Instance.onReleaseObjectFromPool?.Invoke(poolObjectType, obj);
        }

        internal void ResetGenareLevel()
        {
            //int i = 0;
            //while (true)
            //{
            //    await Task.Delay(1);
            //    if (_stackLowerWall.Count == 0) return;

            //    int  poolOrderLowerWall = i % 2;

            //    int poolOrderUpperWall = poolOrderLowerWall+2;

            //    PushToPool((PoolObjectType)poolOrderLowerWall, _stackLowerWall.Last.Value);

            //    PushToPool((PoolObjectType)poolOrderUpperWall, _stackUpperWall.Last.Value);

            //    _stackLowerWall.RemoveLast();

            //    _stackUpperWall.RemoveLast();

            //    i++;
            //}
        }
    }
}