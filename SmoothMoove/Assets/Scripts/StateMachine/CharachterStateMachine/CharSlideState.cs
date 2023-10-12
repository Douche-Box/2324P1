using UnityEngine;

public class CharSlideState : CharBaseState
{
    public CharSlideState(CharStateMachine currentContext, CharStateFactory charachterStateFactory) : base(currentContext, charachterStateFactory) { }

    public override void EnterState()
    {
        Ctx.DesiredMoveForce = Ctx.SlideSpeed;

        Ctx.PlayerObj.localScale = new Vector3(Ctx.PlayerObj.localScale.x, Ctx.SlideYScale, Ctx.PlayerObj.localScale.z);

        Ctx.Rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
    }

    public override void ExitState()
    {
        Ctx.PlayerObj.localScale = new Vector3(Ctx.PlayerObj.localScale.x, Ctx.StartYScale, Ctx.PlayerObj.localScale.z);
    }

    #region MonoBehaveiours

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void FixedUpdateState()
    {
        SlidingMovement();
    }

    public override void LateUpdateState() { }

    #endregion

    public override void InitializeSubState() { }

    public override void CheckSwitchStates()
    {
        if (!Ctx.IsMove)
        {
            SwitchState(Factory.Idle());
        }
        else if (Ctx.IsMove && !Ctx.IsSlide)
        {
            SwitchState(Factory.Walk());
        }
    }

    private void SlidingMovement()
    {
        if (Ctx.IsSloped && Ctx.Rb.velocity.y < 0.1f)
        {
            Ctx.DesiredMoveForce = Ctx.SlopeSlideSpeed;
        }
        else
        {
            Ctx.DesiredMoveForce = Ctx.SlideSpeed;
        }

        if (!Ctx.IsSloped || Ctx.Rb.velocity.y > -0.1f)
        {
            Ctx.Rb.AddForce(Ctx.CurrentMovement.normalized * Ctx.SlideForce, ForceMode.Force);
        }
        else
        {
            Ctx.Rb.AddForce(Ctx.GetSlopeMoveDirection(Ctx.CurrentMovement.normalized) * Ctx.SlideForce, ForceMode.Force);
        }
    }
}
