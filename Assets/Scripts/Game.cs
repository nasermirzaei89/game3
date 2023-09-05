using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public List<GameObject> walls;
    public List<GameObject> opponents;
    public GameObject player;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MovePlayer(new Vector2(-1, 0));
        } else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MovePlayer(new Vector2(0, 1));
        } else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MovePlayer(new Vector2(1, 0));
        } else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MovePlayer(new Vector2(0, -1));
        }
    }

    private bool CanMovePlayerTo(Vector3 position)
    {
        if (GetWallAt(position) != null)
        {
            return false;
        }

        if (GetOpponentAt(position) != null)
        {
            return false;
        }

        return !IsPlayerAt(position);
    }

    private void MovePlayer(Vector2 direction)
    {
        var destination = (Vector2)player.transform.position + direction;

        if (CanMovePlayerTo(destination))
        {
            player.GetComponent<Attributes>().Clock();
            player.transform.position = destination;
        } else if (GetOpponentAt(destination) != null)
        {
            player.GetComponent<Attributes>().Clock();
            AttackOpponentAt(destination);
        } else
        {
            return;
        }

        foreach (GameObject opponent in opponents)
        {
            opponent.GetComponent<Opponent>().Move();
        }
    }

    private void AttackOpponentAt(Vector2 position)
    {
        var opponent = GetOpponentAt(position);

        if (opponent == null) return;

        opponent.GetComponent<Opponent>().ReceiveDamage(1);
    }

    public GameObject GetWallAt(Vector2 position)
    {
        foreach (GameObject wall in walls)
        {
            if ((Vector2)wall.transform.position == position) return wall;
        }

        return null;
    }

    public void RegisterWall(GameObject wall)
    {
        walls.Add(wall);
    }



    public GameObject GetOpponentAt(Vector2 position)
    {
        foreach (GameObject opponent in opponents)
        {
            if ((Vector2)opponent.transform.position == position) return opponent;
        }

        return null;
    }

    public bool IsPlayerAt(Vector2 position)
    {
        return (Vector2)player.transform.position == position;
    }

    public void RegisterOpponent(GameObject opponent)
    {
        opponents.Add(opponent);
    }

    public void UnregisterOpponent(GameObject opponent)
    {
        opponents.Remove(opponent);
    }
}
