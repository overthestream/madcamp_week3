using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{   

    private float walkSpeed=20.0f;

    private float lookSensitivity = 5.0f; 

    [SerializeField]
    private float cameraRotationLimit;  
    private float currentCameraRotationX;  

    [SerializeField]
    private Camera theCamera; 
    private Rigidbody myRigid;


    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {   

        Move();                 // 키보드 입력에 따라 이동
        CameraRotation();       // 마우스를 위아래(Y) 움직임에 따라 카메라 X 축 회전 
        CharacterRotation();    // 마우스 좌우(X) 움직임에 따라 캐릭터 Y 축 회전  
        
    }

        private void Move()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");  
        float _moveDirZ = Input.GetAxisRaw("Vertical");  
        Vector3 _moveHorizontal = transform.right * _moveDirX; 
        Vector3 _moveVertical = transform.forward * _moveDirZ; 

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * walkSpeed; 

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

    public void SetWalkSpeed(float value)
    {
        this.walkSpeed = value;
    }

    public void SetLookSensitivity(float value)
    {
        this.lookSensitivity = value;
    }

}
