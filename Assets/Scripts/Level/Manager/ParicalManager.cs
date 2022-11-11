using Controller;
using Data.UnityObject;
using Data.ValueObject;
using Signals;
using UnityEngine;

namespace Managers
{
    public class ParicalManager : MonoBehaviour
    {
        [SerializeField]
        private ParticalSpawnController particalSpawnController;

        private string _dataPath = "Data/Cd_ParticalData";

        private void Awake() => Init();

        private void Init() => particalSpawnController.SetData(GetData());

        private ParticalData GetData() => Resources.Load<Cd_ParticalData>(_dataPath).ParticalData;

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents() => ParticalSignal.Instance.onSpawnPartical += OnSpawnPartical;

        private void UnsubscribeEvents() => ParticalSignal.Instance.onSpawnPartical -= OnSpawnPartical;

        private void OnDisable() => UnsubscribeEvents();

        private void OnSpawnPartical(Vector3 spawnPoint) => particalSpawnController.SpawnPartical(spawnPoint);
    }
}