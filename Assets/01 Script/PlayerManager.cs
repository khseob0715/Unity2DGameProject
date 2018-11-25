 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MovingObject {

    static public PlayerManager instance;


    public string currentMapName; //  trasnferMap 스크립트에 있는 transferMapName의 값을 저장 시킬 예정.


    //public AudioClip walkSound_1; // 사운드 파일
    //public AudioClip walkSound_2;

    //private AudioSource audioSource; // 사운드 플레이어 

    public string walkSound_1;
    public string walkSound_2;
    public string walkSound_3;
    public string walkSound_4;

    private AudioManager theAudio;


    public float runSpeed;
    private float applyRunSpeed;
    private bool applyRunFlag = false;

    private bool canMove = true;  // 코루틴이 계속 호출 되는 것 방지 

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject); // 파괴 시키지 말라는 뜻. 
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        queue = new Queue<string>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        theAudio = FindObjectOfType<AudioManager>();

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

            bool checkCollisionFlag = base.CheckCollision();
            if (checkCollisionFlag) // 앞에 뭔가 있다! -> true, 가지 않겠다! walking false
                break;

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
