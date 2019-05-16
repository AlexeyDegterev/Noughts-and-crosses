using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IInitializableView
{
    void InitializeView();
}

public interface IAbleToSetEnable
{
    void SetEnable(bool TrueFalse);
}

public interface IAbleToSaveAndLoadWinNumber
{
    void SetAndSaveCountOfWinNumber(int nCount);
    int LoadAndGetCountOfWinNumber();
}

public interface ICanShowWinnerAndStalemate
{
    void ShowWinner(bool bIsCrossesWin);
    void ShowStalemate();
}

public interface IInitializable
{
    void Initialize();
}

public interface ITurnIndicatorShowing
{
    void ShowTurnIndicator(bool bIsThisCrossesTurn, string sPlayerName);
}

public interface ICellIndex
{
    int CellIndex { set; }
}
