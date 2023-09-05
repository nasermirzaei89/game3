using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Opponent : MonoBehaviour
{
    private GameObject controller;
    private GameObject player;
    private Attributes attributes;


    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        controller.GetComponent<Game>().RegisterOpponent(gameObject);

        player = GameObject.FindGameObjectWithTag("Player");

        attributes = gameObject.GetComponent<Attributes>();
    }

    void OnDestroy()
    {
        controller.GetComponent<Game>().UnregisterOpponent(gameObject);
    }

    public void Move()
    {
        attributes.Clock();

        while (attributes.Move())
        {
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

    private void MoveTo(Vector2 position) {
        transform.position = position;
    }

    public void ReceiveDamage(float damage)
    {
        attributes.ReceiveDamage(damage);

        if (attributes.Died())
        {
            Destroy(gameObject);
        }
    }

    private void AttackPlayer()
    {
        player.GetComponent<Player>().ReceiveDamage(attributes.Damage());
    }
}
