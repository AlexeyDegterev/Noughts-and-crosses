using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingFieldFillerScript : MonoBehaviour
{
    public GameControllerScript gcs;
    public GameObject goFieldCellPrefab, goVerticalLinePrefab,
        goHorizontalLinePrefab;

    private int nRowCount = 3;
    [SerializeField]
    private float fSpacing = 0.1f;

    private float fFieldWidth, fScreenRatio, fCellWidth, fCellCenterInterval;
    /// <summary>
    /// Данный алгоритм заполнения поля клетками позволяет создавать поля 
    /// для игры не только размером 3х3, но и больше.
    /// Однако алгоритмы подсчета победителя/ничьей/количества очков 
    /// не доработаны, чтобы играть на таких полях было возможно
    /// </summary>
    /// <param name="nRowCount"></param>
    public void InitializePlayindFieldFillerAtGameStart(int nRowCount = 3)
    {
        this.nRowCount = nRowCount;
        fScreenRatio = gcs.screenSizeController.fScreenRatio;
        //далее вычисление общей ширины экрана в World координатах Unity.
        //Замеры были произведены с помощью элемента buff_ToFitScreenSize
        //на разных разрешениях экрана, затем по линейной регрессии высчитаны
        //соответствующие коэффициенты, применяемые ниже. Да, если у камеры
        //поменять size, то придется пересчитывать. Но не предвидится, что 
        //размер камеры нужно будет менять
        fFieldWidth = 2f * (-2.8356f*fScreenRatio + 10.6581f);
        //далее: вычисление ширины клетки (в которой будет либо крестик, либо
        //нолик), чтобы при заданном количестве клеток (строк/столбцов, то есть
        //чтобы было поле 3х3, 4х4, 5х5, ...) и зазорам между клетками, они 
        //заполняли по ширине весь экран устройства (с небольшими допусками, чтобы
        //уж не совсем впритык к краям)
        fCellWidth = 1f / nRowCount * (fFieldWidth - (nRowCount+1)*fSpacing);
        //расстояние же между центрами соседних клеток, как по горизонтали,
        //так и по вертикали, равно 2 раза по половине ширины клетки + зазор
        //между ними
        fCellCenterInterval = fCellWidth + fSpacing;
    }

    public void DrawPlayingCells()
    {
        int nCellCounter = 0;
        for(int j=0; j<nRowCount; j++)
        {
            for(int i=0; i<nRowCount; i++)
            {  //nRowCount === nColumnCount - для квадратного поля количество строк и столбцов равно
                GameObject goCurrentCell = Instantiate(goFieldCellPrefab,
                    new Vector3(-fFieldWidth/2f + fSpacing + fCellWidth/2f
                        + i*fCellCenterInterval,
                        fFieldWidth/2f - fSpacing - fCellWidth/2f 
                        - j*fCellCenterInterval),
                    Quaternion.identity,
                    transform);
                goCurrentCell.transform.localScale = new Vector3(
                    fCellWidth, fCellWidth, 1f);
                goCurrentCell.GetComponent<ICellIndex>().CellIndex = nCellCounter;
                CellButtonsHandler.AddButtonToList(goCurrentCell);
                nCellCounter++;

            }
        }
    }

    private void PlayingFieldFillerScript_onButtonPressed(int nThisButtonCellIndex)
    {
        throw new System.NotImplementedException();
    }

    public void DrawLatticeLines()
    {
        for (int c=0; c<nRowCount-1; c++)
        {
            GameObject goHorLine = Instantiate(goHorizontalLinePrefab, 
                new Vector3(-fFieldWidth/2f+fSpacing,
                fFieldWidth/2f-(fCellWidth+fSpacing)*(c+1)), 
                goHorizontalLinePrefab.transform.rotation);
            goHorLine.transform.localScale = new Vector3(
                goHorLine.transform.localScale.x,
                fFieldWidth-2f*fSpacing,
                goHorLine.transform.localScale.z);

            GameObject goVertLine = Instantiate(goVerticalLinePrefab,
                new Vector3(fFieldWidth / 2f - (fCellWidth + fSpacing) * (c + 1),
                fFieldWidth / 2f - fSpacing),
                goVerticalLinePrefab.transform.rotation);
            goVertLine.transform.localScale = new Vector3(
                goVertLine.transform.localScale.x,
                fFieldWidth - 2f * fSpacing,
                goVertLine.transform.localScale.z);
        }
    }

    public Vector3 GetCellCenterWorldCoordinates(int nCellIndex)
    {
        int nHorPosition, nVertPosition;
        nHorPosition = nCellIndex % nRowCount;
        nVertPosition = nCellIndex / nRowCount;
        return new Vector3(
            -fFieldWidth / 2f + fSpacing + fCellWidth / 2f + 
                nHorPosition * fCellCenterInterval,
            fFieldWidth / 2f - fSpacing - fCellWidth / 2f
                - nVertPosition * fCellCenterInterval,
            0f);
    }
}
