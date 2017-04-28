using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

public class Enemy3Patrol : EnemyState3
{
    private Enemy3AI3D ai;
    private NavMeshAgent agent;

    public Enemy3Patrol(Enemy3AI3D ai)
    {
        if (ai == null)
        {
            throw new ArgumentNullException("no AI");
        }
        this.ai = ai;
    }
    public void Enter()
    {
       // Debug.Log("Entered Patrol");
    }
    public Enemy3StateData Update(Vector3 pos, float deltaTime, NavMeshAgent agent)
    {
        if (agent.remainingDistance < 0.5f)
            ai.GotoNextPoint();
        int inRange = CalcDistance(pos);
        var enemyStateData = GetEnemyStateData(pos, inRange);
        return enemyStateData;
    }
    private int CalcDistance(Vector3 pos)
    {
        if (Mathf.Abs(ai.gameObject.transform.position.x - pos.x) <= ai.AggroRangeX && Mathf.Abs(ai.gameObject.transform.position.y - pos.y) <= ai.AggroRangeY && Mathf.Abs(ai.gameObject.transform.position.z - pos.z) <= ai.AggroRangeZ)
            return 1;
        if (Mathf.Abs(ai.gameObject.transform.position.x - pos.x) > ai.PatrolRangeX && Mathf.Abs(ai.gameObject.transform.position.z - pos.z) > ai.PatrolRangeZ)
            return 2;
        else
            return 0;
    }

    private Enemy3StateData GetEnemyStateData(Vector3 pos, int inRange)
    {
        var enemyStateData = new Enemy3StateData();
        enemyStateData.Pos = pos;
        switch (inRange)
        {
            case 0:
                break;
            case 1:
                ai.StartChase();
                enemyStateData.NewState = new Enemy3Deal(ai);
                break;
            case 2:
                enemyStateData.NewState = new Enemy3Idle(ai);
                break;

        }
        return enemyStateData;
    }
    public void Exit()
    {
       // Debug.Log("Left Patrol");
    }




}