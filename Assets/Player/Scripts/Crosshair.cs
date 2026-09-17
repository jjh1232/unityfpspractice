using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour {
    [SerializeField]
    private Animator animator;

    //크로스헤어 상태에 따른 총의 정확도
    private float gunAccuracy;

    //크로스 헤어 비활성화 위한 부모객체
    [SerializeField]
    private GameObject go_CrosshairHUD;



    public void JumpAnimation(bool _flag)
    {
        animator.SetBool("jump", _flag);

    }

    public void RunningAnimation(bool _flag)
    {
        animator.SetBool("running", _flag);

    }

    public void FireAnimation(bool _flag)
    {
     animator.SetBool("fire", _flag);

    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
