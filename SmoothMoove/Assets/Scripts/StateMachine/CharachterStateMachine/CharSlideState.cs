using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharSlideState : CharBaseState
{
    public CharSlideState(CharStateMachine currentContext, CharStateFactory charachterStateFactory) : base(currentContext, charachterStateFactory) { }

    public override void EnterState()
    {
        Debug.Log("enter slide");
        Ctx.DesiredMoveForce = Ctx.SlideSpeed;
    }

    public override void ExitState()
    {

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
        if (Ctx.IsMove && !Ctx.IsSlide)
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
