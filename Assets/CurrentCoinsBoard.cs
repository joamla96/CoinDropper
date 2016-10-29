using UnityEngine;
using System.Collections;

public class CurrentCoinsBoard : MonoBehaviour {
	public int[]Coins;

	private int[][] oldPos;
	private int[][] newPos;

	// Use this for initialization
	void Start() {
		oldPos[0] = new int[] {0, 0};
		oldPos[1] = new int[] {0, 1};

		newPos = oldPos;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateCurrentCoins() {
		GameObject OldCoin0 = GameObject.Find("/Current/Coin" + oldPos[0][0] + oldPos[0][1]);
		GameObject OldCoin1 = GameObject.Find("/Current/Coin" + oldPos[1][0] + oldPos[1][1]);

		GameObject NewCoin0 = GameObject.Find("/Current/Coin" + newPos[1][0] + newPos[1][1]);
		GameObject NewCoin1 = GameObject.Find("/Current/Coin" + newPos[1][0] + newPos[1][1]);

		OldCoin1.GetComponent<Coin>().Value = 0;
		OldCoin1.GetComponent<Coin>().Value = 0;

		NewCoin0.GetComponent<Coin>().Value = Coins[0];
		NewCoin1.GetComponent<Coin>().Value = Coins[1];
	}

	public void MoveLeft() {

	}

	public void MoveRight() {

	}


}
