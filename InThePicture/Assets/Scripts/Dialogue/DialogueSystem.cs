using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    [System.Serializable]
    public class SpeakerEntry
    {
        public string name;          
        public GameObject gameObject; 
    }
    

    public List<SpeakerEntry> speakers;  // lista editável no Inspector

    private Dictionary<string, GameObject> speakerDict;

    private int index;
    private bool isDialogueActive = false;  // controla se o diálogo está ativo
    
    void Start()
    {
        speakerDict = new Dictionary<string, GameObject>(); // monta o dicionário para facilitar o acesso
        foreach (var s in speakers)
        {
            if (!speakerDict.ContainsKey(s.name))
                speakerDict.Add(s.name, s.gameObject);
        }
        
        dialogueText.text = string.Empty;
        nameText.text = string.Empty;
        isDialogueActive = false;   // no inicio diálogo desligado
        
        if (panel != null) panel.SetActive(false); // desativar o painel logo ao iniciar
        
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

    void HighlightSpeaker(string currentSpeaker)
    {
        if (string.IsNullOrEmpty(currentSpeaker))
        {
            foreach (var go in speakerDict.Values)
                SetSpeakerState(go, false);
            return;
        }

        foreach (var kvp in speakerDict)
        {
            bool isActive = (kvp.Key == currentSpeaker);
            SetSpeakerState(kvp.Value, isActive);
        }
    }

    void SetSpeakerState(GameObject obj, bool isActive)
    {
        var sr = obj.GetComponent<SpriteRenderer>();
        if (sr == null)
        {
            Debug.LogWarning("SpriteRenderer não encontrado em " + obj.name);
            return;
        }

        if (isActive)
        {
            sr.color = Color.white; // cor normal
            sr.sortingOrder = 0;   // traz para frente
        }
        else
        {
            sr.color = new Color(0.5f, 0.5f, 0.5f, 1f); // "sombra"
            sr.sortingOrder = 0;    // joga para trás
        }
    }



    IEnumerator TypeLine()
    {
        dialogueText.text = "";
        nameText.text = dialogueLines[index].speakerName;
        
        HighlightSpeaker(dialogueLines[index].speakerName); // destaca o personagem atual
        
        foreach (char c in dialogueLines[index].line.ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
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