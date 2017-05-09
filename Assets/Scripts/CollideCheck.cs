using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideCheck : MonoBehaviour {

	public bool collides = false;
	public GameObject otherFlower;
	public bool inMerge = false;

	void Update(){
		if(collides && !otherFlower){
			collides = false;
		}
		if(inMerge && !otherFlower)
			collides = false;
		if(inMerge && otherFlower)
			collides = true;
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Flower"){
			collides=true;
			otherFlower = other.gameObject;
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Flower"){
			collides=false;
			otherFlower = null;
		}
	}

}
