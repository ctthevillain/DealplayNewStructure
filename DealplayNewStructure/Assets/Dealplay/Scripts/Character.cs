using ReadyPlayerMe;
using System.Collections.Generic;
using UnityEngine;

namespace Dealplay
{
    public class Character : MonoBehaviour
    {
        public VoiceHandler voiceHandler;
        public Animator animator;
        public List<Renderer> renderers;
        public bool isHidden = false;

        private Scene scene;
        private bool isCheckTalkFinished = false;
        private bool isFreezeAnim = false;
        private float talkingTime = 0f;
        private float talkingTimeMax = 3600f; // 2f;

        private void Start()
        {
            scene = Scene.Instance;
            IsFreezeAnim = isFreezeAnim;

            if (isHidden)
            {
                foreach (Renderer renderer in renderers)
                {
                    renderer.gameObject.SetActive(false);
                }

                voiceHandler.AudioSource.spatialBlend = 0f;
            }
            else
            {
                foreach (Renderer renderer in renderers)
                {
                    renderer.gameObject.SetActive(true);
                    //renderer.material.color = new Color(1f, 1f, 1f, 0f);

                }

                voiceHandler.AudioSource.spatialBlend = 0.5f;
            }
        }

        private void Update()
        {
            if (isCheckTalkFinished)
            {
                talkingTime += Time.deltaTime;

                if (!voiceHandler.AudioSource.isPlaying || talkingTime >= talkingTimeMax)
                {
                    isCheckTalkFinished = false;
                    if (voiceHandler.AudioSource.isPlaying) voiceHandler.AudioSource.Stop();
                    if (!isFreezeAnim) animator.SetInteger("CurrState", 1);
                    OnTalkFinished();
                }
            }
        }

        public void Talk(AudioClip audioClip)
        {
            //StartCoroutine(TalkE(audioClip));

            voiceHandler.PlayAudioClip(audioClip);
            if (!isFreezeAnim) animator.SetInteger("CurrState", 2);
            isCheckTalkFinished = true;
            talkingTime = 0;
        }

        /*
        private IEnumerator TalkE(AudioClip audioClip)
        {
            yield return null;
            yield return null;

            voiceHandler.PlayAudioClip(audioClip);
            animator.SetInteger("CurrState", 2);
            isCheckTalkFinished = true;
        }
        */

        private void OnTalkFinished()
        {
            scene.OnTalkFinished(this);
        }

        public bool IsFreezeAnim
        {
            get { return isFreezeAnim; }
            set
            {
                isFreezeAnim = value;

                if (isFreezeAnim)
                {
                    animator.SetInteger("CurrState", 5);
                }
                else
                {
                    animator.SetInteger("CurrState", 1);
                }
            }
        }
    }
}