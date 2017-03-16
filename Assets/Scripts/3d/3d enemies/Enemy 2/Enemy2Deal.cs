using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

public class Enemy2Deal : EnemyState2 {

    private Enemy2AI3D ai;
    private bool notdashing = true;
    private float dashtimer = 0.0f;
    private float resettimer = 0.0f;

    private AudioSource source1;
    private AudioClip dashSound;
    private AudioClip dashChargeSound;
    public Enemy2Deal(Enemy2AI3D ai)
    {
        if (ai == null)
        {
            throw new ArgumentNullException("no AI");
        }
        this.ai = ai;
    }
    public Enemy2StateData Update(Vector3 pos, float deltaTime, NavMeshAgent agent)
    {
        if (notdashing)
        {
            agent.destination = pos;
        }
        if (!notdashing && dashtimer <= 0.0f)
        {
            var lookPos = pos - ai.transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            ai.transform.rotation = Quaternion.Slerp(ai.transform.rotation, rotation, Time.deltaTime);
            Dash(agent);
        }

        
        if (!notdashing && dashtimer > 0.0f)
        {
            dashtimer -= deltaTime;
            sources = ai.GetComponents<AudioSource>();
            dashChargeSound = ai.dashCharge;

            if (!sources[2].isPlaying)
            {
                sources[2].PlayOneShot(dashChargeSound);
            }
        }
        if(!notdashing && resettimer > 0.0f)
        {
            resettimer -= deltaTime;
            Debug.Log(resettimer);
            if (resettimer <= 0.0f)
            {
                Debug.Log("stopped dashing");
                agent.velocity = Vector3.zero;
                notdashing = true;
                ai.Resume();
            }
        }

        var enemyStateData = GetEnemyStateData(pos, agent);
        return enemyStateData;

    }
    public void Enter()
    {
        Debug.Log("Entered Deal");
    }
    private Enemy2StateData GetEnemyStateData(Vector3 pos, NavMeshAgent agent)
    {
        var enemyStateData = new Enemy2StateData();
        enemyStateData.Pos = pos;
        if (agent.remainingDistance > Mathf.Sqrt(ai.AggroRangeX * ai.AggroRangeX + ai.AggroRangeZ * ai.AggroRangeZ))
        {
            ai.EndChase();
            enemyStateData.NewState = new Enemy2Patrol(ai);
        }
        if (agent.remainingDistance <= ai.getDashRange() && notdashing)
        {
            ai.Pause();
            dashtimer = 1.0f;
            resettimer = 0.0f;
            notdashing = false;
        }
        return enemyStateData;
    }
    private void Dash(NavMeshAgent agent)
    {
        source1 = ai.GetComponent<AudioSource>();
        dashSound = ai.dash;
        Debug.Log("started dashing");
        if (resettimer == 0.0f)
        {
            source1.PlayOneShot(dashSound);
            agent.velocity += ai.transform.forward * ai.getDashSpeed();
            resettimer = 0.7f;
        }
    }
    public void Exit()
    {
        Debug.Log("Left Deal");
    }
}
