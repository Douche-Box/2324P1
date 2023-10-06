using UnityEngine;
public class CharFallState : CharBaseState
{
    public CharFallState(CharStateMachine currentContext, CharStateFactory charachterStateFactory) : base(currentContext, charachterStateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        InitializeSubState();
        Ctx.MoveMultiplier = Ctx.AirSpeed;
    }

    public override void ExitState() { }

    #region MonoBehaveiours

    public override void UpdateState()
    {
        Ctx.Movement = Ctx.CurrentMovement.normalized;
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
        if (Ctx.IsGrounded)
        {
            SwitchState(Factory.Grounded());
        }
        if (Ctx.IsSloped)
        {
            SwitchState(Factory.Sloped());
        }
        if (Ctx.IsWalled && !(Ctx.WallLeft && Ctx.CurrentMovementInput.x > 0) && !(Ctx.WallRight && Ctx.CurrentMovementInput.x < 0) && Ctx.IsMove)
        {
            SwitchState(Factory.Walled());
        }
        if (Ctx.IsGrappled && Ctx.IsShoot)
        {
            SwitchState(Factory.Grappled());
        }
    }
}