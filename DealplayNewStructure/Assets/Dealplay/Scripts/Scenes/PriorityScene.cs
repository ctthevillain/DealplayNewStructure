using Dealplay;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using VIDE_Data;

public class PriorityScene : Scene
{
    [SerializeField] private VIDE_Assign videAssign;
    [SerializeField] private MultipleChoiceMananger choiceMananger;
    [SerializeField] private Button NextSceneBtn;
    private void Awake()
    {
        base.Awake();
        choiceMananger.OnAnsweredSuccessfullyEvent += moveToNextSceneOnSuccess;

        choiceMananger.OnButtonDroppedIndexEvent += base.AddCurrentScore;

        NextSceneBtn.onClick.AddListener(base.OnClickNextButton);

    }
    private void moveToNextSceneOnSuccess()
    {
        NextSceneBtn.gameObject.SetActive(true);
    }

    private void Start()
    {
        VD.OnNodeChange += OnVDNodeChange;
        VD.OnEnd += OnVDEnd;
        StartCoroutine(BeginDialogueE());

    }
    protected IEnumerator BeginDialogueE()
    {
        yield return new WaitForSeconds(0.3f);


        if (videAssign) VD.BeginDialogue(videAssign);
    }
    private void OnVDEnd(VD.NodeData data)
    {
        //VD.OnNodeChange -= OnVDNodeChange;
        //VD.OnEnd -= OnVDEnd;
        VD.EndDialogue();
    }

    private void OnVDNodeChange(VD.NodeData data)
    {
        choiceMananger.gameObject.SetActive(false);

        if (data.extraVars.ContainsKey("MultipleChoice"))
        {
            choiceMananger.gameObject.SetActive(true);

            for (int i = 0; i < choiceMananger.Draggables.Count; i++)
            {
                if (i < data.comments.Length)
                {
                    choiceMananger.Draggables[i].tMP_Text.text = data.comments[i];
                }
                else
                {
                    choiceMananger.Draggables[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            if (data.audios[data.commentIndex] != null)
            {
                AudioClip audioClip = data.audios[data.commentIndex];

                if (data.extraVars.ContainsKey("Character"))
                {
                    string characterString = (string)data.extraVars["Character"];
                    OnCharacterVDNodeChange(characterString);

                    if (characterString == "Narrator")
                    {
                        if (narratorCharacter) narratorCharacter.Talk(audioClip);
                    }
                    else if (characterString == "Perspective")
                    {
                        if (perspectiveCharacter) perspectiveCharacter.Talk(audioClip);
                    }
                    else if (characterString == "Engage")
                    {
                        if (engageCharacter) engageCharacter.Talk(audioClip);
                    }
                    else
                    {
                        if (narratorCharacter) narratorCharacter.Talk(audioClip);
                    }
                }
                else
                {
                    if (narratorCharacter) narratorCharacter.Talk(audioClip);
                }
            }
            else
            {
                OnTalkFinished(null);
            }
        }
        base.CheckCreateNewTheme();
        base.CheckShowCurrentThemesProgress();
    }
    private void OnDestroy()
    {
        VD.OnNodeChange -= OnVDNodeChange;
        VD.OnEnd -= OnVDEnd;
    }
}
