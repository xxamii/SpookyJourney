using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreUpdate : MonoBehaviour
{
    public static System.Action CallInPreUpdate;

    private void Update()
    {
        CallInPreUpdate?.Invoke();
    }
}
