using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigmapcam : MonoBehaviour
{
    GameObject Players;
    GameObject bigcams;
    // Start is called before the first frame update
    void Start()
    {
        Players = GameObject.Find("Player");
        bigcams = GameObject.Find("bigmapcam");
    }

    // Update is called once per frame
    void Update()
    {
        
        bigcams.transform.position = new Vector3(Players.transform.position.x, bigcams.transform.position.y, Players.transform.position.z);
    }
}
