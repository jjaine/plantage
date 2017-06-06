using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideCheck : MonoBehaviour {

	public bool collides = false;
	public GameObject otherFlower;
	public bool inMerge = false;
	public bool ret = false;
	public Vector3 pos;
	Color origcolor;

	void Start(){
		pos = new Vector3(0,0,-1);
	}

	void Update(){
		if(collides && !otherFlower){
			collides = false;
		}
		if(inMerge && !otherFlower)
			collides = false;
		if(inMerge && otherFlower)
			collides = true;

		if(ret){
			transform.position = pos;
			ret=false;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Flower"){
			collides=true;
			otherFlower = other.transform.parent.gameObject;
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Flower"){
			collides=false;
			otherFlower = null;
		}
	}
}
