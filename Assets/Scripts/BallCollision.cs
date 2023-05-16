using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class BallCollision : MonoBehaviour
{
    //player1
    public GameObject player1;
    private Vector3 player1InitPos;
    private Rigidbody player1Rb;
    private PlayVolleyball playerAgent1;

    //player2
    public GameObject player2;
    private Vector3 player2InitPos;
    private Rigidbody player2Rb;
    private PlayVolleyball playerAgent2;
    //ball
    private Vector3 initPos;
    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        playerAgent1 = player1.GetComponent<PlayVolleyball>();
        playerAgent2 = player2.GetComponent<PlayVolleyball>();


        initPos = transform.localPosition;
        rb = GetComponent<Rigidbody>();
        
        player1InitPos = player1.transform.localPosition;
        player1Rb = player1.GetComponent<Rigidbody>();

        player2InitPos = player2.transform.localPosition;
        player2Rb = player2.GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other) {
        string tag = other.gameObject.tag;
        if (tag == "field1" || tag =="field2"){

            float randPosX = Random.Range(0f,6f);
            float randPosZ = Random.Range(-3f,3f);
            float xPos = tag == "field2" ? player1InitPos.x+randPosX : player2InitPos.x-randPosX;
            transform.localPosition = new Vector3(xPos,initPos.y,randPosZ);

            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            if (tag == "field2"){
                // print("Pelota toca piso2");
                playerAgent2.AddReward(-20);
                playerAgent1.AddReward(20);
                playerAgent1.EndEpisode();
                playerAgent2.EndEpisode();
            }else{
                // print("Pelota toca piso1");
                playerAgent2.AddReward(20);
                playerAgent1.AddReward(-20);
                playerAgent1.EndEpisode();
                playerAgent2.EndEpisode();
            }
        }
    }
}
