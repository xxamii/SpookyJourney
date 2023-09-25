using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearReducer : MonoBehaviour
{
    private void Start()
    {
        FearMeter.Instance.DecreaseFear(999f);
    }
}
