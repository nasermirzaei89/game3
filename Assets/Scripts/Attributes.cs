using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attributes : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public float healthRegenerationRate;

    public enum Attribute
    {
        Strength,
        Agility,
        Intelligence,
    }

    public Attribute primaryAttribute;

    public float strength;
    public float agility;
    public float intelligence;

    public float movementSpeed;
    private float movementCharge;
    public float sightRange;

    public float attackDamage;
    public float attackRange;
    public float attackSpeed;


    public void Clock()
    {
        movementCharge += movementSpeed;

        health = Mathf.Min(health + healthRegenerationRate, maxHealth);
    }

    public bool Move()
    {
        if (!CanMove()) return false;

        movementCharge--;

        return true;
    }

    public bool CanMove()
    {
        return movementCharge >= 1;
    }

    public float AttributeDamage()
    {
        switch (primaryAttribute)
        {
            case Attribute.Strength: return strength;
            case Attribute.Agility: return agility;
            case Attribute.Intelligence: return intelligence;
        }

        return 0;
    }

    public float Damage()
    {
        return attackDamage + AttributeDamage();
    }

    public void ReceiveDamage(float damage)
    {
        health = Mathf.Max(0, health - damage);
    }

    public bool Died()
    {
        return health == 0;
    }
}
