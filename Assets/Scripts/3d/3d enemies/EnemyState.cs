using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public interface EnemyState
{
    void Enter();

    EnemyStateData Update(Vector3 pos, float deltaTime, NavMeshAgent agent);

    void Exit();
}
