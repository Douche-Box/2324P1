using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharJumpState : CharBaseState
{
    public CharJumpState(CharStateMachine currentContext, CharStateFactory charachterStateFactory) : base(currentContext, charachterStateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        InitializeSubState();
        HandleJumpDirection();
        Debug.Log("Jump State Enter");
    }

    public override void ExitState() { }

    #region MonoBehaveiours

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void FixedUpdateState()
    {
        HandleJumpTime();
    }

    #endregion

    public override void InitializeSubState() 
    {
        if (!Ctx.IsMove && !Ctx.IsRun)
        {
            Debug.Log("jump idle");
            SetSubState(Factory.Idle());
        }
        else if (Ctx.IsMove && !Ctx.IsRun)
        {
            Debug.Log("jump Walk");
            SetSubState(Factory.Walk());
        }
        else
        {
            SetSubState(Factory.Run());
        }
    }

    public override void CheckSwitchStates()
    {
        if (Ctx.IsGrounded && Ctx.IsJumpTime == 0)
        {
            //This one is a lil' funky still
            Debug.Log("Jump > Grounded");
            SwitchState(Factory.Grounded());
        }
        else if (!Ctx.IsGrounded && Ctx.IsJumpTime == 0)
        {
            Debug.Log("Jump > Fall");
            SwitchState(Factory.Fall());
        }
    }

    Vector2 JumpDirection;
    void HandleJumpDirection()
    {
        JumpDirection = Ctx.CurrentMovementInput;

        // fix jump
    }

    void HandleJumpTime()
    {
        if (Ctx.IsJumpTime > 0)
        {
            Ctx.IsJumpTime -= Time.deltaTime;
        }
        else
        {
            Ctx.IsJumpTime = 0;
        }
    }
}
