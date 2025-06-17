using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    public TextMeshProUGUI nameText;        // novo campo para o nome da personagem
    public TextMeshProUGUI dialogueText;    // texto da fala
    //public TextMeshProUGUI textComponent;  escrever no report depois
    public GameObject panel;
    public DialogueLine[] dialogueLines;    
    public string nextSceneName;
    public float textSpeed = 0.05f;
    public bool startOnAwake = false;


    [System.Serializable]
    public class DialogueLine
    {
        public string speakerName;
        [TextArea(2, 5)]
        public string line;
    }

    private int index;
    private bool isDialogueActive = false;  // controla se o diálogo está ativo
    
    void Start()
    {
        dialogueText.text = string.Empty;
        nameText.text = string.Empty;
        isDialogueActive = false;   // no inicio diálogo desligado
        
        if (panel != null) panel.SetActive(false); // desativar o painel logo ao iniciar
        
        //StartDialogue();
        
        if (startOnAwake)
        {
            StartForcedDialogue();
        }
    }

    void Update()
    {
        if (!isDialogueActive) return;  // se o diálogo não estiver ativo, ignora cliques

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (dialogueText.text == dialogueLines[index].line)
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[index].line;
            }
        }
    }

//    void StartDialogue()
//    {
//        index = 0; 
//        StartCoroutine(TypeLine());
//    }

    public void StartForcedDialogue()
    {
        index = 0;
        isDialogueActive = true;    // marca o diálogo como ativo
        gameObject.SetActive(true); // ativar o sistema de diálogo
        
        if (panel != null) 
            panel.SetActive(true); // ativa o painel
        
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        dialogueText.text = "";
        nameText.text = dialogueLines[index].speakerName;
        
        foreach (char c in dialogueLines[index].line.ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        //Type each character 1 by 1 para parecer mais um jogo

    }

    void NextLine()
    {
        if (index < dialogueLines.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            nameText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            isDialogueActive = false;  // diálogo acabou, marca como inativo
            dialogueText.text = string.Empty;
            nameText.text = string.Empty;
            
            if (panel != null) panel.SetActive(false); // desativa o painel
            
            if (!string.IsNullOrEmpty(nextSceneName))
            {
                SceneManager.LoadScene(nextSceneName);
            }
            
            //SceneManager.LoadScene("Scene3-Gears");
            //Adicionar mais scenes
            //Scene5-Bus
        }
        
    }
    
}
