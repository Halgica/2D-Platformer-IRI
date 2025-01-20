using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int health;
    [SerializeField] private int maxHealth;

    private void Awake()
    {
        health = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
    
}
