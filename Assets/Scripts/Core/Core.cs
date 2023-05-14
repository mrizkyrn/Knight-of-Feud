using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public Movement Movement
    {
        get => GenericNotImplementedError<Movement>.TryGet(movement, transform.parent.name);
        private set => movement = value;
    }

    public CollisionSenses CollisionSenses
    {
        get => GenericNotImplementedError<CollisionSenses>.TryGet(collisionSenses, transform.parent.name);
        private set => collisionSenses = value;
    }

    public Combat Combat
    {
        get => GenericNotImplementedError<Combat>.TryGet(combat, transform.parent.name);
        private set => combat = value;
    }

    public Stats Stats
    {
        get => GenericNotImplementedError<Stats>.TryGet(stats, transform.parent.name);
        private set => stats = value;
    }

    public ParticleManager ParticleManager
    {
        get => GenericNotImplementedError<ParticleManager>.TryGet(particleManager, transform.parent.name);
        private set => particleManager = value;
    }

    public Death Death
    {
        get => GenericNotImplementedError<Death>.TryGet(death, transform.parent.name);
        private set => death = value;
    }

    private Movement movement;
    private CollisionSenses collisionSenses;
    private Combat combat;
    private Stats stats;
    private ParticleManager particleManager;
    private Death death;

    private void Awake()
    {
        Movement = GetComponentInChildren<Movement>();
        CollisionSenses = GetComponentInChildren<CollisionSenses>();
        Combat = GetComponentInChildren<Combat>();
        Stats = GetComponentInChildren<Stats>();
        ParticleManager = GetComponentInChildren<ParticleManager>();
        death = GetComponentInChildren<Death>();
    }

    public void LogicUpdate()
    {
        Movement.LogicUpdate();
        Combat.LogicUpdate();
    }
}
