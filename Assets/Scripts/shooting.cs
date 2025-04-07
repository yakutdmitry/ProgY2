using System;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;

public class shooting : MonoBehaviour
{
    public GameObject projectile;
    private float _projectileSpeed;
    public GameData gameData;
    
    private void Start()
    {
        
        DataManager.loadData(gameData, "Data.json");
        
        _projectileSpeed = gameData.projectileSpeed;
    }

    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0)) // ЛКМ
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        
        gameData.projectilesFired++;
        DataManager.saveData(gameData, "Data.json");
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 spawnPosition = transform.position + Vector3.up * 1.5f; 
            GameObject bullet = Instantiate(projectile, spawnPosition, Quaternion.identity);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            
            if (rb != null)
            {
                Vector3 direction = (hit.point - Camera.main.transform.position).normalized;
                rb.linearVelocity = direction * _projectileSpeed;
            }
        }
    }
}
