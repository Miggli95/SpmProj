using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public interface BossState
{
    void Enter();

    BossStateData Update(Vector3 pos, float deltaTime, NavMeshAgent agent);

    void Exit();
}
