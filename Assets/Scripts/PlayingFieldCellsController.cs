using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingFieldCellsController : MonoBehaviour
{
    public GameControllerScript gcs;
    static bool bIsThisCrossesTurn = true;
    public static bool GetbIsThisCrossesTurn()
    {
        return bIsThisCrossesTurn;
    }
    public static void ChangeTurn()
    {
        bIsThisCrossesTurn = !bIsThisCrossesTurn;
    }

    int[] nCellValues;
    
    int[] nWinnerTripletIndexes;
    int nRowNumber;
    public void InitializeCellValuesAndTurn()
    {
        nRowNumber = gcs.GetRowNumber();
        nCellValues = new int[nRowNumber * nRowNumber];
        for (int c = 0; c < nCellValues.Length; c++)
        {
            nCellValues[c] = -1;
        }
        bIsThisCrossesTurn = true;
        nWinnerTripletIndexes = null;
    }
    
    public bool IsThisAWinner()
    {
        //алгоритм вычисления победителя реализован только для поля 3х3
        for (int i = 0; i < nRowNumber; i++)
        {
            //проверка горизонталей
            if (IsThisACellsTriplet(i * 3, i * 3 + 1, i * 3 + 2))
            {
                nWinnerTripletIndexes = new int[] { i * 3, i * 3 + 1, i * 3 + 2 };
                return true;
            }

            //проверка вертикалей
            if (IsThisACellsTriplet(i, i + 3, i + 6))
            {
                nWinnerTripletIndexes = new int[] { i, i + 3, i + 6 };
                return true;
            }
        }
        //проверка диагоналей
        if (IsThisACellsTriplet(0, 4, 8))
        {
            nWinnerTripletIndexes = new int[] { 0, 4, 8 };
            return true;
        }
        if (IsThisACellsTriplet(2, 4, 6))
        {
            nWinnerTripletIndexes = new int[] { 2, 4, 6 };
            return true;
        }
        return false;
    }
    public bool IsThisAStalemate()
    {  //алгоритм ничьей реализован для поля 3х3
       //для полей с бОльшим количеством клеток он по сути станет выдавать
       //точку окончания игры (все клетки заполнены), а пот ничья или победа -
       //для этого уже нужно будет каким-либо образом подсчитать очки
        for (int c = 0; c < nCellValues.Length; c++)
        {
            if (nCellValues[c] < 0)
                return false;
        }
        return true;
    }
    public bool IsThisACellsTriplet(int nIndex1, int nIndex2, int nIndex3)
    {
        if (nCellValues[nIndex1] < 0 || nCellValues[nIndex2] < 0
            || nCellValues[nIndex3] < 0)
            return false;
        if (nCellValues[nIndex1] == nCellValues[nIndex2] &&
            nCellValues[nIndex2] == nCellValues[nIndex3])
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsThisTripletOfCrosses()
    {
        if (nWinnerTripletIndexes == null)
            return false;
        if(nCellValues[nWinnerTripletIndexes[0]] == 1)
        {
            return true;
        }
        return false;
    }
    public bool IsThisTripletOfNoughts()
    {
        if (nWinnerTripletIndexes == null)
            return false;
        if (nCellValues[nWinnerTripletIndexes[0]] == 0)
        {
            return true;
        }
        return false;
    }

    public void RegisterACellValueInTurn(int nCellIndex)
    {
        if (bIsThisCrossesTurn)
        {
            nCellValues[nCellIndex] = 1;
        }
        else
        {
            nCellValues[nCellIndex] = 0;
        }
    }
}
