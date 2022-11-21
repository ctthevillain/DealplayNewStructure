using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Simulation;
using VIDE_Data;
using TMPro;

namespace Dealplay
{
    public class IntroductionScene : Scene
    {
        public TMP_Text startCountText;

        public static new IntroductionScene Instance { get { return (IntroductionScene)instance; } }

        private new void Awake()
        {
            base.Awake();

            isBeginDialogueOnStart = false;
        }
        protected new void Start()
        {
            base.Start();

            StartCoroutine(DoStartCount());
        }

        private IEnumerator DoStartCount()
        {
            startCountText.gameObject.SetActive(true);

            startCountText.text = "3";
            yield return new WaitForSeconds(1f);
            startCountText.text = "2";
            yield return new WaitForSeconds(1f);
            startCountText.text = "1";
            yield return new WaitForSeconds(1f);

            startCountText.text = "0";
            startCountText.gameObject.SetActive(false);

            StartCoroutine(BeginDialogueE());
        }
    }
}
