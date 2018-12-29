using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillSelector : MonoBehaviour, IPointerClickHandler
{
    private SpriteRenderer border;
    private bool selected = false;

    private void Start()
    {
        border = GetComponentInChildren<SpriteRenderer>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

    private void Enable(bool flag)
    {
        border.enabled = flag;
    }



    void Damage(int target, int value)
    {
        Debug.Log("Damage");
        Debug.Log("target: " + target + ",  value: " + value);
    }

    void Heal(int target, int value)
    {
        Debug.Log("Heal");
        Debug.Log("target: " + target + ",  value: " + value);
    }

    void Stun(int target, int value)
    {
        Debug.Log("Stun");
        Debug.Log("target: " + target + ",  value: " + value);
    }
}
