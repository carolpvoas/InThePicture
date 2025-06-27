using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public DialogueLine[][] dialoguesByCharacter; // Array de diálogos para cada personagem
    public DialogueSystem dialogueSystem;         // Referência ao seu sistema de diálogo na cena

    private int selectedCharacterIndex = 0;

    public Image characterImage;  // Se tiver uma imagem para mostrar a personagem
    public Text characterNameText; // Se quiser mostrar nome da personagem na seleção

    // Opcional: sprites ou nomes dos personagens para mostrar na UI
    public Sprite[] characterSprites;
    public string[] characterNames;

    void Start()
    {
        UpdateUI();
    }

    public void SelectNextCharacter()
    {
        selectedCharacterIndex++;
        if (selectedCharacterIndex >= dialoguesByCharacter.Length)
            selectedCharacterIndex = 0;

        UpdateUI();
    }

    public void SelectPreviousCharacter()
    {
        selectedCharacterIndex--;
        if (selectedCharacterIndex < 0)
            selectedCharacterIndex = dialoguesByCharacter.Length - 1;

        UpdateUI();
    }

    void UpdateUI()
    {
        if (characterImage != null && characterSprites != null && characterSprites.Length > selectedCharacterIndex)
        {
            characterImage.sprite = characterSprites[selectedCharacterIndex];
        }

        if (characterNameText != null && characterNames != null && characterNames.Length > selectedCharacterIndex)
        {
            characterNameText.text = characterNames[selectedCharacterIndex];
        }
    }

    public void StartDialogueWithSelected()
    {
        dialogueSystem.StartDialogueWithCharacter(selectedCharacterIndex);
    }
}