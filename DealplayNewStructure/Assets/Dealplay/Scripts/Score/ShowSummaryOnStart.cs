using Dealplay;
using UnityEngine;

public class ShowSummaryOnStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        App.Instance.ShowScoreSummary(true);
        App.Instance.Template_UIManager.resetBtn.gameObject.SetActive(true);
        App.Instance.Template_UIManager.actionTimerText.gameObject.SetActive(false);
        App.Instance.Template_UIManager.actionTimerTextRed.gameObject.SetActive(false);
        App.Instance.Template_UIManager.graphController.sliderBar.gameObject.SetActive(false);
        App.Instance.Template_UIManager.graphController.sliderBarAmber.gameObject.SetActive(false);
        App.Instance.Template_UIManager.graphController.sliderBarRed.gameObject.SetActive(false);
    }

}
