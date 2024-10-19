using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class FollowWaypoint : MonoBehaviour
{
    public GameObject[] waypoints;
    int currentWaypoint = 0;
    public float distanceToWaypoint;

    private float speed;
    public float minSpeed;
    public float setSpeed;
    public float rotSpeed;

    [SerializeField] private Animator animator;

    void Start()
    {
        speed = setSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        moveBetweenWaypoints();
    }

    void moveBetweenWaypoints()
    {
        if(Vector3.Distance(this.transform.position, waypoints[currentWaypoint].transform.position) > distanceToWaypoint && speed > minSpeed)
        {
            speed = speed / 1.01f;
        }

        if (Vector3.Distance(this.transform.position, waypoints[currentWaypoint].transform.position) < distanceToWaypoint)
        {
            animator.SetTrigger("turnLeft");
            currentWaypoint++;
            speed = setSpeed;
        }
        if(currentWaypoint >= waypoints.Length)
        {
            currentWaypoint = 0;
        }

        Quaternion lookAtWaypoint = Quaternion.LookRotation(waypoints[currentWaypoint].transform.position - this.transform.position);

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, lookAtWaypoint, rotSpeed * Time.deltaTime);

        this.transform.Translate(0,0,(speed * Time.deltaTime));
    }
}
