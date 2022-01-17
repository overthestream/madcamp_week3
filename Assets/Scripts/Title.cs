using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title : MonoBehaviour
{
    GameObject canvas;
    GameObject fade;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Text");
        fade = GameObject.Find("Fade");
        fade.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            fadeOut();
            Invoke("ChangeScene", 1.2f);
        }
    }

    void fadeOut()
    {
        fade.SetActive(true);
        RawImage rawImage = fade.GetComponent<RawImage>();
        rawImage.canvasRenderer.SetAlpha(0.1f);
        rawImage.CrossFadeAlpha(1f, 1.2f, false);
    }

    void ChangeScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
