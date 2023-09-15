using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CharWalkState : CharBaseState
{
    public CharWalkState(CharStateMachine currentContext, CharStateFactory charachterStateFactory) : base(currentContext, charachterStateFactory) { }

    public override void EnterState()
    {
        Debug.Log("Walk State Enter");
    }

    public override void ExitState() { }

    #region MonoBehaveiours

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void FixedUpdateState()
    {
        Debug.Log(Ctx.Orientation.forward);
        Debug.Log(Ctx.Orientation.right);
        Ctx.CurrentMovement = Ctx.Orientation.forward * Ctx.CurrentMovementInput.y + Ctx.Orientation.right * Ctx.CurrentMovementInput.x;
        // new Vector3(Ctx.CurrentMovementInput.x, 0f, Ctx.CurrentMovementInput.y).normalized;


        Ctx.Rb.AddForce(Ctx.CurrentMovement * Ctx.MoveForce * 10, ForceMode.Force);
    }

    #endregion

    public override void InitializeSubState() { }

    public override void CheckSwitchStates()
    {
        if (!Ctx.IsMove)
        {
            Debug.Log("Walk > Idle");
            SwitchState(Factory.Idle());
        }
        else if (Ctx.IsMove && Ctx.IsRun)
        {
            Debug.Log("Walk > Run");
            SwitchState(Factory.Run());
        }
    }
}
