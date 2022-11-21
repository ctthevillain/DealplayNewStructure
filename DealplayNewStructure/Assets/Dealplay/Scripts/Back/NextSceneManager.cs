using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using System;

public class NextSceneManager : MonoBehaviour
{
    public GameObject fadeCube;
    private float timer = 1f;
    private bool fadeOut = false;
    public String sceneName = "Office";
    // Start is called before the first frame update
    void Start()
    {
        iTween.FadeTo(fadeCube, 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if(fadeOut == true) {
            timer -= Time.deltaTime;

            if(timer <= 0f) {
                SceneManager.LoadScene(sceneName);
            }
        }
    }

    public void NextScene() {
        iTween.FadeTo(fadeCube, 1f, 1f);
        fadeOut = true;
    }
}
