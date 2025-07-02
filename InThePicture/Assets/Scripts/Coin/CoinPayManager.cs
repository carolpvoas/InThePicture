using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinPayManager : MonoBehaviour
{
    public int requiredValue = 10; // Valor necessário para passar de nível
    private int currentValue = 0;
    public string nextSceneName = "NextScene";

    public void AddCoinValue(int coinValue)
    {
        currentValue += coinValue;
        Debug.Log("Valor atual: " + currentValue);

        if (currentValue >= requiredValue)
        {
            Debug.Log("Valor suficiente atingido! Passando de nível...");
            SceneManager.LoadScene(nextSceneName);
        }
    }
}