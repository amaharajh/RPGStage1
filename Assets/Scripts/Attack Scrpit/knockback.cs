using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class knockback : MonoBehaviour
{
    public float thrust; 
    public float knockTime; 
    public float damage; 

private void OnTriggerEnter2D (Collider2D other){

    if(other.gameObject.CompareTag("breakable") && this.gameObject.CompareTag("Player"))
    { // 

        other.GetComponent<pot>().Smash();
    }
    if(other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("Boss")||other.gameObject.CompareTag("Player")){
      //
       //  if (other.gameObject.CompareTag("enemy") && gameObject.CompareTag("enemy")) return;
        Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
        //Rigidbody2D bossHit = other.GetComponent<Rigidbody2D>();

        if(hit != null)
        {
            
               

            if (other.gameObject.CompareTag("enemy") && other.isTrigger)
            {
                Vector3 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust; 
                hit.DOMove(hit.transform.position + difference, knockTime);
               
                hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                other.GetComponent<Enemy>().Knock(hit,knockTime, damage);
                
            }
            if(other.gameObject.CompareTag("Boss")&& other.isTrigger)
            {
                 Vector3 difference = hit.transform.position - transform.position;
                difference = difference.normalized * 1; 
                hit.DOMove(hit.transform.position + difference, (knockTime));
                hit.GetComponent<Boss>().currentState = BossStateMachine.idle;
                other.GetComponent<Boss>().Knock(damage);
            
            }
            if(other.gameObject.CompareTag("Player") && other.isTrigger)
            {
                
                Vector3 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust; 
                hit.DOMove(hit.transform.position + difference, knockTime);
                
                /*Vector2 difference = hit.transform.position - transform.position;
            difference = difference.normalized * thrust; 
            hit.AddForce(difference, ForceMode2D.Impulse); */
               if (other.GetComponent<PlayerMovement>().currentState!=PlayerState.stagger)
                {
                hit.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                other.GetComponent<PlayerMovement>().Knock(knockTime, damage);
                }
                
            }
            
           // StartCoroutine(KnockCo(hit));
        }

    }

}



}