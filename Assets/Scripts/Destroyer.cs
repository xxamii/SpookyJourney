using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public Action<Destroyer> OnDestroyed;

    public void Destroy()
    {
        OnDestroyed?.Invoke(this);
        Destroy(gameObject);
    }
}
