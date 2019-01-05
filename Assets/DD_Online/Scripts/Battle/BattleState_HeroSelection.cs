using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState_HeroSelection : BattleState
{
    public BattleState_HeroSelection(Battle battle, BattleState state)
        : base(battle, state)
    {}

    public override void execute()
    {
       
        if (selectedSkill != null)
        {
            Debug.Log("Used Skill: " + selectedSkill.skillName + ", " + selectedSkill.owner.heroClass +
                 ", " + selectedSkill.owner.team + ", " + selectedSkill.owner.position);

            selectSkill(selectedSkill, false);
            selectedSkill = null;
        }

        currentHero = updateTurnStates(base.heroes, currentHero);

        foreach (Hero hero in heroes)
            hero.setSelected(false);

        selectHero(currentHero);


        battle.battleState = new BattleState_PlayerTurn(battle, this);
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