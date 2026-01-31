using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator _animator;

    private bool _maskOnFace = false;

    public event Action<bool> MaskState; 

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Inputs.Instance.MaskOn.IsPressed() && !_maskOnFace)
        {
            _maskOnFace = true;
            MaskOn();
            return;
        }

        if (!Inputs.Instance.MaskOn.IsPressed() && _maskOnFace)
        {
            _maskOnFace = false;
            MaskOff();
        }
        
    }

    private void MaskOn()
    {
        Debug.Log("Mask ON!");
        _animator.Play("MaskON");
        MaskState?.Invoke(true);
    }

    private void MaskOff()
    {
        Debug.Log("Mask Off!");
        _animator.Play("MaskOFF");
        MaskState?.Invoke(false);
    }
}
