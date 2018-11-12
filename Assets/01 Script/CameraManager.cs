using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    public GameObject target; // 카메라가 따라갈 대상
    public float moveSpeed;   // 카메라가 얼마나 빠른 속도로 대상을 따라갈 것인지. 

    private Vector3 targetPosition; // 대상의 현재 위치 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // 카메라는 매 프레임마다 대상을 따라 가야함 
        if (target.gameObject != null)
        {
            targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);

            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime); // 1초에 moovSpeed 만큼 이동. 
        }

	}
}
