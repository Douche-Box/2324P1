using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
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
        HandleJump();
        // Debug.Log("Jump State Enter");
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
            // Debug.Log("jump idle");
            SetSubState(Factory.Idle());
        }
        else if (Ctx.IsMove && !Ctx.IsRun)
        {
            // Debug.Log("jump Walk");
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
            // Debug.Log("Jump > Grounded");
            SwitchState(Factory.Grounded());
        }
        else if (!Ctx.IsGrounded && Ctx.IsJumpTime == 0)
        {
            // Debug.Log("Jump > Fall");
            SwitchState(Factory.Fall());
        }
    }
    // fix jump
    void HandleJump()
    {
        Ctx.Rb.velocity = new Vector3(Ctx.Rb.velocity.x, 0f, Ctx.Rb.velocity.z);

        Ctx.Rb.AddForce(0, Ctx.JumpForce, 0, ForceMode.Impulse);

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
