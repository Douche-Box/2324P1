using UnityEngine;

public class CharSlideState : CharBaseState
{
    public CharSlideState(CharStateMachine currentContext, CharStateFactory charachterStateFactory) : base(currentContext, charachterStateFactory) { }

    public override void EnterState()
    {
        Ctx.DesiredMoveForce = Ctx.SlideSpeed;

        Ctx.IsSliding = true;

        if (Ctx.MoveForce < Ctx.SlideSpeed)
        {
            Ctx.MoveForce = Ctx.SlideSpeed;
        }

        Ctx.PlayerAnimator.SetBool("Sliding", true);
    }

    public override void ExitState()
    {
        Ctx.IsSliding = false;

        Ctx.PlayerAnimator.SetBool("Sliding", false);
    }

    #region MonoBehaveiours

    public override void UpdateState()
    {
        if (!Ctx.IsSloped || Ctx.Rb.velocity.y > 0.1f)
        {
            Debug.Log("SLOWER");
            Ctx.DesiredMoveForce = Ctx.LowestSlideSpeed;
        }
    }

    public override void FixedUpdateState()
    {
        SlidingMovement();
        CheckSwitchStates();
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
            Debug.Log("slide > walk 1");
            SwitchState(Factory.Walk());
        }
        else if (Ctx.IsMove && Ctx.IsSlide && Ctx.IsWalled)
        {
            Debug.Log("slide > walk 2");

            SwitchState(Factory.Walk());
        }
        else if (Ctx.MoveForce <= Ctx.LowestSlideSpeed)
        {
            Debug.Log("slide > walk 3");
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
            Ctx.DesiredMoveForce = Ctx.LowestSlideSpeed;
        }

        Ctx.Rb.AddForce(Ctx.Movement * Ctx.MoveForce * 10f * Ctx.MoveMultiplier, ForceMode.Force);
    }
}
