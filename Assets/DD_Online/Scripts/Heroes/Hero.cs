using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeroClass { Defender, Priest, Hunter, Berserk, Fairy, Mage }
public enum TurnStates { Done, Undone, InProcess }

// TODO: Delete MonoBehaviour Inheritance
public class Hero : MonoBehaviour
{
    public enum Team { Left, Right }

    public int pureDamage;
    public int pureHealth;
    public int pureSpeed;

    public HeroClass heroClass;
    public int damage;
    public int health;
    public int speed;

    public int ownerID;
    public Team team;
    public int position;
    private TurnStates turnState;
    public GameObject prefab;
    new public BoxCollider2D collider;
    public bool isSelected;
    public GameObject turnStateSelector;
    public GameObject pointerSelector;

    public List<SkillContainer.SkillName> skillNames;

    [HideInInspector]
    public List<Skill> skillList;

    public TurnStates TurnState
    {
        get
        {
            return turnState;
        }
        set
        {
            turnState = value;
            setTurnStateSelector(turnState);
        }
    }

    private void Awake()
    {
        TurnState = TurnStates.Undone;

        foreach (SkillContainer.SkillName skillName in skillNames)
        {
            Skill skill = SkillContainer.getSkill(heroClass, skillName);
            skill.setOwner(this);
            skillList.Add(skill);
        }
    }



    public void destroySkills()
    {
        foreach (Skill skill in skillList)
            skill.destroy();
    }

    public void enableSkills(bool flag)
    {
        foreach (Skill skill in skillList)
            skill.enable(flag);
    }



    public void createPointerSelector(Vector3 position)
    {
        if (pointerSelector == null)
        {
            pointerSelector = MonoBehaviour.Instantiate(Resources.Load("Prefabs/PointerSelectorPrefab") as GameObject,
                   position, Quaternion.identity) as GameObject;
        }
    }

    public void enablePointerSelector(bool flag)
    {
        if (pointerSelector != null)
        {
            pointerSelector.GetComponent<SpriteRenderer>().enabled = flag;
            isSelected = flag;
        }
    }



    private void setTurnStateSelector(TurnStates turnState)
    {
        if (turnStateSelector != null)
        {
            if (TurnState == TurnStates.Undone)
                turnStateSelector.GetComponent<SpriteRenderer>().color = Color.black;
            else if (TurnState == TurnStates.Done)
                turnStateSelector.GetComponent<SpriteRenderer>().color = Color.red;
            else if (TurnState == TurnStates.InProcess)
                turnStateSelector.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }

    public void createTurnStateSelector(Vector3 position)
    {
        if (turnStateSelector == null)
        {
            turnStateSelector = MonoBehaviour.Instantiate(Resources.Load("Prefabs/TurnStateSelectorPrefab") as GameObject,
                    position, Quaternion.identity) as GameObject;

            TurnState = TurnStates.Undone;
        }
    }



    public void instantiate(Vector3 position)
    {
        if (prefab == null)
        {
            prefab = MonoBehaviour.Instantiate(Resources.Load("Prefabs/Heroes/Prefab_" + heroClass.ToString()) as GameObject,
                    position, Quaternion.identity) as GameObject;
            collider = prefab.GetComponent<BoxCollider2D>();
        }
    }

    public void setCollider(BoxCollider2D collider)
    {
        this.collider = collider;
    }




    void UseSkill(Skill skill)
    {

    }

    //List<Skill> GetSkills()
    //{
    //    return skillList;
    //}
}
