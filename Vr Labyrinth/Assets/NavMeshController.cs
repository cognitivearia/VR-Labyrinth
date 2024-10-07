using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshController : MonoBehaviour
{
    [SerializeField] Transform objetivo;
    [SerializeField] float rangoDeteccion = 10f; // Distancia a la cual el enemigo empieza a perseguir
    [SerializeField] float radioPatrulla = 20f;  // Radio en el que patrullar� aleatoriamente
    private NavMeshAgent agente;
    private Vector3 destinoAleatorio;
    private bool patrullando = true;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        GenerarNuevoDestinoAleatorio(); // Inicia con una patrulla aleatoria
    }

    void Update()
    {
        float distanciaObjetivo = Vector3.Distance(transform.position, objetivo.position);

        if (distanciaObjetivo <= rangoDeteccion)
        {
            // Si el objetivo est� dentro del rango, perseguirlo
            patrullando = false;
            agente.destination = objetivo.position;
        }
        else
        {
            // Si el objetivo est� fuera del rango, continuar patrullando
            if (!patrullando)
            {
                patrullando = true;
                GenerarNuevoDestinoAleatorio();
            }

            if (agente.remainingDistance <= agente.stoppingDistance && !agente.pathPending)
            {
                // Si ya ha llegado al destino de patrulla, generar un nuevo destino aleatorio
                GenerarNuevoDestinoAleatorio();
            }
        }
    }

    void GenerarNuevoDestinoAleatorio()
    {
        // Generar una posici�n aleatoria dentro del radio de patrulla
        Vector3 randomDirection = Random.insideUnitSphere * radioPatrulla;
        randomDirection += transform.position;

        // Encontrar una posici�n v�lida dentro del NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, radioPatrulla, 1))
        {
            destinoAleatorio = hit.position;
            agente.SetDestination(destinoAleatorio);
        }
    }
}
