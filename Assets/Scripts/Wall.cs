using UnityEngine;

public class Wall : MonoBehaviour
{
    public GameObject controller;

    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        controller.GetComponent<Game>().RegisterWall(gameObject);
    }
}
