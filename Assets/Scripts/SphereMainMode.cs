using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using UnityEngine.XR.ARSubsystems;
public class SphereMainMode : MonoBehaviour
{
    [SerializeField] ARTrackedImageManager imageManager;
    [SerializeField] GameObject spawnObject;
  
    
    private void Update()
    {

     
        CheckStages();
    }

    private void OnEnable()
    {
        UIController.ShowUI("Main");
        imageManager.trackedImagesChanged += OnTrackedImageChanged;
        InvokeRepeating("MoveObjects",0, 2);
    }

    private void OnDisable()
    {
        CancelInvoke();
        imageManager.trackedImagesChanged -= OnTrackedImageChanged;
    }

    void OnTrackedImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {

        foreach (ARTrackedImage image in eventArgs.added)
        {
            InstantiateObject(image);
        }
        foreach (ARTrackedImage image in eventArgs.updated)
        {
            InstantiateObject(image);
        }

    }

    void InstantiateObject(ARTrackedImage image)
    {
        if (image.trackingState == TrackingState.Tracking)
        {
            if (image.transform.childCount == 0)
            {
                GameObject ball = Instantiate(spawnObject);
                Shader shader = ball.GetComponent<MeshRenderer>().sharedMaterial.shader;
                ball.transform.SetParent(image.transform, false);
                Material material = new Material(shader);

                material.SetColor("_Color", new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f)));
                ball.GetComponent<MeshRenderer>().sharedMaterial = material;
            }
        }
    }



    void CheckStages()
    {
        int count = 0;
        foreach(var image in imageManager.trackables)
        {
            if (image.trackingState != TrackingState.Tracking)
            {
                if (image.transform.childCount > 0)
                {
                    Destroy(image.transform.GetChild(0).gameObject);
                }
            }
            else
            {
                count++;
            }
        }
        if(count == 0)
        {
            InteractionController.EnableMode("Scan");
        }
    }

    void MoveObjects()
    {
        List<ARTrackedImage> images = new List<ARTrackedImage>();

        foreach (var image in imageManager.trackables)
        {
            if (image.trackingState == TrackingState.Tracking)
            {
                images.Add(image);
            }
        }
        int n = images.Count;
        for (int i = 0; i < n; i++)
        {
            int j = i + 1;
            if (j == n) j = 0;

            GameObject ball = images[i].transform.GetChild(0).gameObject;
            ball.transform.SetParent(images[j].transform,true);
            StartCoroutine(MoveBallToParent(ball.transform, 0.2f));
        }
    }

    IEnumerator MoveBallToParent(Transform t, float totalTime)
    {
        Vector3 position = t.localPosition;
        float time = 0;
        while (time < totalTime)
        {
            time += Time.deltaTime;
            if (time >= totalTime) time = totalTime;
            t.localPosition = Vector3.Lerp(position, Vector3.zero,time / totalTime);
            yield return null;
        }
    }
}
