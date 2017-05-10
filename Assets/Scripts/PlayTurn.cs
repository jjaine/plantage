using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayTurn : MonoBehaviour {

	public static bool play = false;
	bool mergeDone = false;
	bool inMerge = false;
	public GameObject[] prefabs;
	public List<GameObject> flowersInGame;
	public GameObject explosion;

	[Header("Button stuff")]
	public Button button;
	public Sprite pauseButton;
	public Sprite playButton; 

 	// Use this for initialization
	void Start () {
		flowersInGame = new List<GameObject>();

		AddNewFlowers();
	}
	
	// Update is called once per frame
	void Update () {
		if(play){

			//check collisions
			bool c = CheckCollisions();

			//all done, next turn
			if(mergeDone){
				foreach (GameObject flower in flowersInGame){
					if(flower!=null){
						flower.GetComponent<PlantInfo>().turnsLeft--;
						Color prev = flower.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
						prev.a = ((float)flower.GetComponent<PlantInfo>().turnsLeft+1)/3;
						flower.transform.GetChild(0).GetComponent<SpriteRenderer>().color = prev;
						flower.transform.GetChild(1).GetComponent<SpriteRenderer>().color = prev;
					}
				}
				//instantiate new things
				AddNewFlowers();
				play = false;
				button.image.sprite = playButton;
				mergeDone = false;
			}
			else if(!c){
				foreach (GameObject flower in flowersInGame){
					if(flower!=null){
						flower.GetComponent<PlantInfo>().turnsLeft--;
						Color prev = flower.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
						prev.a = ((float)flower.GetComponent<PlantInfo>().turnsLeft+1)/3;
						flower.transform.GetChild(0).GetComponent<SpriteRenderer>().color = prev;
						flower.transform.GetChild(1).GetComponent<SpriteRenderer>().color = prev;
					}
				}
				//instantiate new things
				AddNewFlowers();
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

	bool CheckCollisions(){
		foreach (GameObject flower in flowersInGame){
			if(flower!=null){
				if(flower.GetComponent<PlantInfo>().turnsLeft == 0){
					Destroy(flower);
				}
				else{
					if(!flower.GetComponent<CollideCheck>().inMerge && flower.GetComponent<CollideCheck>().collides){
						//merge!
						GameObject otherFlower = flower.GetComponent<CollideCheck>().otherFlower;
						if(otherFlower){
							GameObject newFlower = Combination(flower);
							StartCoroutine(Merge(flower, newFlower, otherFlower));
						}
						return true;
					}
				}
			}
		}
		if(!inMerge)
			return false;
		else
			return true;
	}

	IEnumerator Merge(GameObject flower, GameObject newFlower, GameObject otherFlower){
		inMerge = true;
		flower.GetComponent<CollideCheck>().inMerge = true;
		otherFlower.GetComponent<CollideCheck>().inMerge = true;
        yield return new WaitForSeconds(1);
        flowersInGame.Remove(flower);
        Destroy(flower);
        flowersInGame.Remove(otherFlower);
        Destroy(otherFlower);
        flowersInGame.Add(newFlower);
        
        yield return new WaitForSeconds(1);
        mergeDone = true;
        inMerge = false;
	}

	GameObject Combination(GameObject flower){
		GameObject other = flower.GetComponent<CollideCheck>().otherFlower;
		int lCorners = flower.GetComponent<PlantInfo>().LeafCorners + other.GetComponent<PlantInfo>().LeafCorners;
		int fCorners = flower.GetComponent<PlantInfo>().FlowerCorners + other.GetComponent<PlantInfo>().FlowerCorners;
		if(lCorners < 1) lCorners = -1;
		if(lCorners > 2) lCorners = 2;
		if(fCorners < 1) fCorners = -1;
		if(fCorners > 6) fCorners = 6;
		int i;
		for(i=0; i<prefabs.Length; i++){
			if(prefabs[i].GetComponent<PlantInfo>().LeafCorners == lCorners && 
				prefabs[i].GetComponent<PlantInfo>().FlowerCorners == fCorners){
				break;
			}
		}
		if(i==prefabs.Length){
			Debug.Log("Invalid merge!");
			return null;
		}
		else{
			GameObject f = Instantiate(prefabs[i], (other.transform.position+flower.transform.position)/2, Quaternion.identity);
			f.transform.parent = flower.transform.parent;
			return f;
		}
		
	}

	void AddNewFlowers(){
		for(int i=0; i<2; i++){
			int a = Random.Range(0,7);
			GameObject f = Instantiate(prefabs[a], new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(-5.0f, 5.0f), -1), Quaternion.identity);
			f.transform.parent = gameObject.transform;
			flowersInGame.Add(f);
		}
	}
}
