using System;
using UnityEngine;

public class shooting : MonoBehaviour
{
    public GameObject projectile;
    [SerializeField] private float _projectileSpeed;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ЛКМ
        {
            Shoot();
        }
    }

    private void Shoot()
    {
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
