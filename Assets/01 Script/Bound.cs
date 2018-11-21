﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bound : MonoBehaviour {

    private BoxCollider2D bound;

    private CameraManager theCamera;

	// Use this for initialization
	void Start () {
        bound = GetComponent<BoxCollider2D>();
        theCamera = FindObjectOfType<CameraManager>();
        theCamera.SetBound(bound);    
	}
	
}
