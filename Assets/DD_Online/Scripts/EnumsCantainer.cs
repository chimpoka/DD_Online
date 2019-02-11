using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Team { Default = -1, Left = 0, Right = 1}
public enum HeroClass { Default, Defender, Priest, Hunter, Berserk, Fairy, Mage }
public enum SkillName { Default, Priest1, Priest2, Priest3, Berserk1, Berserk2, Berserk3, Fairy1, Fairy2, Fairy3, Mage1, Mage2, Mage3, SkipTurn }
public enum SkillType { Default, Attack, Heal, Buff, Debuff, Bleed, Poison, Stun }
public enum BuffEffect { Speed, Attack, Armor }
public enum TargetTeam { Default, Allied, Enemy }
public enum TurnStates { Done, Undone, InProcess }

public class EnumsCantainer { }
