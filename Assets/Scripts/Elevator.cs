using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
	GameObject elevator;
	AnimationClip openClip, closeClip;
	bool isClosed = true;
	// Start is called before the first frame update
	void Start()
    {
		elevator = GameObject.Find("ElevatorV2");
		openClip = elevator.GetComponent<Animation>().GetClip("OpenDoors");
		closeClip = elevator.GetComponent<Animation>().GetClip("CloseDoors");
		elevator.GetComponent<Animation>().clip = closeClip;
		elevator.GetComponent<Animation>().Play();
		elevator.GetComponent<Animation>().clip = openClip;
		elevator.GetComponent<Animation>().Play();
	}

    // Update is called once per frame
    void Update()
    {
		if (isClosed) elevator.GetComponent<Animation>().clip = openClip;
		else elevator.GetComponent<Animation>().clip = closeClip;
		if (Input.GetKeyDown(KeyCode.E))
        {
			elevator.GetComponent<Animation>().Play();
			isClosed = !isClosed;
        }
    }
}
