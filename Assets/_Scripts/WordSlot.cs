using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class WordSlot : MonoBehaviour, IDropHandler
{
    public string ID;
    public DragAndDrop other;
    public DialogueController2 controller;
    [HideInInspector] public bool misisonComplete;
    bool draggedCorrectDialogue;

    private void Update()
    {
        if (draggedCorrectDialogue && !misisonComplete)
        {
            controller.levelCompleted = true;
            misisonComplete = true;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            other = eventData.pointerDrag.GetComponent<DragAndDrop>();
            if (other.ID == ID)
            {
                other.doLerp = true;
                draggedCorrectDialogue = true;
                controller.NextSentence();
            }
        }
    }

    public IEnumerator FadeOut(float time, TMP_Text text)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime / time));
            yield return null;
        }
    }
}
