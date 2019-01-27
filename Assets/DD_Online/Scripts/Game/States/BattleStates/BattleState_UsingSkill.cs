using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState_UsingSkill : BattleState
{
    public BattleState_UsingSkill(GameState_Battle battle, BattleState state)
      : base(battle, state)
    { }

    public override void execute()
    {
        if (selectedSkill.skillName == SkillName.SkipTurn)
        {

        }
        else
        {
            foreach (Effect effect in selectedSkill.effects)
            {
                List<Hero> targetHeroes = new List<Hero>();

                if (effect.selfTargeted == true) // must be first
                    targetHeroes.Add(currentHero);
                else if (selectedSkill.multipleTarget == true)
                {
                    foreach (Hero hero in heroes)
                    {
                        if (hero.isTargeted)
                            targetHeroes.Add(hero);
                    }
                }
                else
                    targetHeroes.Add(targetHero);


                foreach (Hero hero in targetHeroes)
                {
                    // TODO: rework with handlers and lambdas
                    if (effect.type == SkillType.Attack)
                        onAttack(hero, effect);

                    else if (effect.type == SkillType.Heal)
                        onHeal(hero, effect);

                    else if (effect.type == SkillType.Buff)
                        onBuff(hero, effect);

                    else if (effect.type == SkillType.Debuff)
                        onDebuff(hero, effect);

                    else if (effect.type == SkillType.Bleed)
                        onBleed(hero, effect);

                    else if (effect.type == SkillType.Poison)
                        onPoison(hero, effect);

                    else if (effect.type == SkillType.Stun)
                        onStun(hero, effect);
                }
            }
        }

        battle.battleState = new BattleState_HeroSelection(battle, this);
    }


    private void onAttack(Hero target, Effect effect)
    {
        target.Health -= effect.value;
        //if (hero.health <= 0)
        //hero.die()
    }

    private void onHeal(Hero target, Effect effect)
    {
        target.Health += effect.value;
    }

    private void onBuff(Hero target, Effect effect)
    {

    }

    private void onDebuff(Hero target, Effect effect)
    {

    }

    private void onBleed(Hero target, Effect effect)
    {

    }

    private void onPoison(Hero target, Effect effect)
    {

    }

    private void onStun(Hero target, Effect effect)
    {

    }
}