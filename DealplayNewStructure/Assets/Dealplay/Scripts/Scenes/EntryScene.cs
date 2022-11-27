using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dealplay
{
    public class EntryScene : MonoBehaviour
    {
        public Animator logoCanvasAnimator;
        public RectTransform termsConditions;
        public AudioSource clickAudio;
        private float logoFadeDuration = 3f;

        protected new void Awake()
        {


            logoCanvasAnimator.enabled = true;
            termsConditions.gameObject.SetActive(false);
        }

        protected new void Start()
        {


            StartCoroutine(WaitAndShowTermsConditions());
        }

        private IEnumerator WaitAndShowTermsConditions()
        {
            yield return new WaitForSeconds(logoFadeDuration);

            termsConditions.gameObject.SetActive(true);
        }
        public void OnClickNextButton()
        {
            int currSceneIndx = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndx = currSceneIndx + 1;
            clickAudio.Play();
            SceneManager.LoadScene(nextSceneIndx);
        }
    }
}
