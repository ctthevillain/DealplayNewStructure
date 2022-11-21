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
    public class EntryScene : Scene
    {
        public Animator logoCanvasAnimator;
        public RectTransform termsConditions;

        private float logoFadeDuration = 3f;

        public static new EntryScene Instance { get { return (EntryScene)instance; } }

        protected new void Awake()
        {
            base.Awake();

            logoCanvasAnimator.enabled = true;
            termsConditions.gameObject.SetActive(false);
        }

        protected new void Start()
        {
            base.Start();

            StartCoroutine(WaitAndShowTermsConditions());
        }

        private IEnumerator WaitAndShowTermsConditions()
        {
            yield return new WaitForSeconds(logoFadeDuration);

            termsConditions.gameObject.SetActive(true);
        }
    }
}
