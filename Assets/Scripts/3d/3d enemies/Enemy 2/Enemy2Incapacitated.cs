using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

public class Enemy2Incapacitated : EnemyState2
{
    private Enemy2AI3D ai;
    private float recoverytime = 4.0f;
    private float timetoswtich = 0.0f;

    private AudioSource[] sources;
    private Material original;
    public AudioClip incapacitate;
    private bool switcher = true;
    private Vector3 org;
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
        org = ai.transform.GetChild(0).gameObject.transform.localEulerAngles;
        ai.transform.GetChild(0).gameObject.transform.localEulerAngles = new Vector3(0, 0, 180);
        sources = ai.GetComponents<AudioSource>();
        incapacitate = ai.incapacitate;
        sources[2].PlayOneShot(incapacitate);
        Debug.Log("Entered Incapacitated");
        ai.Pause();
        original = ai.rend.material;
    }
    public Enemy2StateData Update(Vector3 pos, float deltaTime, NavMeshAgent agent)
    {
        recoverytime -= deltaTime;
        timetoswtich += deltaTime;
        if (recoverytime <= 4.0f && recoverytime > 3.0f && timetoswtich >= 0.5)
        {
            if (switcher)
            {
                ai.rend.material = ai.red;
                switcher = false;
            }
            else
            {
                ai.rend.material= original;
                switcher = true;
            }
            timetoswtich = 0.0f;
        }
        if(recoverytime<= 2.0f && recoverytime > 1.0f && timetoswtich > 0.2)
        {
            if (switcher)
            {
                ai.rend.material = ai.red;
                switcher = false;
            }
            else
            {
                ai.rend.material = original;
                switcher = true;
            }
            timetoswtich = 0.0f;
        }
        if(recoverytime< 1.0f && timetoswtich > 0.1)
        {
            if (switcher)
            {
                ai.rend.material = ai.red;
                switcher = false;
            }
            else
            {
                ai.rend.material = original;
                switcher = true;
            }
            timetoswtich = 0.0f;
        }
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
        ai.rend.material = original;
        ai.transform.GetChild(0).gameObject.transform.localEulerAngles = org;
        Debug.Log("Left Incapicated");
    }


}