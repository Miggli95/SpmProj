using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

public class Enemy2Incapacitated : EnemyState2
{
    private Enemy2AI3D ai;
    private float recoverytime = 2.0f;
    public Enemy2Incapacitated(Enemy2AI3D ai)
    {
        if (ai == null)
        {
            throw new ArgumentNullException("no AI");
        }
        this.ai = ai;
    }
    public void Enter()
    {
        Debug.Log("Entered Incapacitated");
        ai.Pause();
    }
    public Enemy2StateData Update(Vector3 pos, float deltaTime, NavMeshAgent agent)
    {
        recoverytime -= deltaTime;
        var enemyStateData = GetEnemyStateData(pos, agent);
        return enemyStateData;

    }
    private Enemy2StateData GetEnemyStateData(Vector3 pos, NavMeshAgent agent)
    {
        var enemyStateData = new Enemy2StateData();
        enemyStateData.Pos = pos;
        if (recoverytime <= 0)
        {
            ai.Resume();
            ai.isIncapacitated = false;
            enemyStateData.NewState = new Enemy2Deal(ai);

        }
        return enemyStateData;
    }

    public void Exit()
    {
        Debug.Log("Left Incapicated");
    }


}