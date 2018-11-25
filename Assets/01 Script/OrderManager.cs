using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour {

    private PlayerManager thePlayer; // 이벤트 도중에 키 입력 처리 방지. 

    private List<MovingObject> characters;
    // add() Remove() Clear()

	// Use this for initialization
	void Start () {
        thePlayer = FindObjectOfType<PlayerManager>();

	}
	
    public void PreLoadCharacter()
    {
        characters = ToList();
    }

    public List<MovingObject> ToList()
    {

        List<MovingObject> tempList = new List<MovingObject>();
        MovingObject[] temp = FindObjectsOfType<MovingObject>();  // 모든걸 찾아서 반환, s

        for(int i = 0; i < temp.Length; i++)
        {
            tempList.Add(temp[i]);
        }

        return tempList; 
    }

    public void Move(string _name, string _dir) // 어떤 캐릭터를 어떤 방향으로 움직일 것인가? 
    {
        for(int i = 0; i < characters.Count; i++)
        {
            if(_name == characters[i].characterName)
            {
                characters[i].Move(_dir);
            }
        }
    }

    public void Turn(string _name, string _dir)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (_name == characters[i].characterName)
            {
                characters[i].animator.SetFloat("DirX", 0f);
                characters[i].animator.SetFloat("DirY", 0f);
                switch (_dir)
                {
                    case "UP":
                        characters[i].animator.SetFloat("DirY", 1f);
                        break;
                    case "DOWN":
                        characters[i].animator.SetFloat("DirY", -1f);
                        break;
                    case "LEFT":
                        characters[i].animator.SetFloat("DirX", -1f);
                        break;
                    case "RIGHT":
                        characters[i].animator.SetFloat("DirX", 1f);
                        break;
                }
                
            }
        }
    }

    public void SetTransparent(string _name){
        for (int i = 0; i < characters.Count; i++)
        {
            if (_name == characters[i].characterName)
            {
                characters[i].gameObject.SetActive(false);
            }
        }
    }

    public void SetUnTransparent(string _name)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (_name == characters[i].characterName)
            {
                characters[i].gameObject.SetActive(true);
            }
        }
    }

    public void SetThoroght(string _name)  // 벽 뚫기
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (_name == characters[i].characterName)
            {
                characters[i].boxCollider.enabled = false;
            }
        }
    }

    public void SetUnThoroght(string _name)  // 벽 뚫기
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (_name == characters[i].characterName)
            {
                characters[i].boxCollider.enabled = true;
            }
        }
    }


    // Update is called once per frame
    void Update () {
		
	}
}
