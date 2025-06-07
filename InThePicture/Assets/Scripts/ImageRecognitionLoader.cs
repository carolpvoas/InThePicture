using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.SceneManagement;

public class ImageRecognitionLoader : MonoBehaviour
{
    private ARTrackedImageManager trackedImageManager;
    private float timeSinceStart = 0f;
    private bool canLoadScene = false;
    
    [SerializeField]
    private string targetImageName = "TheBus"; 
    // nome da imagem na ReferenceImageLibrary
    
    [SerializeField]
    public string sceneToLoad = "Scene2-Dialogue"; 
    // nome da cena que quero carregar (ainda por definir, mas provavelmente) vai ser "Scene1"
    // Aqui dizemos qual é a cena que o jogo deve abrir quando a imagem for reconhecida.
    
    private bool sceneLoaded = false; 
    // Previne carregar várias vezes
    
    private void Update()
    {
        timeSinceStart += Time.deltaTime;

        // Só deixamos mudar de cena após 3 segundos
        if (timeSinceStart > 3f)
            canLoadScene = true;
    }
    private void Awake()
    {
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();
    }
    // Isto corre logo no início, quando a cena começa.
    // Estamos a dizer: “Procura o ARTrackedImageManager que já existe na cena e guarda-o aqui”.
    
    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }
    // Aqui estamos a dizer:
    // “Quando houver alguma imagem nova ou atualizada, chama a função OnTrackedImagesChanged”.
    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }
    // E isto desliga a função, quando o objeto for desativado. Só para segurança.
    
    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        // Esta função é chamada sempre que o sistema vê uma imagem nova ou vê que uma imagem está a mudar.
        
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            CheckImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            CheckImage(trackedImage);
        }
        
        // nestes dois foreach estamos a dizer:
        // “Olha para todas as imagens novas ou atualizadas, e vê se alguma é a imagem que queremos”.
    }

    
    private void CheckImage(ARTrackedImage trackedImage)
    {
        // Esta função vê se a imagem que o Unity detetou é a imagem certa e se está a ser bem seguida pela câmara.
        
        Debug.Log("Imagem detetada: " + trackedImage.referenceImage.name + " | Estado: " + trackedImage.trackingState);
        
        if (!sceneLoaded &&
            trackedImage.referenceImage.name == targetImageName &&
            trackedImage.trackingState == TrackingState.Tracking)
        {
            Debug.Log("Imagem reconhecida: " + targetImageName);
            sceneLoaded = true;
            SceneManager.LoadScene(sceneToLoad);
            
        }
        // Verifica duas coisas:
        // 1. É a imagem certa? (nome igual)
        // 2. Está visível e a ser seguida pela câmara?
        // Se sim...
        // "SceneManager.LoadScene(sceneToLoad);"
        // Muda de cena para o jogo que queremos iniciar.
   }
    
    // Em resumo:
    // Quando a câmara reconhece a imagem "The-Bus", o jogo muda automaticamente para outra cena chamada
    // neste caso "Scene1"
}

