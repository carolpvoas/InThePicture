using UnityEngine; 
using UnityEngine.UI;

public class CharacterSelectionManager : MonoBehaviour
{
    public GameObject[] characters;   // Personagens para selecionar
    public Button leftArrowButton;
    public Button rightArrowButton;
    public Button confirmButton;
    public DialogueSystem dialogueSystem;

    private int currentIndex = 0;

    void Start()
    {
        UpdateCharacterVisibility();

        leftArrowButton.onClick.AddListener(SelectPrevious);
        rightArrowButton.onClick.AddListener(SelectNext);
        confirmButton.onClick.AddListener(ConfirmSelection);
    }

    void SelectNext()
    {
        Debug.Log("SelectNext called!");
        currentIndex++;
        if (currentIndex >= characters.Length) currentIndex = 0;
        UpdateCharacterVisibility();
    }

    void SelectPrevious()
    {
        Debug.Log("SelectPrevious called!");
        currentIndex--;
        if (currentIndex < 0) currentIndex = characters.Length - 1;
        UpdateCharacterVisibility();
    }

    void UpdateCharacterVisibility()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].SetActive(i == currentIndex);
        }
    }

    public void ConfirmSelection()
    {
        Debug.Log("Personagem Selecionada: " + characters[currentIndex].name);
        PlayerPrefs.SetInt("SelectedCharacter", currentIndex);

        if (dialogueSystem != null)
        {
            dialogueSystem.StartDialogueWithCharacter(currentIndex);
        }
        else
        {
            Debug.LogWarning("DialogueSystem não setado no Inspector!");
        }
    }
}