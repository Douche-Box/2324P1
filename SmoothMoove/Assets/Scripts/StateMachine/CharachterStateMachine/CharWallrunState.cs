using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharWallrunState : CharBaseState
{
    public CharWallrunState(CharStateMachine currentContext, CharStateFactory charachterStateFactory) : base(currentContext, charachterStateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        Ctx.DesiredMoveForce = Ctx.WallRunSpeed;
    }

    public override void ExitState() { }

    #region MonoBehaveiours

    public override void UpdateState()
    {
        Ctx.Movement = Ctx.CurrentMovement.normalized;
    }

    public override void LateUpdateState() { }

    public override void FixedUpdateState()
    {
        CheckSwitchStates();
        WallRunMovement();
    }

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
    }

    public override void CheckSwitchStates()
    {
        // do wallrun check if chek is checked and stuff and shit
        if (!Ctx.IsGrounded && !Ctx.IsSloped)
        {
            SwitchState(Factory.Fall());
        }
        if (Ctx.IsJump)
        {
            SwitchState(Factory.Jump());
        }
        if (Ctx.IsSloped)
        {
            SwitchState(Factory.Sloped());
        }
    }

    private void WallRunMovement()
    {
        Ctx.Rb.useGravity = false;
        Ctx.Rb.velocity = new Vector3(Ctx.Rb.velocity.x, 0f, Ctx.Rb.velocity.z);

        // Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;
    }
}
