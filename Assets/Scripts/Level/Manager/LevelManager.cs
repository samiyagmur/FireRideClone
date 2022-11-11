using Controller;
using Data.UnityObject;
using Data.ValueObject;
using Signals;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour//Modules
    {
        [SerializeField]
        private LevelGanarateController levelGanarateController;

        private string _dataPath = "Data/Cd_LevelData";

        private void Awake() => Init();

        private void Init() => levelGanarateController.SetData(GetLevelData().LevelGanarateData);

        private LevelData GetLevelData() => Resources.Load<Cd_LevelData>(_dataPath).LevelData;

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents() => CoreGameSignals.Instance.onReset += OnReset;

        private void UnsubscribeEvents() => CoreGameSignals.Instance.onReset -= OnReset;

        private void OnDisable() => UnsubscribeEvents();

        internal void UpdateScorePerProgress() => ScoreSignals.Instance.onScoreTaken?.Invoke();

        private void OnReset()
        {
            levelGanarateController.gameObject.SetActive(false);
            levelGanarateController.gameObject.SetActive(true);

            levelGanarateController.ResetGenareLevel();
        }
    }
}