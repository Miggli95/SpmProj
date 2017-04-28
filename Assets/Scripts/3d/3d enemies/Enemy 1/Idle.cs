using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

public class Idle : EnemyState
{
    private EnemyAI3D ai;

    public Idle(EnemyAI3D ai)
    {
        if (ai == null)
        {
            throw new ArgumentNullException("no AI");
        }
        this.ai = ai;
    }
    public void Enter()
    {
        //Debug.Log("Entered Idle");
        ai.Pause();
    }
    public EnemyStateData Update(Vector3 pos, float deltaTime, NavMeshAgent agent)
    {

        var enemyStateData = GetEnemyStateData(pos, agent);
        return enemyStateData;

    }
    private EnemyStateData GetEnemyStateData(Vector3 pos, NavMeshAgent agent)
    {
        var enemyStateData = new EnemyStateData();
        enemyStateData.Pos = pos;
        if (Mathf.Abs(pos.x - agent.gameObject.transform.position.x) <= ai.PatrolRangeX && Mathf.Abs(pos.y - agent.gameObject.transform.position.y) <= ai.PatrolRangeY && Mathf.Abs(pos.z - agent.gameObject.transform.position.z) <= ai.PatrolRangeZ)
        {
            ai.Resume();
            enemyStateData.NewState = new Patrol(ai);

        }
        return enemyStateData;
    }

    public void Exit()
    {
        //Debug.Log("Left Idle");
    }


}