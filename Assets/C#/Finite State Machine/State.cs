using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class State
{
    public enum STATE
    {
        IDLE,
        PATROL,
        PURSUE,
        ATTACK,
        SLEEP,
        RUNAWAY
    };

    public enum EVENT
    {
        ENTER,
        UPDATE,
        EXIT
    };

    public STATE name;
    protected EVENT stage;
    protected GameObject npc;
    protected Animator anim;
    protected Transform player;
    protected State nextState;
    protected NavMeshAgent agent;

    private float visDist = 10.0f;
    private float visAngle = 30.0f;
    private float shootDist = 7.0f;
    
    public State(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player)
    {
        npc = _npc;
        agent = _agent;
        anim = _anim;
        stage = EVENT.ENTER;
        player = _player;
    }

    public virtual void Enter()
    {
        stage = EVENT.UPDATE;
    }

    public virtual void Update()
    {
        stage = EVENT.UPDATE;
    }

    public virtual void Exit()
    {
        stage = EVENT.EXIT;
    }

    public State Process()
    {
        if (stage == EVENT.ENTER) Enter();
        if (stage == EVENT.UPDATE) Update();
        if (stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }

        return this;
    }

    public bool CanSeePlayer()
    {
        Vector3 direction = player.position - npc.transform.position;
        float angle = Vector3.Angle(direction, npc.transform.forward);

        return direction.magnitude < visDist && angle < visAngle;
    }

    public bool IsPlayerBehind()
    {
        Vector3 direction = npc.transform.position - player.position;
        float angle = Vector3.Angle(direction, npc.transform.forward);
        
        return direction.magnitude < 2 && angle < 30;
    }

    public bool CanAttackPlayer()
    {
        Vector3 direction = player.position - npc.transform.position;
        
        return direction.magnitude < shootDist;
    }
}

public class Idle : State
{
    public Idle(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim, _player)
    {
        name = STATE.IDLE;
    }

    public override void Enter()
    {
        anim.SetTrigger("isIdle");
        base.Enter();
    }

    public override void Update()
    {
        if (CanSeePlayer())
        {
            nextState = new Pursue(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
        else if (Random.Range(0, 2000) < 10)
        {
            nextState = new Patrol(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
        

    }

    public override void Exit()
    {
        anim.ResetTrigger("isIdle");
        base.Exit();
    }
}

public class Patrol : State
{
    private int currentIndex = -1;

    public Patrol(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim,
        _player)
    {
        name = STATE.PATROL;
        agent.speed = 2;
        agent.isStopped = false;
    }

    public override void Enter()
    {
        float lastDist = Mathf.Infinity;
        for (int i = 0; i < GameEnvironment.SingleTon.Checkpoints.Count; i++)
        {
            GameObject thisWP = GameEnvironment.SingleTon.Checkpoints[i];
            float distance = Vector3.Distance(npc.transform.position, thisWP.transform.position);
            if (distance < lastDist)
            {
                currentIndex = i - 1;
                lastDist = distance;
            }
        }
        anim.SetTrigger("isWalking");
        base.Enter();
    }
    public override void Update()
    {
        if (agent.remainingDistance < 1)
        {
            if (currentIndex >= GameEnvironment.SingleTon.Checkpoints.Count - 1)
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex++;
            }

            agent.SetDestination(GameEnvironment.SingleTon.Checkpoints[currentIndex].transform.position);
        }
        
        if (CanSeePlayer())
        {
            nextState = new Pursue(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
        else if (IsPlayerBehind())
        {
            nextState = new RunAway(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
        
    }
    public override void Exit()
    {
        anim.ResetTrigger("isWalking");
        base.Exit();
    }
}

public class Pursue : State
{
    public Pursue(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim,
        _player)
    {
        name = STATE.PURSUE;
        agent.speed = 5;
        agent.isStopped = false;
    }

    public override void Enter()
    {
        anim.SetTrigger("isRunning");
        base.Enter();
    }

    public override void Update()
    {
        agent.SetDestination(player.position);
        if (agent.hasPath)
        {
            if (CanAttackPlayer())
            {
                nextState = new Attack(npc, agent, anim, player);
                stage = EVENT.EXIT;
            }
            else if(!CanSeePlayer())
            {
                nextState = new Patrol(npc, agent, anim, player);
                stage = EVENT.EXIT;
            }
        }
    }

    public override void Exit()
    {
        anim.ResetTrigger("isRunning");
        base.Exit();
    }
}

public class Attack : State
{
    private float rotationSpeed = 2.0f;
    private AudioSource shoot;
    
    public Attack(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim,
        _player)
    {
        name = STATE.ATTACK;
        shoot = _npc.GetComponent<AudioSource>();
    }

    public override void Enter()
    {
        anim.SetTrigger("isShooting");
        agent.isStopped = true;
        shoot.Play();
        base.Enter();
    }

    public override void Update()
    {
        Vector3 direction = player.position - npc.transform.position;
        float angle = Vector3.Angle(direction, npc.transform.forward);
        direction.y = 0;

        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, Quaternion.LookRotation(direction),
            Time.deltaTime * rotationSpeed);

        if (!CanSeePlayer())
        {
            nextState = new Idle(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        anim.ResetTrigger("isShooting");
        shoot.Stop();
        base.Exit();
    }
}

public class RunAway : State
{
    private GameObject safeLocation;

    public RunAway(GameObject _npc, NavMeshAgent _agent, Animator _anim, Transform _player) : base(_npc, _agent, _anim,
        _player)
    {
        name = STATE.RUNAWAY;
        safeLocation = GameObject.FindGameObjectWithTag("Safe");
    }

    public override void Enter()
    {
        anim.SetTrigger("isRunning");
        agent.isStopped = false;
        agent.speed = 6;
        agent.SetDestination(safeLocation.transform.position);
        base.Enter();
    }

    public override void Update()
    {
        if (agent.remainingDistance < 1)
        {
            nextState = new Idle(npc, agent, anim, player);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        anim.ResetTrigger("isRunning");
        base.Exit();
    }
}