using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {
	public int Value = 0;
	public Material[] Materials;

	private int OldValue;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(OldValue != Value) {
			gameObject.GetComponent<Renderer>().material = Materials[Value];
			this.OldValue = Value;
		}
	}
}
