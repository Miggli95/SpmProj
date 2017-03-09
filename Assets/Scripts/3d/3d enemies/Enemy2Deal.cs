using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

public class Enemy2Deal : Deal {

    private EnemyAI3D ai;
    private bool notdashing = true;
    private float dashtimer = 0;

    public Enemy2Deal(EnemyAI3D ai):base(ai)
    {

    }
    public EnemyStateData Update(Vector3 pos, float deltaTime, NavMeshAgent agent)
    {
        if (notdashing)
            agent.destination = pos;
        else if (dashtimer < 0)
            Dash(agent);
        else
            dashtimer =- deltaTime;
        var enemyStateData = GetEnemyStateData(pos, agent);
        return enemyStateData;

    }
    private EnemyStateData GetEnemyStateData(Vector3 pos, NavMeshAgent agent)
    {
        var enemyStateData = new EnemyStateData();
        enemyStateData.Pos = pos;
        if (agent.remainingDistance > Mathf.Sqrt(ai.AggroRangeX * ai.AggroRangeX + ai.AggroRangeZ * ai.AggroRangeZ))
        {
            ai.EndChase();
            enemyStateData.NewState = new Patrol(ai);
        }
        if (agent.remainingDistance <= ai.GetDashRange() && notdashing)
        {
            ai.Pause();
            dashtimer = 1f;
            notdashing = false;
        }
        return enemyStateData;
    }
    private void Dash(NavMeshAgent agent)
    {
        agent.velocity = new Vector3 (ai.GetDashSpeed(), 0, 0);
    }
}
