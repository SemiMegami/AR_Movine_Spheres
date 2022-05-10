using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;
[System.Serializable]
public class InteractionModeDictionary : SerializableDictionaryBase<string, GameObject> { }
public class InteractionController : Singleton<InteractionController>
{
    [SerializeField] InteractionModeDictionary interactionMode;
    GameObject currentMode;
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        ResetAllMode();
    }
    private void Start()
    {
        _EnableMode("Startup");
    }

    void ResetAllMode()
    {
        foreach (GameObject mode in interactionMode.Values)
        {
            mode.SetActive(false);
        }
    }

    public static void EnableMode(string name)
    {
       
        Instance?._EnableMode(name);
    }

    void _EnableMode(string name)
    {
      
        GameObject modeObject;
        if(interactionMode.TryGetValue(name, out modeObject))
        {
            StartCoroutine(ChangeMode(modeObject));
        }
        else
        {
            Debug.LogError("Undefind mode named " + name);
        }
    }
    IEnumerator ChangeMode(GameObject mode)
    {
        if(mode == currentMode)
        {
            yield break;
        }
        if (currentMode)
        {
            currentMode.SetActive(false);
            yield return null;
        }
        currentMode = mode;
        mode.SetActive(true);
    }
}
