using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.TextCore;

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
        if (!GameManager.Instance.GameRunning)
        {
            return;
        }

        if (Inputs.Instance.MaskOn.IsPressed() && !FoodManager.Instance.isEating && !_maskOnFace)
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

        if (Inputs.Instance.Eat.IsPressed() && FoodManager.Instance.canEatAgain && !_maskOnFace)
        {
            FoodManager.Instance.Eat();
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
