using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class NavMeshController : MonoBehaviour
{
    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    [SerializeField] private bool isPatroling = true;

    [SerializeField] Transform playerTrans;
    [SerializeField] private bool isHunting = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;

        GotoNextPoint();
    }


    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }


    void Update()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (isPatroling)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GotoNextPoint();
        }else if (isHunting)
        {
            agent.destination = playerTrans.position;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPatroling = false;
            isHunting = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPatroling = true;
            isHunting = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Head Follower")
        {
            //Debug.Log("daougnvaosfuiygbasfg");
            //isPatroling = false;
            //isHunting = false;
            ReiniciarEscena();
        }
    }

    void ReiniciarEscena()
    {
        //Debug.Log("reicino");

        // Obtiene el nombre o índice de la escena actual
        string escenaActual = SceneManager.GetActiveScene().name;

        // Carga la escena actual nuevamente
        SceneManager.LoadScene(escenaActual);
    }


}
