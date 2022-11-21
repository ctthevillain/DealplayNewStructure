using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dealplay
{
    public class App : MonoBehaviour
    {
        [SerializeField] private Canvas canvasScoreSummary;
        [SerializeField] private Transform themesScoreParent;
        [SerializeField] private GameObject ThemeScoreSliderPref;
        public Template_UIManager Template_UIManager;



        public readonly List<string> sceneNames = new List<string>()
        {
            "Dealplay-Entry",
            "Dealplay-Introduction" ,
            "Dealplay-SelectPerspective1",
            "Dealplay-PerspectiveTalking1",
            "Dealplay-EnterRoom1",
            "Dealplay-Reflection1",
            "Dealplay-Engage1",
            "Dealplay-Summary1",
            "Dealplay-SelectPerspective2",
            "Dealplay-PerspectiveTalking2",
            "Dealplay-EnterRoom2",
            "Dealplay-Reflection2",
            "Dealplay-Engage2",
            "Dealplay-Summary2",
            "Dealplay-SelectPerspective3",
            "Dealplay-PerspectiveTalking3",
            "Dealplay-EnterRoom3",
            "Dealplay-Reflection3",
            "Dealplay-Engage3",
            "Dealplay-Summary3",
            "Dealplay-UnderstandOptions",
            "Dealplay-WrapUp"
        };
        private List<Theme> Themes = new List<Theme>();
        private List<GameObject> ThemesObjPref = new List<GameObject>();
        public AudioSource clickAudio;
        public Scene currentScene = null;
        public Dictionary<string, object> sceneData = null;

        private static App instance = null;

        public static App Instance { get { return instance; } }

        private void Awake()
        {
            if (!instance)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

        }

        public void LoadScene(int sceneName, Dictionary<string, object> data = null)
        {
            sceneData = data;

            SceneManager.LoadScene(sceneName);
        }

        public void PlayClick()
        {
            clickAudio.Play();
        }

        public Theme GetCurrentTheme()
        {
            if (Themes.Count <= 0)
            {
                return null;
            }
            return Themes[Themes.Count - 1];
        }

        public void AddNewTheme(Theme theme)
        {
            Themes.Add(theme);
        }

        public void ShowScoreSummary(bool enable)
        {
            if (enable)
            {

                foreach (var pref in ThemesObjPref)
                {
                    Destroy(pref);
                }
                ThemesObjPref.Clear();
                for (int i = 0; i < Themes.Count; i++)
                {
                    var tempScoreSlider = Instantiate(ThemeScoreSliderPref, themesScoreParent, false);

                    tempScoreSlider.SetActive(true);
                    ThemesObjPref.Add(tempScoreSlider);
                    ThemeScoreSlider themeScoreSlider = tempScoreSlider.GetComponent<ThemeScoreSlider>();
                    string themeName = "Theme" + (i + 1);
                    float percent = (Themes[i].CurrentScore / Themes[i].TotalScore) * 100;
                    themeScoreSlider.SetThemeScore(themeName, percent);
                }
            }
            canvasScoreSummary.gameObject.SetActive(enable);
        }
        public void CleareThemes()
        {
            Themes.Clear();
        }
    }
    public class Theme
    {
        public int TotalScore;
        public float CurrentScore;
    }

}
