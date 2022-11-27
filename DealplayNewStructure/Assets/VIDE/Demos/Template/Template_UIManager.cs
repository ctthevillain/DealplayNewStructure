/*
 *  This is a template verison of the VIDEUIManager1.cs script. Check that script out and the "Player Interaction" demo for more reference.
 *  This one doesn't include an item popup as that demo was mostly hard coded.
 *  Doesn't include reference to a player script or gameobject. How you handle that is up to you.
 *  Doesn't save dialogue and VA state.
 *  Player choices are not instantiated. You need to set the references manually.
    
 *  You are NOT limited to what this script can do. This script is only for convenience. You are completely free to write your own manager or build from this one.
 */

using Dealplay;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VIDE_Data; //<--- Import to use VD class

public class Template_UIManager : MonoBehaviour
{
    #region VARS

    public Button resetBtn;
    public GraphController graphController;
    // ScrollRect scrollRect;
    //public GameObject scrollContentArea;
    public Text actionTimerText;
    public Text actionTimerTextRed;


    private bool latch = false;
    private float actionTimer = 0f;
    private int minutes = 0;
    private int seconds = 0;
    private string minutesText = "";
    private string secondsText = "";

    private List<UsedComment> usedComments;


    bool dialoguePaused = false; //Custom variable to prevent the manager from calling VD.Next
    bool animatingText = false; //Will help us know when text is currently being animated
    int availableChoices = 0;

    #endregion

    #region MAIN

    void Awake()
    {
        usedComments = new List<UsedComment>();
        resetBtn.onClick.AddListener(RestartApp);
        resetBtn.gameObject.SetActive(false);
        graphController.OnValue += SetCurrentValue;
        graphController.OnPatience += SetCurrentPatience;
    }
    private void RestartApp()
    {
        resetBtn.gameObject.SetActive(false);
        graphController.ResetValues();
        App.Instance.CleareThemes();
        SceneManager.LoadScene(1);


    }

    public void ShowReset(bool showResetBtn = true)
    {
        VD.EndDialogue();

        resetBtn.gameObject.SetActive(true);
        actionTimerText.enabled = false;
        actionTimerTextRed.enabled = false;
    }


    public void SetCurrentValue(object sender, OnValueEventArgs valueArgs)
    {
        actionTimerText.enabled = false;
        actionTimerTextRed.enabled = false;
    }

    public void SetCurrentPatience(object sender, OnPatienceEventArgs patienceArgs)
    {
        if (patienceArgs.Patience <= 0)
        {

            ShowReset(false);
        }
    }

    //Call this to begin the dialogue and advance through it
    public void Interact(VIDE_Assign dialogue)
    {
        //Sometimes, we might want to check the ExtraVariables and VAs before moving forward
        //We might want to modify the dialogue or perhaps go to another node, or dont start the dialogue at all
        //In such cases, the function will return true
        var doNotInteract = PreConditions(dialogue);
        if (doNotInteract) return;

        if (!VD.isActive)
        {
            Begin(dialogue);
        }
        else
        {
            CallNext();
        }
    }
    //Calls next node in the dialogue
    public void CallNext()
    {
        //Let's not go forward if text is currently being animated, but let's speed it up.
        //if (animatingText) { CutTextAnim(); return; }

        if (!dialoguePaused) //Only if
        {
            VD.Next(); //We call the next node and populate nodeData with new data. Will fire OnNodeChange.
        }
        else
        {
            //Stuff we can do instead if dialogue is paused
        }
    }

    bool PreConditions(VIDE_Assign assigned)
    {
        var data = VD.nodeData;
        if (VD.isActive)
        {
            if (!data.isPlayer)
            {

            }
            else
            {

            }
        }
        else
        {

        }

        return false;
    }

    //This begins the conversation. 
    void Begin(VIDE_Assign dialogue)
    {



        VD.BeginDialogue(dialogue); //Begins dialogue, will call the first OnNodeChange

        //dialogueContainer.SetActive(true); //Let's make our dialogue container visible
    }

    //If not using local input, then the UI buttons are going to call this method when you tap/click them!
    //They will send along the choice index
    public void SelectChoice(int choice)
    {
        VD.nodeData.commentIndex = choice;
        // VD.Next(); 
    }

    //Input related stuff (scroll through player choices and update highlight)
    void Update()
    {
        if (actionTimer > 0f && !animatingText)
        {
            actionTimer -= Time.deltaTime;
            //justSetUp = false;
            if (actionTimerText.enabled == true)
            {
                actionTimerText.text = GetTime(actionTimer);
            }

            if (actionTimer <= 0f)
            {
                graphController.AddPatience(-10f);

                StopTimer();

            }
        }



        //Note you could also use Unity's Navi system, in which case you would tick the useNavigation flag.
    }

    public void SetActionTimer(float time)
    {

        actionTimerText.enabled = true;
        //actionTimerTextRed.enabled = false;
        actionTimerText.enabled = true;
        actionTimer = time;
        //justSetUp = true;
        actionTimerText.text = GetTime(actionTimer);
    }

    public string GetTime(float time)
    {

        minutesText = "";
        secondsText = "";

        minutes = (int)(time / 60f);
        seconds = (int)Mathf.Ceil((time - (minutes * 60f)));

        if (seconds < 0)
        {
            seconds = 0;
        }

        if (minutes < 10)
        {
            minutesText = "0" + minutes;
        }
        else
        {
            minutesText = minutes.ToString();
        }
        if (seconds < 10)
        {
            secondsText = "0" + seconds;
        }
        else
        {
            secondsText = seconds.ToString();
        }
        return minutesText + ":" + secondsText;
    }

    public void SetComentIndex(int newIndex)
    {
        var data = VD.nodeData;
        bool[] usedArray = GetUsedArray(data.nodeID);
        int index = data.commentIndex;

        //if(justSetUp = false) {
        actionTimer = -1f;
        //}


        if (newIndex < availableChoices && newIndex >= 0 && usedArray[newIndex] == false && !animatingText && latch == false)
        {
            data.commentIndex = newIndex;

            SetUsedArrayOption(VD.nodeData.nodeID, data.commentIndex, true);

            VD.Next();

        }
    }

    /*private void GetNextUp() {
        var data = VD.nodeData;
        bool[] usedArray = GetUsedArray(data.nodeID);
        bool found = false;
        int index = data.commentIndex;

        int breakOut = 0;

        while(found == false && data.commentIndex < usedArray.Length) {


            //Debug.Log(data.commentIndex < usedArray.Length)

            if (data.commentIndex < availableChoices - 1) {
                data.commentIndex++;

                if(usedArray[data.commentIndex] == false) {
                    found = true;
                }
            }  

            breakOut++;
            if(breakOut > 10) {
                Debug.Log("Breakout 1 used");
                break;
            }            
        }

        if(found == false){
            data.commentIndex = index;
        }

        if (data.audios[data.commentIndex] != null)
        {
            audioSource.clip = data.audios[data.commentIndex];
            audioSource.Play();
        }
    }

    private void GetNextDown() {
        var data = VD.nodeData;
        bool[] usedArray = GetUsedArray(data.nodeID);
        bool found = false;
        int index = data.commentIndex;

        int breakOut = 0;

        while(found == false && data.commentIndex > 0) {
            if (data.commentIndex > 0) {
                data.commentIndex--;

                if(usedArray[data.commentIndex] == false) {
                    found = true;
                }
            }

            breakOut++;
            if(breakOut > 10) {
                Debug.Log("Breakout 2 used");
                break;
            } 
        }

        if(found == false){
            data.commentIndex = index;
        }

        if (data.audios[data.commentIndex] != null)
        {
            audioSource.clip = data.audios[data.commentIndex];
            audioSource.Play();
        }
    }*/

    private void SetUsedArrayOption(int nodeID, int commentIndex, bool val)
    {
        bool found = false;
        int index = 0;
        int breakOut = 0;

        while (found == false && index < usedComments.Count)
        {
            if (usedComments[index].nodeID == nodeID)
            {

                if (usedComments[index].used != null && commentIndex < usedComments[index].used.Length)
                {
                    usedComments[index].used[commentIndex] = val;


                }

                found = true;

            }
            index++;

            breakOut++;
            if (breakOut > 10)
            {
                break;
            }
        }
    }

    private bool[] GetUsedArray(int nodeID)
    {
        bool[] returnVal = null;
        bool found = false;
        int index = 0;

        int breakOut = 0;


        while (found == false && index < usedComments.Count)
        {
            if (usedComments[index].nodeID == nodeID)
            {
                returnVal = usedComments[index].used;

                found = true;
            }
            index++;

            breakOut++;
            if (breakOut > 10)
            {
                break;
            }
        }



        if (found == true)
        {

            return returnVal;
        }

        UsedComment usedComment = new UsedComment();
        usedComment.used = new bool[VD.nodeData.comments.Length];


        for (int i = 0; i < VD.nodeData.comments.Length; i++)
        {
            usedComment.used[i] = false;
        }

        returnVal = usedComment.used;
        usedComments.Add(usedComment);
        usedComments[usedComments.Count - 1].nodeID = nodeID;


        return returnVal;
    }





    #endregion

    #region DIALOGUE CONDITIONS 

    //DIALOGUE CONDITIONS --------------------------------------------


    void ReplaceWord(VD.NodeData data)
    {
        if (data.comments[data.commentIndex].Contains("[NAME]"))
            data.comments[data.commentIndex] = data.comments[data.commentIndex].Replace("[NAME]", VD.assigned.gameObject.name);

        if (data.comments[data.commentIndex].Contains("[WEAPON]"))
            data.comments[data.commentIndex] = data.comments[data.commentIndex].Replace("[WEAPON]", "sword");
    }

    #endregion

    #region EVENTS AND HANDLERS

    //Called when dialogue sare finished loading
    void OnLoadedAction()
    {
        //Debug.Log("Finished loading all dialogues");
        VD.OnLoaded -= OnLoadedAction;
    }

    public void StopTimer()
    {
        actionTimer = 0f;
        actionTimerText.enabled = false;
        actionTimerTextRed.enabled = true;
        actionTimerTextRed.text = "00:00";
    }


    public void OnDestroy()
    {
        graphController.OnValue -= SetCurrentValue;
        graphController.OnPatience -= SetCurrentPatience;
    }


    #endregion

    //Utility note: If you're on MonoDevelop. Go to Tools > Options > General and enable code folding.
    //That way you can exapnd and collapse the regions and methods

}

public class UsedComment
{
    public int nodeID;
    public bool[] used;
}


