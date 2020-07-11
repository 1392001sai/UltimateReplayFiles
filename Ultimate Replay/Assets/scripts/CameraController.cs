using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float MoveSpeed;
    float OrbitRotateSpeed;
    public float ScreenRotateSpeed;
    public float ZoomSpeed;
    public float FOVMin;
    public float FOVMax;
    Vector3 MoveDir;
    Vector2 MousePos;
    Vector2 OrgMousePos;
    Vector3 Difference;
    Vector3 RotateAxis;
    Vector3 OrgPosition;
    Quaternion OrgRotation;
    float OrgFOV;
    Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
        OrgPosition = transform.position;
        OrgRotation = transform.rotation;
        OrgFOV = cam.fieldOfView;
    }
    private void Update()
    {
        if (RecordPlayer.replay_state != Replay_State.Recording && RecordPlayer.replay_state != Replay_State.none)
        {
            Move();
            Rotate();
            Zoom();
            if(Input.GetKeyDown(KeyCode.R))
            {
                ResetCamera();
            }
        }
    }

    void Move()
    {
        MoveDir = Vector3.zero;
        MoveDir = Input.GetAxisRaw("Horizontal") * Vector2.right + Input.GetAxisRaw("Vertical") * Vector2.up;
        MoveDir = MoveDir.normalized;
        transform.Translate(MoveDir * MoveSpeed * Time.deltaTime);
    }

    void Rotate()
    {
        
        if(Input.GetMouseButtonDown(2))
        {
            OrgMousePos = Input.mousePosition;
        }
        if(Input.GetMouseButton(2))
        {
            //Debug.Log(1);
            MousePos = Input.mousePosition;
            if (MousePos != OrgMousePos)
            {
                Difference = MousePos - OrgMousePos;
                OrbitRotateSpeed = Vector3.Distance(Difference, Vector3.zero);
                RotateAxis = (transform.up * Difference.x + -transform.right * Difference.y).normalized;
                transform.Rotate(RotateAxis, OrbitRotateSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0);            }
        }


    }

    void Zoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (cam.fieldOfView < FOVMax)
            {
                cam.fieldOfView += ZoomSpeed * Time.deltaTime;
            }
            if (cam.fieldOfView > FOVMax)
            {
                cam.fieldOfView = FOVMax;
            }
        }
        else if(Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (cam.fieldOfView > FOVMin)
            {
                cam.fieldOfView -= ZoomSpeed * Time.deltaTime;
            }
            if (cam.fieldOfView < FOVMin)
            {
                cam.fieldOfView = FOVMin;
            }
        }
    }


    void ResetCamera()
    {
        transform.position = OrgPosition;
        transform.rotation = OrgRotation;
        cam.fieldOfView = OrgFOV;
        
    }
}
