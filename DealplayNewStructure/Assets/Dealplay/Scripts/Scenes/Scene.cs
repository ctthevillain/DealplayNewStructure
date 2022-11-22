using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;
using VIDE_Data;

namespace Dealplay
{

    public class Scene : MonoBehaviour
    {
        public Character narratorCharacter = null;
        public Character perspectiveCharacter = null;
        public Character engageCharacter = null;
        public bool isBlurButtonOnChoice = false;
        public bool isBeginDialogueOnStart = true;
        public bool isGoNextSceneOnVDEnd = false;

        protected static Scene instance = null;
        protected App app;

        private VIDE_Assign videAssign;
        private XRDeviceSimulator xrDeviceSimulator;
        private MainCanvas mainCanvas;
        private GameObject choicesCont;
        private Button nextButton;
        private List<TMP_Text> choiceTexts;
        private XROrigin xrOrigin;

        public static Scene Instance { get { return instance; } }

        protected void Awake()
        {
            if (!instance)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            app = App.Instance;
            app.currentScene = this;

            videAssign = FindObjectOfType<VIDE_Assign>(true);
            xrDeviceSimulator = FindObjectOfType<XRDeviceSimulator>(true);
            xrDeviceSimulator.gameObject.SetActive(Application.isEditor);

            mainCanvas = FindObjectOfType<MainCanvas>(true);
            xrOrigin = FindObjectOfType<XROrigin>(true);

            if (!Application.isEditor) xrOrigin.CameraFloorOffsetObject.transform.localPosition = Vector3.zero;

            if (mainCanvas)
            {
                choicesCont = mainCanvas.choicesCont;
                nextButton = mainCanvas.nextButton;

                nextButton.gameObject.SetActive(false);
                choicesCont.SetActive(false);
                choiceTexts = new List<TMP_Text>();

                int i = 0;
                foreach (Transform btnTransform in choicesCont.transform)
                {
                    int index = i;
                    if (btnTransform.GetComponent<Button>() == null)
                        continue;
                    btnTransform.GetComponent<Button>().onClick.AddListener(() => { OnClickChoice(index); });
                    btnTransform.GetComponent<Button>().interactable = true;
                    choiceTexts.Add(btnTransform.GetChild(0).GetComponent<TMP_Text>());
                    i++;
                }
            }
        }

        protected void Start()
        {
            VD.OnNodeChange += OnVDNodeChange;
            VD.OnEnd += OnVDEnd;

            if (isBeginDialogueOnStart)
            {
                StartCoroutine(BeginDialogueE());
            }
        }

        protected IEnumerator BeginDialogueE()
        {
            yield return null;
            yield return null;

            if (videAssign) VD.BeginDialogue(videAssign);

        }

        private void OnVDNodeChange(VD.NodeData data)
        {
            CheckCreateNewTheme();
            CheckShowCurrentThemesProgress();
            choicesCont.SetActive(false);

            if (data.isPlayer)
            {
                choicesCont.SetActive(true);

                bool isInteractableButtonFound = false;

                for (int i = 0; i < choiceTexts.Count; i++)
                {
                    if (i < data.comments.Length)
                    {
                        choiceTexts[i].transform.parent.gameObject.SetActive(true);
                        choiceTexts[i].text = data.comments[i];
                        if (choiceTexts[i].transform.parent.GetComponent<Button>().interactable) isInteractableButtonFound = true;
                    }
                    else
                    {
                        choiceTexts[i].transform.parent.gameObject.SetActive(false);
                    }
                }

                if (!isInteractableButtonFound)
                {
                    choicesCont.SetActive(false);
                    OnVDEnd(data);
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
        }

        protected virtual void OnCharacterVDNodeChange(string characterString)
        {

        }

        private void OnVDEnd(VD.NodeData data)
        {
            app.Template_UIManager.StopTimer();
            VD.OnNodeChange -= OnVDNodeChange;
            VD.OnEnd -= OnVDEnd;
            VD.EndDialogue();
            //WipeAll();

            if (isGoNextSceneOnVDEnd)
            {
                GoNextScene();
            }
            else
            {
                nextButton.gameObject.SetActive(true);
            }
        }

        private void OnDisable()
        {
            VD.OnNodeChange -= OnVDNodeChange;
            VD.OnEnd -= OnVDEnd;
        }

        public virtual void OnTalkFinished(Character character)
        {
            VD.Next();
        }

        public void OnClickNextButton()
        {
            app.PlayClick();
            GoNextScene();
        }

        private void GoNextScene()
        {
            VD.EndDialogue();
            app.Template_UIManager.StopTimer();
            // int currSceneIndx = app.sceneNames.IndexOf(SceneManager.GetActiveScene().name);
            int currSceneIndx = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndx = currSceneIndx + 1;
            if (nextSceneIndx >= app.sceneNames.Count) nextSceneIndx = 0;

            app.LoadScene(nextSceneIndx);
        }

        private void OnClickChoice(int choice)
        {
            app.PlayClick();
            app.Template_UIManager.StopTimer();
            if (isBlurButtonOnChoice)
            {
                choiceTexts[choice].transform.parent.GetComponent<Button>().interactable = false;
            }
            if (VD.nodeData == null)
            {
                return;
            }
            VD.nodeData.commentIndex = choice;

            // calculate score
            AddCurrentScore(choice);

            VD.Next();
            CheckShowCurrentThemesProgress();
        }

        public void CheckShowCurrentThemesProgress()
        {
            if (VD.nodeData == null)
            {
                return;
            }
            if (VD.nodeData?.tag.ToLower() == "themescore")
            {
                app.ShowScoreSummary(true);
            }
        }

        public void CheckCreateNewTheme()
        {
            if (VD.nodeData == null)
            {
                return;
            }
            if (VD.nodeData?.tag.ToLower() == "newtheme")
            {

                app.AddNewTheme(new Theme());
            }
        }

        public void AddCurrentScore(int choice)
        {
            if (app.GetCurrentTheme() == null)
                return;
            float answer = 0;
            float.TryParse(VD.nodeData.extraData[choice], out answer);
            app.GetCurrentTheme().TotalScore++;
            app.GetCurrentTheme().CurrentScore += answer;
        }
    }
}
