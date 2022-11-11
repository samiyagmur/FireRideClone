using Extantions;
using UnityEngine.Events;

namespace Signals
{
    public class InputSignals : MonoSingleton<InputSignals>
    {
        public UnityAction onInputTouch = delegate { };
        public UnityAction onInputReleased = delegate { };
    }
}