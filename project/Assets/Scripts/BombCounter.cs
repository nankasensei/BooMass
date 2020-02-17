using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombCounter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		GameSceneManager.instance.IncrementNumAllBomb();
    }	
}
