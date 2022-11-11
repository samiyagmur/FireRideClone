using Controller;
using Data.UnityObject;
using Data.ValueObject;
using Signals;
using Type;
using UnityEngine;

namespace Managers
{
    public class PendulumManager : MonoBehaviour
    {
        [SerializeField]
        private PendulumMovementController movementController;

        private int _isFirstTouch = 0;

        private string _dataPath = "Data/Cd_PendulumData";

        private void Awake() => Init();

        private void Init() => movementController.SetData(GetData().PendulumMovementData);

        private PendulumData GetData() => Resources.Load<Cd_PendulumData>(_dataPath).PendulumData;

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputTouch += OnInputTouch;

            InputSignals.Instance.onInputReleased += OnInputReleased;

            CoreGameSignals.Instance.onPlay += OnPlay;

            CoreGameSignals.Instance.onReset += OnReset;
        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputTouch -= OnInputTouch;

            InputSignals.Instance.onInputReleased -= OnInputReleased;

            CoreGameSignals.Instance.onPlay -= OnPlay;

            CoreGameSignals.Instance.onReset -= OnReset;
        }

        private void OnDisable() => UnsubscribeEvents();

        private void OnInputTouch()
        {
            if (_isFirstTouch == 0)
            {
                movementController.AddSpringJoint();
            }

            _isFirstTouch++;

            movementController.ConnectToRoof();
        }

        private void OnInputReleased()
        {
            _isFirstTouch = 0;
            movementController.DisConnectToRoof();
        }

        internal void WhenHitPartical(int takenScoreToPartical) => ScoreSignals.Instance.onScoreMultiply?.Invoke(takenScoreToPartical);

        internal void WhenHitWall()
        {
            UISignals.Instance.onOpenPanel(UIPanelType.EndPanel);

            CoreGameSignals.Instance.onFail?.Invoke();
        }

        internal void ChangeLevelColor() => CoreGameSignals.Instance.onChangeLevel?.Invoke();

        private void OnPlay() => movementController.gameObject.SetActive(true);

        private void OnReset()
        {
            movementController.transform.position += GetData().ResetYPos;

            movementController.gameObject.SetActive(false);
        }
    }
}