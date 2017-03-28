using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public interface EnemyState3
{
    void Enter();

    Enemy3StateData Update(Vector3 pos, float deltaTime, NavMeshAgent agent);

    void Exit();
}
