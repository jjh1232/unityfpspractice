using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minimap : MonoBehaviour
{
    public int minmap = 0;
	[SerializeField]
    private GameObject go_bigmapBase;
    [SerializeField]
    private GameObject go_minimapBase;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TryOpenminimap();

    }
    private void TryOpenminimap()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {	
			switch(minmap)
			{
			case 0:
				Openminimap();
				minmap = 1;
				break;
			case 1:
				Closeminimap();
				openbigmap();
				minmap = 2;
				break;
			case 2:
				closebigmap();
				minmap =0;
				break;
            

            }
        }

    }
    private void Openminimap()
    {
        go_minimapBase.SetActive(true);
    }
    private void Closeminimap()
    {
        go_minimapBase.SetActive(false);
    }
	private void closebigmap()
	{
		go_bigmapBase.SetActive(false);
	}
	private void openbigmap()
	{
		go_bigmapBase.SetActive(true);
	}
}
