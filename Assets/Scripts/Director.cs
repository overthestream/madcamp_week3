using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Director : MonoBehaviour
{
    GameObject fade;
    // Start is called before the first frame update
    void Start()
    {
        fade = GameObject.Find("Fade");
        fade.GetComponent<RawImage>().CrossFadeAlpha(0, 1.5f,false) ;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
