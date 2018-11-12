using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{

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
        animator = GetComponent<Animator>();
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

            if(vector.x != 0)
            {
                vector.y = 0;
            }

            if(vector.y != 0)
            {
                vector.x = 0;
            }
            animator.SetFloat("DirX", vector.x); // vector.x 의 값을 DirX로 전달.
            animator.SetFloat("DirY", vector.y);
            animator.SetBool("Walking", true);

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
