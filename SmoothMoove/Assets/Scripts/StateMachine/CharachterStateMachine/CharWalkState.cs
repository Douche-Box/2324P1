using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CharWalkState : CharBaseState
{
    public CharWalkState(CharStateMachine currentContext, CharStateFactory charachterStateFactory) : base(currentContext, charachterStateFactory) { }

    public override void EnterState()
    {
        // Debug.Log("Walk State Enter");
    }

    public override void ExitState() { }

    #region MonoBehaveiours

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void LateUpdateState()
    {

    }

    public override void FixedUpdateState()
    {
        Ctx.MoveForce = 7.49f;

        Ctx.CurrentMovement = Ctx.Orientation.forward * Ctx.CurrentMovementInput.y + Ctx.Orientation.right * Ctx.CurrentMovementInput.x;

        if (Ctx.IsSloped)
        {
            Ctx.Rb.AddForce(Ctx.GetSlopeMoveDirection() * Ctx.MoveForce * 20f, ForceMode.Force);
        }
        else
        {
            Ctx.Rb.AddForce(Ctx.CurrentMovement * Ctx.MoveForce * 10f, ForceMode.Force);
        }
    }

    #endregion

    public override void InitializeSubState() { }

    public override void CheckSwitchStates()
    {
        if (!Ctx.IsMove)
        {
            SwitchState(Factory.Idle());
        }
        else if (Ctx.IsMove && Ctx.IsSlide)
        {
            SwitchState(Factory.Slide());
        }
    }
}
