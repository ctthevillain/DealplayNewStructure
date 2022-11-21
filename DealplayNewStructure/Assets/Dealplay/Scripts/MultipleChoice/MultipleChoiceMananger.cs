using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MultipleChoiceMananger : MonoBehaviour
{
    public List<Dropable> Dropables;
    public List<Draggable> Draggables;
    public delegate void OnButtonAction();
    public delegate void OnButtonActionIndex(int index);
    public event OnButtonAction OnAnsweredSuccessfullyEvent;
    public event OnButtonAction OnButtonDroppedEvent;
    public event OnButtonActionIndex OnButtonDroppedIndexEvent;
    [SerializeField] private TMP_Text result;
    [SerializeField] private GameObject LeftHandButton;
    [SerializeField] private GameObject RightHandButton;


    public void CheckAllCorrectAnswers()
    {
        int countCorrect = 0;
        for (int i = 0; i < Dropables.Count; i++)
        {
            // if there was choice and its value true
            if (Dropables[i].GetChoicePrperties() == null)
            {
                return;
            }
            if (Dropables[i].GetChoicePrperties().IsCorrect)
            {
                countCorrect++;
            }

        }
        SetResult(countCorrect);
        //if all ansers are correct
        if (Dropables.Count == countCorrect)
        {
            OnAnsweredSuccessfullyEvent.Invoke();
        }

    }
    private void SetResult(int countCorrectAnswers)
    {
        result.text = "Correct Answers:3/" + countCorrectAnswers;
    }
    public void SetResult(string result)
    {
        this.result.text = result;
    }
    public void ActivateTempButton(bool value)
    {
        LeftHandButton.SetActive(value);
        RightHandButton.SetActive(value);
    }
    public void OnButtonDropped(int index)
    {
        OnButtonDroppedEvent?.Invoke();
        OnButtonDroppedIndexEvent.Invoke(index);
    }
}
