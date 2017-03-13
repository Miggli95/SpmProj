using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

public class Enemy2Idle : EnemyState2
{
    private Enemy2AI3D ai;

    public Enemy2Idle(Enemy2AI3D ai)
    {
        if (ai == null)
        {
            throw new ArgumentNullException("no AI");
        }
        this.ai = ai;
    }
    public void Enter()
    {
        Debug.Log("Entered Idle");
        ai.Pause();
    }
    public Enemy2StateData Update(Vector3 pos, float deltaTime, NavMeshAgent agent)
    {

        var enemyStateData = GetEnemyStateData(pos, agent);
        return enemyStateData;

    }
    private Enemy2StateData GetEnemyStateData(Vector3 pos, NavMeshAgent agent)
    {
        var enemyStateData = new Enemy2StateData();
        enemyStateData.Pos = pos;
        if (Mathf.Abs(pos.x - agent.gameObject.transform.position.x) <= ai.PatrolRangeX && Mathf.Abs(pos.y - agent.gameObject.transform.position.y) <= ai.PatrolRangeY && Mathf.Abs(pos.z - agent.gameObject.transform.position.z) <= ai.PatrolRangeZ)
        {
            ai.Resume();
            enemyStateData.NewState = new Enemy2Patrol(ai);

        }
        return enemyStateData;
    }

    public void Exit()
    {
        Debug.Log("Left Idle");
    }


}