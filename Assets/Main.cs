using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

	public Material[] CoinMaterials;
	public GameObject GO;
	public GameObject[,] Grid = new GameObject[5, 5];

	private int BoardWidth = 5;
	private int BoardHeight = 5;

	// Use this for initialization
	void Start () {
		for (int x = 0; x < BoardWidth; x++) {
			for (int y = 0; y < BoardHeight; y++) {
				GameObject gO = Instantiate(GO, new Vector2(x,y), Quaternion.identity) as GameObject;
				Grid[x, y] = gO;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		Grid[3, 3].GetComponent<Renderer>().material = CoinMaterials[1];
		setType(3,4, CoinMaterials[2]);
	}

	void setType(int x, int y, Material Material) {
		Grid[x, y].GetComponent<Renderer>().material = Material;
	}
}
