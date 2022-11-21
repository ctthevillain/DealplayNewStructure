using System;
using UnityEngine;
using UnityEngine.UI;

public class GraphController : MonoBehaviour
{
    // public Image patienceBar;
    // public Image patienceBarAmber;
    // public Image patienceBarRed;

    public Slider sliderBar;
    public Slider sliderBarAmber;
    public Slider sliderBarRed;
    //public Image valueBar;
    //public Image timeBar;
    public EventHandler<OnValueEventArgs> OnValue = delegate { };
    public EventHandler<OnPatienceEventArgs> OnPatience = delegate { };

    private float patienceHeight = 120f;
    private float theMaxPatience = 120f;
    private float theMaxTime = 120f;

    private bool timing = true;
    // Start is called before the first frame update
    void Start()
    {
        SetGraph();
    }

    // Update is called once per frame
    void Update()
    {
        if (timing == true)
        {
            AddTime(0f - Time.deltaTime);
        }

    }

    public void AddPatience(float patienceToAdd)
    {
        patienceHeight += patienceToAdd * (120f / theMaxPatience);
        OnPatience(this, new OnPatienceEventArgs((int)patienceHeight));
        SetGraph();
    }

    public void AddValue(float valueToAdd)
    {
        OnValue(this, new OnValueEventArgs((int)valueToAdd));
        // valueHeight += valueToAdd * (theMaxValue / 120f);
        // SetGraph();
    }
    public void ResetValues()
    {
        patienceHeight = 120f;
        theMaxPatience = 120f;
        SetGraph();
    }

    public void AddTime(float timeToAdd)
    {
        // timeHeight += timeToAdd * (theMaxTime / 120f);
        // SetGraph();
    }


    public void SetGraph()
    {

        //float timeVal = timeHeight;
        float patienceVal = patienceHeight;
        //float valueVal = valueHeight;

        // if(timeVal < 0) {
        //     timeVal = 0;
        // }
        // if(timeVal > 120) {
        //     timeVal = 120;
        // }
        if (patienceVal < 0)
        {
            patienceVal = 0;
        }
        if (patienceVal > 120)
        {
            patienceVal = 120;
        }
        // if(valueVal < 0) {
        //     valueVal = 0;
        // }
        // if(valueVal > 120) {
        //     valueVal = 120;
        // }


        if (patienceVal >= 120f)
        {
            sliderBar.gameObject.SetActive(true);
        }


        if (sliderBar.gameObject.activeSelf == true)
        {
            //patienceBar.GetComponent<RectTransform>().sizeDelta = new Vector2(patienceBar.GetComponent<RectTransform>().sizeDelta.x, patienceVal);
            sliderBar.value = patienceVal / 120f;
        }
        if (sliderBarAmber.gameObject.activeSelf == true)
        {
            //patienceBarAmber.GetComponent<RectTransform>().sizeDelta = new Vector2(patienceBar.GetComponent<RectTransform>().sizeDelta.x, patienceVal);
            sliderBarAmber.value = patienceVal / 120f;
        }
        if (sliderBarRed.gameObject.activeSelf == true)
        {
            //patienceBarRed.GetComponent<RectTransform>().sizeDelta = new Vector2(patienceBar.GetComponent<RectTransform>().sizeDelta.x, patienceVal);
            sliderBarRed.value = patienceVal / 120f;
        }

        if (patienceVal < (120f * 0.6f))
        {
            //patienceBar.enabled = false;
            sliderBar.gameObject.SetActive(false);
        }
        if (patienceVal < (120f * 0.3f))
        {
            //patienceBarAmber.enabled = false;
            sliderBarAmber.gameObject.SetActive(false);
        }

    }

    public void SetMaxPatience(float maxPatience)
    {
        theMaxPatience = maxPatience;
    }

    public void SetMaxTime(float maxTime)
    {
        theMaxTime = maxTime;
    }

    public void StopTimer()
    {
        timing = false;
    }
}


public class OnValueEventArgs : EventArgs
{
    public int Value = 0;

    public OnValueEventArgs(int value)
    {
        Value = value;
    }
}

public class OnPatienceEventArgs : EventArgs
{
    public int Patience = 0;

    public OnPatienceEventArgs(int value)
    {
        Patience = value;
    }
}