using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Goal : MonoBehaviour
{
    public delegate void GoalCollision(int objectType);
    public static event GoalCollision OnGoalCollision;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            OnGoalCollision(5);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            OnGoalCollision(6);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            OnGoalCollision(7);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            OnGoalCollision(8);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Wall_0" |
            collision.gameObject.name == "Wall_1" |
            collision.gameObject.name == "Wall_2" |
            collision.gameObject.name == "Wall_3")
        {
            OnGoalCollision(0);

        }

        // if (collision.gameObject.name == "Wall_2" |
        //     collision.gameObject.name == "Wall_3")
        // {
        //     OnGoalCollision(1);

        // }

        if (collision.gameObject.name == "GoalWalls_1" |
            collision.gameObject.name == "GoalWalls_2")
        {
            OnGoalCollision(2);
        }


    }
}


