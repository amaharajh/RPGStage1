using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState {
        walk, 
        idle,
        attack,
        interact,
        stagger
}

public enum PlayerAttackType{
    unhanded, 
    sword, 
    axe, 
    bow, 
    spear
}
public class PlayerMovement : MonoBehaviour
{
    [Header("Player States")]
    public PlayerState currentState;
    public PlayerAttackType currentAttackState; 
    
    [Header("Player Physics Characteristics")]
     private float speed = 7; 
    public static string spawnPointName; 
    private Rigidbody2D myRigidbody; 
    private Animator animator;
    private Vector3 change; 
    
    
    [Header("Player Health Characteristics")]
    public FloatValue currentHealth; 
    public SignalSender PlayerHealthSignal; 
    public SignalSender playerDeathSignal; 

    [Header("Player Inventory Characteristics")]
    public Inventory playerInventory;
    public SpriteRenderer receivedItemSprite; 
    
    [Header("Player Weapons Characteristics")]
    public GameObject projectile; 

    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.walk;
        currentAttackState = PlayerAttackType.unhanded; 
        animator = GetComponent<Animator>(); 
        myRigidbody = GetComponent<Rigidbody2D>(); 
        animator.SetInteger("WeaponState", 0); 
        animator.SetFloat("MoveX", 0);
        animator.SetFloat("MoveY", -1);
        Application.targetFrameRate = 60;
    }

   private void OnEnable()
    {
        currentState = PlayerState.idle;
        currentAttackState = PlayerAttackType.unhanded;  
        animator = GetComponent<Animator>(); 
        myRigidbody = GetComponent<Rigidbody2D>(); 
        animator.SetInteger("WeaponState", 0); 
        animator.SetFloat("MoveX", 0);
        animator.SetFloat("MoveY", -1);
    }
   
    void Update() 
    {
       
        if(currentState == PlayerState.interact)
        {
          return;   
        }
        change = Vector3.zero; 
        change.x = Input.GetAxisRaw("Horizontal"); 
        change.y = Input.GetAxisRaw("Vertical");

    #region WeaponState

        if(Input.GetButton("unhanded") && currentState != PlayerState.attack && 
        currentState != PlayerState.stagger && this.gameObject.CompareTag("Player"))
        {
            currentAttackState = PlayerAttackType.unhanded; 
            animator.SetInteger("WeaponState", 0); 
            speed = 13;
        }

        if(Input.GetButton("sword") && currentState != PlayerState.attack && 
        currentState != PlayerState.stagger && this.gameObject.CompareTag("Player"))
        {
            currentAttackState = PlayerAttackType.sword; 
            animator.SetInteger("WeaponState", 1); 
             speed = 10;
        }
        else if (Input.GetButton("axe") && currentState != PlayerState.attack && 
        currentState != PlayerState.stagger && this.gameObject.CompareTag("Player"))
        {
            currentAttackState = PlayerAttackType.axe;
            animator.SetInteger("WeaponState", 2); 
             speed = 7;
        }
        else if (Input.GetButton("bow") && currentState != PlayerState.attack && 
        currentState != PlayerState.stagger && this.gameObject.CompareTag("Player"))
        {
            currentAttackState = PlayerAttackType.bow;
             speed = 11;
        }
        else if (Input.GetButton("spear") && currentState != PlayerState.attack && 
        currentState != PlayerState.stagger && this.gameObject.CompareTag("Player"))
        {
            currentAttackState = PlayerAttackType.spear;
             animator.SetInteger("WeaponState", 4);
              speed = 8; 
        }
    #endregion

    #region AttackingState
        if(Input.GetButtonDown("attack") && currentState != PlayerState.attack && 
                currentState != PlayerState.stagger && currentAttackState == PlayerAttackType.sword && 
                this.gameObject.CompareTag("Player"))
        {
          StartCoroutine(SwordAttackCo());
        }
        else if(Input.GetButtonDown("attack") && currentState != PlayerState.attack && 
                currentState != PlayerState.stagger && currentAttackState == PlayerAttackType.bow && 
                this.gameObject.CompareTag("Player"))
        {
          StartCoroutine(bowAttackCo());
        }
        if(Input.GetButtonDown("attack") && currentState != PlayerState.attack && 
                currentState != PlayerState.stagger && currentAttackState == PlayerAttackType.axe && 
                this.gameObject.CompareTag("Player"))
        {
          StartCoroutine(AxeAttackCo());
        }

         if(Input.GetButtonDown("attack") && currentState != PlayerState.attack && 
                currentState != PlayerState.stagger && currentAttackState == PlayerAttackType.spear && 
                this.gameObject.CompareTag("Player"))
        {
          StartCoroutine(SpearThrustAttackCo());
        }

        if(Input.GetButtonDown("attack2") && currentState != PlayerState.attack && 
                currentState != PlayerState.stagger && currentAttackState == PlayerAttackType.spear && 
                this.gameObject.CompareTag("Player"))
        {
          StartCoroutine(SpearSlashAttackCo());
        }


        else if(currentState == PlayerState.walk|| currentState == PlayerState.idle){
            UpdateAnimationAndMove();
            }

    #endregion
    }
    private IEnumerator SwordAttackCo(){

        animator.SetInteger("attacking",1);
        currentState = PlayerState.attack;
        yield return null; 
        animator.SetInteger("attacking",0);
        yield return new WaitForSeconds(.3f);
        if(currentState != PlayerState.interact)
        {
          currentState = PlayerState.idle;   
        }   
    }
   private IEnumerator AxeAttackCo()
   {
        animator.SetInteger("attacking",1);
        currentState = PlayerState.attack;
        yield return null; 
        animator.SetInteger("attacking",0);
        yield return new WaitForSeconds(1f);
        if(currentState != PlayerState.interact)
        {
          currentState = PlayerState.idle;   
        } 
    }
    private IEnumerator SpearThrustAttackCo()
    {
        animator.SetInteger("attacking",1);
        currentState = PlayerState.attack;
        yield return null; 
        animator.SetInteger("attacking", 0);
        yield return new WaitForSeconds(.3f);
        if(currentState != PlayerState.interact)
        {
          currentState = PlayerState.idle;   
        }   
    }
    private IEnumerator SpearSlashAttackCo()
    {
        animator.SetInteger("attacking",2);
        currentState = PlayerState.attack;
        yield return null; 
        animator.SetInteger("attacking", 0);
        yield return new WaitForSeconds(.42f);
        if(currentState != PlayerState.interact)
        {
          currentState = PlayerState.idle;   
        }    
    }
     private IEnumerator bowAttackCo()
     {
       //animator.SetBool("Attacking",true);
        currentState = PlayerState.attack;
        yield return null; 
        MakeArrow();
        //animator.SetBool("Attacking", false);
        yield return new WaitForSeconds(.3f);
        if(currentState != PlayerState.interact)
        {
          currentState = PlayerState.idle;  
        }
        
    }
    private void MakeArrow()
    {
        Vector2 temp = new Vector2(animator.GetFloat("MoveX"), animator.GetFloat("MoveY"));
        Arrow arrow = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Arrow>();
        arrow.Setup(temp, ChooseArrorDirection());
    }
    Vector3 ChooseArrorDirection()
    {
        float temp = Mathf.Atan2(animator.GetFloat("MoveY"), animator.GetFloat("MoveX"))*Mathf.Rad2Deg;
        return new Vector3(0,0,temp); 
    }
    public void RaisedItem()
    {
        if (playerInventory.currentItem != null)
        {
           if (currentState != PlayerState.interact)
             {
              animator.SetBool("Receive Item", true); 
              currentState = PlayerState.interact; 
              receivedItemSprite.sprite = playerInventory.currentItem.itemSprite;
            }else
             {
              animator.SetBool("Receive Item", false);  
              currentState = PlayerState.idle; 
              receivedItemSprite.sprite = null; 
              playerInventory.currentItem = null; 
            }

        }
    }
    void UpdateAnimationAndMove(){
        if(change != Vector3.zero){
            MoveCharacter();
            change.x = Mathf.Round(change.x);
            change.y = Mathf.Round(change.y);
            animator.SetFloat("MoveX", change.x);
            animator.SetFloat("MoveY", change.y);
            currentState = PlayerState.walk;
            animator.SetBool("Moving", true);
        }else{
            currentState = PlayerState.idle;  
            animator.SetBool("Moving", false);
        }
    } 
    void MoveCharacter(){

        change.Normalize();
        myRigidbody.MovePosition(
            transform.position + change * speed * Time.fixedDeltaTime  
            );
    }
    public void Knock(float knockTime, float damage)
    {
        currentHealth.RuntimeValue -= damage;
        PlayerHealthSignal.Raise(); 
        if(currentHealth.RuntimeValue> 0 )
        {
          //  PlayerHealthSignal.Raise(); // take off to show zero hearts
             StartCoroutine(KnockCo(knockTime));
        }else{
            
            this.gameObject.SetActive(false);
            playerDeathSignal.Raise();
        }
        
    }
    private IEnumerator KnockCo( float knockTime){

    if(myRigidbody != null){
        yield return new WaitForSeconds(knockTime);
        myRigidbody.velocity = Vector2.zero; 
        currentState = PlayerState.idle;
        myRigidbody.velocity = Vector2.zero;
    }
}
    
}