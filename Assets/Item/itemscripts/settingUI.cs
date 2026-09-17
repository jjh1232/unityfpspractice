using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class settingUI : MonoBehaviour
{
    GameObject PlayerState;
    GameObject ItemState;
    GameObject Minimap;
    Canvas myCanvas;

    float Cwidth, Cheight, myScale;

    // Start is called before the first frame update
    void Start()
    {
        PlayerState = GameObject.Find("status");
        ItemState = GameObject.Find("Inventory");
        Minimap = GameObject.Find("RawImage");
        myCanvas = FindObjectOfType<Canvas>();

        Cwidth = myCanvas.GetComponent<RectTransform>().rect.width;
        Cheight = myCanvas.GetComponent<RectTransform>().rect.height;


       myScale = Cwidth / PlayerState.GetComponent<RectTransform>().rect.width;

        //PlayerState.transform.localScale = new Vector2(myScale, myScale);

        PlayerState.transform.position = new Vector2(Cwidth/2, Cheight-(myScale * 7));
        ItemState.transform.position = new Vector2(myScale * 5,myScale * 5);
        Minimap.transform.position = new Vector2(Cwidth - (myScale * 10),myScale *10);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Cwidth + " " + Cheight + " " + PlayerState.GetComponent<RectTransform>().rect.width);
    }
}
