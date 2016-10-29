using UnityEngine;
using System.Collections;

public class NextCoinsBoard : MonoBehaviour {

	public int[] Coins;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void UpdateNextCoins() {
		GameObject Coin0 = GameObject.Find("/Next/Coin0");
		GameObject Coin1 = GameObject.Find("/Next/Coin1");

		Coin0.GetComponent<Coin>().Value = Coins[1];
		Coin1.GetComponent<Coin>().Value = Coins[0];
	}
}
