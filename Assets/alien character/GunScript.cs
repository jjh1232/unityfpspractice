using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public Rigidbody Gun;
    public float BulletSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        Rigidbody rigidbody = (Rigidbody)Instantiate(Gun, transform.position, transform.rotation);
        rigidbody.velocity = transform.TransformDirection(new Vector3(0, 0, BulletSpeed));
    }
}
