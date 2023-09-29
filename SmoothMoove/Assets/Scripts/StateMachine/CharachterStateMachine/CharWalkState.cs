using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CharWalkState : CharBaseState
{
    public CharWalkState(CharStateMachine currentContext, CharStateFactory charachterStateFactory) : base(currentContext, charachterStateFactory) { }

    public override void EnterState()
    {
        Debug.Log("enter walk");
        Ctx.DesiredMoveForce = Ctx.MoveSpeed;
    }

    public override void ExitState() { }

    #region MonoBehaveiours

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void LateUpdateState() { }

    public override void FixedUpdateState()
    {
        WalkMovement();
    }

    #endregion

    public override void InitializeSubState() { }

    public override void CheckSwitchStates()
    {
        if (!Ctx.IsMove)
        {
            SwitchState(Factory.Idle());
        }
        if (Ctx.IsSlide && Ctx.IsMove)
        {
            SwitchState(Factory.Slide());
        }
    }

    private void WalkMovement()
    {
        if (!Ctx.IsSloped && Ctx.IsGrounded)
        {
            Ctx.Rb.AddForce(Ctx.CurrentMovement.normalized * Ctx.MoveForce * 10f, ForceMode.Force);
        }
        else if (Ctx.IsSloped && Ctx.Rb.velocity.y > 0)
        {
            Ctx.Rb.AddForce(Ctx.GetSlopeMoveDirection(Ctx.CurrentMovement.normalized) * Ctx.MoveForce * 20f, ForceMode.Force);
        }
        else if (!Ctx.IsGrounded)
        {
            Ctx.Rb.AddForce(Ctx.GetSlopeMoveDirection(Ctx.CurrentMovement.normalized) * Ctx.MoveForce * 10f * Ctx.AirMultiplier, ForceMode.Force);
        }
    }

}