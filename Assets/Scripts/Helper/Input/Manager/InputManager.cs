using Signals;
using UnityEngine;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        private bool _isFirstTouchTaken;

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onFail += OnFail;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onFail -= OnFail;
        }

        private void OnDisable() => UnsubscribeEvents();

        private void Update()
        {
            if (_isFirstTouchTaken)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    InputSignals.Instance.onInputTouch?.Invoke();
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    InputSignals.Instance.onInputReleased?.Invoke();
                }
            }
        }

        private void OnPlay() => StartToInput();

        private void OnFail() => StopToInput();

        private void StartToInput() => _isFirstTouchTaken = true;

        private void StopToInput() => _isFirstTouchTaken = false;
    }
}