using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lettuce : MonoBehaviour
{
    GameObject player;
    public bool isObsessed = false;
    public bool isOver = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("HandHolder");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isObsessed && IsPlayerNear())
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isObsessed = true;
                ClownController clownController = GameObject.Find("Clown2").GetComponent<ClownController>();
                clownController.Init();
            }
        }

        if (isObsessed && !isOver)
        {
            this.transform.position = player.transform.position + new Vector3(-1, -1, 0);
        }
    }

    bool IsPlayerNear()
    {
        if ((player.transform.position - transform.position).magnitude < 2.0f)
        {
            return true;
        }
        else return false;
    }
    public bool getIsObsessed()
    {
        return isObsessed;
    }
    public void setIsOver()
    {
        isOver = true;
    }
}
