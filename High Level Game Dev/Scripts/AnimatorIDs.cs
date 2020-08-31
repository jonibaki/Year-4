using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorIDs : MonoBehaviour
{
    public int Idle;
    public int Locomotion;
    public int Speed;
    public int TurnSpeed;

    void Awake()
    {
        Idle = Animator.StringToHash("Idle");
        Locomotion = Animator.StringToHash("Locomotion");
        Speed = Animator.StringToHash("Speed");
        TurnSpeed = Animator.StringToHash("TurnSpeed");
    }
}
