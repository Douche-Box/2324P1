using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CharSlideState : CharBaseState
{
    public CharSlideState(CharStateMachine currentContext, CharStateFactory charachterStateFactory) : base(currentContext, charachterStateFactory) { }

    public override void EnterState()
    {
        Debug.Log("Slide State Enter");
        Ctx.IsSliding = true;
        Ctx.playerScale = Ctx.PlayerObj.localScale.y;
        Ctx.PlayerObj.localScale = new Vector3(Ctx.PlayerObj.localScale.x, Ctx.minScale, Ctx.PlayerObj.localScale.z);
        Ctx.Rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        Ctx.SlideTime = Ctx.MaxSlideTime;
    }

    public override void ExitState()
    {
        Debug.Log("Slide State Exit");
        Ctx.IsSliding = false;
        Ctx.PlayerObj.localScale = new Vector3(Ctx.PlayerObj.localScale.x, Ctx.playerScale, Ctx.PlayerObj.localScale.z);
    }

    #region MonoBehaveiours

    public override void UpdateState()
    {

    }

    public override void FixedUpdateState()
    {
        CheckSwitchStates();
        SlidingMovement();
    }

    public override void LateUpdateState()
    {

    }

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
        if (Ctx.IsMove && Ctx.SlideTime <= 0)
        {
            SwitchState(Factory.Walk());
        }
        if (!Ctx.IsMove && Ctx.SlideTime <= 0)
        {
            SwitchState(Factory.Idle());
        }
    }

    private void SlidingMovement()
    {
        Ctx.CurrentMovement = Ctx.Orientation.forward * Ctx.CurrentMovementInput.y + Ctx.Orientation.right * Ctx.CurrentMovementInput.x;

        if (Ctx.IsSloped)
        {
            Ctx.Rb.AddForce(Ctx.GetSlopeMoveDirection(Ctx.CurrentMovement) * Ctx.MoveForce * 20f, ForceMode.Force);
        }
        else
        {
            Ctx.Rb.AddForce(Ctx.CurrentMovement * Ctx.MoveForce * 10f, ForceMode.Force);
            Ctx.SlideTime -= Time.deltaTime;
        }

    }
}
