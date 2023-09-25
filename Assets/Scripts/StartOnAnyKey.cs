using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartOnAnyKey : MonoBehaviour
{
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneTransitioner.Instance.StartTransitionNext();
        }
    }
}
