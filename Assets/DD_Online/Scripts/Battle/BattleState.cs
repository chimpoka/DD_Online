using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleState
{
    private enum EffectState { Positive, Negative }

    public Battle battle;

    public Skill selectedSkill;
    public GameObject skillSelector;
    public List<GameObject> effectIndicators;

    [HideInInspector]
    public List<Hero> heroes;

    [HideInInspector]
    public Hero currentHero;



    public BattleState(Battle battle, BattleState state = null)
    {
        this.battle = battle;
        if (state != null)
        {
            this.selectedSkill = state.selectedSkill;
            this.skillSelector = state.skillSelector;
            this.heroes = state.heroes;
            this.currentHero = state.currentHero;
            this.effectIndicators = state.effectIndicators;
        }
    }

    

    public virtual void execute() {}



    public void selectHero(Hero hero)
    {
        if (hero != null)
        {
            foreach (Hero h in heroes)
            {
                if (h.team == hero.team)
                    h.setSelected(false);
            }
            hero.setSelected(true);
        }
    }



    public void selectSkill(Skill skill, bool enableFlag = true)
    {
        if (skill != null)
        {
            if (enableFlag == true)
            {
                foreach (Skill s in skill.owner.skillList)
                    s.setSelected(false);
            }

            skill.setSelected(enableFlag);
            setSkillSelector(skill, enableFlag);
            setEffectIndicators(skill, enableFlag);
            //setHerosTargetedBySkill(skill, enableFlag);
        }
    }

    private void setSkillSelector(Skill skill, bool flag)
    {
        skillSelector.transform.position = skill.prefab.transform.position;
        skillSelector.GetComponent<SpriteRenderer>().enabled = flag;
    }

    private void setEffectIndicators(Skill skill, bool enableFlag = true)
    {
        if (enableFlag == true)
        {
            string prefabPath = "";
            Hero.Team targetTeam = Hero.Team.Left;

            if (effectIndicators != null) destroyEffectIndicators();
            else effectIndicators = new List<GameObject>();

            if (skill.targetTeam == TargetTeam.Enemy)
            {
                prefabPath = "Prefabs/EnemySelectorPrefab";
                targetTeam = skill.owner.oppositeTeam;
            }
            else if (skill.targetTeam == TargetTeam.Allied)
            {
                prefabPath = "Prefabs/AlliedSelectorPrefab";
                targetTeam = skill.owner.team;
            }

            foreach (int position in skill.targetPositions)
            {
                GameObject prefab = MonoBehaviour.Instantiate(Resources.Load(prefabPath) as GameObject,
                   SceneElementsContainer.turnStateIndicatorTransforms[(int)targetTeam][position - 1].position + Vector3.up * 0.7f,
                   Quaternion.identity) as GameObject;
                effectIndicators.Add(prefab);

                Hero targetHero = getHero(targetTeam, position);
                if (targetHero != null)
                    targetHero.setTargeted(true);
            }
        }
        else if (enableFlag == false)
        {
            destroyEffectIndicators();
            foreach (Hero hero in heroes)
                hero.setTargeted(false);
        }
    }

    private void destroyEffectIndicators()
    {
        foreach (GameObject prefab in effectIndicators)
            GameObject.Destroy(prefab);

        effectIndicators = new List<GameObject>();
    }

    private Hero getHero(Hero.Team team, int position)
    {
        foreach (Hero hero in heroes)
        {
            if (hero.team == team && hero.position == position)
                return hero;
        }

        return null;
    }

    //private void setHerosTargetedBySkill(Skill skill, bool flag)
    //{
    //    if (flag == true)
    //    {
    //        Hero.Team targetTeam = Hero.Team.Left;

    //        if (skill.targetTeam == TargetTeam.Enemy)
    //            targetTeam = skill.owner.oppositeTeam;
    //        else if (skill.targetTeam == TargetTeam.Allied)
    //            targetTeam = skill.owner.team;

    //        foreach (int position in skill.targetPositions)
    //        {
    //            Hero targetHero = getHero(targetTeam, position);
    //            if (targetHero != null)
    //                targetHero.setTargeted(true);
    //        }
    //    }
    //    else if (flag == false)
    //    {
    //        foreach (Hero hero in heroes)
    //            hero.setTargeted(false);
    //    }
    //}
}