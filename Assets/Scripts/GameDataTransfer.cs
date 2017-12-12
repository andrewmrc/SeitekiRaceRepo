using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataTransfer : MonoBehaviour {

	public int SelectedPlayer = 0;

    private static GameDataTransfer instanceRef;

    void Awake()
    {
        if (instanceRef == null)
        {
            instanceRef = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

}
