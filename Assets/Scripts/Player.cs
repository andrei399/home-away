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
        EatAnimationManager.OnEatingAnimationEnd += StopEating;
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

        if (Inputs.Instance.Eat.IsPressed())
        {
            Debug.Log($"isEating {_isEating}");
            Debug.Log($"mask on face { _maskOnFace }");
            if (!_isEating && !_maskOnFace)
            {
                Eat();
            }
        }
        // if (Inputs.Instance.Eat.IsPressed() && !_isEating && !_maskOnFace)
        // {
        //     Eat();
        // }
        
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
        _isEating = true;
        Debug.Log("StartEating");
        _animator.Play("EatFull", 0, 0f);
    }

    private void StopEating()
    {
        Debug.Log("StopEating");
        _isEating = false;
        Debug.Log($"_isEating = {_isEating}");
    }
}
