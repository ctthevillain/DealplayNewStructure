using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ThemeScoreSlider : MonoBehaviour
{
    [SerializeField] private TMP_Text NameTxt;
    [SerializeField] private TMP_Text percentTxt;
    [SerializeField] private Slider Slider;
    public void SetThemeScore(string name, float percent)
    {
        NameTxt.text = name;
        percentTxt.text = "" + (int)percent + "%";
        Slider.value = percent / 100;
    }

}
