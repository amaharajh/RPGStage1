using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Echo_tutorial : Enemy
{
    public Rigidbody2D myRigidbody; 
    public Transform target; 
    public float chaseRadius; 
    public float attackRadius; 
    //public Transform homePosition; 
    public Animator anim; 
   public float roundingDistance; 
    // Start is called before the first frame update
   public void Start()
    {
        currentState = EnemyState.idle; 
        anim = GetComponent<Animator>(); 
        myRigidbody = GetComponent<Rigidbody2D>(); 
        target = GameObject.FindWithTag("Player").transform; 
        
        
      
        
    }
   

    // Update is called once per frame
   public void FixedUpdate()
    {
        CheckDistance();
    }

    public virtual void CheckDistance(){

        if (Vector3.Distance(target.position,
                             transform.position)<=chaseRadius
                             && Vector3.Distance(target.position, 
                             transform.position)>attackRadius)
       {
                if((currentState == EnemyState.idle || 
                    currentState == EnemyState.walk) 
                   && currentState != EnemyState.stagger){

            //if (currentState != EnemyState.stagger){
            Vector3 temp = Vector3.MoveTowards(transform.position, target.position, 
                           moveSpeed*Time.deltaTime);
           
            changeAnim(temp-transform.position);
            myRigidbody.MovePosition(temp);
            ChangeState(EnemyState.walk);
            anim.SetBool("moving", true);
                }
       }
       else if ((Vector3.Distance(target.position, transform.position) <= chaseRadius) &&
        (Vector3.Distance(target.position, transform.position) <= attackRadius)){
           // anim.SetBool("moving",false);
              if((currentState == EnemyState.idle || 
                  currentState == EnemyState.walk) 
                   && currentState != EnemyState.stagger){
                StartCoroutine(AttackCo());
            }
       }

       else if( (Vector3.Distance(target.position, transform.position) > chaseRadius)){
        /*anim.SetBool("moving", false);
        ChangeState(EnemyState.idle); */
        
            if(Vector3.Distance(transform.position,StartPosition) > roundingDistance)
                {
                    Vector3 temp = Vector3.MoveTowards(transform.position, StartPosition, moveSpeed*Time.deltaTime);
                    changeAnim(temp-transform.position);
                    myRigidbody.MovePosition(temp);
                    ChangeState(EnemyState.walk);
                    anim.SetBool("moving",true);
                }

            else if(Vector3.Distance(transform.position,StartPosition) <= roundingDistance)
                {
                    anim.SetBool("moving", false);
                    SetAnimFloat(Vector2.down);
                    ChangeState(EnemyState.idle);  
                }
        
       }

    }

 private IEnumerator AttackCo(){

        /*currentState = EnemyState.attack;
        anim.SetBool("Attacking", true);
        yield return new WaitForSeconds(1f);
        currentState = EnemyState.walk;
        anim.SetBool("Attacking", false); */
        anim.SetBool("moving", false);
        currentState = EnemyState.attack;
        anim.SetBool("Attacking", true);
        yield return null;
        anim.SetBool("Attacking", false);
        yield return new WaitForSeconds(1f);
        currentState = EnemyState.walk;
        
    }

    public void SetAnimFloat(Vector2 setVector){
        anim.SetFloat("moveX", setVector.x);
        anim.SetFloat("moveY", setVector.y);
    }

    public void changeAnim(Vector2 direction){
        if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if(direction.x > 0){
                SetAnimFloat(Vector2.right);
            }else if (direction.x < 0)
            {
                SetAnimFloat(Vector2.left);
            }
        }else if(Mathf.Abs(direction.x) < Mathf.Abs(direction.y)){
            if(direction.y > 0)
            {
                SetAnimFloat(Vector2.up);
            }
            else if (direction.y < 0)
            {
                SetAnimFloat(Vector2.down);
            }
        }
    }
    public void ChangeState(EnemyState newState){

        if(currentState != newState){
            currentState = newState;
        }

    }
    
}
