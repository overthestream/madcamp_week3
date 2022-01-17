using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{   
    //속도 관련 변수
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    private float lookSensitivity = 4.0f; 
    [SerializeField]
    private float crouchSpeed;
    private float applySpeed;

    //상태 변수
    private bool isCrouch=false;
    private bool isRun=false;

    //카메라 회전 각도 관련 변수
    [SerializeField]
    private float cameraRotationLimit;  
    private float currentCameraRotationX;  
    
    //카메라와 플레이어 
    [SerializeField]
    private Camera theCamera; 
    private Rigidbody myRigid;
    private CapsuleCollider capsuleCollider;

    //앉았을 때 얼마나 앉을지 결정
    private float crouchPosY;
    private float originPosY;
    private float applyCrouchPosY;


    //애니메이션 설정을 위한 변수
    [SerializeField]
    Animator animator;

    //열쇠 사용해서 문 열기 구현을 위한 변수
    int numKey;


    // Start is called before the first frame update
    void Start()
    {
        //변수 초기화
        myRigid = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();

        numKey =0;
        applySpeed= walkSpeed;
        originPosY = theCamera.transform.localPosition.y;
        applyCrouchPosY = originPosY;

    }

    // Update is called once per frame
    void Update()
    {   
        TryRun();   //달리기
        TryCrouch();    //앉기
        Move();                 // 키보드 입력에 따라 이동
        CameraRotation();       // 마우스를 위아래(Y) 움직임에 따라 카메라 X 축 회전 
        CharacterRotation();    // 마우스 좌우(X) 움직임에 따라 캐릭터 Y 축 회전  
        
    }

        private void Move()
    {   
        float _moveDirX = Input.GetAxisRaw("Horizontal");  
        float _moveDirZ = Input.GetAxisRaw("Vertical");  
        
        //손 애니메이션 설정
        if (_moveDirX!=0 || _moveDirZ!=0) {
            animator.SetBool("walking", true);
        }

        else { animator.SetBool("walking",false);}
            
        Vector3 _moveHorizontal = transform.right * _moveDirX; 
        Vector3 _moveVertical = transform.forward * _moveDirZ; 

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed; 

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }

        private void CameraRotation()  //위 아래 카메라 회전
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y"); 
        float _cameraRotationX = _xRotation * lookSensitivity;
        
        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }

    private void CharacterRotation()  // 좌우 캐릭터 회전
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY)); // 쿼터니언 * 쿼터니언
        // Debug.Log(myRigid.rotation);  // 쿼터니언
        // Debug.Log(myRigid.rotation.eulerAngles); // 벡터
    }

    // 달리기 시도
    private void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {   
            animator.SetBool("running", true);
            Running();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {   
            animator.SetBool("running",false);
            RunningCancel();
        }
    }

    // 달리기
    private void Running()
    {
        if (isCrouch)
            Crouch();

        isRun = true;
        applySpeed = runSpeed;
    }

    // 달리기 취소
    private void RunningCancel()
    {
        isRun = false;
        applySpeed = walkSpeed;
    }

    // 앉기 시도
    private void TryCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch();
        }
    }

    // 앉기 동작 - 카메라만 움직인다.
    private void Crouch()
    {
        isCrouch = !isCrouch;
        if (isCrouch)
        {
            applySpeed = crouchSpeed;
            applyCrouchPosY = crouchPosY;
        }
        else
        {
            applySpeed = walkSpeed;
            applyCrouchPosY = originPosY;
        }

        StartCoroutine(CrouchCoroutine());
    }

    // 부드러운 앉기 동작
    IEnumerator CrouchCoroutine()
    {   
        float _posY = theCamera.transform.localPosition.y;
        int count = 0;

        while(_posY != applyCrouchPosY)
        {
            count++;
            _posY = Mathf.Lerp(_posY, applyCrouchPosY, 0.2f);
            theCamera.transform.localPosition = new Vector3(0, _posY, 0);
            capsuleCollider.height=_posY;
            if(count > 15)
                break;
            yield return null;
        }
        theCamera.transform.localPosition = new Vector3(0, applyCrouchPosY, 0);
    }

    /*void OnCollisionEnter(Collision collision) {
        

        if(collision.gameObject.tag =="Key") {
            //Vector3 keyPosition = collision.gameObject.GetComponent<Transform>().position;
            if (Input.GetKey(KeyCode.E)) {
                numKey += 1;
                Destroy(collision.gameObject);
            }
        }


    }*/

}
