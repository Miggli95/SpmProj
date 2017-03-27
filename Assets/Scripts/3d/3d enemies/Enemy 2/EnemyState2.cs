using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public interface EnemyState2
{
    void Enter();

    Enemy2StateData Update(Vector3 pos, float deltaTime, NavMeshAgent agent);

    void Exit();
}
