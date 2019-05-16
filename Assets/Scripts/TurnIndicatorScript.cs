using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnIndicatorScript : MonoBehaviour, IInitializable, ITurnIndicatorShowing
{
    public TextMesh txtmshTurnIndicator;

    public void Initialize()
    {
        txtmshTurnIndicator.text = "";
    }

    public void ShowTurnIndicator(bool bIsThisCrossesTurn, string sPlayerName)
    {
        if (bIsThisCrossesTurn)
        {
            txtmshTurnIndicator.text = ProjectStrings.GetStringTurnCrosses() + "\n" + sPlayerName;
        }
        else
        {
            txtmshTurnIndicator.text = ProjectStrings.GetStringTurnNoughts() + "\n" + sPlayerName;
        }
    }
}
