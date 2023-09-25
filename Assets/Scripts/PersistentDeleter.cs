using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentDeleter : MonoBehaviour
{
    private void Start()
    {
        if (GameState.Instance != null)
        {
            Destroy(GameState.Instance.gameObject);
        }
    }
}
