﻿using UnityEngine;
using System.Collections;

public class Yrotator : MonoBehaviour {
    /// <summary>
    /// /////////////////////////////////////////////////
    /// </summary>
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);

    }
}
