using System.Collections.Generic;
using UnityEngine;

public class WaypointFollow : MonoBehaviour
{
    public List<GameObject> wayPoints;
    public Queue<GameObject> path;

    bool lookingBack = false;
    bool inCooldown = false;
    bool endTrack = false;
    bool finalWayPoint = false;

    public float walkSpeed = 15f;
    public float smoothness;

    public float limitDis;

    private GameObject _go;
    private Quaternion originalRotation;
    private Vector3 targetAngles;

    public float maxTime;
    float timing;

    public GameObject footstep;

    public GameObject panelPerdiste;

    void Start()
    {
        panelPerdiste.SetActive(false);

        footstep.SetActive(false);

        path = new Queue<GameObject>();

        foreach(GameObject wayPoint in wayPoints)
        {
            path.Enqueue(wayPoint);
        }

        _go = path.Dequeue();
    }

    
    void Update()
    {
        LookBack();
        if(!lookingBack && !inCooldown && !endTrack)
        {
            FollowPath();
        }

        if(inCooldown)
        {
            timing -= Time.deltaTime;
            if(timing < 0)
            {
                inCooldown = false;
            }
        }

        
    }

    void FollowPath()
    {
        float walkingSpeed = walkSpeed * Time.deltaTime;

        Vector3 direction = (_go.transform.position - transform.position).normalized;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            Quaternion yRotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);

            transform.rotation = Quaternion.Slerp(transform.rotation, yRotation, Time.deltaTime * smoothness);
        }

        /*if (Input.GetMouseButton(0))
        {
            transform.Translate(Vector3.forward * walkingSpeed, Space.Self);
        }*/
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * walkingSpeed, Space.Self);
            footsteps();
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            footstep.SetActive(false);
        }

        float dis = Vector3.Distance(transform.position, _go.transform.position);

        if (dis < limitDis)
        {
            if (path.Count > 0)
            {
                _go = path.Dequeue();

                if (path.Count == 0)
                {
                    finalWayPoint = true;
                }
            }

            else if (finalWayPoint)
            {
                endTrack = true;
            }

        }

    }

    void LookBack()
    {

        Quaternion lookBackAngle = Quaternion.LookRotation(-transform.forward, Vector3.up);

        /*if(Input.GetMouseButtonDown(1))
        {
            originalRotation = transform.rotation;
            lookingBack = true;
        }

        if(Input.GetMouseButton(1))
        {

            transform.GetChild(0).rotation = Quaternion.Slerp(transform.GetChild(0).rotation, lookBackAngle, Time.deltaTime * smoothness);
            //transform.Rotate(0, smoothness * Time.deltaTime, 0, Space.Self);
            //transform.Rotate(transform.forward, 180f, Space.Self);
        }

        if(Input.GetMouseButtonUp(1))
        {
            lookingBack = false;
            timing = maxTime;
            inCooldown = true;
        }*/
        if (Input.GetKeyDown(KeyCode.S))
        {
            originalRotation = transform.rotation;
            lookingBack = true;
            footstep.SetActive(false);
        }

        if (Input.GetKey(KeyCode.S))
        {

            transform.GetChild(0).rotation = Quaternion.Slerp(transform.GetChild(0).rotation, lookBackAngle, Time.deltaTime * smoothness);
            //transform.Rotate(0, smoothness * Time.deltaTime, 0, Space.Self);
            //transform.Rotate(transform.forward, 180f, Space.Self);
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            lookingBack = false;
            timing = maxTime;
            inCooldown = true;
        }

        if (!lookingBack && inCooldown)
        {
            transform.GetChild(0).rotation = Quaternion.Slerp(transform.GetChild(0).rotation, originalRotation, Time.deltaTime * smoothness);
        }
    }


    void footsteps()
    {
        footstep.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemigo"))
        {
            panelPerdiste.SetActive(true);
        }
    }

}
