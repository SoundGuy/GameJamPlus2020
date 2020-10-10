using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class HealthBar : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Image attackImage;
    [SerializeField] Image defenseImage;

    Animator animator;

    public void UpdateHealth(float hp, float maxHp)
    {
        float normalizedHealth = (maxHp != 0) ? hp / maxHp : 0;
        if (!animator) animator = GetComponent<Animator>();
        animator.SetFloat("Health", normalizedHealth);
        text.text = $"{hp} / {maxHp}";
    }

    public void UpdateBeat(Sprite attackSprite, Sprite defenseSprite)
    {
        attackImage.sprite = attackSprite ? attackSprite : defaultSprite;
        defenseImage.sprite = defenseSprite ? defenseSprite : defaultSprite;
    }
}
