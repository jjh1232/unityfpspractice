using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // Inspector 뷰에 노출시키려면 이것을 명시해야함
public class Anim
{

    public AnimationClip idle;
    public AnimationClip runForward;
    public AnimationClip runBackward;
    public AnimationClip runRight;
    public AnimationClip runLeft;
    //애니메이션
}


public class PlayerCtrl : MonoBehaviour {
    
    private float h = 0.0f;
    private float v = 0.0f;
    private float jumpForce = 6.0f;


    public Transform player;
    public float rotSpeed = 100.0f; // 마우스 회전속도
  


    public Anim anim;
    public Animation _animation;

    //상태변수
   
    static public bool isGround = true;

 

    
    //땅 착지 여부
    private CapsuleCollider playerCollider;

    //필요 컴포넌트
    private Rigidbody myRigid;

    private Transform tr; //이동속도변수
    public float moveSpeed = 10.0f; // 플레이어 이동속도
    private Crosshair theCrosshair;

    [SerializeField]
    private Inventory theInventory;
	public StatusController thePlayer;
	public AudioClip itempick;
	public AudioClip itempick2;


    void Start () {
        theCrosshair = FindObjectOfType<Crosshair>();
        myRigid = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
        tr = GetComponent<Transform>();
        _animation = GetComponentInChildren<Animation>();

        _animation.clip = anim.idle;
        _animation.Play();
    
}

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Item")
        {
            other.gameObject.SetActive(false);
			sound.instance.RandomizeSfx(itempick);
            theInventory.AcquireItem(other.GetComponent<ItemPickUp>().item);
        }
        if (other.tag == "bullet")
        {
            other.gameObject.SetActive(false);
			sound.instance.RandomizeSfx(itempick2);
            thePlayer.bulletin(10);
        }
		 if (other.tag == "mbullet")
        {
            other.gameObject.SetActive(false);
            thePlayer.DecreaseHP(10);
        }
    }

    //땅에 있을때만 점프 체크
    private void IsGround()
    {
        //
        isGround = Physics.Raycast(transform.position, Vector3.down, 0.1f);
        theCrosshair.JumpAnimation(false);
    }



    //점프함수
    private void Jump()
    {
        myRigid.velocity = transform.up * jumpForce;
        theCrosshair.JumpAnimation(true);
    }


 
        
    



    // Update is called once per frame
    void Update () {
        h = Input.GetAxis("Horizontal"); //수평
        v = Input.GetAxis("Vertical"); //수직

       



        //땅과 출돌체크
        IsGround();



        

        //상,하,좌,우 움직임
        //Debug.Log("H = " + h.ToString());
        //Debug.Log("V = " + v.ToString());

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        
        tr.Translate(moveDir * Time.deltaTime * moveSpeed, Space.Self);




        //카메라
        tr.Rotate(Vector3.up * Time.deltaTime * rotSpeed * Input.GetAxis("Mouse X"));  // 카메라 좌우 방향 움직임 (세로방향은 CameraCtrl 스크립트에 있음
        

        //F키 누르면 180도 회전
        if (Input.GetKeyDown(KeyCode.F))
        {

            tr.Rotate(0f, 180f, 0f);

        }




        //달리기(속도변경)
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 20.0f;
            theCrosshair.RunningAnimation(true);
            
        }



        //걷기
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = 10.0f;

            theCrosshair.RunningAnimation(false);
            



            
        }

        
        









        //점프
        if (Input.GetKeyDown(KeyCode.Space) && isGround == true)
        {
            Jump();
        }


        






        //각 애니메이션 움직임때 마다 조정
            if (v >= 0.1f)
        {

            _animation.CrossFade(anim.runForward.name, 0.03f);
        }

        else if (v <= -0.1f)
        {
            _animation.CrossFade(anim.runBackward.name, 0.3f);
        }
        else if (h >= 0.1f)
        {
            _animation.CrossFade(anim.runRight.name, 0.3f);
        }
        else if (h <= -0.1f)
        {
            _animation.CrossFade(anim.runLeft.name, 0.3f);
        }
        else
        {
            _animation.CrossFade(anim.idle.name, 0.3f);

        }
    }



   






}
