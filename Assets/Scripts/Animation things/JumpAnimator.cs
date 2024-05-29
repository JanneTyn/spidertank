using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class JumpAnimator : MonoBehaviour
{
    [SerializeField] CharacterController character;
    [SerializeField] Rig rig;

    private void FixedUpdate()
    {
        if (!character.isGrounded)
        {
            rig.weight = 0;
        }
        else
        {
            rig.weight = 1;
        }
    }
}
