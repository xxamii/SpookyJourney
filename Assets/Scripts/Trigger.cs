using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public Action<Collider2D> OnTrigger;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameState.Instance.State == GameState.States.GameOn)
        {
            OnTrigger?.Invoke(other);
        }
    }
}
