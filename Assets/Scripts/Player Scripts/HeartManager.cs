using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    public Image[] hearts; 
    public Sprite fullHeart; 
    public Sprite halfFullHeart; 
    public Sprite emptyHeart; 
    public FloatValue heartContainers; 
    public FloatValue playerCurrentHealth;
    // Start is called before the first frame update
    void Start()
    {
        InitHearts(); 
    }

    public void InitHearts()
    {
        float tempHealth = playerCurrentHealth.RuntimeValue/2;
        for (int i = 0; i<heartContainers.RuntimeValue; i++)
        {
            if(i <hearts.Length)
            {
                hearts[i].gameObject.SetActive(true);
                hearts[i].sprite = fullHeart;
            }
            initialHeartCont(tempHealth, i);
        }
    }

    public void initialHeartCont(float health, int i)
    {
        if(i <= health-1)
            {
                //full heart
                hearts[i].sprite = fullHeart; 
            }else if (i >= health)
            {
                //empty heart
                hearts[i].sprite = emptyHeart; 
            }else
            {
               //half full heart
               hearts[i].sprite = halfFullHeart;
            }
    }

    public void UpdateHearts()
    {
        InitHearts();
        float tempHealth = playerCurrentHealth.RuntimeValue/2;
        for (int i = 0; i < heartContainers.RuntimeValue; i++)
        {
            if(i <= tempHealth-1)
            {
                //full heart
                hearts[i].sprite = fullHeart; 
            }else if (i >= tempHealth)
            {
                //empty heart
                hearts[i].sprite = emptyHeart; 
            }else
            {
               //half full heart
               hearts[i].sprite = halfFullHeart;
            }
        }
    }

    public void HeartActives(float currheartContainers)
    {
        int containerNum = (int)currheartContainers; 
        hearts[containerNum].gameObject.SetActive(false);
        
    }
}
