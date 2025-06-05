using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ImageRecognitionManager : MonoBehaviour
{
    [SerializeField] 
    private ARTrackedImageManager trackedImageManager;

    public GameObject canvasUI;

    void Awake()
    {
        //Debug.Log("Lala");
        /*if (trackedImageManager == null)
        {
            trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
            Debug.Log("Assigned ARTrackedImageManager dynamically: " + trackedImageManager);
        }*/
    }

    void Update()
    {
        //Debug.Log("Hello");
       /* if (Input.GetKeyDown("space"))
        {
            Debug.Log("Hello");
            //canvasUI.SetActive(true);
        }*/
    }

    void OnEnable()
    {
        //Debug.Log("Lala");
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        canvasUI.SetActive(true);
        Debug.Log("Lala");

        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            HandleImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            if (trackedImage.trackingState == TrackingState.Tracking)
                HandleImage(trackedImage);
        }
    }

    private void HandleImage(ARTrackedImage image)
    {
        canvasUI.SetActive(true);
        string imageName = image.referenceImage.name;
        Debug.Log("Lalala: " + imageName);

        if (imageName == "Image")
        {
            Debug.Log("Picture recognized!");
            // Example: load a 2D game scene
           // SceneManager.LoadScene("My2DGameScene");
        }
    }
}