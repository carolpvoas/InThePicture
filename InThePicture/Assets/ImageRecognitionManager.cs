using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageRecognitionManager : MonoBehaviour
{
    public ARTrackedImageManager trackedImageManager;
    public GameObject uiCanvasPrefab;

    void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (ARTrackedImage trackedImage in args.added)
        {
            // Instantiate UI as child of tracked image
            GameObject ui = Instantiate(uiCanvasPrefab, trackedImage.transform);
            ui.transform.localPosition = Vector3.zero;
            ui.transform.localRotation = Quaternion.identity;
            ui.transform.localScale = Vector3.one * 0.01f; // Adjust scale
        }
    }
}