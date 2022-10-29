using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    public PlayerData playerData;

    public float maxBlood;
    private float currentBlood;

    public float bloodLossModifier;
    public float intenseBloodLossModifier;
}
