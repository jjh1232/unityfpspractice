using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EatGround : MonoBehaviour
{
    public int playsize = 50;

    GameObject player;
    float posX, posZ; // 캐릭터의 x, z 좌표
    int tileX, tileY; // 캐릭터가 밟고있는 타일좌표 보기편하게 Z는 Y로 치환
    int tempX, tempY; // 계산량을 줄이기 위한 임시값 *밟고있는 타일좌표가 바뀔때만 계산하도록

    static public bool safe, init;

    static public bool[,] safeZone = new bool[50, 50]; // My Ground = T
    bool[,] wayZone = new bool[50, 50]; // 지나고 있는 경로 = T
    int[] maxX = new int[50]; // 이동범위 중 행의 최대값
    int[] maxY = new int[50]; // 이동범위 중 열의 최대값
    int[] minX = new int[50]; // 이동범위 중 행의 최솟값
    int[] minY = new int[50]; // 이동범위 중 열의 최솟값

    //public NavMeshObstacle[,] navZone = new NavMeshObstacle[50, 50];

    // 캐릭터의 안전여부 판단하는 함수
    private void CheckSafe(int xx, int yy)
    {
        if (safeZone[xx, yy] == false)
        { // 땅을 먹으러 나가는 순간
            safe = false;
        }
        else if (safeZone[xx, yy] == true)
        { // 땅을 먹는 순간
            safe = true;
            EatMyGround(); // 땅 먹는 함수
            ReSet(); // 땅먹는 관련 변수들 초기화
        }
    }

    private void EatMyGround()
    {
        Debug.Log("땅먹음!");
        for (int i = 0; i < playsize; i++)
        {
            for (int j = 0; j < playsize; j++)
            {
                if (j >= minX[i] && j <= maxX[i] && i >= minY[j] && i <= maxY[j])
                {
                    safeZone[i, j] = true;
                    MakeTile.mytile[i, j].state = 4;
                    MakeTile.mytile[i, j].tile.GetComponent<NavMeshObstacle>().enabled = true;
                }
            }
        }

        Innerpaint();

        //throw new NotImplementedException();
    }

    private void Innerpaint()
    {
        for (int i = 0; i < playsize; i++)
        {
            for (int j = 0; j < playsize; j++)
            {
                if (!safeZone[i, j])
                {
                    bool left = false, right = false, up = false, down = false;
                    for (int a = i; a >= 0; a--)
                    {
                        if (safeZone[a, j] == true)
                        {
                            up = true;
                            break;
                        }
                    }
                    if (up)
                    {
                        for (int a = i; a < playsize; a++)
                        {
                            if (safeZone[a, j] == true)
                            {
                                down = true;
                                break;
                            }
                        }
                    }
                    if (down)
                    {
                        for (int a = j; a < playsize; a++)
                        {
                            if (safeZone[i, a] == true)
                            {
                                right = true;
                                break;
                            }
                        }
                    }
                    if (right)
                    {
                        for (int a = j; a >= 0; a--)
                        {
                            if (safeZone[i, a] == true)
                            {
                                left = true;
                                break;
                            }
                        }
                    }
                    if (left && right && up && down)
                    {
                        safeZone[i, j] = true;
                        MakeTile.mytile[i, j].state = 4;
                    }
                }
            }
        }

        //throw new NotImplementedException();
    }

    private void ReSet()
    {
        for (int i = 0; i < playsize; i++)
        {
            maxX[i] = -1;
            maxY[i] = -1;
            minX[i] = -1;
            minY[i] = -1;
        }
        for (int i = 0; i < playsize; i++)
            for (int j = 0; j < playsize; j++)
                wayZone[i, j] = false;

        //throw new NotImplementedException();
    }

    private void CheckArea(int xx, int yy)
    { // xx, yy는 캐릭터 현재위치
        if (maxX[xx] == -1 && minX[xx] == -1)
        {
            maxX[xx] = yy; minX[xx] = yy;
        }
        if (maxY[yy] == -1 && minY[yy] == -1)
        {
            maxY[yy] = xx; minY[yy] = xx;
        }

        if (maxX[xx] < yy)
        { // 저장된 X의 최대값 보다 현재 X위치가 크면
            maxX[xx] = yy;
        }
        if (minX[xx] > yy)
        {
            minX[xx] = yy;
        }

        if (maxY[yy] < xx)
        {
            maxY[yy] = xx;
        }
        if (minY[yy] > xx)
        {
            minY[yy] = xx;
        }
    }

    private void PaintareaX(int py)
    {
        bool low = false;
        for (int i = 0; i < playsize; i++)
        {
            if (minX[i] == maxX[i] && minX[i] != -1)
            {
                int temp = py;
                for (int j = temp; j >= 0; j--)
                {
                    if (safeZone[i, j] == true)
                    {
                        minX[i] = j;
                        low = true;
                        break;
                    }
                }
            }
        }
        if (!low)
        {
            for (int i = 0; i < playsize; i++)
            {
                if (minX[i] == maxX[i] && minX[i] != -1)
                {
                    int temp = py;
                    for (int j = temp; j < playsize; j++)
                    {
                        if (safeZone[i, j] == true)
                        {
                            maxX[i] = j;
                            break;
                        }
                    }
                }
            }
        }
    }

    private void PaintareaY(int px)
    {
        bool low = false;
        for (int i = 0; i < playsize; i++)
        {
            if (minY[i] == maxY[i] && minY[i] != -1)
            {
                int temp = px;
                for (int j = temp; j >= 0; j--)
                {
                    if (safeZone[j, i] == true)
                    {
                        minY[i] = j;
                        low = true;
                        break;
                    }
                }
            }
        }
        if (!low)
        {
            for (int i = 0; i < playsize; i++)
            {
                if (minY[i] == maxY[i] && minY[i] != -1)
                {
                    int temp = px;
                    for (int j = temp; j < playsize; j++)
                    {
                        if (safeZone[j, i] == true)
                        {
                            maxY[i] = j;
                            break;
                        }
                    }
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        safe = true; init = false;

        for (int i = 0; i < 50; i++)
            for (int j = 0; j < 50; j++)
                safeZone[i, j] = false;

        posX = player.transform.position.x;
        posZ = player.transform.position.z;

        tileX = (int)((posX + 2.5) / 5);
        tileY = (int)((posZ + 2.5) / 5);

        tempX = tileX; tempY = tileY;
    }

    // Update is called once per frame
    void Update()
    {
        if (!init)
        {
            ReSet();
            init = true;
            for (int i = 24; i < 27; i++)
            {
                for (int j = 24; j < 27; j++)
                {
                    safeZone[i, j] = true;
                    MakeTile.mytile[i, j].state = 4;
                    MakeTile.mytile[i, j].tile.GetComponent<NavMeshObstacle>().enabled = true;
                }
            }
        }

        posX = player.transform.position.x;
        posZ = player.transform.position.z;

        //Debug.Log("캐릭터의 위치는 " + posX + " " + posZ);

        tileX = (int)((posX + 2.5) / 5);
        tileY = (int)((posZ + 2.5) / 5);

        if(tileX!=tempX || tileY != tempY)
        {
            // 타일을 이동했을 때.
            Debug.Log("타일이동!");
            Debug.Log("캐릭터의 타일위치는 " + tileX + " " + tileY);

            CheckSafe(tileX, tileY);

            if (safeZone[tileX, tileY] != true)
            {
                MakeTile.mytile[tileX, tileY].state = 3;
                wayZone[tileX, tileY] = true;

                CheckArea(tileX, tileY);
            }

            PaintareaX(tileY);
            PaintareaY(tileX);


            tempX = tileX; tempY = tileY;
        }

        //MakeTile.mytile[tileX, tileY].state = 3;

        //Debug.Log("캐릭터의 타일위치는 " + tileX + " " + tileY);

        //for (int i=0;i<50;i++)
        //MakeTile.mytile[1, i].state = 3;
    }
}
