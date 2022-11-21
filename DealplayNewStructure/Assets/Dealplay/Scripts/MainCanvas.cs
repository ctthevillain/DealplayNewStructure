using UnityEngine;
using UnityEngine.UI;

namespace Dealplay
{
    public class MainCanvas : MonoBehaviour
    {
        public GameObject choicesCont;
        public Button nextButton;

        private Scene scene;

        private void Awake()
        {
            scene = Scene.Instance;
            GetComponent<Canvas>().worldCamera = Camera.main;
        }

        public void OnClickNextButton()
        {
            scene.OnClickNextButton();
        }
    }
}