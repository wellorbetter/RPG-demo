using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [Header("Stunned Info")]
    public float stunDuration;
    public Vector2 stunDirection;
    protected bool canBeStunned;
    [SerializeField] protected GameObject counterImage;

    [SerializeField]protected LayerMask whatIsPlayer;
    [Header("Move Info")]
    public float moveSpeed;
    public float IdleTime;
    public float battleTime;

    [Header("Attack Info")]
    public float attackDistance;
    public float attackCooldown;

    [HideInInspector]public float lastTimeAttack;

    public EnemyStateMachine stateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();

    }
    protected override void Start()
    {
        base.Start();
        
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }

    public virtual void OpenCounterAttackWindow()
    {
        canBeStunned = true;
        counterImage.SetActive(true);
    }

    public virtual void CloseCounterAttackWindow()
    {
        canBeStunned = false;
        counterImage.SetActive(false);
    }
    public virtual bool CanBeStunned()
    {
        if (canBeStunned)
        {
            CloseCounterAttackWindow();
            return true;
        }
        return false;
    }
    public virtual void AnimationTrigger() => stateMachine.currentState.AnimationTrigger();
    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, 50, whatIsPlayer);
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));
    }
}
