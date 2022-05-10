using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
public class ImageScanMode : MonoBehaviour
{
    [SerializeField] ARTrackedImageManager imageManager;
    // Start is called before the first frame update
    void Start()
    {
        UIController.ShowUI("Scan");
    }

    // Update is called once per frame
    void Update()
    {
        if(imageManager.trackables.count > 0)
        {
            InteractionController.EnableMode("Main");
        }
        else
        {
            InteractionController.EnableMode("Scan");
        }
    }
}
