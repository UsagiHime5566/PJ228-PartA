using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateTester : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            SignalAdapter.FakeInvoke();
        }
    }
}
