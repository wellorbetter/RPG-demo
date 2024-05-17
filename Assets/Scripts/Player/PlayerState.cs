using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    // 因为一个玩家会有很多状态，而且可能不同玩家会有不同的状态
    // 所以这里需要指定是哪个玩家的状态，这里的Player就是指定的玩家
    // 但是这里的stateMachine，也许多余了，因为Player里面已经有了一个stateMachine
    // 但是这里的stateMachine是为了方便PlayerState里面的状态之间的切换,所以这里还是需要的
    protected PlayerStateMachine stateMachine;
    protected Player player;
    protected Rigidbody2D rb;

    protected float xInput;
    protected float yInput;
    private string animBoolName;

    protected float stateTimer;
    protected bool triggerCalled;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }
    // 虚函数，就是可以被子类重写的函数，子类可以根据自己的需求重写这个函数
    public virtual void Enter()
    {
        // 进入的时候需要将当前的动画状态设置为true，离开的时候设置为false
        player.anim.SetBool(animBoolName, true);
        rb = player.rb;
        triggerCalled = false;
    }
    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
    }
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        yInput = Input.GetAxisRaw("Vertical");
        xInput = Input.GetAxisRaw("Horizontal");
        player.anim.SetFloat("yVelocity", rb.velocity.y);
    }

    public virtual void AnimationTrigger() => triggerCalled = true;
}
