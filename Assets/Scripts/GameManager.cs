using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
public class GameManager : MonoBehaviour
{
    InputAction leftMouse;

    public GameObject foodObj;
    public GameObject humanObj;
    
    public List<GameObject> allFood = new List<GameObject>();
    public List<GameObject> allPeople = new List<GameObject>();

    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;
        
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leftMouse = InputSystem.actions.FindAction("MouseClick");
    }

    // Update is called once per frame
    void Update()
    {
        if (leftMouse.WasReleasedThisFrame())
        {
            //create a food
            MakeFood();
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            MakeHuman();
        }
    }

    void MakeFood()
    {
        Vector3 newPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        newPos.z = 0;
        allFood.Add(Instantiate(foodObj, newPos, Quaternion.identity));
    }

    void MakeHuman()
    {
        float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        
        Vector3 spawnPos = new Vector3(randomX, randomY, 0);
        spawnPos.z = 0;
        allPeople.Add(Instantiate(humanObj, spawnPos, Quaternion.identity));
        
    }
}
