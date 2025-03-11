using System;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }
}
