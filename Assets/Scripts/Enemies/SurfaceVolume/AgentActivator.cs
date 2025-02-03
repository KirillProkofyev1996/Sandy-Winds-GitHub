using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentActivator : MonoBehaviour
{
    [SerializeField] private GameObject agent;

    private void Start()
    {
        if (agent != null)
        {
            agent.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ship"))
        {
            if (agent != null)
            {
                agent.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ship"))
        {
            if (agent != null)
            {
                agent.SetActive(false);
            }
        }
    }
}
