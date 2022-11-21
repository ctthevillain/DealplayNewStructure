using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using J4RV.VisualDebug;
using System.Collections.Generic;
using System.Text;

[RequireComponent(typeof(Canvas))]   
public class VisualDebug : MonoBehaviour {

    [SerializeField] private LogColors colors;
    [Range(0, 1)]
    [SerializeField] private float opacity = 0.98f;
    [SerializeField] private bool startVisible = false;
    [SerializeField] private KeyCode toggleKey = KeyCode.Tab;
    [SerializeField] private KeyCode clearKey = KeyCode.Delete;

    // Visual Logger will only be active in Development Builds and Editor
    #if DEVELOPMENT_BUILD || UNITY_EDITOR

        private static readonly int MAX_CHARS = 16250;
        private static readonly int REMOVE_CHARS_AMOUNT = MAX_CHARS/2;
        private static readonly Regex FIRST_LINE_REGEX = new Regex("^.*(\r?\n).*");

        private static Dictionary<LogType, string> colorsDictionary;
        private static Text uiText;
        private static Canvas canvas;
        private static StringBuilder textContent;


        void Awake(){
            colorsDictionary = colors.GetDictionary();
            uiText = GetComponentInChildren<Text>();
            Image background = GetComponentInChildren<Image>();
            canvas = GetComponentInChildren<Canvas>();
            textContent = new StringBuilder();

            Assert.IsNotNull(uiText);
            Assert.IsNotNull(uiText);
            Assert.IsNotNull(background);

            Application.logMessageReceived += LogMessageReceived;

            canvas.enabled = startVisible;
            background.color = new Color(0, 0, 0, opacity);

            DontDestroyOnLoad(gameObject);
        }

        void OnDestroy(){
            Application.logMessageReceived -= LogMessageReceived;
        }

        void Update(){
            if(Input.GetKeyDown(toggleKey)){
                canvas.enabled = !canvas.enabled;
            }
            if (Input.GetKeyDown(clearKey)){
                uiText.text = "";
            }
        }


        private static void CheckMaxLength(){
            if(textContent.Length >= MAX_CHARS){
                textContent.Remove(0, REMOVE_CHARS_AMOUNT);
                CleanFirstLine();
            }
        }

        private static void CleanFirstLine(){
            int charsToRemove = FIRST_LINE_REGEX.Match(textContent.ToString()).Groups[0].Length;
            textContent.Remove(0, charsToRemove);
        }

        private static void LogMessageReceived(string message, string stackTrace, LogType type){
            string color = colorsDictionary[type];
            string moment = TimeUtils.GetFormattedMoment();
            string formattedMessage = string.Format("<color=#{1}>[{2}] {0}</color>\n", message, color, moment);
            textContent.Append(formattedMessage);
            CheckMaxLength();
            uiText.text = textContent.ToString();
        }

    #else

        // We don't need the VisualDebug GameObject in a final build
        void Awake(){
            Destroy(gameObject);
        }

    #endif

}
