using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyState{
    idle,
    walk,
    attack,
    stagger
}

public class Enemy : MonoBehaviour
{

    public EnemyState currentState; 
    public FloatValue maxHealth;
    public float health; 
    public string enemyName; 
    public int baseAttack; 
    public float moveSpeed; 
    public GameObject deathEffect; 
    public Vector3 StartPosition; 
    public SignalSender enemyDeathSignal; 
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
    currentState = EnemyState.idle; 
    //anim.SetBool("moving", true);
} 


public void TakeDamage(float damage){
    health -= damage; 
    if(health <= 0)
        {
            DeathEffect();
            MakeLoot();
            if (enemyDeathSignal != null)
            {
                enemyDeathSignal.Raise();
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

 public virtual void Knock(Rigidbody2D myRigidbody, float knockTime, float damage)
    {
        StartCoroutine(KnockCo(myRigidbody, knockTime));
        TakeDamage(damage);
    }
private IEnumerator KnockCo(Rigidbody2D myRigidbody, float knocktime){

    if(myRigidbody != null ){
        yield return new WaitForSeconds(knocktime);
        myRigidbody.velocity = Vector2.zero; 
        currentState= EnemyState.idle;
        myRigidbody.velocity = Vector2.zero;
    }
}

}