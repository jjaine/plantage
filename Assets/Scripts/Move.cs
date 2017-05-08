using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

	float x,y;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		x = Input.mousePosition.x;
    	y = Input.mousePosition.y;
	}
	void OnMouseDrag(){
    	transform.position = Camera.main.ScreenToWorldPoint(new Vector3(x,y,9.0f));
	}
}
