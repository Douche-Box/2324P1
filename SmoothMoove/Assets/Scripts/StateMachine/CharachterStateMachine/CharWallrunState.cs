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
        if (!Ctx.IsMove)
        {
            SetSubState(Factory.Idle());
        }
        if (Ctx.IsMove && !Ctx.IsSlide)
        {
            SetSubState(Factory.Walk());
        }
    }

    public override void CheckSwitchStates()
    {
        // do wallrun check if chek is checked and stuff and shit
        if (!Ctx.IsGrounded && !Ctx.IsWalled || !Ctx.IsMove && !Ctx.IsGrounded)
        {
            SwitchState(Factory.Fall());
        }
    }

    private void WallRunMovement()
    {
        // MAKE WALL RUN CHECK FOR IF YOU ARE LOOKING IN THE DIRECTION OF THE WALL
        // MAKE WALL RUN CHECK FOR IF YOU ARE LOOKING IN THE DIRECTION OF THE WALL
        Ctx.Rb.velocity = new Vector3(Ctx.Rb.velocity.x, 0f, Ctx.Rb.velocity.z);

        Vector3 wallNormal = Ctx.WallRight ? Ctx.RightWallHit.normal : Ctx.LeftWallHit.normal;

        Vector3 wallForward = Vector3.Cross(wallNormal, Ctx.transform.up);


        if ((Ctx.Orientation.forward - wallForward).magnitude > (Ctx.Orientation.forward - -wallForward).magnitude)
            wallForward = -wallForward;

        Debug.Log(wallForward.ToString());
        // us wallrunforce
        Debug.DrawRay(Ctx.transform.position, wallForward, Color.green);

        Ctx.Movement = wallForward;

        Ctx.MoveMultiplier = 2f;
        // MAKE WALL RUN CHECK FOR IF YOU ARE LOOKING IN THE DIRECTION OF THE WALL
        // MAKE WALL RUN CHECK FOR IF YOU ARE LOOKING IN THE DIRECTION OF THE WALL

    }
}