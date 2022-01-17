using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class elevator2F : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Capsule");
    }

    // Update is called once per frame
    void Update()
    {
        if((player.transform.position - this.transform.position).magnitude < 0.4f)
        {
            SceneManager.LoadScene("FinalScene");
        }
    }
}
