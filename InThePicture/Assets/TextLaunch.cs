using UnityEngine;
using UnityEngine.UI;

public class TextLaunch : MonoBehaviour
{
    public GameObject uiPanel;

    void Start()
    {
        // UI is shown when AR detects the tracked image
        uiPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnStartGame()
    {
        uiPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}