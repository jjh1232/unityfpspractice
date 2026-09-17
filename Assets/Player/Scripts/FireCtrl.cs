using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour {
    public GameObject bullet; //총알발사좌표
    public Transform firePos;

    //마우스왼쪽버튼을클릭했을때Fire 함수호출
    private Crosshair theCrosshair;

    

    // Use this for initialization
    void Start () {
        theCrosshair = FindObjectOfType<Crosshair>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
            theCrosshair.FireAnimation(true);
           
        }
        else
            theCrosshair.FireAnimation(false);
    }
	




    void Fire()  {
        CreateBullet();
    }

    void CreateBullet()
    {
        Instantiate(bullet, firePos.position, firePos.rotation);
        

    }
}

