﻿using UnityEngine;
using System.Collections;
using System;

public class Main : MonoBehaviour {

	public Material[] CoinMaterials;
	public GameObject GO;
	public GameObject[,] Grid = new GameObject[5, 5];

	private int BoardWidth = 5;
	private int BoardHeight = 5;

	private int[] CurrentCoins;
	private int[] NextCoins;

	// Use this for initialization
	void Start () {
		for (int x = 0; x < BoardWidth; x++) {
			for (int y = 0; y < BoardHeight; y++) {
				GameObject gO = Instantiate(GO, new Vector2(x,y), Quaternion.identity) as GameObject;
				Grid[x, y] = gO;
			}
		}

		NextCoins = findNextCoins();
		UpdateNextCoins();
	}
	

	// Update is called once per frame
	void Update () {
		DropAll();
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

	int[] findNextCoins() {
		System.Random rnd = new System.Random();
		int[] Coins = new int[2];
		Coins[0] = rnd.Next(1,3);
		Coins[1] = rnd.Next(1, 3);
		return Coins;
	}

	void UpdateNextCoins() {
		GameObject Coin0 = GameObject.Find("/Next/Coin0");
		GameObject Coin1 = GameObject.Find("/Next/Coin1");

		Coin0.GetComponent<Coin>().Value = NextCoins[0];
		Coin1.GetComponent<Coin>().Value = NextCoins[1];
	}
}
