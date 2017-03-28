using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

public class Enemy3Idle : EnemyState3
{
    private Enemy3AI3D ai;

    public Enemy3Idle(Enemy3AI3D ai)
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
    public Enemy3StateData Update(Vector3 pos, float deltaTime, NavMeshAgent agent)
    {

        var enemyStateData = GetEnemyStateData(pos, agent);
        return enemyStateData;

    }
    private Enemy3StateData GetEnemyStateData(Vector3 pos, NavMeshAgent agent)
    {
        var enemyStateData = new Enemy3StateData();
        enemyStateData.Pos = pos;
        if (Mathf.Abs(pos.x - agent.gameObject.transform.position.x) <= ai.PatrolRangeX && Mathf.Abs(pos.y - agent.gameObject.transform.position.y) <= ai.PatrolRangeY && Mathf.Abs(pos.z - agent.gameObject.transform.position.z) <= ai.PatrolRangeZ)
        {
            ai.Resume();
            enemyStateData.NewState = new Enemy3Patrol(ai);

        }
        return enemyStateData;
    }

    public void Exit()
    {
        Debug.Log("Left Idle");
    }


}