using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

public class Enemy3Deal : EnemyState3
{
    private Enemy3AI3D ai;

    public Enemy3Deal(Enemy3AI3D ai)
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
    public Enemy3StateData Update(Vector3 pos, float deltaTime, NavMeshAgent agent)
    {
        RaycastHit rayhit;
        if (Physics.Raycast(ai.transform.position, Vector3.forward, out rayhit))
        {
            if (rayhit.collider.tag == "Player" && rayhit.distance < 0.3f)
            {
                ai.getDamage();
            }
        }
        if (Physics.Raycast(ai.transform.position, Vector3.left, out rayhit))
        {
            if (rayhit.collider.tag == "Player" && rayhit.distance < 0.3f)
            {
                ai.getDamage();
            }
        }
        if (Physics.Raycast(ai.transform.position, Vector3.right, out rayhit))
        {
            if (rayhit.collider.tag == "Player" && rayhit.distance < 0.3f)
            {
                ai.getDamage();
            }
        }
        if (Physics.Raycast(ai.transform.position, Vector3.back, out rayhit))
        {
            if (rayhit.collider.tag == "Player" && rayhit.distance < 0.3f)
            {
                ai.getDamage();
            }
        }
        agent.destination = pos;
        var enemyStateData = GetEnemyStateData(pos, agent);
        return enemyStateData;

    }
    private Enemy3StateData GetEnemyStateData(Vector3 pos, NavMeshAgent agent)
    {
        var enemyStateData = new Enemy3StateData();
        enemyStateData.Pos = pos;
        if (agent.remainingDistance > Mathf.Sqrt(ai.AggroRangeX * ai.AggroRangeX + ai.AggroRangeZ * ai.AggroRangeZ))
        {
            ai.EndChase();
            enemyStateData.NewState = new Enemy3Patrol(ai);
        }
        return enemyStateData;
    }

    public void Exit()
    {
        Debug.Log("Left Deal");
    }


}