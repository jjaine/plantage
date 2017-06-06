using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

	float x,y;
	public bool allow = true;
	float speed = 30.0f;
	float maxSpeed = 100.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		x = Input.mousePosition.x;
    	y = Input.mousePosition.y;
    	if(!Input.GetMouseButton(0)){
    		transform.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    	}
	}

	void OnMouseDrag(){
		if(allow){
			if(!PlayTurn.play){
				Vector3 v = (Camera.main.ScreenToWorldPoint(new Vector3(x,y,9.0f))-transform.position)*speed;
    			if(v.magnitude > maxSpeed) 
    				v*=maxSpeed/v.magnitude;
    				transform.GetComponent<Rigidbody2D>().velocity = v;
			}
		}
	}
}