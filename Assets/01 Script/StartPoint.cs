﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour {
    public string startPoint;   // 맵 이동시 플레이어가 시작될 위치 

    private PlayerManager thePlayer;
    private CameraManager theCamera;

	// Use this for initialization
	void Start () {

        thePlayer = FindObjectOfType<PlayerManager>();
        theCamera = FindObjectOfType<CameraManager>();

        if(startPoint == thePlayer.currentMapName)
        {
            // 
            theCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, theCamera.transform.position.z);
            thePlayer.transform.position = this.transform.position;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
