
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;
[System.Serializable]
public class UIPanelDictionary : SerializableDictionaryBase<string, CanvasGroup> { }
public class UIController : Singleton<UIController>
{
    [SerializeField] UIPanelDictionary uiPanels;
    CanvasGroup currentPanel;
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        ResetAllUI();
    }

    // Update is called once per frame
 
    void ResetAllUI()
    {
        foreach (CanvasGroup panel in uiPanels.Values)
        {
            panel.gameObject.SetActive(false);
        }
    }
    public static void ShowUI(string name)
    {
        Instance._ShowUI(name);
    }

    void _ShowUI(string name)
    {
        CanvasGroup panel;
        if(uiPanels.TryGetValue(name, out panel))
        {
            
            ChangeUI(panel);

        }
        else
        {
            Debug.LogError("Undefined ui panel " + name);
        }
    }
    void ChangeUI(CanvasGroup panel)
    {
        if(panel == currentPanel)
        {
            return;
        }
        if (currentPanel)
        {
            currentPanel.gameObject.SetActive(false);
        }
      
        currentPanel = panel;
        if (panel)
        {
            panel.gameObject.SetActive(true);
        }
    }
}
