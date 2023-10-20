using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CharSlideState : CharBaseState
{
    public CharSlideState(CharStateMachine currentContext, CharStateFactory charachterStateFactory) : base(currentContext, charachterStateFactory) { }

    public override void EnterState()
    {
        Ctx.DesiredMoveForce = Ctx.SlideSpeed;

        Ctx.IsSliding = true;

        Ctx.Colliders[0].enabled = false;
        Ctx.Colliders[1].enabled = true;

        if (Ctx.MoveForce < Ctx.SlideSpeed)
        {
            Ctx.MoveForce = Ctx.SlideSpeed;
        }

        Ctx.PlayerAnimator.SetBool("Sliding", true);


    }

    public override void ExitState()
    {
        Ctx.IsSliding = false;

        Ctx.Colliders[0].enabled = true;
        Ctx.Colliders[1].enabled = false;
        Ctx.PlayerAnimator.SetBool("Sliding", false);
    }

    #region MonoBehaveiours

    public override void UpdateState() { }

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
        else if (Ctx.IsMove && Ctx.IsJumping)
        {
            SwitchState(Factory.Walk());
        }
        else if (Ctx.IsMove && !Ctx.IsSlide)
        {
            SwitchState(Factory.Walk());
        }
        else if (Ctx.IsMove && Ctx.IsSlide && Ctx.IsWalled)
        {
            SwitchState(Factory.Walk());
        }
        else if (Ctx.MoveForce <= Ctx.LowestSlideSpeed)
        {
            SwitchState(Factory.Walk());
        }
    }

    private void SlidingMovement()
    {
        Debug.Log(Ctx.IsSloped);
        if (Ctx.IsSloped && Ctx.Rb.velocity.y < 0.1f)
        {
            Ctx.DesiredMoveForce = Ctx.SlopeSlideSpeed;
        }
        else if (!Ctx.IsSloped && Ctx.IsGrounded || Ctx.Rb.velocity.y > 0.1f && Ctx.IsGrounded)
        {
            Ctx.DesiredMoveForce = Ctx.LowestSlideSpeed;
        }

        Ctx.Rb.AddForce(Ctx.Movement * Ctx.MoveForce * 10f * Ctx.MoveMultiplier, ForceMode.Force);
    }
}
