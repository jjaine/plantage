using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateGoal : MonoBehaviour {

	public GameObject Flowers;
	int l=0, f=0;
	string c;

	// Use this for initialization
	void Start () {
		int i = Random.Range(0, Flowers.GetComponent<PlayTurn>().prefabs.Length);
		GameObject goal = Instantiate(Flowers.GetComponent<PlayTurn>().prefabs[i], new Vector2(0,0), Quaternion.identity);
		Sprite flower = goal.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
		Sprite leaf = goal.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite;
		gameObject.transform.GetChild(0).GetComponent<Image>().sprite = flower;
		gameObject.transform.GetChild(1).GetComponent<Image>().sprite = leaf;
		c = goal.GetComponent<PlantInfo>().Color;
		l = goal.GetComponent<PlantInfo>().LeafCorners;
		f = goal.GetComponent<PlantInfo>().FlowerCorners;
		Destroy(goal);
		Debug.Log("Should be of color "+c+" with flower "+f+" and "+l);
	}
	
	// Update is called once per frame
	void Update () {
		foreach(GameObject o in Flowers.GetComponent<PlayTurn>().flowersInGame){
			if(o!=null && o.GetComponent<PlantInfo>().Color == c &&
				o.GetComponent<PlantInfo>().FlowerCorners == f &&
				o.GetComponent<PlantInfo>().LeafCorners == l){
					Start();
					AkSoundEngine.PostEvent("Play_Goal", gameObject);

			}
		}
	}
}
