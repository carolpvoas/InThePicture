using UnityEngine; 
using UnityEngine.UI;
using TMPro;

public class CharacterSelectionManager : MonoBehaviour
{
    public GameObject[] characters;   // Personagens para selecionar
    public Button leftArrowButton;
    public Button rightArrowButton;
    public Button confirmButton;
    public DialogueSystem dialogueSystem;

    public TextMeshProUGUI nameLabel;
    public string[] characterNames;
    
    public GameObject childPrompt; 

    private int currentIndex = 0;

    void Start()
    {
        leftArrowButton.onClick.AddListener(SelectPrevious);
        rightArrowButton.onClick.AddListener(SelectNext);
        confirmButton.onClick.AddListener(ConfirmSelection);

        UpdateCharacterVisibility();
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

        if (characterNames != null && currentIndex < characterNames.Length)
        {
            nameLabel.text = characterNames[currentIndex];
        }
        else
        {
            nameLabel.text = "";
        }
    }



    public void ConfirmSelection()
    {
        Debug.Log("Personagem Selecionada: " + characters[currentIndex].name);
        PlayerPrefs.SetInt("SelectedCharacter", currentIndex);
        
        leftArrowButton.gameObject.SetActive(false);
        rightArrowButton.gameObject.SetActive(false);
        confirmButton.gameObject.SetActive(false);
        childPrompt.SetActive(false); 

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