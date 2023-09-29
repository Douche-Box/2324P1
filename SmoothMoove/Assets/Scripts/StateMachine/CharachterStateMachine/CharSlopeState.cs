using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CharSlopeState : CharBaseState
{
    public CharSlopeState(CharStateMachine currentContext, CharStateFactory charachterStateFactory) : base(currentContext, charachterStateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        InitializeSubState();
        Ctx.Rb.useGravity = false;
    }

    public override void ExitState()
    {
        Ctx.Rb.useGravity = true;
    }

    #region MonoBehaveiours

    public override void UpdateState()
    {
        if (Ctx.Rb.velocity.y > 0 || Ctx.Rb.velocity.y > 0)
        {
            Ctx.Rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }
    }

    public override void FixedUpdateState()
    {
        CheckSwitchStates();
    }

    public override void LateUpdateState() { }

    #endregion

    public override void InitializeSubState()
    {
        if (!Ctx.IsMove)
        {
            SetSubState(Factory.Idle());
        }
        if (Ctx.IsMove && !Ctx.IsSlide)
        {
            SetSubState(Factory.Walk());
        }
        if (Ctx.IsSlide && Ctx.IsMove)
        {
            SetSubState(Factory.Slide());
        }
    }

    public override void CheckSwitchStates()
    {
        if (Ctx.IsGrounded && !Ctx.IsSloped)
        {
            SwitchState(Factory.Grounded());
        }
        if (!Ctx.IsGrounded && !Ctx.IsSloped)
        {
            SwitchState(Factory.Fall());
        }
        if (Ctx.IsJump)
        {
            SwitchState(Factory.Jump());
        }
    }
}
