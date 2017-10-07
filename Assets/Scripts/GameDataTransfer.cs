using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataTransfer : MonoBehaviour {

	public int SelectedPlayer = 0;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
	}
}
