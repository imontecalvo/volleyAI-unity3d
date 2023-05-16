using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class PlayVolleyball : Agent
{
    [SerializeField] private GameObject field;
    bool ballInMyField;

    [SerializeField] private Transform opponentTransform;
    [SerializeField] private GameObject ball;
    [SerializeField] private Transform netTransform;
    bool isJumping = false;
    private Rigidbody rb;

    private Vector3 initPosition;

    private void Start() {
        initPosition = transform.localPosition;
        rb = GetComponent<Rigidbody>();
        ballInMyField = field.tag == "field1" ? ball.transform.localPosition.x < 0f : ball.transform.localPosition.x > 0f;
        //lo invierto para que empiece bien en OnEpisodeBegin()
        ballInMyField = !ballInMyField;
    }


    public override void OnEpisodeBegin(){
        transform.position = initPosition;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        float randPosX = Random.Range(-6f,0f);
        float randPosZ = Random.Range(-5f,5f);
        transform.Translate(new Vector3(randPosX,0f,randPosZ));
        print(initPosition);

        //al comienzo del episodio cambio al valor opuesto (si el punto del ep anterior fue en mi cancha ahora la pelota aparece en el otro lado)
        ballInMyField = !ballInMyField;
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

        // Ball went over the net -> Reward 15
        bool prevBallField = ballInMyField;
        ballInMyField = field.tag == "field1" ? ball.transform.localPosition.x < 0f : ball.transform.localPosition.x > 0f;
        if ((ballInMyField != prevBallField) && !ballInMyField){
            // print("Pasa red " + gameObject.name);
            AddReward(15f);
        }

        // Distance to ball -> Reward [-2,2]
        if (ballInMyField){
            float MAX_DIST = 22f;
            float distanceToBall = Vector3.Distance(transform.localPosition, ball.transform.localPosition);
            float reward = 4*((-distanceToBall/MAX_DIST)+1)-2;
            // print("Distancia pelota " + distanceToBall + "rw: " + reward);
            AddReward(reward);
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut){
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        // continuousActions[0] = Input.GetAxisRaw("Horizontal");
        // continuousActions[1] = Input.GetAxisRaw("Vertical");

        continuousActions[1] = -Input.GetAxisRaw("Horizontal");
        continuousActions[0] = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.Space))
        {
            continuousActions[2] = 1;
        }else{
            continuousActions[2] = 0;
        }

    }

    private void OnCollisionEnter(Collision other){
        string tag = other.gameObject.tag;
        if (tag == "field1" || tag == "field2"){
            isJumping = false;
        }

        else if (tag == "ball"){
            float hitBallRw = 7;
            // print("Golpeo pelota " + gameObject.name);
            AddReward(hitBallRw);
        }

        else if (tag == "wall" || tag == "net"){
            float touchWallRw = -1.5f;
            // print("Toco pared");
            AddReward(touchWallRw);
        }
    }

}
