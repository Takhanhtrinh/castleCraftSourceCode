
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManage
{
    public List<GameLevel> m_Levels;
    public LevelManage() 
    {
        m_Levels = new List<GameLevel>();
        m_Levels.Add(new Level1());
        m_Levels.Add(new Level2());
        m_Levels.Add(new Level3());
        m_Levels.Add(new Level4());
        m_Levels.Add(new Level5());

    }

}