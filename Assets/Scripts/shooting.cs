using System;
using System.IO;
using UnityEngine;

public class shooting : MonoBehaviour
{
    public GameObject projectile;
    private float _projectileSpeed;
    public GameData gameData;
    private string _filePath;

    private void loadData()
    {
        if (File.Exists(filePath))
        {
            string josn = File.ReadAllText(gameData);
            JsonUtility.FromJsonOverwrite(josn, gameData);
        }
        else
        {
            Debug.Log("File not found");
        }
    }
    
    private void Start()
    {
        _filePath = Path.Combine(Application.persistentDataPath, "Data.json");
        
        loadData();
        
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
        gameData.projectileSpeed++;
        saveData();
        
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

    private void saveData()
    {
        string json = JsonUtility.ToJson(gameData);
        File.WriteAllText(_filePath, json);
        Debug.Log(_filePath);
    }
}
