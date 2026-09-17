using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlienCtrl : MonoBehaviour {

    public enum CurrentState { idle, trace, attack, hit, dead };
    public CurrentState curState = CurrentState.idle;

    private Transform _transform;
    private Transform playerTransform;
    private NavMeshAgent nvAgent;
    private Animator _animator;

    public float traceDist = 30.0f; // 추적 범위
    public float attackDist = 15.0f; // 공격 범위
    public int HP = 1;
    public bool Look = true;
    public bool Reptile = false;

    private bool isDead = false; // 사망 여부

    public Transform[] points;
    public int nextIdx = 1;

    public float speed = 1.0f; //이동속도
    public float damping = 5.0f; //회전속도


    public GunScript Gun;
    public float CurrentDelay = 10;
    public bool Attacktrigger;

    void Start() {
        _transform = this.gameObject.GetComponent<Transform>();
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
        _animator = this.gameObject.GetComponent<Animator>();

        StartCoroutine(this.CheckState());
        StartCoroutine(this.CheckStateForAction());

        points = GameObject.Find("WayPointGroup (1)").GetComponentsInChildren<Transform>();

        Attacktrigger = false;
    }


    IEnumerator CheckState()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(0.01f);

            float dist = Vector3.Distance(playerTransform.position, _transform.position);

            if (dist <= attackDist)
            {
                curState = CurrentState.attack;
            }
            else if (dist <= traceDist)
            {
                curState = CurrentState.trace;
            }
            else
            {
                Quaternion rot = Quaternion.LookRotation(points[nextIdx].position - _transform.position);
                _transform.rotation = Quaternion.Slerp(_transform.rotation, rot, Time.deltaTime * damping);
                _transform.Translate(Vector3.forward * Time.deltaTime * speed);
                
                curState = CurrentState.idle;
            }
        }
    }
    IEnumerator CheckStateForAction()
    {
        while (!isDead)
        {
            switch (curState)
            {
                case CurrentState.idle:
                    _animator.SetBool("isTrace", false);
                    _animator.SetBool("isAttack", false);
                    break;
                case CurrentState.trace:
                    nvAgent.destination = playerTransform.position;
                    _animator.SetBool("isTrace", true);
                    _animator.SetBool("isAttack", false);

                    CurrentDelay -= 0.1f; //공격투사체 딜레이
                    if (Attacktrigger == true && CurrentDelay <= 0) // 투사체 날리는 함수 실행
                    {
                        Gun.Fire();
                        Attacktrigger = false;
                        CurrentDelay = 3;
                    }

                    if (Look == true)
                        if (Reptile == true)
                            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerTransform.transform.position - transform.position), Time.smoothDeltaTime * 5.0f);
                        else if (Reptile == false)
                            this.transform.LookAt(this.playerTransform.position);
                    break;
                case CurrentState.attack:
                    _animator.SetBool("isAttack", true);
                    if (Look == true)
                    {
                        if(Reptile == true)
                            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerTransform.transform.position - transform.position), Time.smoothDeltaTime * 5.0f);
                        else if(Reptile == false)
                            this.transform.LookAt(this.playerTransform.position);                        
                    }

                    CurrentDelay -= 0.1f; //공격투사체 딜레이
                    if (Attacktrigger == true && CurrentDelay <= 0) // 투사체 날리는 함수 실행
                    {
                        Gun.Fire();
                        Attacktrigger = false;
                        CurrentDelay = 3;
                    }
                    break;
            }

            yield return null;
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "BULLET")
        {
            curState = CurrentState.hit;
            Destroy(coll.gameObject);
            _animator.SetTrigger("isHit");
            HP = HP - 1;
            if (HP<=0)
            {
                StopAllCoroutines();
                curState = CurrentState.dead;
                _animator.SetTrigger("isDead"); // 애니메이터 변수
                isDead = true; //현재 상태
                gameObject.GetComponentInChildren<CapsuleCollider>().enabled = false; // 콜라이더 끄기
                Destroy(gameObject, 7f); //시체삭제
            }
        }
    }

    // Update is called once per frame
    void Update () {
        
    }
    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "WAY_POINT")
        {
            nextIdx = (++nextIdx >= points.Length) ? 1 : nextIdx;
           
        }       

    }
}
