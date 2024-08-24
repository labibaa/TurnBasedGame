using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UltimateActionsFactory : ScriptableObject
{
    public GameObject ultimateButton;
    public int actionThreshold;
    public int ultimateRange;
    public GameObject particlePrefab;
    public AudioClip actionSound;
    public abstract IUltimate CreateUltimate();
    public abstract bool IsUltimateEnabled();
}
