using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.TextCore;

public class Player : MonoBehaviour
{
    private Animator _animator;

    private bool _maskOnFace = false;

    public event Action<bool> MaskState; 
    private bool _isEating; 

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        EatAnimationManager.OnAnimationEnd += HandleAnimationSequence;
    }

    private void Update()
    {
        if (Inputs.Instance.MaskOn.IsPressed() && !_isEating && !_maskOnFace)
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
            return;
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
    
    private void Eat()
    {
        EatIn();
        Debug.Log("StopEating");
    }
    
    private void EatIn()
    {
        Debug.Log("Eating");
        _isEating = true;
        _animator.Play("EatCameraDown");
    }

    private void HandleAnimationSequence(string animationName)
    {
        switch (animationName)
        {
            case "EatCameraDown":
                _animator.Play("EatIn");
                break; 
            case "EatIn":
                _animator.Play("EatOut");
                break; 
            case "EatOut":
                _animator.Play("EatCameraUp");
                break; 
            case "EatCameraUp":
                _isEating = false;
                break;
        }
    }
}
