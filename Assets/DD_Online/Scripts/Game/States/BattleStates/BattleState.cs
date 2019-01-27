using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleState
{
    public GameState_Battle battle;

    public Skill selectedSkill;
    public Hero targetHero;
    public GameObject skillSelector;
    public List<GameObject> effectIndicators;
    public List<Hero> heroes;
    public Hero currentHero;
    public Skill hoveredSkill;



    public BattleState(GameState_Battle battle, BattleState state = null)
    {
        this.battle = battle;
        if (state != null)
        {
            this.selectedSkill = state.selectedSkill;
            this.targetHero = state.targetHero;
            this.skillSelector = state.skillSelector;
            this.heroes = state.heroes;
            this.currentHero = state.currentHero;
            this.effectIndicators = state.effectIndicators;
            this.hoveredSkill = state.hoveredSkill;
        }
    }

    

    public virtual void execute() {}
    public virtual void showSkillHover()
    {
        Skill skill = getSkillByRaycast();
        if (skill != null)
        {
            if (hoveredSkill != skill)
            {
                onStartHoverSkill(skill);
                hoveredSkill = skill;
            }
        }
        else
        {
            if (hoveredSkill != null)
            {
                onEndHoverSkill();
                hoveredSkill = null;
            }
        }
    }


    private void onStartHoverSkill(Skill skill)
    {
        destroySkillHover();
        createSkillHower(skill);
    }

    private void onEndHoverSkill()
    {
        destroySkillHover();
    }



    protected void createSkillHower(Skill skill)
    {
        if (skill != null)
            skill.instantiateHover();
    }

    protected void destroySkillHover()
    {
        if (hoveredSkill != null)
            hoveredSkill.destroyHover();
    }





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
                selectedSkill = skill;
                foreach (Skill s in skill.owner.skillList)
                    s.setSelected(false);
            }
            else if (enableFlag == false)
                selectedSkill = null;

            skill.setSelected(enableFlag);
            setSkillSelector(skill, enableFlag);
            setEffectIndicators(skill, enableFlag);
        }
    }

    private void setSkillSelector(Skill skill, bool flag)
    {
        skillSelector.transform.position = skill.prefab.transform.position;
        skillSelector.GetComponent<SpriteRenderer>().enabled = flag;
    }

    private void setEffectIndicators(Skill skill, bool enableFlag = true)
    {
        destroyEffectIndicators();
        foreach (Hero hero in heroes)
            hero.setTargeted(false);

        if (enableFlag == true)
        {
            if (skill.skillName == SkillName.SkipTurn)
                return;

            string prefabPath = "";
            Hero.Team targetTeam = Hero.Team.Left;

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

            if (skill.targetPositions.Count > 0)
            {
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
            // self targeted skill
            else
            {
                GameObject prefab = MonoBehaviour.Instantiate(Resources.Load(prefabPath) as GameObject,
                       SceneElementsContainer.turnStateIndicatorTransforms[(int)currentHero.team][currentHero.position - 1].position + Vector3.up * 0.7f,
                       Quaternion.identity) as GameObject;
                effectIndicators.Add(prefab);

                currentHero.setTargeted(true);
            }
        }
    }

    private void destroyEffectIndicators()
    {
        if (effectIndicators != null)
        {
            foreach (GameObject prefab in effectIndicators)
                GameObject.Destroy(prefab);
        }
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



    protected Skill getSkillByRaycast()
    {
        BoxCollider2D collider = (BoxCollider2D)checkHitCollider();
        if (collider != null)
        {
            foreach (Hero hero in heroes)
            {
                if (hero.IsSelected == true)
                {
                    foreach (Skill skill in hero.skillList)
                    {
                        if (skill.collider == collider)
                            return skill;
                    }
                }
            }
        }

        return null;
    }

    protected Hero getHeroByRaycast()
    {
        BoxCollider2D collider = (BoxCollider2D)checkHitCollider();
        if (collider != null)
        {
            foreach (Hero hero in heroes)
            {
                if (hero.collider == collider)
                    return hero;
            }
        }

        return null;
    }

    private Collider2D checkHitCollider()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

        if (hit.collider != null)
            return hit.collider;

        return null;
    }
}