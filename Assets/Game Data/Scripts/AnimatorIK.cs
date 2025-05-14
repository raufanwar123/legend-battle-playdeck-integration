using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorIK : MonoBehaviour 
{
    public Transform leftHandPos;
    public Transform rightHandPos;
    //public Transform leftFootPos;
    //public Transform rightFootPos;
    Animator animator;
	void Start ()
    {
        animator = GetComponent<Animator>();
    }



    private void OnAnimatorIK(int layerIndex)
    {
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
        animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);

        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);
        animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1);

        animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandPos.position);
        animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandPos.position);
        //animator.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootPos.position);
        //animator.SetIKPosition(AvatarIKGoal.RightFoot, rightFootPos.position);

        animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandPos.rotation);
        animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandPos.rotation);
        //animator.SetIKRotation(AvatarIKGoal.LeftFoot, leftFootPos.rotation);
        //animator.SetIKRotation(AvatarIKGoal.RightFoot, rightFootPos.rotation);
    }
}
