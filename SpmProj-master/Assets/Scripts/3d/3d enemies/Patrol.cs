using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

public class Patrol : EnemyState
{
    private EnemyAI3D ai;
    private NavMeshAgent agent;

    public Patrol(EnemyAI3D ai)
    {
        if (ai == null)
        {
            throw new ArgumentNullException("no AI");
        }
        this.ai = ai;
        ai.GotoNextPoint();
    }
    public void Enter()
    {
        //do things that you do when you start patrolling
    }
    public EnemyStateData Update(Vector3 pos, float deltaTime, NavMeshAgent agent)
    {
        if (agent.remainingDistance < 0.5f)
            ai.GotoNextPoint();
        bool inRange = CalcDistance(pos);
        var enemyStateData = GetEnemyStateData(pos, inRange);
        return enemyStateData;
    }
    private bool CalcDistance(Vector3 pos)
    {
        return (Mathf.Abs(ai.gameObject.transform.position.x - pos.x) <= ai.AggroRangeX && Mathf.Abs(ai.gameObject.transform.position.y - pos.y) <= ai.AggroRangeY && Mathf.Abs(ai.gameObject.transform.position.z - pos.z) <= ai.AggroRangeZ);     
    }
    private EnemyStateData GetEnemyStateData(Vector3 pos, bool inRange)
    {
        var enemyStateData = new EnemyStateData();
        enemyStateData.Pos = pos;
        if (inRange)
        {
            enemyStateData.NewState = new Deal(ai);
        }
        return enemyStateData;
    }
    public void Exit()
    {
        //do things you do when you stop patrolling
    }



    
}