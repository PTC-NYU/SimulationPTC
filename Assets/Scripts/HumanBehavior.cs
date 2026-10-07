using UnityEngine;

public class HumanBehavior : MonoBehaviour
{

    float fullnessVal = 5f;

    float needsTime;
    public float needsTimeReset;
    public float needsTimeStep;

    public float starvingTime = 5f;
    
    public GameManager myManager;
    
    Vector3 targetPos;
    bool moving;
    bool starving = false;

    float starvationTicks; 
    
    GameObject targetFood;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        needsTime = needsTimeReset;
    }

    // Update is called once per frame
    void Update()
    {
        needsTime -= needsTimeStep * Time.deltaTime;
        if (needsTime < 0)
        {
            IncrementNeeds();
        }

        if (moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, 2f * Time.deltaTime);

            if (targetFood != null && Vector3.Distance(transform.position, targetFood.transform.position) <= 0f)
            {
                EatFood();
            }
        }

        if (starving)
        {
            starvationTicks -= Time.deltaTime;

            if (starvationTicks <= 0)
            {
                Die();
            }
        }
    }

    void IncrementNeeds()
    {
        fullnessVal -= 1;
        needsTime = needsTimeReset;
        Debug.Log(fullnessVal);
        if (fullnessVal <= 0)
        {
            starving = true;
            starvationTicks = starvingTime;
            
            FindFood();
        }
    }

    void FindFood()
    {
        float dist = 2000f;
        GameObject closestFood =  null;
        foreach (GameObject food in myManager.allFood)
        {
            if (Vector3.Distance(transform.position, food.transform.position) < dist)
            {
                dist = Vector3.Distance(transform.position, food.transform.position);
                closestFood = food;
            }
        }
        //GO TO THE FOOD!
        if (closestFood != null)
        {
            targetFood = closestFood;
            targetPos = closestFood.transform.position;
            moving = true; 
        }
    }

    void EatFood()
    {
        if (targetFood == null)
        {
            return;
        }

        Debug.Log("ate food");

        fullnessVal = 5f;
        
        starving = false;
        starvationTicks = 0;
        moving = false;
        
        myManager.allFood.Remove(targetFood);
        
        Destroy(targetFood);
        
        targetFood = null;
    }

    void Die()
    {
        Debug.Log("Human Starved and Died");
        
        Destroy(gameObject);
    }
}