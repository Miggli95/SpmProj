using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

public class Death : EnemyState
{
    private EnemyAI3D ai;
    private float timetoDeath = 0.2f;

    public Death(EnemyAI3D ai)
    {
        if (ai == null)
        {
            throw new ArgumentNullException("no AI");
        }
        this.ai = ai;
        ai.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        BoxCollider boxy = ai.GetComponent<BoxCollider>();
        boxy.enabled = false;
    }
    public void Enter()
    {
        Debug.Log("Entered Death");
    }
    public EnemyStateData Update(Vector3 pos, float deltaTime, NavMeshAgent agent)
    {
        agent.velocity = new Vector3(0, 2, 0);
        timetoDeath -= Time.deltaTime;
        if (timetoDeath <= 0)
            ai.kill();
        var enemyStateData = GetEnemyStateData(pos, agent);
        return enemyStateData;

    }
    private EnemyStateData GetEnemyStateData(Vector3 pos, NavMeshAgent agent)
    {
        var enemyStateData = new EnemyStateData();
        return enemyStateData;
    }

    public void Exit()
    {
        Debug.Log("Left Death?");
    }


}