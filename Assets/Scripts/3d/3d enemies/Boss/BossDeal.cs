using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

public class BossDeal : BossState
{

    private BossAI ai;
    private bool notdashing = true;
    private float dashtimer = 0.0f;
    private float resettimer = 0.0f;

    /*private AudioSource[] sources;
    private AudioClip dashSound;
    private AudioClip dashChargeSound;*/
    public BossDeal(BossAI ai)
    {
        if (ai == null)
        {
            throw new ArgumentNullException("no AI");
        }
        this.ai = ai;
    }
    public BossStateData Update(Vector3 pos, float deltaTime, NavMeshAgent agent)
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
        if (notdashing)
        {
            agent.destination = pos;
        }
        if (!notdashing && dashtimer <= 0.0f)
        {
            var lookPos = pos - ai.transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            //ai.transform.rotation = Quaternion.Slerp(ai.transform.rotation, rotation, Time.deltaTime);
            Dash(agent);
        }


        if (!notdashing && dashtimer > 0.0f)
        {
            var lookPos = pos - ai.transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            ai.transform.rotation = Quaternion.Slerp(ai.transform.rotation, rotation, 1);
            dashtimer -= deltaTime;
            //sources = ai.GetComponents<AudioSource>();
            //dashChargeSound = ai.dashCharge;

           /* if (!sources[0].isPlaying)
            {
                sources[0].PlayOneShot(dashChargeSound);
            }*/
        }
        if (!notdashing && resettimer > 0.0f)
        {
            resettimer -= deltaTime;
           // Debug.Log(resettimer);
            if (resettimer <= 0.0f)
            {
               // Debug.Log("stopped dashing");
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
       // Debug.Log("Entered Deal");
    }
    private BossStateData GetEnemyStateData(Vector3 pos, NavMeshAgent agent)
    {
        var enemyStateData = new BossStateData();
        enemyStateData.Pos = pos;
        if (agent.remainingDistance <= ai.getDashRange() && notdashing)
        {
            ai.Pause();
            dashtimer = 2.0f;
            resettimer = 0.0f;
            notdashing = false;
        }

        return enemyStateData;
    }
    private void Dash(NavMeshAgent agent)
    {
        //sources[0] = ai.GetComponent<AudioSource>();
        //dashSound = ai.dash;
       // Debug.Log("started dashing");
        if (resettimer == 0.0f)
        {
            //sources[0].PlayOneShot(dashSound);
            agent.velocity += ai.transform.forward * ai.getDashSpeed();
            resettimer = 2.7f;
        }
    }
    public void Exit()
    {
       // Debug.Log("Left Deal");
    }
}
