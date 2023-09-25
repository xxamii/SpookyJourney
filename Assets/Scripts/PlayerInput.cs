using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public FrameInput Input { get; private set; }

    private void OnEnable()
    {
        PreUpdate.CallInPreUpdate += GatherInput;
    }

    private void OnDisable()
    {
        PreUpdate.CallInPreUpdate -= GatherInput;
    }

    private void GatherInput()
    {
        if (GameState.Instance.State == GameState.States.Pause)
        {
            Input = new FrameInput();
            return;
        }

        Input = new FrameInput
        {
            JumpDown = UnityEngine.Input.GetButtonDown("Jump"),
            JumpUp = UnityEngine.Input.GetButtonUp("Jump"),
            X = UnityEngine.Input.GetAxisRaw("Horizontal"),
            Attack = UnityEngine.Input.GetKeyDown(KeyCode.Z) || UnityEngine.Input.GetMouseButtonDown(0)
        };
    }
}
