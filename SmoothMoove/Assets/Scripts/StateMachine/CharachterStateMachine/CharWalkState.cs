using System.Collections;
using System.Collections.Generic;
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
        Ctx.CurrentMovement = new Vector3(Ctx.CurrentMovementInput.x, 0f, Ctx.CurrentMovementInput.y).normalized;
        Ctx.Rb.AddForce(Ctx.CurrentMovement * Ctx.MoveForce * 10, ForceMode.Force);

        float tragetAngle = Mathf.Atan2(Ctx.CurrentMovement.x, Ctx.CurrentMovement.z) * Mathf.Rad2Deg;

        Ctx.Rotation.rotation = Quaternion.Euler(0f, tragetAngle, 0f);

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
