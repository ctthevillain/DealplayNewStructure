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
    public class EnterRoomScene : Scene
    {
        public Renderer fadeBox;

        public static new EnterRoomScene Instance { get { return (EnterRoomScene)instance; } }

        protected new void Awake()
        {
            base.Awake();

            engageCharacter.gameObject.SetActive(false);
            fadeBox.gameObject.SetActive(false);
        }

        public override void OnTalkFinished(Character character)
        {
            if (!engageCharacter.gameObject.activeSelf)
            {
                StartCoroutine(FadeInEngageCharacter());
            }
            else
            {
                VD.Next();
            }
        }

        private IEnumerator FadeInEngageCharacter()
        {
            fadeBox.gameObject.SetActive(true);

            float deltaTime = 0.1f;
            Color color = new Color(1f, 1f, 1f, 0f);

            while (color.a < 1f)
            {
                color.a += 2f * deltaTime;
                if (color.a > 1f) color.a = 1f;

                fadeBox.material.color = color;
                yield return new WaitForSeconds(deltaTime);
            }

            engageCharacter.gameObject.SetActive(true);

            while (color.a > 0f)
            {
                color.a -= 0.333f * deltaTime;
                if (color.a < 0f) color.a = 0f;

                fadeBox.material.color = color;
                yield return new WaitForSeconds(deltaTime);
            }

            fadeBox.gameObject.SetActive(false);

            VD.Next();

            yield return null;
        }
    }
}
