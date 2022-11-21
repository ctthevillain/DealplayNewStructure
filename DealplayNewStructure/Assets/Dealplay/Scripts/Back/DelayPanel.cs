using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayPanel : MonoBehaviour
{
    public GameObject panel;
    private float panelTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(panelTimer > 0f) {
            panelTimer -= Time.deltaTime;
            if(panelTimer <= 0f) {
                panel.SetActive(true);
            }
        }
    }

    void OnEnable() {
        panelTimer = 0.2f;
        panel.SetActive(false);
    }
}
