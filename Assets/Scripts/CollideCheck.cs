using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideCheck : MonoBehaviour {

	public bool collides = false;

	void OnTriggerStay2D(Collider2D other){
		if(PlayTurn.play){
			if(other.tag == "Flower"){
				collides=true;
				Debug.Log("Collisions detected with " + other.name);
			}
		}
	}

}
