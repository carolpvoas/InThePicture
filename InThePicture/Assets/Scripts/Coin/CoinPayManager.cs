using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinPayManager : MonoBehaviour
{
    public int requiredCoins = 3;
    private int currentCoins = 0;
    public string nextSceneName = "NextScene";

    public void AddCoin()
    {
        currentCoins++;
        if (currentCoins >= requiredCoins)
        {
            Debug.Log("Todas as moedas colocadas!");
            SceneManager.LoadScene(nextSceneName);
        }
    }
}