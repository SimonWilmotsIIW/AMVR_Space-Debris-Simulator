﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyFind : MonoBehaviour {
    
    private Text exclamation;

    void Awake()
    {
        //exclamation = this.transform.Find("ExCanvas/ExclamationText").GetComponent<Text>();
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter(Collider other)
    {
        //Debug.LogError("enter");
        //exclamation.enabled = true;
    }

    void OnTriggerExit(Collider other)
    {
        //Debug.LogError("exit");
        //exclamation.enabled = false;
    }
}
