using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharWallrunState : CharBaseState
{
    public CharWallrunState(CharStateMachine currentContext, CharStateFactory charachterStateFactory) : base(currentContext, charachterStateFactory)
    {
        IsRootState = true;
    }

    public override void EnterState()
    {
        Debug.Log("Enter wall");
        InitializeSubState();
        Ctx.Rb.useGravity = false;
        Ctx.DesiredMoveForce = Ctx.WallRunSpeed;
    }

    public override void ExitState()
    {
        Debug.Log("exit wall run");
        Ctx.Rb.useGravity = true;
    }

    #region MonoBehaveiours

    public override void UpdateState()
    {

    }

    public override void LateUpdateState() { }

    public override void FixedUpdateState()
    {
        CheckSwitchStates();
        WallRunMovement();
    }

    #endregion

    public override void InitializeSubState()
    {
        if (Ctx.IsMove)
        {
            Debug.Log("walk wall");
            SetSubState(Factory.Walk());
        }
    }

    public override void CheckSwitchStates()
    {
        if (!Ctx.IsMove || !Ctx.IsWalled)
        {
            SwitchState(Factory.Fall());
        }
        if (Ctx.IsJump && Ctx.WallLeft && Ctx.CurrentMovementInput.x > 0 || Ctx.IsJump && Ctx.WallRight && Ctx.CurrentMovementInput.x < 0)
        {
            SwitchState(Factory.Jump());
        }
    }
    // || Ctx.IsJump && !(Ctx.WallRight && Ctx.CurrentMovementInput.x < 0)
    private void WallRunMovement()
    {
        Ctx.Rb.velocity = new Vector3(Ctx.Rb.velocity.x, 0f, Ctx.Rb.velocity.z);



        if ((Ctx.PlayerObj.forward - Ctx.WallForward).magnitude > (Ctx.PlayerObj.forward - -Ctx.WallForward).magnitude)
            Ctx.WallForward = -Ctx.WallForward;

        Debug.DrawRay(Ctx.transform.position, Ctx.WallForward, Color.green);


        // forward force

        Ctx.Movement = Ctx.WallForward;
        Ctx.MoveMultiplier = 2f;

        // Ctx.Rb.AddForce(wallForward * wallRunForce, ForceMode.Force);

        // upwards/downwards force
        // if (upwardsRunning)
        //     rb.velocity = new Vector3(rb.velocity.x, wallClimbSpeed, rb.velocity.z);
        // if (downwardsRunning)
        //     rb.velocity = new Vector3(rb.velocity.x, -wallClimbSpeed, rb.velocity.z);

        // push to wall force
        if (!(Ctx.WallLeft && Ctx.CurrentMovementInput.x > 0) && !(Ctx.WallRight && Ctx.CurrentMovementInput.x < 0))
            Ctx.Rb.AddForce(-Ctx.WallNormal * 100, ForceMode.Force);
    }
}