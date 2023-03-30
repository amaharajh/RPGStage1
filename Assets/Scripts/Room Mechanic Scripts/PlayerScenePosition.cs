using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScenePosition : MonoBehaviour
{
    private  PlayerMovement Player; 
   // public Vector2 StartDirection; 
   // public VectorVal PlayerSpawnLocation;  
    public string spawnName; 
    // Start is called before the first frame update
    void Awake()
    {
        Player = FindObjectOfType<PlayerMovement>(); 

        if (PlayerMovement.spawnPointName == spawnName)
        {
        Player.transform.position = transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
