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
        disableSkillSelector();
        battle.currentHero = updateTurnStates(battle.heroes, battle.currentHero);

        foreach (Hero hero in battle.heroes)
            hero.setSelected(false);

        battle.currentHero.setSelected(true);


        battle.battleState = new BattleState_PlayerTurn(battle);
    }




    private void disableSkillSelector()
    {
        foreach (Hero hero in battle.heroes)
        {
            foreach (Skill skill in hero.skillList)
            {
                if (skill.IsSelected == true)
                {
                    battle.selectSkill(skill, false);
                    return;
                }
            }
        }
    }

    private Hero updateTurnStates(List<Hero> heroList, Hero currentHero)
    {
        if (heroList.Count == 0)
        {
            Debug.Log("selectHero | heroList.Count == 0");
            return null;
        }

        if (currentHero != null && currentHero.TurnState == TurnStates.InProcess)
            currentHero.TurnState = TurnStates.Done;

        Hero nextHero = new Hero();
        int speed = -100;

        foreach (Hero hero in heroList)
        {
            if (hero.TurnState == TurnStates.Undone)
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
            startNewRound(heroList);
            nextHero = updateTurnStates(heroList, currentHero);
        }
        else nextHero.TurnState = TurnStates.InProcess;

        return nextHero;
    }

    private void startNewRound(List<Hero> heroList)
    {
        foreach (Hero hero in heroList)
            hero.TurnState = TurnStates.Undone;
    }

}
