using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;
using System;

//using System.IO;
//using Newtonsoft.Json;


public class SkillContainer
{
    public enum SkillName { Smite, BattleHeal, StunningBlow }

    public static Skill getSkill(HeroClass heroClass, SkillName skillName)
    {
        string SkillsJson = "Assets/DD_Online/Scripts/Skills.txt";
        var N = JSON.Parse(File.ReadAllText(SkillsJson));

        return parseJson(N[heroClass.ToString()][skillName.ToString()], skillName);
    }

    private static Skill parseJson(JSONNode json, SkillName skillName)
    {
        SkillType type = SkillType.Default;
        Team targetTeam = Team.Default;
        int value;
        
    
        foreach (SkillType _type in (SkillType[])Enum.GetValues(typeof(SkillType)))
        {
            if (_type.ToString() == json["Type"]) type = _type;
        }

        foreach (Team _targetTeam in (Team[])Enum.GetValues(typeof(SkillType)))
        {
            if (_targetTeam.ToString() == json["Team"]) targetTeam = _targetTeam;
        }

        value = json["Value"];


        Skill skill = new Skill
        {
            skillName = skillName,
            type = type,
            targetTeam = targetTeam,
            value = value
        };

        return skill;
    }
}
