using UnityEngine;
using UnityEngine.UIElements;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueSystem dialogueSystem; // arrastar o sistema de diálogo aqui
    public DragBoy childBoy;
    private bool hasTriggered = false;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.gameObject.CompareTag("Boy")) //perguntar ao professor se isto pode ser problema ou nao por ter um "ç" na tag
                                                                     //sendo que ha teclados que nem sequer conhecem este caracte
                                                                    //Resposta.... sim... trava tudo
        {
            hasTriggered = true;
            dialogueSystem.StartForcedDialogue(); // método personalizado para começar o diálogo
            
            if (childBoy != null)
            {
                childBoy.ResetPosition();
            }
            else
            {
                Debug.LogWarning("DragBoy não está ligado no Inspector!");
            }
        }
    }
}