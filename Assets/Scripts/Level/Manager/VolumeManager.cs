using Signals;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Managers
{
    public class VolumeManager : MonoBehaviour
    {
        [SerializeField]
        private Volume volume;

        private void Start() => volume = GetComponent<Volume>();

        private void OnEnable() => SubscribeEvents();

        private void SubscribeEvents() => CoreGameSignals.Instance.onChangeLevel += OnChangeLevel;

        private void UnsubscribeEvents() => CoreGameSignals.Instance.onChangeLevel += OnChangeLevel;

        private void OnDisable() => UnsubscribeEvents();

        private void OnChangeLevel()
        {
            if (volume.profile.TryGet(out ColorAdjustments companents))
            {
                companents.hueShift.value = Random.Range(-180, +180);
            }
        }
    }
}