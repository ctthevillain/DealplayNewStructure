using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using VIDE_Data;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(ChoiceProperties))]
public class Draggable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas Canvas;
    [SerializeField] MultipleChoiceMananger choiceMananger;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Dropable dropableObject;// current dropable that holds thos draggable
    private Vector3 originPosition;
    private ChoiceProperties choiceProperties;
    private Animator animator;
    public TMP_Text tMP_Text;


    private void Awake()
    {
        originPosition = transform.position;
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        choiceProperties = GetComponent<ChoiceProperties>();
        animator = GetComponent<Animator>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {

        choiceMananger.ActivateTempButton(true);
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = .6f;

    }

    public void OnDrag(PointerEventData eventData)
    {

        rectTransform.anchoredPosition = eventData.position;
        //transform.position = new Vector3(transform.position.x + eventData.delta.x, transform.position.z, transform.position.z + eventData.delta.y);
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        choiceMananger.ActivateTempButton(false);
        if (dropableObject == null && eventData.pointerEnter != null && eventData.pointerEnter.GetComponent<Dropable>() != null)
        {
            dropableObject = eventData.pointerEnter.GetComponent<Dropable>();
            gameObject.transform.position =
            dropableObject.GetComponent<Transform>().position;
            dropableObject.SetChoicePrperties(choiceProperties);
            choiceMananger.CheckAllCorrectAnswers();
            //play next node value
            int currentIndex = choiceMananger.Draggables.IndexOf(this);
            VD.nodeData.commentIndex = currentIndex;
            choiceMananger.OnButtonDropped(currentIndex);
            VD.Next();

        }
        else
        {
            transform.position = originPosition;

            dropableObject?.SetChoicePrperties(null);
            dropableObject = null;

            choiceMananger.SetResult("");
        }
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
        Debug.Log("DragEnd");
    }
    public void SetDropable(Dropable dropable)
    {
        dropableObject = dropable;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // on hover should overlay other buttons
        transform.SetAsLastSibling();
        animator.SetBool("Highlighted", true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("hittttttt");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetBool("Normal", true);
    }
}
