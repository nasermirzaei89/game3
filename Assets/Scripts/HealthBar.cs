using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public float health, maxHealth;

    void Update()
    {
        transform.Find("Bar").localScale= new Vector3(health/maxHealth, 1, 1);
    }
}
