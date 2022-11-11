using Extantions;
using UnityEngine.Events;

namespace Signals
{
    public class ScoreSignals : MonoSingleton<ScoreSignals>
    {
        public UnityAction<int> onScoreMultiply = delegate { };

        public UnityAction onScoreTaken = delegate { };
    }
}