using Extantions;
using Type;
using UnityEngine.Events;

namespace Signals
{
    public class UISignals : MonoSingleton<UISignals>
    {
        public UnityAction<UIPanelType> onOpenPanel = delegate { };
        public UnityAction<UIPanelType> onClosePanel = delegate { };
    }
}