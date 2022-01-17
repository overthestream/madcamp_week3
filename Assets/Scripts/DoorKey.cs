using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKey : MonoBehaviour
{   
    [SerializeField]
    private Component door;

    private BoxCollider doorcollider;
    public bool inTrigger= false;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inTrigger = true;
            Debug.Log("Enter");
       }
    }
    void OnTriggerExit(Collider other) {
        if(other.tag == "Player")
        inTrigger= false;
    }

    // Start is called before the first frame update
    void Start()
    {
        doorcollider= door.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {   
        //플레이어가 열쇠의 콜라이더 내에 있고, E버튼을 누른 경우 해당되는 문을 활성화 시킨다.
        if (inTrigger && Input.GetKey(KeyCode.E)) {
            doorcollider.isTrigger= true;
            Destroy(this.gameObject);
        }
    }
}
