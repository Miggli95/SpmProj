using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

public class Deal : EnemyState
{
    private EnemyAI3D ai;
    private NavMeshAgent agent;

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
        //do things that you do when you start chasing
    }
    public EnemyStateData Update(Vector3 pos, float deltaTime, NavMeshAgent agent)
    {
        agent.destination = pos;
        var enemyStateData = GetEnemyStateData(pos);
        return enemyStateData;
        
    }
    private EnemyStateData GetEnemyStateData(Vector3 pos)
    {
        var enemyStateData = new EnemyStateData();
        enemyStateData.Pos = pos;
        if (agent.remainingDistance > 20f)
        {
            enemyStateData.NewState = new Patrol(ai);
        }
        return enemyStateData;
    }

    public void Exit()
    {
        //do things you do when you stop chasing
    }


}