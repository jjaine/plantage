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
			otherFlower = other.gameObject;
		}
		if(other.tag == "Bar"){
			GetComponent<Move>().allow=false;
			if(other.transform.name == "bottombar"){
				if(transform.position.x <= other.transform.position.x){
	            	pos = new Vector3(transform.position.x-0.2f, transform.position.y, -1);
	            }
	            else{
	            	pos = new Vector3(transform.position.x+0.2f, transform.position.y, -1);
	            }
			}
			if(other.transform.name == "rightbar"){
				Debug.Log(transform.position);
				if(transform.position.x > 4.8f){
					if(transform.position.y <= 2.2f)
						pos = new Vector3(transform.position.x+0.3f, transform.position.y-0.3f, -1);
					else{
					 	if(transform.position.y <= transform.position.x)
							pos = new Vector3(transform.position.x-0.3f, transform.position.y+0.3f, -1);
						else
							pos = new Vector3(transform.position.x+0.3f, transform.position.y-0.3f, -1);
					}
				}
				else{
					if(transform.position.y > 2.2f)
						pos = new Vector3(transform.position.x-0.3f, transform.position.y+0.3f, -1);
					else{
						if(transform.position.y <= transform.position.x)
							pos = new Vector3(transform.position.x-0.3f, transform.position.y+0.3f, -1);
						else
							pos = new Vector3(transform.position.x+0.3f, transform.position.y-0.3f, -1);
					}
				}		
			}
			if(other.transform.name == "leftbar"){
				Debug.Log(transform.position);
				if(transform.position.x < -4.8f){
					if(transform.position.y < 2.2f)
						pos = new Vector3(transform.position.x-0.3f, transform.position.y-0.3f, -1);
					else{
					 	if(transform.position.y <= transform.position.x)
							pos = new Vector3(transform.position.x+0.3f, transform.position.y+0.3f, -1);
						else
							pos = new Vector3(transform.position.x-0.3f, transform.position.y-0.3f, -1);
					}
				}
				else{
					if(transform.position.y > 2.2f)
						pos = new Vector3(transform.position.x+0.3f, transform.position.y+0.3f, -1);
					else{
						if(transform.position.y <= transform.position.x)
							pos = new Vector3(transform.position.x+0.3f, transform.position.y+0.3f, -1);
						else
							pos = new Vector3(transform.position.x-0.3f, transform.position.y-0.3f, -1);
					}
				}		
			}
            ret = true;
            origcolor = transform.GetChild(0).GetComponent<SpriteRenderer>().color;
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Flower"){
			collides=false;
			otherFlower = null;
		}
		if(other.tag == "Bar"){
			StartCoroutine(Wait());
		}
	}

	IEnumerator Wait(){
		yield return new WaitForSeconds(1);
		GetComponent<Move>().allow=true;
		transform.GetChild(0).GetComponent<SpriteRenderer>().color = origcolor;
	}

}
