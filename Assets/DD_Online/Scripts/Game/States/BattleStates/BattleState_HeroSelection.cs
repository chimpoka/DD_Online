using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState_HeroSelection : BattleState
{
    public BattleState_HeroSelection(GameState_Battle battle, BattleState state)
        : base(battle, state)
    {}

    public override void execute()
    {
        if (selectedSkill != null)
            selectSkill(selectedSkill, false);

        foreach (Hero hero in heroes)
            hero.setSelected(false);

        currentHero = updateTurnStates(base.heroes, currentHero);
        selectHero(currentHero);
        battle.battleState = new BattleState_TurnBeforeSkillSelected(battle, this);
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
            Debug.Log("New Round");
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