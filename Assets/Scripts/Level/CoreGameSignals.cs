using Extantions;
using UnityEngine.Events;

namespace Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {
        public UnityAction<int> onScoreReNew = delegate { };

        public UnityAction onPlay = delegate { };

        public UnityAction onReset = delegate { };

        public UnityAction onFail = delegate { };

        public UnityAction onChangeLevel = delegate { };
    }
}