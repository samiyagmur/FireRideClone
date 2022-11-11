using Data.UnityObject;
using Signals;
using UnityEngine;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField]
        private int _currentScore;

        private string _dataPath = "Data/Cd_ScoreData";

        public Cd_ScoreData GetData() => Resources.Load<Cd_ScoreData>(_dataPath);

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            ScoreSignals.Instance.onScoreMultiply += OnScoreMultiply;
            ScoreSignals.Instance.onScoreTaken += OnScoreTaken;
            CoreGameSignals.Instance.onReset += OnReset;
        }

        private void UnsubscribeEvents()
        {
            ScoreSignals.Instance.onScoreMultiply -= OnScoreMultiply;
            ScoreSignals.Instance.onScoreTaken -= OnScoreTaken;
            CoreGameSignals.Instance.onReset -= OnReset;
        }

        private void OnDisable() => UnsubscribeEvents();

        private void OnScoreTaken()
        {
            _currentScore++;

            CoreGameSignals.Instance.onScoreReNew?.Invoke(_currentScore);
        }

        private void OnScoreMultiply(int score) => _currentScore *= score;

        private void OnReset() => _currentScore = 0;
    }
}