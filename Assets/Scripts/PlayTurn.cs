using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayTurn : MonoBehaviour {

	public static bool play = false;
	bool mergeDone = false;
	public Plant[] prefabs;

	[Header("Button stuff")]
	public Button button;
	public Sprite pauseButton;
	public Sprite playButton;
	public List<GameObject> flowersInGame;
 
 	[System.Serializable]
    public class Plant
    {
        public GameObject Flower;
        public GameObject Leaf;
    }

 	// Use this for initialization
	void Start () {
		flowersInGame = new List<GameObject>();
		foreach (Transform child in transform){
            flowersInGame.Add(child.gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(play){
			//instantiate new things

			//check collisions
			foreach (GameObject flower in flowersInGame){
				if(flower!=null){
					if(!flower.GetComponent<CollideCheck>().inMerge && flower.GetComponent<CollideCheck>().collides){
						//merge!
						foundCollision = true;
						GameObject otherFlower = flower.GetComponent<CollideCheck>().otherFlower;
						if(otherFlower){
							GameObject newFlower = Combination(flower);
							StartCoroutine(Merge(flower, newFlower, otherFlower));
						}
						break;
					}
				}
			}

			//all done, next turn
			if(mergeDone){
				play = false;
				button.image.sprite = playButton;
				mergeDone = false;
			}
		}
	}

	public void Play(){
		button.image.sprite = pauseButton;
		play = true;
	}

	IEnumerator Wait(){
		yield return new WaitForSeconds(1);
	}

	IEnumerator Merge(GameObject flower, GameObject newFlower, GameObject otherFlower){
		flower.GetComponent<CollideCheck>().inMerge = true;
		otherFlower.GetComponent<CollideCheck>().inMerge = true;
        yield return new WaitForSeconds(1);
        flowersInGame.Remove(flower);
        Destroy(flower);
        flowersInGame.Remove(otherFlower);
        Destroy(otherFlower);
        flowersInGame.Add(newFlower);
        mergeDone = true;
	}

	GameObject Combination(GameObject flower){
		GameObject other = flower.GetComponent<CollideCheck>().otherFlower;
		Debug.Log("Comibing " + flower.name + " and "+ other.name);
		GameObject f = Instantiate(prefabs[0].Flower, (other.transform.position+flower.transform.position)/2, Quaternion.identity);
		GameObject l = Instantiate(prefabs[0].Leaf, (other.transform.position+flower.transform.position)/2, Quaternion.identity);
		f.transform.parent = flower.transform.parent;
		l.transform.parent = f.transform;
		return f;
	}
}
