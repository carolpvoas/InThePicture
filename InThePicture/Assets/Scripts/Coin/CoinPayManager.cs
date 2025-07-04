using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinPayManager : MonoBehaviour
{
    public int requiredCoins = 10; //valor necessario para passar de lvl
    private int currentCoins = 0;
    public string nextSceneName = "NextScene";

    // Novo: controle do offset para empilhamento visível
    private Vector2 currentOffset = Vector2.zero;
    public Vector2 offsetStep = new Vector2(10f, -10f); // cada nova moeda move-se ligeiramente

    public Vector2 GetNextOffset()
    {
        Vector2 thisOffset = currentOffset;
        currentOffset += offsetStep; // aumenta para a próxima moeda
        return thisOffset;
    }

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