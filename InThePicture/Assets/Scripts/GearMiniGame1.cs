using UnityEngine;
using UnityEngine.SceneManagement;

public class GearMiniGame1 : MonoBehaviour
{
    public Gears[] gears;
    private bool sceneLoaded = false;

    void Update()
    {
        if (!sceneLoaded && AllGearsPlaced())
        {
            Debug.Log("Parabéns! Todas as engrenagens estão no lugar.");
            sceneLoaded = true;
            LoadNextScene();
        }
    }

    bool AllGearsPlaced()
    {
        foreach (Gears gear in gears)
        {
            Debug.Log($"{gear.name} placedCorrectly: {gear.placedCorrectly}");
            if (!gear.placedCorrectly)
                return false;
        }
        return true;
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene("Scene4-Dialogue2");
    }
    
    
}