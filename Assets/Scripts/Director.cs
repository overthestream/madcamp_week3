using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Director : MonoBehaviour
{
    GameObject fade;
    GameObject end;
    GameObject endText; 
    // Start is called before the first frame update
    void Start()
    {
        fade = GameObject.Find("Fade");
        fade.GetComponent<RawImage>().CrossFadeAlpha(0, 1.5f,false) ;

        end = GameObject.Find("EndScene");
        endText = GameObject.Find("EndText");
        end.GetComponent<RawImage>().canvasRenderer.SetAlpha(0);
        endText.GetComponent<Text>().canvasRenderer.SetAlpha(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
