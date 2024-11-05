using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolBehavior : MonoBehaviour
{
    [SerializeField] private GameObject pointA;
    [SerializeField] private GameObject pointB;
    [SerializeField] private bool fromAtoB;
    [SerializeField] 
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        //agent.SetDestination(pointA.transform.position);
        

    }

    // Update is called once per frame
    void Update()
    {
        //if agent reaches its destination, move to next point
        if (fromAtoB == true)
        {
            agent.SetDestination(pointB.transform.position);
        }

        if (fromAtoB == false)
        {
            agent.SetDestination(pointA.transform.position);
        }

        if (Vector2.Distance(agent.transform.position, pointB.transform.position) <= 0.1f)
        {
            fromAtoB = false;
        }
        
        if (Vector2.Distance(agent.transform.position, pointA.transform.position) <= 0.1f)
        {
            fromAtoB = true;
        }

        ;
    }
}
