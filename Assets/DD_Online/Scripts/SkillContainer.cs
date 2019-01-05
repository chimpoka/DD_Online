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
    public enum SkillName { Smite, BattleHeal, StunningBlow, SkipTurn }

    public static Skill getSkill(HeroClass heroClass, SkillName skillName)
    {
        string SkillsJson = "Assets/DD_Online/Scripts/Skills.txt";
        var file = JSON.Parse(File.ReadAllText(SkillsJson));
        Skill skill = parseJson(file[heroClass.ToString()][skillName.ToString()], skillName);

        return skill;
    }

    private static Skill parseJson(JSONNode json, SkillName skillName)
    {
        SkillType type = SkillType.Default;
        TargetTeam targetTeam = TargetTeam.Default;
        List<int> targetPositions;
        List<int> usePositions;
        int value;
        
        // type
        foreach (SkillType _type in (SkillType[])Enum.GetValues(typeof(SkillType)))
        {
            if (_type.ToString() == json["Type"]) type = _type;
        }

        // targetTeam
        foreach (TargetTeam _targetTeam in (TargetTeam[])Enum.GetValues(typeof(TargetTeam)))
        {
            if (_targetTeam.ToString() == json["Team"]) targetTeam = _targetTeam;
        }

        //targetPositions
        string s = json["TargetPositions"].ToString();
        targetPositions = new List<int>();
        for (int i = 0; i < s.Length; ++i)
        {
            if (s[i] == '1')
                targetPositions.Add(i + 1);
        }

        //usePositions
        s = json["TargetPositions"].ToString();
        usePositions = new List<int>();
        for (int i = 0; i < s.Length; ++i)
        {
            if (s[i] == '1')
                usePositions.Add(i + 1);
        }

        // value
        value = json["Value"];



        Skill skill = new Skill
        {
            skillName = skillName,
            type = type,
            targetTeam = targetTeam,
            targetPositions = targetPositions,
            usePositions = usePositions,
            value = value
        };

        return skill;
    }
}
