using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class PlaceObjectsOnPlane : MonoBehaviour
{
    [SerializeField] GameObject placePrefab;
    GameObject spawnedObject;
    ARRaycastManager raycaster;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    // Start is called before the first frame update
    void Start()
    {
        raycaster = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnPlaceObject(InputValue value)
    {
        Vector2 touchPosition = value.Get<Vector2>();
        
        
        if (raycaster.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;
            if(spawnedObject == null)
            {
                spawnedObject = Instantiate(placePrefab, hitPose.position, hitPose.rotation);
            }
            else
            {
                spawnedObject.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
            }
        }
    }
}
