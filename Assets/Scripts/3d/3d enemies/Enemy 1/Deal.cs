using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

public class Deal : EnemyState
{
    private EnemyAI3D ai;

    public Deal(EnemyAI3D ai)
    {
        if (ai == null)
        {
            throw new ArgumentNullException("no AI");
        }
        this.ai = ai;
    }
    public void Enter()
    {
        Debug.Log("Entered Deal");
    }
    public EnemyStateData Update(Vector3 pos, float deltaTime, NavMeshAgent agent)
    {
        agent.destination = pos;
        var enemyStateData = GetEnemyStateData(pos, agent);
        return enemyStateData;
        
    }
    private EnemyStateData GetEnemyStateData(Vector3 pos, NavMeshAgent agent)
    {
        var enemyStateData = new EnemyStateData();
        enemyStateData.Pos = pos;
        if (agent.remainingDistance > Mathf.Sqrt(ai.AggroRangeX* ai.AggroRangeX + ai.AggroRangeZ* ai.AggroRangeZ))
        {
            ai.EndChase();
            enemyStateData.NewState = new Patrol(ai);
        }
        return enemyStateData;
    }

    public void Exit()
    {
        Debug.Log("Left Deal");
    }


}