using System.Collections;
using UnityEngine;

public enum BossState{
    Phase1, 
    Phase2
}
public class BossScript : Boss
{
    [Header("Enemy States")]
    public BossState currPhase; 

    [Header("")]
    public Transform target; 
    public float chaseRadius; 
    public float attackRadius; 
    public float roundingDistance; 
    public int courseAssignmentCode; 

    [Header("")]
    public Animator anim; 
    public Rigidbody2D myRigidbody;



        void Start()
        {
           
            currentState = BossStateMachine.idle; 
            currPhase = BossState.Phase1;
            anim = GetComponent<Animator>(); 
            myRigidbody = GetComponent<Rigidbody2D>(); 
            target = GameObject.FindWithTag("Player").transform; 
            
        }
        public void FixedUpdate()
        {
         CheckDistance();

            if(health <= maxHealth.RuntimeValue/2)
            {
               // Debug.Log("hald health");
            //}
               currPhase =BossState.Phase2;
                anim.SetBool("phase2", true); 
            }
            else 
            {
                currPhase = BossState.Phase1; 
            }
        }


    public virtual void CheckDistance(){

        if (Vector3.Distance(target.position,
                            transform.position)<=chaseRadius
                            && Vector3.Distance(target.position, 
                            transform.position)>attackRadius)
        {
                    if((currentState == BossStateMachine.idle || 
                        currentState == BossStateMachine.walk))
                    {

                        //if (currentState != EnemyState.stagger){
                        Vector3 temp = Vector3.MoveTowards(transform.position, target.position, 
                                    moveSpeed*Time.deltaTime);
                    
                        changeAnim(temp-transform.position);
                        myRigidbody.MovePosition(temp);
                        ChangeState(BossStateMachine.walk);
                        anim.SetBool("moving", true);
                    }
        }
        else if ((Vector3.Distance(target.position, transform.position) <= chaseRadius) &&
            (Vector3.Distance(target.position, transform.position) <= attackRadius))
        {
            // anim.SetBool("moving",false);
                if((currentState == BossStateMachine.idle || 
                    currentState == BossStateMachine.walk) )
                    {
                        if(currPhase == BossState.Phase1)
                        StartCoroutine(Phase1AttackCo());

                        else if(currPhase == BossState.Phase2)
                        StartCoroutine(Phase2AttackCo());
                        //BossAttackBehaviour();
                        //StartCoroutine(Phase1AttackCo());
                }
        }

        else if( (Vector3.Distance(target.position, transform.position) > chaseRadius)){
            
                if(Vector3.Distance(transform.position,StartPosition) > roundingDistance)
                    {
                        Vector3 temp = Vector3.MoveTowards(transform.position, StartPosition, moveSpeed*Time.deltaTime);
                        changeAnim(temp-transform.position);
                        myRigidbody.MovePosition(temp);
                        ChangeState(BossStateMachine.walk);
                        anim.SetBool("moving",true);
                    }

                else if(Vector3.Distance(transform.position,StartPosition) <= roundingDistance)
                    {
                        anim.SetBool("moving", false);
                        SetAnimFloat(Vector2.down);
                        ChangeState(BossStateMachine.idle);  
                    }
            
        }
    }
    private void BossAttackBehaviour()
    {
        if(PhaseStates() == BossState.Phase1)
        {
            StartCoroutine(Phase1AttackCo());
        }
        else if(PhaseStates() == BossState.Phase2)
        {
           
            StartCoroutine(Phase2AttackCo());
        }
    }   
 private BossState PhaseStates()
        {
            BossState NowPhase = currPhase; 
            if(health == (maxHealth.RuntimeValue/2))
            {
                NowPhase = BossState.Phase2;
                anim.SetBool("phase2",true); 
            }
            else 
            {
                NowPhase = BossState.Phase1; 
            }
            return NowPhase; 
        }

    private IEnumerator Phase1AttackCo(){

            anim.SetBool("moving", false);
            currentState = BossStateMachine.attack;
            anim.SetBool("attacking", true);
            yield return new WaitForSeconds(1.5f);
           // yield return null;
            anim.SetBool("attacking", false);
            yield return new WaitForSeconds(1.5f);
            currentState = BossStateMachine.walk;
            
        }

    private IEnumerator Phase2AttackCo(){

            anim.SetBool("moving", false);
            currentState = BossStateMachine.attack;
            anim.SetBool("attacking", true);
            yield return null;
            anim.SetBool("attacking", false);
            yield return new WaitForSeconds(1.5f);
            currentState = BossStateMachine.walk;
            
        } 
        

    
    public void SetAnimFloat(Vector2 setVector){
            anim.SetFloat("moveX", setVector.x);
            anim.SetFloat("moveY", setVector.y);
        }

//come back to change anim
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

        public void ChangeState(BossStateMachine newState)
        {
            if(currentState != newState){
                currentState = newState;
            }
        }

       
}
