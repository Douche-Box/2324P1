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
    }

    private void WallRunMovement()
    {
        Ctx.Rb.velocity = new Vector3(Ctx.Rb.velocity.x, 0f, Ctx.Rb.velocity.z);

        Vector3 wallNormal = Ctx.WallRight ? Ctx.RightWallHit.normal : Ctx.LeftWallHit.normal;

        Vector3 wallForward = Vector3.Cross(wallNormal, Ctx.transform.up);

        if ((Ctx.PlayerObj.forward - wallForward).magnitude > (Ctx.PlayerObj.forward - -wallForward).magnitude)
            wallForward = -wallForward;

        Debug.DrawRay(Ctx.transform.position, wallForward, Color.green);


        // forward force

        Ctx.Movement = wallForward;
        Ctx.MoveMultiplier = 2f;

        // Ctx.Rb.AddForce(wallForward * wallRunForce, ForceMode.Force);

        // upwards/downwards force
        // if (upwardsRunning)
        //     rb.velocity = new Vector3(rb.velocity.x, wallClimbSpeed, rb.velocity.z);
        // if (downwardsRunning)
        //     rb.velocity = new Vector3(rb.velocity.x, -wallClimbSpeed, rb.velocity.z);

        // push to wall force
        if (!(Ctx.WallLeft && Ctx.CurrentMovementInput.x > 0) && !(Ctx.WallRight && Ctx.CurrentMovementInput.x < 0))
            Ctx.Rb.AddForce(-wallNormal * 100, ForceMode.Force);
    }
}