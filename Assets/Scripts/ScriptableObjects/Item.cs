using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class Item : ScriptableObject
{
    // Start is called before the first frame update
    public Sprite itemSprite; 
    public string itemDescription; 
    public bool CopperKey;
    public bool SilverKey; 
    public bool GoldKey;  

}
