using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public CanvasGroup fadeCanvas;
    public TextMeshProUGUI tutorialText;
    public Image tutorialImage;

    public float fadeDuration = 1f;
    public float messageDuration = 2f;

    public static SceneTransitionManager Instance;
    private static bool hasInitialized = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            if (!hasInitialized)
            {
                fadeCanvas.alpha = 0f;
                hasInitialized = true;
            }
        }
        
        else
        {
            Destroy(gameObject);
            return;
        }
        
        fadeCanvas.gameObject.SetActive(true);
    }

    public void ChangeSceneWithTutorial(string sceneName, string text = "", Sprite image = null)
    {
        StartCoroutine(FadeAndLoadScene(sceneName, text, image));
    }

    IEnumerator FadeAndLoadScene(string sceneName, string text, Sprite image)
    {
        // Mostrar elementos se houver
        if (!string.IsNullOrEmpty(text))
        {
            tutorialText.text = text;
            tutorialText.gameObject.SetActive(true);
        }
        else
        {
            tutorialText.gameObject.SetActive(false);
        }

        if (image != null)
        {
            tutorialImage.sprite = image;
            tutorialImage.gameObject.SetActive(true);
        }
        else
        {
            tutorialImage.gameObject.SetActive(false);
        }

        // Ativa bloqueio de interação antes do fade in
        fadeCanvas.blocksRaycasts = true;
        fadeCanvas.interactable = true;

        // Fade in (escurecer)
        yield return StartCoroutine(Fade(0, 1));

        // Esperar para mostrar o tutorial
        yield return new WaitForSeconds(messageDuration);

        // Carregar nova cena
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Fade out (voltar à cena)
        yield return StartCoroutine(Fade(1, 0));

        // Esconder os elementos
        tutorialText.gameObject.SetActive(false);
        tutorialImage.gameObject.SetActive(false);

        // Desativa bloqueio de interação após fade out
        fadeCanvas.blocksRaycasts = false;
        fadeCanvas.interactable = false;
    }


    IEnumerator Fade(float from, float to)
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            fadeCanvas.alpha = Mathf.Lerp(from, to, elapsed / fadeDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        fadeCanvas.alpha = to;
    }
}