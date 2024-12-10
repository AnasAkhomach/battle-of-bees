using UnityEngine;
using System.Collections;

public class MyPref
{
    public static void load()
    {
        //load level
        if (!PlayerPrefs.HasKey(SPath.LEVEL))
            PlayerPrefs.SetInt(SPath.LEVEL, 1);

        //Load Score for first time
        if (!PlayerPrefs.HasKey(SPath.PREF_SCORE))
            PlayerPrefs.SetInt(SPath.PREF_SCORE, 0);

        //Load Sound and Music for first time
        if (!PlayerPrefs.HasKey(SPath.PREF_SOUND))
            setSound(true);
        if (!PlayerPrefs.HasKey(SPath.PREF_VOLUME_SOUND))
            setVolumeSound(1f);
        if (!PlayerPrefs.HasKey(SPath.PREF_MUSIC))
            setMusic(true);
        if (!PlayerPrefs.HasKey(SPath.PREF_VOLUME_MUSIC))
            setVolumeMusic(1f);

        //load bullet default
        if (!PlayerPrefs.HasKey(SPath.PREF_BULLET + 1))
            setDan(1, 999);
        if (!PlayerPrefs.HasKey(SPath.PREF_BULLET + 2))
            setDan(2, 0);
        if (!PlayerPrefs.HasKey(SPath.PREF_BULLET + 3))
            setDan(3, 0);
        if (!PlayerPrefs.HasKey(SPath.PREF_BULLET + 4))
            setDan(4, 30);
        if (!PlayerPrefs.HasKey(SPath.PREF_GUN))
            setGun(0);

        //Set hp Defualt
        if (!PlayerPrefs.HasKey(SPath.PREF_HP))
            setHP(SPath.MAX_HP_PLAYER);

        // save so luong enemy va bonus
        if (!PlayerPrefs.HasKey(SPath.PREF_COUNT_ONGBOM))
            PlayerPrefs.SetInt(SPath.PREF_COUNT_ONGBOM, 0);
        if (!PlayerPrefs.HasKey(SPath.PREF_BONUS_ONGBOM))
            PlayerPrefs.SetInt(SPath.PREF_BONUS_ONGBOM, 0);
        if (!PlayerPrefs.HasKey(SPath.PREF_COUNT_ONGCHICH))
            PlayerPrefs.SetInt(SPath.PREF_COUNT_ONGCHICH, 0);
        if (!PlayerPrefs.HasKey(SPath.PREF_BONUS_ONGCHICH))
            PlayerPrefs.SetInt(SPath.PREF_BONUS_ONGCHICH, 0);
        if (!PlayerPrefs.HasKey(SPath.PREF_SCORE_CURRENT))
            PlayerPrefs.SetInt(SPath.PREF_SCORE_CURRENT, 0);
        if (!PlayerPrefs.HasKey(SPath.PREF_SAVE_GAME))
            PlayerPrefs.SetInt(SPath.PREF_SAVE_GAME, 0);

        //coin vs ruby
        if (!PlayerPrefs.HasKey(SPath.PREF_SAVE_COIN))
            PlayerPrefs.SetInt(SPath.PREF_SAVE_COIN, 0);
        if (!PlayerPrefs.HasKey(SPath.PREF_SAVE_RUBY))
            PlayerPrefs.SetInt(SPath.PREF_SAVE_RUBY, 0);
        

    }

    public static void Reset_Component()
    {
        //  SPath.COUNT_ONG_BOM = 0;
        // SPath.COUNT_ONG_CHICH = 0;
        //  SPath.COUNT_SCORE_BONUS = 0;
        //  SPath.COUNT_SCORE_BONUS_2 = 0;
        //  SPath.COUNT_SCORE = 0;5
        if (!GetStateGame())
        {
            SPath.COUNT_SCORE_BONUS_3 = 0; // ko can save
            if (PlayerPrefs.HasKey(SPath.PREF_COUNT_ONGBOM))
                PlayerPrefs.DeleteKey(SPath.PREF_COUNT_ONGBOM);
            if (PlayerPrefs.HasKey(SPath.PREF_BONUS_ONGBOM))
                PlayerPrefs.DeleteKey(SPath.PREF_BONUS_ONGBOM);
            if (PlayerPrefs.HasKey(SPath.PREF_COUNT_ONGCHICH))
                PlayerPrefs.DeleteKey(SPath.PREF_COUNT_ONGCHICH);
            if (PlayerPrefs.HasKey(SPath.PREF_BONUS_ONGCHICH))
                PlayerPrefs.DeleteKey(SPath.PREF_BONUS_ONGCHICH);
            if (PlayerPrefs.HasKey(SPath.PREF_SCORE_CURRENT))
                PlayerPrefs.DeleteKey(SPath.PREF_SCORE_CURRENT);
            if (MyPref.getDan(1) < 1000)
            {
                if (PlayerPrefs.HasKey(SPath.PREF_GUN))
                    PlayerPrefs.DeleteKey(SPath.PREF_GUN);
            }

            //if (PlayerPrefs.HasKey(SPath.PREF_GUN1))
            //    PlayerPrefs.DeleteKey(SPath.PREF_GUN1);
            //if (PlayerPrefs.HasKey(SPath.PREF_GUN2))
            //    PlayerPrefs.DeleteKey(SPath.PREF_GUN2);
            //if (PlayerPrefs.HasKey(SPath.PREF_GUN3))
            //    PlayerPrefs.DeleteKey(SPath.PREF_GUN3);
            if (PlayerPrefs.HasKey(SPath.LEVEL))
                PlayerPrefs.DeleteKey(SPath.LEVEL);
            if (PlayerPrefs.HasKey(SPath.PREF_HP))
                PlayerPrefs.DeleteKey(SPath.PREF_HP);
            for (int i = 1; i <= 4; i++)
            {
                if (PlayerPrefs.HasKey(SPath.PREF_BULLET + i))
                    PlayerPrefs.DeleteKey(SPath.PREF_BULLET + i);
            }
        }
    }

    ////Save vat pham mua duoc
    //public static void AddItem1(int value)
    //{

    //}

    //Gold vs Coin
    public static void SaveCoin(int value)
    {
        int old_value = GetCoin();
        PlayerPrefs.SetInt(SPath.PREF_SAVE_COIN, value + old_value);
    }
    public static int GetCoin()
    {
        return PlayerPrefs.GetInt(SPath.PREF_SAVE_COIN);
    }

    public static void SaveRuby(int value)
    {
        int old_value = GetRuby();
        PlayerPrefs.SetInt(SPath.PREF_SAVE_RUBY,value+old_value);
    }
    public static int GetRuby()
    {
        return PlayerPrefs.GetInt(SPath.PREF_SAVE_RUBY);
    }

    //Save score and bonus
   
    

    public static void Save_Count_OngBom(int value)
    {
        int old_value = Get_Count_OngBom();
        PlayerPrefs.SetInt(SPath.PREF_COUNT_ONGBOM, value + old_value);
    }
    public static int Get_Count_OngBom()
    {
        return PlayerPrefs.GetInt(SPath.PREF_COUNT_ONGBOM);
    }

    public static void Save_Bonus_OngBom(int value)
    {
        int old_value = Get_Bonus_OngBom();
        PlayerPrefs.SetInt(SPath.PREF_BONUS_ONGBOM, value + old_value);
    }
    public static int Get_Bonus_OngBom()
    {
        return PlayerPrefs.GetInt(SPath.PREF_BONUS_ONGBOM);
    }
    public static void Reset_Bonus_OngBom()
    {
        PlayerPrefs.SetInt(SPath.PREF_BONUS_ONGBOM, 0);
    }

    public static void Save_Count_OngChich(int value)
    {
        int old_value = Get_Count_OngChich();
        PlayerPrefs.SetInt(SPath.PREF_COUNT_ONGCHICH, value + old_value);
    }
    public static int Get_Count_OngChich()
    {
        return PlayerPrefs.GetInt(SPath.PREF_COUNT_ONGCHICH);
    }
    public static void Save_Bonus_OngChich(int value)
    {
        int old_value = Get_Bonus_OngChich();
        PlayerPrefs.SetInt(SPath.PREF_BONUS_ONGCHICH, value + old_value);
    }
    public static int Get_Bonus_OngChich()
    {
        return PlayerPrefs.GetInt(SPath.PREF_BONUS_ONGCHICH);
    }
    public static void Reset_Bonus_OngChich()
    {
        PlayerPrefs.SetInt(SPath.PREF_BONUS_ONGCHICH, 0);
    }
    public static void Save_Score_Current(int value)
    {
        int old_value = Get_Score_Current();
        PlayerPrefs.SetInt(SPath.PREF_SCORE_CURRENT, value + old_value);
    }

    public static int Get_Score_Current()
    {
        return PlayerPrefs.GetInt(SPath.PREF_SCORE_CURRENT);
    }

    public static void SaveGame(bool save)
    {
        PlayerPrefs.SetInt(SPath.PREF_SAVE_GAME, save ? 1 : 0);
    }
    public static bool GetStateGame()
    {
        return PlayerPrefs.GetInt(SPath.PREF_SAVE_GAME) == 1;
    }

    //HP
    public static void setHP(float value)
    {
        PlayerPrefs.SetFloat(SPath.PREF_HP, value);
    }
    public static float getHP()
    {
        return PlayerPrefs.GetFloat(SPath.PREF_HP);
    }

    /*
     *Dan
     *id = 1 -> sung 1
     *
     */
    public static void setDan(int id, int value)
    {
        PlayerPrefs.SetInt(SPath.PREF_BULLET + id, value);
    }

    public static void addDan(int id, int newValue)
    {
        int oldValue = getDan(id);
        PlayerPrefs.SetInt(SPath.PREF_BULLET + id, oldValue + newValue);
    }

    public static int getDan(int id)
    {
        return PlayerPrefs.GetInt(SPath.PREF_BULLET + id);
    }

    public static void setGun(int type)
    {
        PlayerPrefs.SetInt(SPath.PREF_GUN, type);
    }
    public static int getGun()
    {
        return PlayerPrefs.GetInt(SPath.PREF_GUN);
    }


    /*
     * bool active =  true ? 1 : 0; 
     */
    public static void setTypeGun(int type, bool active)
    {
        if (type <= 0 || type > 3)
            return;
        PlayerPrefs.SetInt(SPath.PREF_GUN + type, active ? 1 : 0);
    }

    public static bool getTypeGun(int type)
    {
        return PlayerPrefs.GetInt(SPath.PREF_GUN + type) == 1;
    }


    //Sound
    public static void setSound(bool value)
    {
        PlayerPrefs.SetInt(SPath.PREF_SOUND, value == true ? 1 : 0);
    }
    public static bool getSound()
    {
        return PlayerPrefs.GetInt(SPath.PREF_SOUND) == 1 ? true : false;
    }

    //Music
    public static void setMusic(bool value)
    {
        PlayerPrefs.SetInt(SPath.PREF_MUSIC, value == true ? 1 : 0);
    }
    public static bool getMusic()
    {
        return PlayerPrefs.GetInt(SPath.PREF_MUSIC) == 1 ? true : false;
    }

    //Value volume sound
    public static void setVolumeSound(float value)
    {
        if (value < 0 || value > 1)
            return;
        PlayerPrefs.SetFloat(SPath.PREF_VOLUME_SOUND, value);
    }
    public static float getVolumeSound()
    {
        return PlayerPrefs.GetFloat(SPath.PREF_VOLUME_SOUND);
    }

    //Value volume music
    public static void setVolumeMusic(float value)
    {
        if (value < 0 || value > 1)
            return;
        PlayerPrefs.SetFloat(SPath.PREF_VOLUME_MUSIC, value);
    }
    public static float getVolumeMusic()
    {
        return PlayerPrefs.GetFloat(SPath.PREF_VOLUME_MUSIC);
    }

    //Score
    public static void setScore(int value)
    {
        if (value <= getScore())
            return;
        PlayerPrefs.SetInt(SPath.PREF_SCORE, value);
    }
    public static int getScore()
    {
        return PlayerPrefs.GetInt(SPath.PREF_SCORE);
    }
}
