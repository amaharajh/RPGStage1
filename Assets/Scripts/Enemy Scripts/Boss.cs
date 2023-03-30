using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BossStateMachine{
    idle,
    walk,
    attack,

}

public class Boss : MonoBehaviour
{

    public BossStateMachine currentState; 
    [SerializeField] private BoolValue bosscheck; 
    public FloatValue maxHealth;
    public float health; 
    public string BossName; 
    public int baseAttack; 
    public float moveSpeed; 
    public GameObject deathEffect; 
    public Vector3 StartPosition; 
    public SignalSender BossDeathSignal; 
    public LootTable thisLoot; 

private void Awake()
{
        health = maxHealth.RuntimeValue; 
        StartPosition = transform.position; 
}

    public void OnEnable()
{
    health = maxHealth.RuntimeValue; 
    transform.position = StartPosition; 
    currentState = BossStateMachine.idle; 
    //anim.SetBool("moving", true);
} 


public void TakeDamage(float damage){
    health -= damage; 
    if(health <= 0)
        {
            DeathEffect();
            MakeLoot();
            bosscheck.RuntimeValue = true; 
            if (BossDeathSignal != null)
            {
                BossDeathSignal.Raise();
                
            }
        
        this.gameObject.SetActive(false);
    }
    //TakeDamage(damage);
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
private void DeathEffect()
{
    if(deathEffect != null) 
    {
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f);
    }
}
    // Start is called before the first frame update

 public void Knock(float damage)
    {
        
        TakeDamage(damage);
    }

}

