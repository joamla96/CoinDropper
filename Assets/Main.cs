using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Main : MonoBehaviour {

	public Material[] CoinMaterials;
	public GameObject GO;
	public GameObject[,] Grid = new GameObject[5, 5];

	private int BoardWidth = 5;
	private int BoardHeight = 5;

	private bool BoardUpdated = false;

	NextCoinsBoard NCB;
	CurrentCoinsBoard CCB;

	// Use this for initialization
	void Start () {
		NCB = gameObject.GetComponent<NextCoinsBoard>();
		CCB = gameObject.GetComponent<CurrentCoinsBoard>();

		CCB.setWidth(BoardWidth);

		for (int x = 0; x < BoardWidth; x++) {
			for (int y = 0; y < BoardHeight; y++) {
				GameObject gO = Instantiate(GO, new Vector2(x,y), Quaternion.identity) as GameObject;
				Grid[x, y] = gO;
			}
		}
	}
	

	// Update is called once per frame
	void Update () {
		if (BoardUpdated) {
			Debug.Log("getType: " + getType(0, 0));

			List<int[]> List = new List<int[]>();
			FindConnected(List, 0, 0, getType(0, 0));

			if(List.Count >= 3) CombineCoins(List);

			Debug.Log("List Length: " + List.Count);

			BoardUpdated = false;
		}

		if (Input.GetKeyDown("space")) {
			NCB.Coins = new int[2] { 1, 3 };
			CCB.setCoins(NCB.Coins);
			CCB.UpdateCurrentCoins();

			NCB.Coins = findNextCoins();
			NCB.UpdateNextCoins();

			List<int[]> List = new List<int[]>();
			List.Add(new int[2] { 0, 0 });
			Debug.Log(inList(List, 0,0));
		}

		if (Input.GetKeyDown(KeyCode.RightArrow))
			CCB.MoveRight();

		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			CCB.MoveLeft();
		}

		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			CCB.MoveRotate();
		}

		if (Input.GetKeyDown(KeyCode.DownArrow)) {
			int[] Pos1 = CCB.getNewPos(1);
			int[] Pos0 = CCB.getNewPos(0);

			if (!addCoin(Pos1[1], CCB.getCoinValue(1))) GameOver();
			DropAll();

			if (!addCoin(Pos0[1], CCB.getCoinValue(0))) GameOver();
			DropAll();

			UpdateCoins();
			BoardUpdated = true;
		}
	}

	bool ofType(int Value, int x, int y) {
		Debug.Log("Comparing getType and value: "+ getType(x, y)+" == "+Value);
		if (getType(x, y) == Value) return true;
		else return false;
	}

	bool insideBoard(int x, int y) {
		try {
			getType(x, y);
			return true;
		} catch(IndexOutOfRangeException) {
			return false;
		}
	}

	bool inList(List<int[]> List, int x, int y) {
		foreach(int[] Pos in List) {
			if (Pos[0] == y && Pos[1] == x) return true;
		}

		return false;
	}

	List<int[]> FindConnected(List<int[]> List, int x,int y, int Value) {
		if (!insideBoard(x, y)) return List;
		if (inList(List, x,y)) return List;
		if (!ofType(Value, x, y)) return List;
		Debug.Log("Checking xy: " + x +" | "+ y);

		List.Add(new int[2] { y, x });
		List = FindConnected(List, x - 1, y, Value);
		List = FindConnected(List, x + 1, y, Value);
		List = FindConnected(List, x, y - 1, Value);
		List = FindConnected(List, x, y + 1, Value);

		return List;
	}

	void CombineCoins(List<int[]> List) {
		bool firstEntry = true;

		foreach (int[] Pos in List) {
			if(firstEntry) {
				setType(Pos[1], Pos[0], getType(Pos[1], Pos[0]) + 1);
				firstEntry = false;
			} else {
				setType(Pos[1], Pos[0], 0);
			}
		}
	}

	private void UpdateCoins() {
		CCB.setCoins(NCB.Coins);
		CCB.UpdateCurrentCoins();

		NCB.Coins = findNextCoins();
		NCB.UpdateNextCoins();
	}

	private void GameOver() {
		Time.timeScale = 0;
		Debug.Log("Game over");
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

	void DropAll() {
		for (int x = 0; x < BoardWidth; x++) {
			for (int y = 0; y < BoardHeight; y++) {
				dropCoinToBottom(x,y);
			}
		}
	}

	public bool addCoin(int x, int Value) {
		if (getType(x, BoardHeight - 1) == 0) {
			setType(x, BoardHeight - 1, Value);
			return true;
		} else return false;
	}

	int[] findNextCoins() {
		System.Random rnd = new System.Random();
		int[] Coins = new int[2];
		Coins[0] = rnd.Next(1, 4);
		Coins[1] = rnd.Next(1, 4);

		return Coins;
	}


}
