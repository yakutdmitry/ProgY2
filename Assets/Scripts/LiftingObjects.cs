using System;
using UnityEngine;

public class LiftingObjects : MonoBehaviour
{
    public float forceMultiplier; 
    public float liftDuration; 
    private float timer = 0f; 

    private Rigidbody rb;
    private bool isLifting = false; 
    public GameData gameData;

    void Start()
    {
        gameData = Resources.Load<GameData>("GameData");
        DataManager.loadData(gameData, "Data.json");
        
        rb = GetComponent<Rigidbody>();
        
        forceMultiplier = gameData.forceMultiplier;
        liftDuration = gameData.liftDuration;
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isLifting)
        {
            isLifting = true;
            timer = 0f; 
        }
    }

    void FixedUpdate()
    {
        if (isLifting)
        {
            
            Vector3 upwardForce = -Physics.gravity * rb.mass * forceMultiplier;
            rb.AddForce(upwardForce);
            
            timer += Time.fixedDeltaTime;

            
            if (timer >= liftDuration)
            {
                isLifting = false;
                gameData.objectsLifted++;
            }
        }
    }

    private void OnApplicationQuit()
    {
        DataManager.saveData(gameData, "Data.json");
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        DataManager.saveData(gameData, "Data.json");
    }
}
