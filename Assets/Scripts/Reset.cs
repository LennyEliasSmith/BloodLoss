using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    public static Action CallReset;

    public void ResetAll() => CallReset?.Invoke();
}
