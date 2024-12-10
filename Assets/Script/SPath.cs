using UnityEngine;
using System.Collections;

public sealed class SPath  {

    public const string LEVEL = "LEVEL";
    public const string TAG_PLAYER = "Player";
    public const string TAG_ONGBOM = "ongbom";
    public const string TAG_ONGCHICH = "ongchich";
    public const string TAG_ONGMAT = "ongmat";
    public const string TAG_BOSS = "boss";
    public const string TAG_BULLET_BOSS = "bulletBoss";
    public const string TAG_BULLET = "bullet";
    public const string TAG_ITEM_PROTECT = "iprotect";
    public const string TAG_ITEM_BULLET = "ibullet";
    public const string TAG_ITEM_DEFENSE = "idef";
    public const string TAG_ITEM_HP = "iHp";
    public const string TAG_MAIN_OPTION = "mainOption";
    public const string TAG_AUDIO = "audio";
    public const string TAG_PANEL_GAMEOVER = "panelGameOver";
    public const string TAG_HET_DAN = "lbHetDan";
    public const string TAG_PANEL_GAME_LOSE = "panelGameLose";
    public const string TAG_GAME_SCREEN = "gamescreen";

    //Screen
    public const string S_GAME = "Game";
    public const string S_GAMESCREEN = "GameScreen";
    public const string S_GAMEWIN = "GameWin";
    public const string S_GAMELOSE = "GameLose";
    public const string S_GAMEMENU = "GameMenu";
    public const string S_VIDEOLOADING = "VideoLoading";
    public const string S_LOADING = "Loading";

    //Pref
    public const string PREF_HP = "hp";
    public const string PREF_GUN = "gun";
    public const string PREF_GUN1 = "gun1";
    public const string PREF_GUN2 = "gun2";
    public const string PREF_GUN3 = "gun3";
    public const string PREF_BULLET = "bullet";
    public const string PREF_SOUND = "sound";
    public const string PREF_MUSIC = "music";
    public const string PREF_VOLUME_SOUND = "volume_sound";
    public const string PREF_VOLUME_MUSIC = "volume_music";
    public const string PREF_SCORE = "score";
    public const string PREF_COUNT_ONGBOM = "count_ongbom";
    public const string PREF_BONUS_ONGBOM = "bonus_ongbom";
    public const string PREF_COUNT_ONGCHICH = "count_ongchich";
    public const string PREF_BONUS_ONGCHICH = "bonus_ongchich";
    public const string PREF_SCORE_CURRENT = "score_current";
    public const string PREF_SAVE_GAME = "save_game";
    public const string PREF_SAVE_COIN = "coin_game";
    public const string PREF_SAVE_RUBY = "ruby_game";

    //Integer
    public const int NUMBER_BULLET_MIN = 20;
    public const int NUMBER_BULLET_MAX = 50;
    public const int NUMBER_BULLET_MAX_2 = 100;
    public const int TIME_DEFENSE_MAX = 10;
    public static int MAX_HP_PLAYER = 5;

    public static int Score_current;
    public static int COUNT_SCORE_BONUS_3;
    public static int CHECK_BOSS = 0;
    

    //Daikon
    public const string MOVE_IN_PAUSE = "movein";
    public const string MOVE_OUT_PAUSE = "moveout";
    public const string COLOR_IN_PAUSE = "colorin";
    public const string COLOR_OUT_PAUSE = "colorout";


    //Shop
    public static bool Check_Coin = true; // show coin, hide ruby
    public const int ITEM_1 = 200;
    public const int ITEM_2 = 400;
    public const int ITEM_3 = 600;
    public const int ITEM_4 = 800;
    public const int TOTAL_BULLET_ITEM_1 = 800;
    public const int TOTAL_BULLET_ITEM_2 = 600;
    public const int TOTAL_BULLET_ITEM_3 = 400;
    public const int TOTAL_BULLET_ITEM_4 = 200;

}
