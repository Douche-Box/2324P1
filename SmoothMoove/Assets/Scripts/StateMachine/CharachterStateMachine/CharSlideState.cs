using UnityEngine;

public class CharSlideState : CharBaseState
{
    public CharSlideState(CharStateMachine currentContext, CharStateFactory charachterStateFactory) : base(currentContext, charachterStateFactory) { }

    public override void EnterState()
    {
        Ctx.DesiredMoveForce = Ctx.SlideSpeed;
        Ctx.MoveForce = Ctx.SlideSpeed;

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
        else if (Ctx.IsMove && !Ctx.IsSlide || Ctx.IsMove && Ctx.IsSlide && Ctx.IsWalled)
        {
            SwitchState(Factory.Walk());
        }
    }

    private void SlidingMovement()
    {
        Ctx.Rb.AddForce(Ctx.Movement * Ctx.SlideForce, ForceMode.Force);
    }
}
