using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    [SerializeField]
    private EnemyController _enemyController;

    [SerializeField]
    private float RotationalSpeed = 1f;

    [SerializeField]
    private bool isMeelee = true;

    //[SerializeField]
    //private float nearAttackDistance = 4f;


    void Start()
    {
        _enemyController._agent.SetDestination(_enemyController._targetTransform.position);
    }

    private void Update()
    {
        if (!GameSettings.singleton.isGameStarted || GameSettings.singleton.isGamePaused || _enemyController.isDead)
            return;

        if (_enemyController._agent.remainingDistance <= _enemyController._agent.stoppingDistance)
        {
            Rotate();
        }
        else
        {
            Attack(false);
            _enemyController._agent.SetDestination(_enemyController._targetTransform.position);
        }
    }

    private void Rotate()
    {
        _enemyController._animator.SetBool("run", false);
        Vector3 orientationForwardVector = _enemyController._targetTransform.position - transform.position;
        orientationForwardVector.y = 0;
        if (orientationForwardVector.magnitude < _enemyController._agent.stoppingDistance)
        {
            Quaternion tempRot = Quaternion.LookRotation(orientationForwardVector);
            transform.rotation = Quaternion.Slerp(transform.rotation, tempRot, Time.deltaTime * RotationalSpeed);

            float angle = Quaternion.Angle(transform.rotation, tempRot);
            if (angle < 10)
            {
                Attack();
            }
        }
        else
        {
            Attack(false);
            _enemyController._agent.SetDestination(_enemyController._targetTransform.position);
        }

    }

    private void Attack(bool attack = true)
    {
        if (attack)
        {
            if (_enemyController._enemyAttack.Attack(isMeelee))
            {
                _enemyController._animator.SetBool("attack", true);
            }
            else
            {
                _enemyController._animator.SetBool("attack", false);
            }
        }
        else
        {
            _enemyController._enemyAttack.NotAttack();
            _enemyController._animator.SetBool("run", true);
            _enemyController._animator.SetBool("attack", false);
        }
    }

    //void Update()
    //{
    //    if (_agent.remainingDistance <= _agent.stoppingDistance) //done with path
    //    {
    //        Vector3 point;
    //        if (RandomPoint(_targetTransform.position, range, out point)) //pass in our centre point and radius of area
    //        {
    //            Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
    //            _agent.SetDestination(point);
    //        }
    //    }

    //}
    //bool RandomPoint(Vector3 center, float range, out Vector3 result)
    //{
    //    Vector2 randUnitCircle = Random.insideUnitCircle.normalized;
    //    Vector3 randomPoint = center + new Vector3(randUnitCircle.x, 0, randUnitCircle.y) * range; //random point in a sphere 
    //    NavMeshHit hit;
    //    if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
    //    {
    //        //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
    //        //or add a for loop like in the documentation
    //        result = hit.position;
    //        return true;
    //    }

    //    result = Vector3.zero;
    //    return false;
    //}
}
