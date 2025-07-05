/*
using System.Collections.Generic;
using UnityEngine;

public class GameProgressManager : MonoBehaviour
{
    public static GameProgressManager Instance;

    private HashSet<string> unlockedCharacters = new HashSet<string>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Desbloqueia o primeiro personagem por defeito
            UnlockCharacter("Kid");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UnlockCharacter(string name)
    {
        unlockedCharacters.Add(name);
        Debug.Log("Personagem desbloqueado: " + name);
    }

    public bool IsCharacterUnlocked(string name)
    {
        return unlockedCharacters.Contains(name);
    }

    public List<string> GetUnlockedCharacters()
    {
        return new List<string>(unlockedCharacters);
    }
} */