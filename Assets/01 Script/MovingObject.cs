﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    static public MovingObject instance;


    public string currentMapName; //  trasnferMap 스크립트에 있는 transferMapName의 값을 저장 시킬 예정.

    private BoxCollider2D boxCollider;
    public LayerMask layerMask;  // 통과 불가능한 레이어 설정, 레이캐스트 

    //public AudioClip walkSound_1; // 사운드 파일
    //public AudioClip walkSound_2;

    //private AudioSource audioSource; // 사운드 플레이어 

    public string walkSound_1;
    public string walkSound_2;
    public string walkSound_3;
    public string walkSound_4;

    private AudioManager theAudio;

    public float speed;        // 캐릭터의 속도 담당

    private Vector3 vector;    // 3개의 값을 동시에 갖는 값,

    public float runSpeed;
    private float applyRunSpeed;
    private bool applyRunFlag = false;


    public int walkCount;
    private int currentWalkCount;

    private bool canMove = true;  // 코루틴이 계속 호출 되는 것 방지 

    private Animator animator;

    // Use this for initialization
    void Start()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject); // 파괴 시키지 말라는 뜻. 
            animator = GetComponent<Animator>();
            //audioSource = GetComponent<AudioSource>();
            boxCollider = GetComponent<BoxCollider2D>();
            theAudio = FindObjectOfType<AudioManager>();
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    IEnumerator MoveCoroutine()
    {
        while (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                applyRunSpeed = runSpeed;
                applyRunFlag = true;
            }
            else
            {
                applyRunSpeed = 0;
                applyRunFlag = false;
            }

            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);
            // z 축은 변하지 않는 값

            if (vector.x != 0)
            {
                vector.y = 0;
            }

            if (vector.y != 0)
            {
                vector.x = 0;
            }
            animator.SetFloat("DirX", vector.x); // vector.x 의 값을 DirX로 전달.
            animator.SetFloat("DirY", vector.y);

            RaycastHit2D hit;
            // A지점에서 B 지점까지 레이저를 쐈을 때, B지점에 도착하였을 경우 hit = null / 도착하지 못하고 방해물에 도착하였을 경우 hit = 방해물 오브젝트  
            Vector2 Start = transform.position;  // A 지점  // 캐릭터의 현재 위치 값 
            Vector2 End = Start + new Vector2(vector.x * speed * walkCount, vector.y * speed * walkCount);    // B 지점  // 캐릭터가 이동하고자 하는 위치 값

            boxCollider.enabled = false;
            hit = Physics2D.Linecast(Start, End, layerMask);
            boxCollider.enabled = true;


            if (hit.transform != null)
            {
                break;
            }

            animator.SetBool("Walking", true);

            int temp = Random.Range(1, 4);
            switch (temp)
            {
                case 1:
                    theAudio.Play(walkSound_1);
                    break;
                case 2:
                    theAudio.Play(walkSound_2);
                    break;
                case 3:
                    theAudio.Play(walkSound_3);
                    break;
                case 4:
                    theAudio.Play(walkSound_4);
                    break;
            }


            while (currentWalkCount < walkCount)
            {
                if (vector.x != 0)
                {
                    transform.Translate(vector.x * (speed + applyRunSpeed), 0, 0);

                }
                else if (vector.y != 0)
                {
                    transform.Translate(0, vector.y * (speed + applyRunSpeed), 0);
                    // Translate 함수는 현재 있는 값에서 주어진 값 만큼 더해주는 것
                }
                if (applyRunFlag)
                {
                    currentWalkCount++;
                }
                currentWalkCount++;
                yield return new WaitForSeconds(0.01f);


            }
            currentWalkCount = 0;

        }

        animator.SetBool("Walking", false);


        canMove = true;

    }

    // Update is called once per frame
    void Update()
    {
        // 매 프레임마다 함수를 실행. 

        if (canMove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {   // 우 방향키가 눌리면 1리턴, 좌 방향키가 눌리면 -1리턴
                // Input.GetAxisRaw("Vertical")   상향키가 눌리면 1, 하향키가 눌리면 -1 리턴

                // 위 조건은 상하좌우키가 눌린 것. 

                canMove = false;
                StartCoroutine(MoveCoroutine());

            }
        }


    }
}
