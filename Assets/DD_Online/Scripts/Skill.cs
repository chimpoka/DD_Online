using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class Skill : IPointerClickHandler
{
    public SkillName skillName;
    //public int damage;
    public Hero owner;
    public SkillType type;
    public int value;
    public TargetTeam targetTeam;
    public bool isActive;
    public GameObject prefab;
    public GameObject hoverPrefab;
    public BoxCollider2D collider;
    public GameObject selector;
    public bool isSelected;

    public bool multipleTarget;
    public List<int> usePositions;
    public List<int> targetPositions;

    public List<Effect> effects;

    public bool IsSelected
    {
        get
        {
            return isSelected;
        }
        set
        {
            isSelected = value;
            if (isSelected == true)
                startAnimation();
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked!");
    }


    public void setOwner(Hero hero)
    {
        owner = hero;
    }

    public void instantiate(Vector3 position)
    {
        if (prefab == null)
        {
            prefab = MonoBehaviour.Instantiate(Resources.Load("Prefabs/Skills/Prefab_" + skillName.ToString()) as GameObject,
                    position, Quaternion.identity) as GameObject;
            collider = prefab.GetComponent<BoxCollider2D>();
        }
    }

    public void destroy()
    {
        if (prefab != null)
        {
            GameObject.Destroy(prefab);
            prefab = null;
            collider = null;
        }
    }

    public void enable(bool flag)
    {
        if (prefab != null)
        {
            isActive = flag;
            SpriteRenderer[] sprites = prefab.GetComponentsInChildren<SpriteRenderer>();

            foreach (SpriteRenderer sprite in sprites)
            {
                Color color = new Color();
                color.r = sprite.color.r;
                color.g = sprite.color.g;
                color.b = sprite.color.b;
                if (flag == true)
                    color.a = 1f;
                else
                    color.a = 0.25f;

                sprite.color = color;
            }
        }
    }



    public void setSelected(bool flag)
    {
        IsSelected = flag;
    }

    private void startAnimation()
    {
        Animator animator = prefab.GetComponent<Animator>();
        if (animator != null) animator.SetTrigger("Select");
    }


    public void instantiateHover()
    {
        string fileName = "Prefabs/SkillInfo/Prefab_" + skillName.ToString();
        Object obj = Resources.Load(fileName);
        if (obj == null)
            Debug.LogError("No file '" + fileName + "'!");
        else
            hoverPrefab = MonoBehaviour.Instantiate((obj) as GameObject,
                prefab.transform.position - Vector3.up * 0.4f, Quaternion.identity) as GameObject;
    }

    public void destroyHover()
    {
        if (hoverPrefab != null)
        {
            GameObject.Destroy(hoverPrefab);
            hoverPrefab = null;
        }
    }
}