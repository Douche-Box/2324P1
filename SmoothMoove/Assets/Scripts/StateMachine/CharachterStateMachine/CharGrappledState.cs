using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharGrappledState : CharBaseState
{
    public CharGrappledState(CharStateMachine currentContext, CharStateFactory charachterStateFactory) : base(currentContext, charachterStateFactory)
    {
        IsRootState = true;
    }
    private SpringJoint joint;

    public override void EnterState()
    {
        Ctx.DesiredMoveForce = Ctx.GrappleSpeed;
    }

    public override void ExitState()
    {
        Debug.Log("exit grapple");
    }

    #region MonoBehaveiours

    public override void UpdateState()
    {

    }

    public override void LateUpdateState() { }

    public override void FixedUpdateState()
    {
        CheckSwitchStates();

    }

    #endregion

    public override void InitializeSubState()
    {

    }

    public override void CheckSwitchStates()
    {

    }

    private void GrappleHandler()
    {


        Vector3 grapplePoint = Ctx.GrappleHit.point;
        joint = Ctx.transform.gameObject.AddComponent<SpringJoint>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = grapplePoint;

        float distanceFromPoint = Vector3.Distance(Ctx.transform.position, grapplePoint);

        //The distance grapple will try to keep from grapple point. 
        joint.maxDistance = distanceFromPoint * 0.8f;
        joint.minDistance = distanceFromPoint * 0.25f;

        //Adjust these values to fit your game.
        joint.spring = 4.5f;
        joint.damper = 7f;
        joint.massScale = 4.5f;

        //lr.positionCount = 2;
        //currentGrapplePosition = gunTip.position;

    }
}
