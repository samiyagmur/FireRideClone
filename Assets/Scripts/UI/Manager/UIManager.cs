using Controller;
using Signals;
using TMPro;
using Type;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI scoreText;

        [SerializeField]
        private TextMeshProUGUI lastScore;

        [SerializeField]
        private UIPanelController uIPanelController;

        private int _lastScore;

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onScoreReNew += OnScoreReNew;
            UISignals.Instance.onOpenPanel += OnOpenPanel;
            UISignals.Instance.onClosePanel += OnClosePanel;
            CoreGameSignals.Instance.onFail += OnLastScore;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onScoreReNew -= OnScoreReNew;
            UISignals.Instance.onOpenPanel -= OnOpenPanel;
            UISignals.Instance.onClosePanel -= OnClosePanel;
            CoreGameSignals.Instance.onFail -= OnLastScore;
        }

        private void OnDisable() => UnsubscribeEvents();

        private void OnOpenPanel(UIPanelType panelType) => uIPanelController.ChangePanel(panelType, true);

        private void OnClosePanel(UIPanelType panelType) => uIPanelController.ChangePanel(panelType, false);

        private void OnScoreReNew(int newScore)
        {
            scoreText.text = "SCORE: " + newScore.ToString();

            _lastScore = newScore;
        }

        private void OnLastScore()
        {
            OnClosePanel(UIPanelType.LevelPanel);

            lastScore.text = "SCORE:\n" + _lastScore.ToString();
        }

        public void StartButton()
        {
            OnClosePanel(UIPanelType.StartPanel);

            OnOpenPanel(UIPanelType.LevelPanel);

            CoreGameSignals.Instance.onPlay?.Invoke();
        }

        public void TryAgain()
        {
            scoreText.text = "SCORE: " + 0;

            lastScore.text = "SCORE:\n" + 0;

            OnClosePanel(UIPanelType.EndPanel);

            OnOpenPanel(UIPanelType.StartPanel);

            CoreGameSignals.Instance.onReset?.Invoke();
        }
    }
}