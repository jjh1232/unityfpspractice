using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camareTest : MonoBehaviour
{
    public Transform mainCamera;

    float cameraY = 0;


    void Update()
    {
        MouseLook();
    }

    void MouseLook()
    {
       

        cameraY -= Input.GetAxisRaw("Mouse Y") * 100.0f * Time.deltaTime;     //상하는 카메라만 움직인다.
        

        if (cameraY < -60)      //위로 쳐다볼때 뒤로 넘어가지 않도록 한다.
            cameraY = -60;
        else if (60 < cameraY)  //아래로 쳐다볼때 뒤로 넘어가지 않도록 한다.
            cameraY = 60;

    


        mainCamera.localRotation = Quaternion.Euler(cameraY, 0, 0); //카메라 움직이는 기능
        
    }
}



