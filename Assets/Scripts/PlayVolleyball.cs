using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class PlayVolleyball : Agent
{
    [SerializeField] private Transform opponentTransform;
    [SerializeField] private GameObject ball;
    [SerializeField] private Transform netTransform;
    bool isJumping = false;
    private Rigidbody rb;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin(){
        // transform.localPosition = new Vector3(Random.Range(-4f,4f),0,Random.Range(-3f,3f));;
        // targetTransform.localPosition = new Vector3(Random.Range(5f,9f),0,Random.Range(-3f,3f));
    }

    public override void CollectObservations(VectorSensor sensor){
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(ball.transform.localPosition);
        sensor.AddObservation(ball.GetComponent<Rigidbody>().velocity);
        sensor.AddObservation(opponentTransform.localPosition);
        sensor.AddObservation(netTransform.localPosition.y+(netTransform.localScale.y/2));
        sensor.AddObservation(Mathf.Abs(netTransform.localPosition.x-transform.localPosition.x));

    }

    public override void OnActionReceived(ActionBuffers actions){
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];
        float jump = actions.ContinuousActions[2];


        float moveSpeed = 5f;
        float jumpForce = 6.5f;

        Vector3 movement = new Vector3(moveX, 0f, moveZ) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);


        // transform.localPosition += new Vector3(moveX,0,moveZ) * Time.deltaTime * moveSpeed;
        if (!isJumping && jump > 0){
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut){
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[1] = -Input.GetAxisRaw("Horizontal");
        continuousActions[0] = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.Space))
        {
            continuousActions[2] = 1;
        }else{
            continuousActions[2] = 0;
        }

    }

    private void OnTriggerEnter(Collider other){
        // if (other.gameObject.tag == "Goal"){
        //     SetReward(1f);
        //     print("win");
        //     floorMeshRenderer.material = winMaterial;
        //     EndEpisode();
        // }

        // if (other.gameObject.tag == "Wall"){
        //     SetReward(-1f);
        //     floorMeshRenderer.material = loseMaterial;
        //     EndEpisode();
        // }

    }

    private void OnCollisionEnter(Collision other){
        string tag = other.gameObject.tag;
        if (tag == "field1" || tag == "field2"){
            isJumping = false;
        }
    }
}