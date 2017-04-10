using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

public class Enemy2Incapacitated : EnemyState2
{
    private Enemy2AI3D ai;
    private float recoverytime = 4.0f;

    private AudioSource[] sources;
    public AudioClip incapacitate;
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
        ai.transform.GetChild(0).gameObject.transform.localEulerAngles = new Vector3(0, 0, 180);
        sources = ai.GetComponents<AudioSource>();
        incapacitate = ai.incapacitate;
        sources[2].PlayOneShot(incapacitate);
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
        ai.transform.GetChild(0).gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
        Debug.Log("Left Incapicated");
    }


}