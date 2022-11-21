using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobeWish.Learn.VR
{
    public class CaptureScreenshot : MonoBehaviour
    {
        public bool capture;

        private void Awake()
        {
            capture = false;
        }
        
        void Update()
        {
            if (capture)
            {
                TakeScreenshot();
                capture = false;
            }
        }

        public void TakeScreenshot()
        {
            string path = Application.persistentDataPath + "/screenshot_" + System.DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".png";

            ScreenCapture.CaptureScreenshot(path);

            Debug.Log("Saved screenshot to: " + path);
        }
    }
}
