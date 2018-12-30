using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState_HeroSelection : BattleState
{
    public BattleState_HeroSelection(Battle battle)
        : base(battle)
    {}

    public override void execute()
    {
        battle.currentHero = selectHero(battle.heroes, battle.currentHero);
        createHeroSelectorPrefabs(battle.currentHero);
        foreach (Hero hero in battle.heroes) HeroTurnStateSelector.setSelector(hero);

        //int index = 0;
        //foreach (Skill skill in battle.currentHero.skillList)
        //{
        //    //int team = -1;
        //    //if (battle.currentHero.ownerID == battle.playerID1) team = 0;
        //    //else if (battle.currentHero.ownerID == battle.playerID2) team = 1;

        //    //battle.skillPrefabs[(int)battle.currentHero.team][index++].GetComponent<SpriteRenderer>().sprite =
        //    //    SkillSpritesContainer.getSprite("Skill_" + skill.skillName.ToString());

        //    skill.setImage();
        //}

        battle.currentHero.SetSkillImages();

        battle.battleState = new BattleState_PlayerTurn(battle);
    }





    private void createHeroSelectorPrefabs(Hero currentHero)
    {
        if (battle.heroSelectorPrefabs != null)
        {
            foreach (GameObject prefab in battle.heroSelectorPrefabs)
                MonoBehaviour.Destroy(prefab);
        }

        battle.heroSelectorPrefabs = new GameObject[2];
        battle.heroSelectorPrefabs[(int)currentHero.team] = MonoBehaviour.Instantiate(Resources.Load("Prefabs/HeroSelectorPrefab") as GameObject,
                    battle.selectedHeroPoints[(int)currentHero.team][currentHero.position - 1].position, Quaternion.identity) as GameObject;
    }

    private Hero selectHero(List<Hero> heroList, Hero currentHero)
    {
        if (heroList.Count == 0)
        {
            Debug.Log("selectHero | heroList.Count == 0");
            return null;
        }

        if (currentHero != null && currentHero.turnState == TurnState.InProcess)
            currentHero.turnState = TurnState.Done;

        Hero nextHero = new Hero();
        int speed = -100;

        foreach (Hero hero in heroList)
        {
            if (hero.turnState == TurnState.Undone)
            {
                if (hero.speed >= speed)
                {
                    nextHero = hero;
                    speed = hero.speed;
                }
            }
        }

        if (nextHero == null)
        {
            Debug.Log("NULL");
            startNewRound(heroList, currentHero);
            nextHero = selectHero(heroList, currentHero);
        }
        else nextHero.turnState = TurnState.InProcess;

        return nextHero;
    }



    private void startNewRound(List<Hero> heroList, Hero currentHero)
    {
        foreach (Hero hero in heroList)
        {
            hero.turnState = TurnState.Undone;
            //  currentHero = null;
        }
    }

}
