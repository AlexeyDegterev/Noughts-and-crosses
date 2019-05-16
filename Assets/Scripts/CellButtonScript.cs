using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellButtonScript : MonoBehaviour, IInitializableView, IAbleToSetEnable, ICellIndex
{
    public GameObject goSpriteCross, goSpriteNought;

    private bool bButtonEnable = true;
    public void SetEnable(bool TrueFalse)
    {
        bButtonEnable = TrueFalse;
    }

    private int _nCellIndex = -1;

    public int CellIndex
    {
        set
        {
            _nCellIndex = value;
        }
    }

    void DrawCellSprite(bool bIsThisACross)
    {
        if (bIsThisACross)
        {
            goSpriteCross.SetActive(true);
        }else
        {
            goSpriteNought.SetActive(true);
        }
    }

    private void Start()
    {
        InitializeView();
    }

    private void OnMouseUp()
    {
        if (!bButtonEnable)
            return;
        DrawCellSprite(PlayingFieldCellsController.GetbIsThisCrossesTurn());
        onButtonPressed(_nCellIndex);
        bButtonEnable = false;
    }

    public void InitializeView()
    {
        goSpriteCross.SetActive(false);
        goSpriteNought.SetActive(false);
    }

    public delegate void MethodContainer(int nThisButtonCellIndex);
    public event MethodContainer onButtonPressed;
}
