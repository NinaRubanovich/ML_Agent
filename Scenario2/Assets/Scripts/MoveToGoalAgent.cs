using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MoveToGoalAgent : Agent
{
    [SerializeField] private Transform targetTransform;

    [SerializeField] private Material winMaterial;
    [SerializeField] private Material loseMaterial;
    [SerializeField] private Material victoryMaterial;
    [SerializeField] private MeshRenderer floorMeshRenderer;
    [SerializeField] private float moveSpeed;

    private float wallGoalCenter_X = 0f;
    private float wallGoalCenter_Z = 5.5f;
    private float oldDistance = 50.0f;
    void update()
    {


        float newX = targetTransform.localPosition.x - wallGoalCenter_X;
        float newZ = targetTransform.localPosition.z - wallGoalCenter_Z;

        float newDistance = Mathf.Pow(Mathf.Pow(newX, 2) + Mathf.Pow(newZ, 2), 0.5f);

        if (newDistance < oldDistance)
        {
            SetReward(0.01f);
        }
        else
            SetReward(-0.01f);

        oldDistance = newDistance;
    }
    public override void OnEpisodeBegin()
    {
        Goal.OnGoalCollision += collisionOccur;
        transform.localPosition = new Vector3(Random.Range(-3f, +3f), -0.5f, Random.Range(-3f, +3f));
        targetTransform.localPosition = new Vector3(Random.Range(-2f, +2f), 0, Random.Range(-2f, +2f));
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransform.localPosition);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        //float moveSpeed = 1f;
        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Wall>(out Wall wall))
        {
            Debug.Log("Trigger and Walls Collision");
            SetReward(-1f);
            floorMeshRenderer.material = loseMaterial;
            EndEpisode();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Wall_0" |
            collision.gameObject.name == "Wall_1" |
            collision.gameObject.name == "Wall_2" |
            collision.gameObject.name == "Wall_3")
        {
            Debug.Log("Agent and Walls Collision");
            AddReward(-0.1f);
            this.floorMeshRenderer.material = loseMaterial;
            myEndEpisode();
        }

        // if (collision.gameObject.name == "Goal")
        // {
        //     Debug.Log("Goal Collision");
        //     SetReward(0.01f);
        // }
    }

    private void collisionOccur(int colType)
    {
        if (Input.GetKeyDown(KeyCode.W))
        {

        }
        if (Input.GetKeyDown(KeyCode.S))
        {

        }
        if (Input.GetKeyDown(KeyCode.A))
        {

        }
        if (Input.GetKeyDown(KeyCode.D))
        {

        }

        switch (colType)
        {


            case 5:
                {
                    Debug.Log("Key W");
                    Vector3 position = this.transform.localPosition;
                    position.x -= .1f;
                    this.transform.localPosition = position;

                    Vector3 rot = this.transform.rotation.eulerAngles;
                    rot = new Vector3(rot.x, 270, rot.z);
                    this.transform.rotation = Quaternion.Euler(rot);
                }
                break;

            case 6:
                {
                    Debug.Log("Key S");
                    Vector3 position = this.transform.localPosition;
                    position.x += .1f;
                    this.transform.localPosition = position;

                    Vector3 rot = this.transform.rotation.eulerAngles;
                    rot = new Vector3(rot.x, 90, rot.z);
                    this.transform.rotation = Quaternion.Euler(rot);
                }
                break;
            case 7:
                {
                    Debug.Log("Key A");
                    Vector3 position = this.transform.localPosition;
                    position.z -= .1f;
                    this.transform.localPosition = position;

                    Vector3 rot = this.transform.rotation.eulerAngles;
                    rot = new Vector3(rot.x, 180, rot.z);
                    this.transform.rotation = Quaternion.Euler(rot);

                }
                break;
            case 8:
                {
                    Debug.Log("Key D");
                    Vector3 position = this.transform.localPosition;
                    position.z += .1f;
                    this.transform.localPosition = position;

                    Vector3 rot = this.transform.rotation.eulerAngles;
                    rot = new Vector3(rot.x, 0, rot.z);
                    this.transform.rotation = Quaternion.Euler(rot);


                }
                break;


            case 0:
                {
                    // Debug.Log("Wall behind the Goal Collision");
                    // SetReward(-1f);
                    // this.floorMeshRenderer.material = loseMaterial;
                    // myEndEpisode();
                }
                break;
            // case 1:
            //     {
            //         Debug.Log("wrong Wall Collision");
            //         SetReward(-1f);
            //         this.floorMeshRenderer.material = loseMaterial;
            //         myEndEpisode();
            //     }
            //     break;
            case 2:
                {
                    Debug.Log("Goal Wall Collision");
                    SetReward(3f);
                    this.floorMeshRenderer.material = winMaterial;
                    myEndEpisode();
                }
                // code block
                break;
            default:
                Debug.Log("Something Else Collision");
                break;
        }
    }
    private void myEndEpisode()
    {
        Goal.OnGoalCollision += collisionOccur;
        EndEpisode();
    }

}
