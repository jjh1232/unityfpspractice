using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour {
    public int damage = 20; //총알발사속도 
    public float speed = 1000.0f;
    public float timedie = 10.0f;

   

    

        void Start()
    {
        //ScreenCenter = new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2);
        //transform.LookAt(go.transform);
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
        Destroy(this.gameObject, timedie);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
