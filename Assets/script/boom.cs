using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boom : MonoBehaviour
{
    private void Destroying()
    {
        Destroy(gameObject,0.5f);
    }
}
