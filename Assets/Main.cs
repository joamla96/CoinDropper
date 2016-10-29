using UnityEngine;
using System.Collections;
using System;

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

		setType(3, 3, 1);
		setType(3, 4, 2);
		dropCoinToBottom(3, 4);
		dropCoinToBottom(3, 3);
		dropCoinToBottom(3, 4);
	}
	

	// Update is called once per frame
	void Update () {

	}

	void setType(int x, int y, int Value) {
		Grid[x, y].GetComponent<Coin>().Value = Value;
	}

	int getType(int x, int y) {
		return Grid[x, y].GetComponent<Coin>().Value;
	}

	bool dropCoin(int x, int y) {
		int Ty = y - 1;
		int TargetValue;
		try {
			TargetValue = getType(x, Ty);
		} catch(IndexOutOfRangeException) {
			return false;
		}

		if(TargetValue == 0) {
			setType(x, Ty, getType(x, y));
			setType(x,y,0);
			return true;
		}

		return false;
	}

	void dropCoinToBottom(int x, int y) {
		while(dropCoin(x,y)) {
			y--;
		}
	}
}
