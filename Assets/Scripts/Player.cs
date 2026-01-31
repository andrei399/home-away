using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.TextCore;

public class Player : MonoBehaviour
{
    private Animator _animator;

    private bool _maskOnFace = false;

    public event Action<bool> MaskState; 
    public event bool isEating; 

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        EatAnimationManager.OnEatingAnimationEnd += StopEating;
    }

    private void Update()
    {
        if (Inputs.Instance.MaskOn.IsPressed() && !isEating && !_maskOnFace)
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

        if (Inputs.Instance.Eat.IsPressed() && !_isEating && !_maskOnFace)
        {
            Eat();
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
    
    private void EatIn()
    {
        Debug.Log("Eating");
        _animator.Play("EatIn");
        isEating = true;
    }

    private void EatOut()
    {
        Debug.Log("StopEating");
        _animator.Play("EatOut");
        isEating = false;
    }
}
