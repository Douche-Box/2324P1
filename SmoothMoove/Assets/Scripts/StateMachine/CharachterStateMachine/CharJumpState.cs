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
        Ctx.IsExitingSlope = true;
        HandleJump();
    }

    public override void ExitState() { }

    #region MonoBehaveiours

    public override void UpdateState() { }

    public override void FixedUpdateState()
    {
        CheckSwitchStates();
        HandleJumpTime();
    }

    public override void LateUpdateState() { }

    #endregion

    public override void InitializeSubState()
    {
        if (!Ctx.IsMove)
        {
            SetSubState(Factory.Idle());
        }
        else if (Ctx.IsMove && !Ctx.IsSlide)
        {
            SetSubState(Factory.Walk());
        }
        else if (Ctx.IsMove && Ctx.IsSlide)
        {
            SetSubState(Factory.Slide());
        }
    }

    public override void CheckSwitchStates()
    {
        if (!Ctx.IsGrounded && !Ctx.IsSloped)
        {
            SwitchState(Factory.Fall());
        }
        else if (Ctx.IsGrounded)
        {
            SwitchState(Factory.Grounded());
        }
        else if (Ctx.IsSloped)
        {
            SwitchState(Factory.Sloped());

        }
    }

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
