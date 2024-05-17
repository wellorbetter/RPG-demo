using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [Header("Attack details")]
    public Vector2[] attackMovement;
    public float counterAttackDuration = .2f;

    public bool isBusy { get; private set; }
    
    [Header("Move info")]
    public float moveSpeed = 12;
    public float jumpForce = 12;

    [Header("Dash info")]
    public float dashSpeed;
    public float dashDuration;
    public float dashDir { get; private set;}

    #region States
    public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallSlide { get; private set; }
    public PlayerWallJump wallJump { get; private set; } 
    public PlayerPrimaryAttackState primaryAttack { get; private set; }
    public PlayerCounterAttackState counterAttackState { get; private set; }
    public PlayerAimSwordState aimSwordState { get; private set; }
    public PlayerCatchSwordState catchSwordState { get; private set; }
    #endregion

    // Awake����Start֮ǰ����
    protected override void Awake()
    {
        base.Awake();
        // ����ÿ����Ҷ��ԣ�����Ψһ��Ӧһ��״̬��
        // ÿ����һ��кܶ಻ͬ��״̬������Idle��Move��Attack�ȵ�
        // ������Щ״̬��ĳ��ʱ��ֻ����һ���ǵ�ǰ״̬������״̬֮����л�����״̬�������Ƶ�
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState  = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlide = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJump = new PlayerWallJump(this, stateMachine, "Jump");
        primaryAttack = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        counterAttackState = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");
        aimSwordState = new PlayerAimSwordState(this, stateMachine, "AimSword");
        catchSwordState = new PlayerCatchSwordState(this, stateMachine, "CatchSword"); 
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        CheckForDashInput();
    }

    // IEnumerator��һ���ӿڣ����������ǿ�����һ����������;��ͣ��Ȼ����ĳ�������¼���ִ��
    // �����BusyFor����һ��Э�̣������������������ĳ��ʱ������޷����в���
    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;
        // ��������������ǵȴ�_seconds��֮���ټ���ִ��
        yield return new WaitForSeconds(_seconds);
        isBusy = false;
    }
    
    public void AnimationTrigger() => stateMachine.currentState.AnimationTrigger();

    // ר�ŵģ���Ϊ��Ϸϣ������ʲôʱ�򶼿��Խ��г��
    private void CheckForDashInput()
    {
        
        // ֮ǰ��ÿ����ٵ��߼���д��player�����
        // ����д����SkillManager�����Dash_Skillʵ��dash����
        // ֻ��Ҫ���dash�����Ƿ���ü���
        if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.dash.CanUseSkill())
        {
            // ��Ϊ��ϣ�����е�ʱ�򶼿��Գ�̣����Թ���״̬Ҳ���Գ��
            // ��������ϣ���ó��������˺������ǹ�����ʱ����û���ƶ��ģ�Ҳ����˵���ʱ��
            // ����ܿ����泯���ˣ�����������Ҫ����ָ�������̵ķ���Ȼ��Ĭ�ϵĻ�facingDir
            dashDir = Input.GetAxisRaw("Horizontal");
            if (dashDir == 0) dashDir = facingDir;
            stateMachine.ChangeState(dashState);
        }
    }
    
    
    
}
