using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Attributes attributes;

    void Start()
    {
        attributes = transform.parent.GetComponent<Attributes>();
    }

    void Update()
    {
        transform.Find("Bar").localScale= new Vector3(attributes.health / attributes.maxHealth, 1, 1);
    }
}
