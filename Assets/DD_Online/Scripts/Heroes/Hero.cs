﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



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
    public Team oppositeTeam;
    public int position;
    private TurnStates turnState;
    public GameObject prefab;
    new public BoxCollider2D collider;
    private bool isSelected;
    public GameObject turnDoneIndicator;
    public GameObject turnInProcessIndicator;
    public GameObject selector;
    public bool isTargeted;

    public List<SkillName> skillNames;

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

        foreach (SkillName skillName in skillNames)
        {
            Skill skill = SkillContainer.getSkill(heroClass, skillName);
            skill.setOwner(this);
            skillList.Add(skill);
        }
        addSkipTurnSkill();

        if (team == Team.Left)
            oppositeTeam = Team.Right;
        else if (team == Team.Right)
            oppositeTeam = Team.Left;
    }

    private void addSkipTurnSkill()
    {
        Skill skill = new Skill();
        skill.setOwner(this);
        skill.skillName = SkillName.SkipTurn;
        skill.usePositions = new List<int>() { 1,2,3,4 };
        skillList.Add(skill);
    }



    private void instantiateSkills()
    {
        int index = 0;

        if (turnState == TurnStates.InProcess)
        {
            foreach (Skill skill in skillList)
            {
                skill.instantiate(SceneElementsContainer.skillTransforms[(int)team][index++].position);
                skill.enable(checkSkillUsePosition(skill));
            }
        }
        else
        {
            foreach (Skill skill in skillList)
            {
                if (skill.skillName != SkillName.SkipTurn)
                {
                    skill.instantiate(SceneElementsContainer.skillTransforms[(int)team][index++].position);
                    skill.enable(false);
                }
            }
        }
    }

    private bool checkSkillUsePosition(Skill skill)
    {
        //if (heroClass == HeroClass.Priest)
        //{
        //    Debug.Log("Skill: " + skill.skillName + ";   this.position: " + position);
        //    foreach (int position in skill.usePositions)
        //        Debug.Log("pos: " + position);
        //}

        foreach (int position in skill.usePositions)
        {
            if (position == this.position)
                return true;
        }

        return false;
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


    public void setTargeted(bool flag)
    {
        isTargeted = flag;
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