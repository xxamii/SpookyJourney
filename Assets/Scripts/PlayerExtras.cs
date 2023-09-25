using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct FrameInput
{
    public float X;
    public bool JumpDown;
    public bool JumpUp;
    public bool Attack;
}

[Serializable]
public class RectangleArea
{
    public Vector2 TopLeft;
    public Vector2 BottomRight;
}

public enum EnemyState
{
    Stop,
    Fly
}

public struct EnemyFrameInput
{
    public EnemyState State;
    public Vector2 MovementDirection;
    public float MovementMultiplier;
}

public interface IHealth
{
    public void TakeDamage(float amount);
}

public interface IDamageable
{
    public void TakeDamage(float amount, Vector2 from);
}

public interface IEnemyInput
{
    public EnemyFrameInput Input { get; }

    public void Move();
    public void Stop();
}

public struct RayRange
{
    public RayRange(float x1, float y1, float x2, float y2, Vector2 dir)
    {
        Start = new Vector2(x1, y1);
        End = new Vector2(x2, y2);
        Dir = dir;
    }

    public readonly Vector2 Start, End, Dir;
}