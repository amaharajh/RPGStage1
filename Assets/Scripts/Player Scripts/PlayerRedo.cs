using System.Collections;
using UnityEngine;

public class PlayerRedo : MonoBehaviour
{
    public FloatValue currHealth; 
    public FloatValue currHeartContainers; 
    public HeartManager initialheart; 
    public GameObject player; 
    public PlayerMovement playerInitial; 
    
    public void PlayerReserect()
    {
        StartCoroutine(WaitingCo());
       
    }

    private IEnumerator WaitingCo()
    {
        yield return new WaitForSeconds(1f);
         player.SetActive(true);

        if(currHeartContainers.RuntimeValue > 3)
        {
        currHeartContainers.RuntimeValue = currHeartContainers.RuntimeValue-1; 
        currHealth.RuntimeValue = currHeartContainers.RuntimeValue*2f; 
        }
        else
        {
            currHeartContainers.RuntimeValue = currHeartContainers.initialValue; 
            currHealth.RuntimeValue = currHeartContainers.RuntimeValue*2f; 
        }
        initialheart.InitHearts();
        initialheart.HeartActives(currHeartContainers.RuntimeValue); 
    }

}
