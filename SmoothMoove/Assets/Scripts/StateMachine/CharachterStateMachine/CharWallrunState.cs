using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        Ctx.IsWallRunning = true;
        Ctx.Rb.useGravity = false;

        // NEEDS WORK NEEDS WORK NEEDS WORK NEEDS WORK
        // Ctx.JumpDirection = Ctx.GetWallJumpDirection(Ctx.WallNormal);
        // NEEDS WORK NEEDS WORK NEEDS WORK NEEDS WORK

        Ctx.Rb.velocity = new Vector3(Ctx.Rb.velocity.x, 0f, Ctx.Rb.velocity.z);

        if (Ctx.CurrentWall != null)
        {
            Ctx.CurrentWall = Ctx.WallLeft ? Ctx.LeftWallHit.transform : Ctx.WallRight ? Ctx.RightWallHit.transform : null;
            if (Ctx.CurrentWall != Ctx.PreviousWall)
            {
                Ctx.WallClingTime = Ctx.MaxWallClingTime;
                Ctx.CanStartWallTimer = true;
            }
            else
            {
                Ctx.CanStartWallTimer = true;
            }
        }
        else
        {
            Debug.Log("DEEZ");
            Ctx.CurrentWall = Ctx.WallLeft ? Ctx.LeftWallHit.transform : Ctx.WallRight ? Ctx.RightWallHit.transform : null;
            Ctx.CanStartWallTimer = true;
        }

    }

    public override void ExitState()
    {
        Debug.Log("exit wall run");
        Ctx.IsWallRunning = false;
        Ctx.Rb.useGravity = true;
        Ctx.PreviousWall = Ctx.CurrentWall;
    }

    #region MonoBehaveiours

    public override void UpdateState()
    {
        if (Ctx.MoveForce <= 7)
        {
            Ctx.Rb.AddForce(Vector3.down, ForceMode.Force);
        }
        if (Ctx.WallClingTime <= 0)
        {
            Ctx.DesiredMoveForce = 0f;
            Ctx.MoveMultiplier = 0.5f;
        }
        else
        {
            Ctx.DesiredMoveForce = Ctx.WallRunSpeed;
        }
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
        if (!Ctx.IsGrounded && !Ctx.IsMove || !Ctx.IsGrounded && !Ctx.IsWalled)
        {
            SwitchState(Factory.Fall());
        }
        else if (Ctx.IsGrounded && !Ctx.IsMove || Ctx.IsGrounded && !Ctx.IsWalled)
        {
            SwitchState(Factory.Grounded());
        }
        else if (Ctx.IsJump)
        {
            SwitchState(Factory.Jump());
        }
    }

    public override void CheckSwitchStates()
    {
        if (Ctx.IsMove)
        {
            SetSubState(Factory.Walk());
        }
        else if (Ctx.IsSlide && !Ctx.IsWallRunning)
        {
            SetSubState(Factory.Slide());
        }
    }

    private void WallRunMovement()
    {
        Ctx.Rb.velocity = new Vector3(Ctx.Rb.velocity.x, 0f, Ctx.Rb.velocity.z);


        if ((Ctx.PlayerObj.forward - Ctx.WallForward).magnitude > (Ctx.PlayerObj.forward - -Ctx.WallForward).magnitude)
        {
            Ctx.WallForward = new Vector3(-Ctx.WallForward.x, -Ctx.WallForward.y, -Ctx.WallForward.z);
        }

        Ctx.Rb.AddForce(-Ctx.WallNormal.normalized * 225, ForceMode.Force);

        Debug.DrawRay(Ctx.transform.position, Ctx.WallForward, Color.green);

        Ctx.Movement = new Vector3(Ctx.WallForward.x, 0, Ctx.WallForward.z).normalized;
        Ctx.MoveMultiplier = 2f;

        // upwards/downwards force
        // if (upwardsRunning)
        //     rb.velocity = new Vector3(rb.velocity.x, wallClimbSpeed, rb.velocity.z);
        // if (downwardsRunning)
        //     rb.velocity = new Vector3(rb.velocity.x, -wallClimbSpeed, rb.velocity.z);


        // ?? Maybe use this ?? if (!(Ctx.WallLeft && Ctx.CurrentMovementInput.x > 0) && !(Ctx.WallRight && Ctx.CurrentMovementInput.x < 0))
    }
}