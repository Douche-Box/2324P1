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
        Debug.Log("Slope State Enter");
    }

    public override void ExitState()
    {
        Ctx.Rb.useGravity = true;
    }

    #region MonoBehaveiours

    public override void UpdateState()
    {
        CheckSwitchStates();
        if (Ctx.Rb.velocity.y > 0)
        {
            Ctx.Rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        }

    }

    public override void FixedUpdateState()
    {

    }

    #endregion

    public override void InitializeSubState()
    {
        if (!Ctx.IsMove && !Ctx.IsRun)
        {
            Debug.Log("Sloped & Idle");
            SetSubState(Factory.Idle());
        }
        else if (Ctx.IsMove && !Ctx.IsRun)
        {
            Debug.Log("Sloped & Walk");
            SetSubState(Factory.Walk());
        }
        else if (Ctx.IsMove && Ctx.IsRun)
        {
            Debug.Log("Sloped & Run");
            SetSubState(Factory.Run());
        }
    }

    public override void CheckSwitchStates()
    {
        if (Ctx.IsGrounded && !Ctx.IsSloped)
        {
            Debug.Log("Sloped > Grounded");
            SwitchState(Factory.Grounded());
        }
        if (!Ctx.IsGrounded && !Ctx.IsJump)
        {
            Debug.Log("Sloped > Fall");
            SwitchState(Factory.Fall());
        }
        if (Ctx.IsJump)
        {
            Debug.Log("Sloped > Jump");
            SwitchState(Factory.Jump());
        }
    }
}
