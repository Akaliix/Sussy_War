using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    public Transform _targetTransform;
    [SerializeField]
    public NavMeshAgent _agent;
    [SerializeField]
    public Animator _animator;
    [SerializeField]
    public EnemyAttack _enemyAttack;
    [SerializeField]
    public Collider _enemyCollider;

    public bool isDead = false;

    private void Awake()
    {
        if (_targetTransform == null)
        {
            _targetTransform = PlayerController.singleton.ReturnTransform();
            if (_targetTransform == null)
            {
                Debug.LogError("Target bulunamadi!!!");
                //UnityEditor.EditorApplication.isPlaying = false;
            }
        }
        if (_agent == null && !TryGetComponent(out _agent))
        {
            Debug.LogError("NavMeshAgent bulunamadi!!!");
            //UnityEditor.EditorApplication.isPlaying = false;
        }
    }

    public float GetDistanceBetweenTarget()
    {
        Vector3 distVector = transform.position - _targetTransform.position;
        return distVector.x * distVector.x + distVector.z * distVector.z;
        //return _agent.remainingDistance;
    }
}
