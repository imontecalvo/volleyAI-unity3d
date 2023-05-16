using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTracker : MonoBehaviour
{
    [SerializeField] private Transform ballTransform;
    void Start()
    {
        transform.position = new Vector3(ballTransform.position.x,0f,ballTransform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(ballTransform.position.x,0f,ballTransform.position.z);
    }
}
