using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
public class ScanMode : MonoBehaviour
{
    [SerializeField] ARPlaneManager planeManager;

    private void OnEnable()
    {
        UIController.ShowUI("Scan");
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(planeManager.trackables.count > 0)
        {
            InteractionController.EnableMode("Main");
        }
    }
}
