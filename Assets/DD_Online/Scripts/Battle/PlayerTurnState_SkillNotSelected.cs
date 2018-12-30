using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnState_SkillNotSelected : PlayerTurnState
{ 
    public PlayerTurnState_SkillNotSelected(BattleState_PlayerTurn playerTurn)
        : base(playerTurn)
    { }

    public override void selectHero()
    {
        BoxCollider2D collider = (BoxCollider2D)checkHitCollider();
        if (collider != null)
        {
            Hero hero = getSelectedHero(collider);
            onSelectHero(hero);
        }
    }





    private void onSelectHero(Hero hero)
    {
        if (hero != null)
        {
            setSelector(hero);
            setSkills(hero);
        }
    }


    private void setSkills(Hero hero)
    {
        hero.SetSkillImages();
        hero.skillList[1].enable(false);
        hero.skillList[2].removeImage();
    }


    private void setSelector(Hero hero)
    {
        if (playerTurn.battle.heroSelectorPrefabs != null)
        {
            if (playerTurn.battle.heroSelectorPrefabs[(int)hero.team] != null)
            {
                Vector3 selectorPos = playerTurn.battle.selectedHeroPoints[(int)hero.team][hero.position - 1].position;
                playerTurn.battle.heroSelectorPrefabs[(int)hero.team].transform.position = selectorPos;
            }
            else
            {
                playerTurn.battle.heroSelectorPrefabs[(int)hero.team] = MonoBehaviour.Instantiate(Resources.Load("Prefabs/HeroSelectorPrefab") as GameObject,
                playerTurn.battle.selectedHeroPoints[(int)hero.team][hero.position - 1].position, Quaternion.identity) as GameObject;
            }
        }
    }

    private Hero getSelectedHero(BoxCollider2D collider)
    {
        foreach (Hero hero in playerTurn.battle.heroes)
        {
            if (hero.collider == collider)
                return hero;
        }

        return null;
    }


    private Collider2D checkHitCollider()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null)
                return hit.collider;
        }

        return null;
    }


}
