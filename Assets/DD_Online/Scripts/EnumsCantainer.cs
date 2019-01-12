using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HeroClass { Defender, Priest, Hunter, Berserk, Fairy, Mage }
public enum SkillName { Smite, BattleHeal, StunningBlow, SkipTurn }
public enum SkillType { Default, Attack, Heal, Buff, Debuff, Bleed, Poison, Stun }
public enum BuffEffect { Speed, Attack, Armor }
public enum TargetTeam { Default, Allied, Enemy }
public enum TurnStates { Done, Undone, InProcess }

public class EnumsCantainer { }
