using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentActivator : MonoBehaviour
{
    [SerializeField] private GameObject agent;

    private void Start()
    {
        agent.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ship"))
        {
            agent.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ship"))
        {
            agent.SetActive(false);
        }
    }
}
