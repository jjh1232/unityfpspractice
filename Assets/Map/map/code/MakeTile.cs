using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public struct tileSet
{
    
    public GameObject tile; // 타일 오브젝트 저장
    public int state; // 타일의 상태
    public int nature; // 타일위에 환경요소 뭐 들어갈지 정수로 저장
    // 1이면 HP아이템 2면 기력아이템
    /*tileSet()
    {
        tile = null; state = 0; nature = 0;
    }*/
}

public class MakeTile : MonoBehaviour
{
    // Start is called before the first frame update
    public int numSelectors = 0;
    public GameObject[] selectorArr;
    double sum = -2.5;
    double sum2 = -2.5;
    double sum3 = 7.5;
    double sum4 = 17.5;



    static public tileSet[,] mytile = new tileSet[50, 50];

    public GameObject selector;

    public GameObject HPitem, CPitem, BULLETitem,shielditem,stopitem;
    static int count = 0;

    public Material texture1;// 타일 이미지 1 
    public Material texture2;// 타일 이미지 2
    public Material texture3;// 타일 이미지 3 Way
    public Material texture4;// 타일 이미지 4 My Ground

    public GameObject fence1;
    public GameObject crossfence1;

    void Start()
    {
        // 구조체 배열 초기화
        for (int i = 0; i < numSelectors; i++)
        {
            for (int j = 0; j < numSelectors; j++)
            {
                int rnum = Random.Range(1, 500);

                mytile[i, j].tile = null;
                mytile[i, j].state = 0;
                mytile[i, j].nature = 0;

                if (rnum <= 1)
                {
                    mytile[i, j].nature = 1; // HP 아이템
                }
                else if (rnum <= 2)
                {
                    mytile[i, j].nature = 2; // CP 아이템
                }
                else if (rnum <= 3)
                {
                    mytile[i, j].nature = 3; // 총알증가 아이템
                }
                //mytile[i, j].navZone = GetComponent<NavMeshObstacle>();
            }
        }

        //selectorArr = new GameObject[numSelectors* numSelectors];

        for (int i = 0; i < numSelectors; i++)
        {
            for (int k = 0; k < numSelectors; k++)
            {
                //홀수 일때 타일 생성
                if ((i + k) % 2 == 1)
                {
                    mytile[i, k].tile = Instantiate(selector);
                    mytile[i, k].tile.transform.localPosition = new Vector3(5 * i, 0f, 5 * k);
                    mytile[i, k].state = 1;
                    
                    count++;
                }
                //짝수 일때 타일 생성
                else
                {
                    mytile[i, k].tile = Instantiate(selector);
                    mytile[i, k].tile.transform.localPosition = new Vector3(5 * i, 0f, 5 * k);
                    mytile[i, k].state = 2;
                    
                    count++;
                }
                if(mytile[i, k].nature == 1)
                {
                    GameObject item = Instantiate(HPitem);
                    item.transform.localPosition = new Vector3(5 * i, 0.2f, 5 * k);
                }
                else if (mytile[i, k].nature == 2)
                {
                    GameObject item = Instantiate(CPitem);
                    item.transform.localPosition = new Vector3(5 * i, 0.2f, 5 * k);
                }
                else if (mytile[i, k].nature == 3)
                {
                    GameObject item = Instantiate(BULLETitem);
                    item.transform.localPosition = new Vector3(5 * i, 0.2f, 5 * k);
                }
            }
        }

        // 담장
        for (int i = 0; i < 25; i++)
        {
            for (int k = 0; k < 25; k++)
            {
                if (i == 0)
                {
                    GameObject go = Instantiate(fence1);
                    go.transform.localPosition = new Vector3(-2.5f, 4, (float)sum);
                    go.transform.localRotation = Quaternion.Euler(new Vector3(0f, -90f, 0f));
                    sum += 10; 
                    
                }
                else if (k == 0)
                {
                    GameObject go2 = Instantiate(fence1);
                    go2.transform.localPosition = new Vector3((float)sum2 , 4, 247.5f);
                    sum2 += 10;
                }

                else if (k == 4)
                {
                    GameObject go3 = Instantiate(fence1);
                    go3.transform.localPosition = new Vector3((float)sum3, 4, -2.5f);
                    go3.transform.localRotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
                    sum3 += 10;
                }

                else if (i == 4)
                {
                    GameObject go4 = Instantiate(fence1);
                    go4.transform.localPosition = new Vector3(247.5f, 4, (float)sum4);
                    go4.transform.localRotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
                    sum4 += 10;

                }
                if (i == 4 && k == 0)
                {
                    GameObject go5 = Instantiate(crossfence1);
                    go5.transform.localPosition = new Vector3(247.5f, 4, -2.5f);
                }
                if (i == 4 && k == 4)
                {
                    GameObject go6= Instantiate(crossfence1);
                    go6.transform.localPosition = new Vector3(247.5f, 4, 247.5f);
                    go6.transform.localRotation = Quaternion.Euler(new Vector3(0f, -90f, 0f));
                }





            }
            
        }
    }

    void Update()
    {
        for (int i = 0; i < numSelectors; i++)
        {
            for (int j = 0; j < numSelectors; j++)
            {
                if (mytile[i, j].state == 1)
                {
                    mytile[i, j].tile.GetComponent<MeshRenderer>().material = texture1;// 홀 수 타일 맵
                }
                else if (mytile[i, j].state == 2)
                {
                    mytile[i, j].tile.GetComponent<MeshRenderer>().material = texture2;// 짝 수 타일 맵
                }
                else if (mytile[i, j].state == 3)
                {
                    mytile[i, j].tile.GetComponent<MeshRenderer>().material = texture3;// Way
                }
                else if (mytile[i, j].state == 4)
                {
                    mytile[i, j].tile.GetComponent<MeshRenderer>().material = texture4;// My Ground
                }
            }
        }
    }
}
