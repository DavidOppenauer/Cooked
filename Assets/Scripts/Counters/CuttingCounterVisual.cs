using System;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    private const string CUT = "Cut";
    private CuttingCounter cuttingCounter;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        cuttingCounter = GetComponentInParent<CuttingCounter>();
    }

    private void Start()
    {
        cuttingCounter.OnCut += CuttingCounterCounter_OnPlayerCutObject;
    }

    private void CuttingCounterCounter_OnPlayerCutObject(object sender, EventArgs e)
    {
        animator.SetTrigger(CUT);
    }
}
