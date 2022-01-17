using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoor : MonoBehaviour
{   
    Animator _doorAnim;
    private bool isActive = false;
    private bool isOpen = false;

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player")
            //_doorAnim.SetBool("open", true);
            isActive= true;
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player")
            //_doorAnim.SetBool("open", false);
            isActive=false;
    }
    void Start() {
        _doorAnim=GetComponent<Animator>();
    }

    void Update() {
        //플레이어가 문의 콜라이더 영역 내에 있고, 스페이스바 키를 누른 경우 문을 열고 닫는다.
        if(isActive) {
            if(!isOpen && Input.GetKeyDown(KeyCode.Space)) {
                _doorAnim.SetBool("open", true);
                isOpen= true;
            }

            else if (isOpen && Input.GetKeyDown(KeyCode.Space)) {
                _doorAnim.SetBool("open", false);
                isOpen= false;
            }

        }
    }

}
