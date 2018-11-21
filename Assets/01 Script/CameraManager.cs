using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    static public CameraManager instance;

    public GameObject target; // 카메라가 따라갈 대상
    public float moveSpeed;   // 카메라가 얼마나 빠른 속도로 대상을 따라갈 것인지. 

    private Vector3 targetPosition; // 대상의 현재 위치 

    public BoxCollider2D bound;

    private Vector3 minBound;
    private Vector3 maxBound;

    // 박스 컬라이더 영역의 최소 최대 xyz 값을 지님. 

    private float halfWidth;
    private float halfHeight;
    // 카메라의 반너비 반 높이 값을 지닐 변수 

    private Camera theCamera;
    // 카메라의 반 높이 값을 구할 속성을 이용하기 위한 변수 

    private void Awake()
    {
        // start 보다 먼저 실행 됨. 
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }

    // Use this for initialization
    void Start()
    {
        theCamera = GetComponent<Camera>();
        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;

        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;   // 해상도

    }
	
	// Update is called once per frame
	void Update () {
        // 카메라는 매 프레임마다 대상을 따라 가야함 
        if (target.gameObject != null)
        {
            targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);

            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime); // 1초에 moovSpeed 만큼 이동. 


            // 실질적인 카메라 영역 가두기 
            float clampedxX = Mathf.Clamp(this.transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);
            float clampedxY = Mathf.Clamp(this.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);
            // (10, 0, 100)  // 10이 값, 0이 최소값, 100이 최대값
            // 리턴 값 10 

            // (-100, 0, 100)
            // 리턴 값 0

            // (1000, 0, 100)
            // 리턴값 100

            this.transform.position = new Vector3(clampedxX, clampedxY, this.transform.position.z);
        }

	}

    public void SetBound(BoxCollider2D newBound)
    {
        bound = newBound;

        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;

    }
}
