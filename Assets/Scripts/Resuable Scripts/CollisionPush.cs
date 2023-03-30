using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPush : MonoBehaviour
{
    public CapsuleCollider2D characterCollider; 
    public CapsuleCollider2D characterBlockerColider; 
    void Start()
    {
       Physics2D.IgnoreCollision(characterCollider,characterBlockerColider,true); 
    }

    
}
