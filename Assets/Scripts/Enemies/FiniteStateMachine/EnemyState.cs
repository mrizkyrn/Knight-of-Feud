using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected Entity entity;

    protected float startTime;

    private string animBoolName;

    public EnemyState(Entity entity, string animBoolName)
    {
        this.entity = entity;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        DoChecks();
        startTime = Time.time;
        
        entity.Anim.SetBool(animBoolName, true);

        Debug.Log("Enemy: " + animBoolName);
    }

    public virtual void Exit()
    {
        entity.Anim.SetBool(animBoolName, false);
    }

    public virtual void LogicUpdate()
    {
        
    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks()
    {

    }
}
