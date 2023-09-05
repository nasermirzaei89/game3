using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxHealth;

    void Start()
    {
        transform.Find("Health Bar").GetComponent<HealthBar>().maxHealth = maxHealth;
        transform.Find("Health Bar").GetComponent<HealthBar>().health = maxHealth;
    }

    public void ReceiveDamage(float damage)
    {
        transform.Find("Health Bar").GetComponent<HealthBar>().health -= damage;

        if (transform.Find("Health Bar").GetComponent<HealthBar>().health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
