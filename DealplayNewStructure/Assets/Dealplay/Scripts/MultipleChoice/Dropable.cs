using UnityEngine;
using UnityEngine.EventSystems;
public class Dropable : MonoBehaviour, IDropHandler
{
    private ChoiceProperties choiceProperties;


    public void OnDrop(PointerEventData eventData)
    {
    }
    public void SetChoicePrperties(ChoiceProperties choiceProperties)
    {
        this.choiceProperties = choiceProperties;
    }
    public ChoiceProperties GetChoicePrperties()
    {
        return choiceProperties;
    }
}
