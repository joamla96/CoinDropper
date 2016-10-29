using UnityEngine;
using System.Collections;

public class CurrentCoinsBoard : MonoBehaviour {
	public int[]Coins;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateCurrentCoins() {
		GameObject Coin0 = GameObject.Find("/Current/Coin00");
		GameObject Coin1 = GameObject.Find("/Current/Coin10");

		Coin0.GetComponent<Coin>().Value = Coins[0];
		Coin1.GetComponent<Coin>().Value = Coins[1];
	}


}
