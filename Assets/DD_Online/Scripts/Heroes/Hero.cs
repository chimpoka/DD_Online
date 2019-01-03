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
    private bool isSelected;
    public GameObject turnDoneIndicator;
    public GameObject turnInProcessIndicator;
    public GameObject selector;

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
            setTurnDoneIndicator(turnState);
            setTurnInProcessIndicator(turnState);
        }
    }

    public bool IsSelected
    {
        get
        {
            return isSelected;
        }
        set
        {
            isSelected = value;

            setSelector(isSelected);
            if (isSelected == true)
                instantiateSkills();
            else
                destroySkills();
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



    private void instantiateSkills()
    {
        int index = 0;
        foreach (Skill skill in skillList)
            skill.instantiate(SceneElementsContainer.skillTransforms[(int)team][index++].position);

        if (turnState == TurnStates.InProcess)
            enableSkills(true);
        else
            enableSkills(false);
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



    public void setSelected(bool flag)
    {
        IsSelected = flag;
    }

    private void setSelector(bool flag)
    {
        if (selector != null)
        {
            if (flag == true)
                selector.transform.position = prefab.transform.position + Vector3.up * 2.5f;

            selector.GetComponent<SpriteRenderer>().enabled = flag;
        }
    }



    private void setTurnDoneIndicator(TurnStates turnState)
    {
        if (TurnState == TurnStates.Undone)
            enableTurnDoneIndicator(false);
        else if (TurnState == TurnStates.Done)
            enableTurnDoneIndicator(true);
        else if (TurnState == TurnStates.InProcess)
            enableTurnDoneIndicator(false);
    }

    private void enableTurnDoneIndicator(bool flag)
    {
        if (turnDoneIndicator == null && flag == true)
        {
            turnDoneIndicator = MonoBehaviour.Instantiate(Resources.Load("Prefabs/TurnDoneIndicatorPrefab") as GameObject,
                    SceneElementsContainer.turnStateIndicatorTransforms[(int)team][position - 1].position + Vector3.right * 0.5f, Quaternion.identity) as GameObject;
        }
        else if (turnDoneIndicator != null)
        {
            turnDoneIndicator.GetComponent<SpriteRenderer>().enabled = flag;
        }
    }

    private void setTurnInProcessIndicator(TurnStates turnState)
    {
        if (TurnState == TurnStates.Undone)
            enableTurnInProcessIndicator(false);
        else if (TurnState == TurnStates.Done)
            enableTurnInProcessIndicator(false);
        else if (TurnState == TurnStates.InProcess)
            enableTurnInProcessIndicator(true);
    }

    private void enableTurnInProcessIndicator(bool flag)
    {
        if (turnInProcessIndicator != null)
        {
            if (flag == true)
                turnInProcessIndicator.transform.position = 
                    SceneElementsContainer.turnStateIndicatorTransforms[(int)team][position - 1].position;

            turnInProcessIndicator.GetComponent<SpriteRenderer>().enabled = flag;
        }
    }



    public void instantiate()
    {
        if (prefab == null)
        {
            prefab = MonoBehaviour.Instantiate(Resources.Load("Prefabs/Heroes/Prefab_" + heroClass.ToString()) as GameObject,
                    SceneElementsContainer.heroTransforms[(int)team][position - 1].position, Quaternion.identity) as GameObject;

            if (team == Team.Right)
            {
                Vector3 rotation = new Vector3(0, 180, 0);
                prefab.transform.eulerAngles = rotation;
            }

            collider = SceneElementsContainer.heroColliders[(int)team][position - 1];
        }
    }




    //void UseSkill(Skill skill)
    //{

    //}

    //List<Skill> GetSkills()
    //{
    //    return skillList;
    //}
}