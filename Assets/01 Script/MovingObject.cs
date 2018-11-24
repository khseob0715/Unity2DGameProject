using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float speed;        // 캐릭터의 속도 담당
    public int walkCount;
    protected int currentWalkCount;

    protected Vector3 vector;    // 3개의 값을 동시에 갖는 값,

    public BoxCollider2D boxCollider;
    public LayerMask layerMask;  // 통과 불가능한 레이어 설정, 레이캐스트 
    public Animator animator;

    protected bool npcCanMove = true;

    protected void Move(string _dir, int _frequency)
    {
        StartCoroutine(MoveCoroutine(_dir, _frequency));
    }

    IEnumerator MoveCoroutine(string _dir, int _frequncy)
    {
        npcCanMove = false;
        vector.Set(0, 0, vector.z);
        switch (_dir)
        {
            case "UP":
                vector.y = 1f;
                break;
            case "DOWN":
                vector.y = -1f;
                break;
            case "RIGHT":
                vector.x = 1f;
                break;
            case "LEFT":
                vector.x = -1f;
                break;
        }

        animator.SetFloat("DirX", vector.x);
        animator.SetFloat("DirY", vector.y);
        animator.SetBool("Walking", true);

        while (currentWalkCount < walkCount)
        {
            transform.Translate(vector.x * speed, vector.y * speed, 0);

            currentWalkCount++;
            yield return new WaitForSeconds(0.01f);
        }
        currentWalkCount = 0;
        if (_frequncy != 5)
        {
            animator.SetBool("Walking", false);
        }
        

        npcCanMove = true;
    }

    protected bool CheckCollision()
    {
        RaycastHit2D hit;

        Vector2 start = transform.position; // A 지점, 캐릭터의 현재 위치 값.
        Vector2 end = start + new Vector2(vector.x * speed * walkCount, vector.y * speed * walkCount); 
        // B 지점, 캐릭터가 이동하고자 하는 위치 값. 

        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, layerMask);
        boxCollider.enabled = true;

        if(hit.transform != null)
        {
            return true;
        }

        return false;
    }
}
