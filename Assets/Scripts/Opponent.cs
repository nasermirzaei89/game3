using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Opponent : MonoBehaviour
{
    private GameObject controller;
    private GameObject player;

    public float speed;
    private float energy;

    public float maxHealth;


    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        controller.GetComponent<Game>().RegisterOpponent(gameObject);

        player = GameObject.FindGameObjectWithTag("Player");

        transform.Find("Health Bar").GetComponent<HealthBar>().maxHealth = maxHealth;
        transform.Find("Health Bar").GetComponent<HealthBar>().health = maxHealth;
    }

    void OnDestroy()
    {
        controller.GetComponent<Game>().UnregisterOpponent(gameObject);
    }

    public void Move()
    {
        energy += speed;

        while (energy >= 1)
        {
            energy -= 1;

            var distance = player.transform.position - transform.position;

            if (Mathf.Abs(distance.x) >= Mathf.Abs(distance.y))
            {
                if (distance.x != 0)
                {
                    var destination = transform.position + new Vector3(Mathf.Sign(distance.x), 0);

                    if (CanMoveTo(destination))
                    {
                        MoveTo(destination);
                        if (IsPlayerAccesible()) AttackPlayer();
                        return;
                    } else if (IsPlayerAccesible())
                    {
                        AttackPlayer();
                        return;
                    }
                }

                if (distance.y != 0)
                {
                    var destination = transform.position + new Vector3(0, Mathf.Sign(distance.y));

                    if (CanMoveTo(destination))
                    {
                        MoveTo(destination);
                        if (IsPlayerAccesible()) AttackPlayer();
                        return;
                    } else if (IsPlayerAccesible())
                    {
                        AttackPlayer();
                        return;
                    }
                }
            }

            
            if (distance.y != 0)
            {
                var destination = transform.position + new Vector3(0, Mathf.Sign(distance.y));

                if (CanMoveTo(destination))
                {
                    MoveTo(destination);
                    if (IsPlayerAccesible()) AttackPlayer();
                    return;
                } else if (IsPlayerAccesible())
                {
                    AttackPlayer();
                    return;
                }
            }

            if (distance.x != 0)
            {
                var destination = transform.position + new Vector3(Mathf.Sign(distance.x), 0);

                if (CanMoveTo(destination))
                {
                    MoveTo(destination);
                    if (IsPlayerAccesible()) AttackPlayer();
                    return;
                } else if (IsPlayerAccesible())
                {
                    AttackPlayer();
                    return;
                }
            }
        }
    }

    private bool CanMoveTo(Vector2 position)
    {
        if (controller.GetComponent<Game>().GetWallAt(position) != null) {
            return false;
        }

        var opponent = controller.GetComponent<Game>().GetOpponentAt(position);
        if (opponent != null && opponent != gameObject)
        {
            return false;
        }

        return !IsPlayerAt(position);
    }

    private bool IsPlayerAt(Vector2 position)
    {
        return controller.GetComponent<Game>().IsPlayerAt(position);
    }

    private bool IsPlayerAccesible()
    {
        if (IsPlayerAt(transform.position + new Vector3(1, 0, 0))) return true;
        if (IsPlayerAt(transform.position + new Vector3(0, 1, 0))) return true;
        if (IsPlayerAt(transform.position + new Vector3(-1, 0, 0))) return true;
        if (IsPlayerAt(transform.position + new Vector3(0, 1, 0))) return true;

        return false;
    }

    private void MoveTo(Vector3 position) {
        transform.position = position;
    }

    public void ReceiveDamage(float damage)
    {
        transform.Find("Health Bar").GetComponent<HealthBar>().health -= damage;

        if (transform.Find("Health Bar").GetComponent<HealthBar>().health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void AttackPlayer()
    {
        player.GetComponent<Player>().ReceiveDamage(1);
    }
}
