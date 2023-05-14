using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    //player1
    [SerializeField] private GameObject player1;
    private Vector3 player1InitPos;
    private Rigidbody player1Rb;

    //player2
    [SerializeField] private GameObject player2;
    private Vector3 player2InitPos;
    private Rigidbody player2Rb;
    //ball
    private Vector3 initPos;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;
        rb = GetComponent<Rigidbody>();
        
        player1InitPos = player1.transform.position;
        player1Rb = player1.GetComponent<Rigidbody>();

        player2InitPos = player2.transform.position;
        player2Rb = player2.GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other) {
        string tag = other.gameObject.tag;
        if (tag == "field1" || tag =="field2"){
            player1.transform.position = player1InitPos;
            player1Rb.velocity = Vector3.zero;
            player1Rb.angularVelocity = Vector3.zero;

            player2.transform.position = player2InitPos;
            player2Rb.velocity = Vector3.zero;
            player2Rb.angularVelocity = Vector3.zero;

            float xPos = tag == "field2" ? player1InitPos.x : player2InitPos.x;
            transform.position = new Vector3(xPos,initPos.y,initPos.z);
            //transform.position = initPos;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
