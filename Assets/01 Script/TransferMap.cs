using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour {

    public string trasferMapName; // 이동할 맵의 이름

    public BoxCollider2D targetBound;

    //private CameraManager theCamera;
    private MovingObject thePlayer;
    
    // Use this for initialization
	void Start () {
        thePlayer = FindObjectOfType<MovingObject>(); // 다수의 객체. getComponent랑 쓰임새는 비슷하지만 범위의 차이. 
        //theCamera = FindObjectOfType<CameraManager>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 내장 함수. // 박스 콜라이더에 is triger를 활성화 시켜준다. 
        // 움직이는 물체는 rigidbody를 갖고 있어야 한다. 
        if(collision.gameObject.name == "Player")
        {
            thePlayer.currentMapName = trasferMapName;

            //theCamera.SetBound(targetBound);

            SceneManager.LoadScene(trasferMapName);

        }
    }
}
