using System;
using UnityEngine;

public class CrossAndNoughtsPlayer: CrossNoughtsBasePlayer, IAbleToSaveAndLoadWinNumber
{
    private int _nCountOfWin;

    public int nCountOfWin
    {
        get
        {
            return _nCountOfWin;
        }
    }

    public CrossAndNoughtsPlayer(string sName, bool bIsThisACrosses) : base(sName, bIsThisACrosses)
    {
    }

    public void SetAndSaveCountOfWinNumber(int nCount)
    {
        _nCountOfWin = nCount;
        PlayerPrefs.SetInt(sName + "_nCountOfWin", nCount);
    }

    public int LoadAndGetCountOfWinNumber()
    {
        int nCount = PlayerPrefs.GetInt(sName + "_nCountOfWin", -1);
        if (nCount < 0)
        {
            _nCountOfWin = 0;
        }
        else
        {
            _nCountOfWin = nCount;
        }
        return _nCountOfWin;
    }
}
