using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Animator))]
public class HealthBar : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    Animator animator;

    //private void Start()
    //{
    //    animator = GetComponent<Animator>();
    //}

    public void UpdateHealth(float hp, float maxHp)
    {
        float normalizedHealth = (maxHp != 0) ? hp / maxHp : 0;
        if (!animator) animator = GetComponent<Animator>();
        animator.SetFloat("Health", normalizedHealth);
        text.text = $"{hp} / {maxHp}";
    }
}
