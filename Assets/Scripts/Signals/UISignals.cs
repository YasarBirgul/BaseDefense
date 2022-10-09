using Extentions;
using UnityEngine.Events;

public class UISignals : MonoSingleton<UISignals>
{
    
    public UnityAction<UIPanels> onOpenPanel=delegate {  };
    public UnityAction<UIPanels> onClosePanel=delegate {  };
    public UnityAction<int> onSetMoneyScoreText = delegate {  }; 
    public UnityAction<int> onSetGemScoreText = delegate {  }; 
}