using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        if (playerTransform == null)
        {
            Debug.LogError("Player bulunamadi!!!");
            UnityEditor.EditorApplication.isPlaying = false;
        }
        if (agent == null && !TryGetComponent(out agent))
        {
            Debug.LogError("NavMeshAgent bulunamadi!!!");
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }

    private void Update()
    {
        agent.destination = playerTransform.position;
    }
}
