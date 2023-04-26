using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Unity.VisualScripting;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public string ID;
    [HideInInspector]public  TMP_Text text;
    public Color correctTextColour;
    public Color transparrent;
    public bool doLerp;
    public bool fadeOut;
    float lerpSpeed = 1.5f;
    float lerpSpeedFADE = 5f;

    bool doOnce;

    private void Awake()
    {
        text = this.GetComponent<TMP_Text>();
        canvas = FindAnyObjectByType<BigBoyDialogue>().GetComponent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup= GetComponent<CanvasGroup>();
       // rightBtn = GetComponent
    }

    private void Start()
    {
        
    }
    private void Update()
    {

        if (doLerp)
        {
            text.color = Color.Lerp(text.color, correctTextColour, Time.deltaTime * lerpSpeed);
        }

        if (CompareColor(text.color, correctTextColour, 0.2f) && fadeOut)
        {
            doLerp = false;
            doOnce = true;

        }

        if (doOnce) 
        {
            text.color = Color.Lerp(text.color, transparrent, Time.deltaTime * lerpSpeedFADE);
        }
    }

    bool CompareColor(Color c1, Color c2, float epslon)
    {
        if (Mathf.Abs(c1.r - c2.r) <= epslon &&
            Mathf.Abs(c1.g - c2.g) <= epslon &&
            Mathf.Abs(c1.b - c2.b) <= epslon &&
            Mathf.Abs(c1.a - c2.a) <= epslon)
        {
            return true;
        }

        return false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }

    public void OnDrop(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

}