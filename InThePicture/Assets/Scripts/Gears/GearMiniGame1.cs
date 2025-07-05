using UnityEngine;
using UnityEngine.SceneManagement;

public class GearMiniGame1 : MonoBehaviour
{
    [Tooltip("Arraste aqui todas as engrenagens da cena")]
    public GearsScript[] gears;

    [Tooltip("Nome da próxima cena para carregar quando todas as engrenagens estiverem corretas")]
    public string nextSceneName = "Scene4-Dialogue2";

    private bool sceneLoaded = false;

    void LateUpdate()
    {
        if (!sceneLoaded && AllGearsPlacedCorrectly())
        {
            Debug.Log("Todas as engrenagens estão corretamente colocadas!");
            sceneLoaded = true;
            SceneManager.LoadScene(nextSceneName);
        }
    }

    bool AllGearsPlacedCorrectly()
    {
        foreach (GearsScript gear in gears)
        {
            if (!gear.IsPlacedCorrectly())
                return false;
        }
        //GameProgressManager.Instance.UnlockCharacter("Worker");
        return true;
    }
}