using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ManagerParameters
{
    // Audio related parameters
    public const string BGM = "bgm";
    public const string EFFECT = "effect";
    public const float COE = 20.0f;

    // Record related parameters
    // kill record
    public const string KILL_RECORD_LEVEL = "kill-record level";
    public const string KILL_RECORD_KILL = "kill-record kill";
    public const string KILL_RECORD_MAX_HEALTH = "kill-record max health";
    public const string KILL_RECORD_ATTACK = "kill-record attack";
    public const string KILL_RECORD_DEFENSE = "kill-record defense";
    public static string[] KILL = {
        KILL_RECORD_KILL,
        KILL_RECORD_LEVEL,
        KILL_RECORD_MAX_HEALTH,
        KILL_RECORD_ATTACK,
        KILL_RECORD_DEFENSE
    };
    // level record
    public const string LEVEL_RECORD_KILL = "level-record kill";    
    public const string LEVEL_RECORD_LEVEL = "level-record level";
    public const string LEVEL_RECORD_MAX_HEALTH = "level-record max health";
    public const string LEVEL_RECORD_ATTACK = "level-record attack";
    public const string LEVEL_RECORD_DEFENSE = "level-record defense";
    public static string[] LEVEL = {
        LEVEL_RECORD_KILL,
        LEVEL_RECORD_LEVEL,
        LEVEL_RECORD_MAX_HEALTH,
        LEVEL_RECORD_ATTACK,
        LEVEL_RECORD_DEFENSE
    };

    // current
    public const string CURRNET_KILL = "current kill";    
    public const string CURRNET_LEVEL = "current level";
    public const string CURRNET_MAX_HEALTH = "current max health";
    public const string CURRNET_ATTACK = "current attack";
    public const string CURRNET_DEFENSE = "current defense";
    public static string[] CURRENT = {
        CURRNET_KILL,
        CURRNET_LEVEL,
        CURRNET_ATTACK,
        CURRNET_DEFENSE,
        CURRNET_MAX_HEALTH,
    };

    public const string BREAK_KILL_RECORD = "break kill record";
    public const string BREAK_LEVEL_RECORD = "break level record";

    // Scene related parameters
    public const string MENU_SCENE = "Menu Scene";
    public const string GAME_SCENE = "PG Test Scene";
    public const string TRANSITION_SCENE = "Transition Scene";
    public const string END_SCENE = "End Scene";
    public const string RECORD_SCENE = "Record Scene";

    // Level related parameters
    public const int MAX_LEVEL = 3;
    // public const string LEVEL = "level"; // which is current_level
}
