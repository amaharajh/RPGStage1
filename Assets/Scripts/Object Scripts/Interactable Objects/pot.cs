using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pot : MonoBehaviour
{

    private Animator anim; 
    public LootTable thisLoot; 

    // Start is called before the first frame update
    void Start()
    {
            anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Smash(){
       anim.SetBool("smash",true); 
       StartCoroutine(breakCo());
      
    }

    IEnumerator breakCo(){
        yield return new WaitForSeconds(.3f);
        this.gameObject.SetActive(false);
         MakeLoot();
    }

    private void MakeLoot()
{
    if(thisLoot != null)
    {
        Resources current = thisLoot.LootResources(); 
        if(current != null)
        {
            Instantiate(current.gameObject, transform.position, Quaternion.identity);
        }
    }
}
}
