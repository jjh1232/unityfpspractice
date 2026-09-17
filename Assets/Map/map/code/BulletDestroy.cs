using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "BULLET")
            Destroy(coll.gameObject);

    }
    // Update is called once per frame

}
