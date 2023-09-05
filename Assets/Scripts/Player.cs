using UnityEngine;

public class Player : MonoBehaviour
{
    private Attributes attributes;

    void Start()
    {
        attributes = gameObject.GetComponent<Attributes>();
    }

    public void ReceiveDamage(float damage)
    {
        attributes.ReceiveDamage(damage);

        if (attributes.Died())
        {
            Destroy(gameObject);
        }
    }
}
