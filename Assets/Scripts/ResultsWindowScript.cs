using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultsWindowScript : MonoBehaviour, ICanShowWinnerAndStalemate, IInitializable
{
    public Text textStatistics, textWhoWin;

    public void Initialize()
    {
        textWhoWin.text = "";
        textStatistics.text = "";
    }
    public void ShowWinner(bool bIsCrossesWin)
    {
        if (bIsCrossesWin)
        {
            textWhoWin.text = ProjectStrings.GetStringResWinCrosses();
        }
        else
        {
            textWhoWin.text = ProjectStrings.GetStringResWinNoughts();
        }
    }

    public void ShowStalemate()
    {
        textWhoWin.text = ProjectStrings.GetStringResStalemate();
    }

    public void SetTextStatistics(string sText)
    {
        textStatistics.text = sText;
    }    

    public string PrepareStatisticsString(CrossAndNoughtsPlayer playerOne, CrossAndNoughtsPlayer playerTwo)
    {
        string sResults = "";
        sResults += playerOne.sName;
        if (playerOne.bIsThisTypeIsCrosses)
        {
            sResults += " (" +ProjectStrings.GetStringCrosses()+ ") ";
        }
        else
        {
            sResults += " (" + ProjectStrings.GetStringNoughts() + ") ";
        }
        sResults += playerOne.LoadAndGetCountOfWinNumber().ToString() + "\n";
        sResults += playerTwo.sName;
        if (playerTwo.bIsThisTypeIsCrosses)
        {
            sResults += " (" + ProjectStrings.GetStringCrosses() + ") ";
        }
        else
        {
            sResults += " (" + ProjectStrings.GetStringNoughts() + ") ";
        }
        sResults += playerTwo.LoadAndGetCountOfWinNumber().ToString();
        return sResults;
    }
}
