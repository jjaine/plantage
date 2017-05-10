using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateGoal : MonoBehaviour {

	public GameObject Flowers;
	// Use this for initialization
	void Start () {
		int i = Random.Range(0, Flowers.GetComponent<PlayTurn>().prefabs.Length);
		GameObject goal = Instantiate(Flowers.GetComponent<PlayTurn>().prefabs[i], new Vector2(0,0), Quaternion.identity);
		Sprite flower = goal.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
		Sprite leaf = goal.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite;
		gameObject.transform.GetChild(0).GetComponent<Image>().sprite = flower;
		gameObject.transform.GetChild(1).GetComponent<Image>().sprite = leaf;
		Destroy(goal);


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
