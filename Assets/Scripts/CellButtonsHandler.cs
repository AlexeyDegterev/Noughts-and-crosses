using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellButtonsHandler : MonoBehaviour
{
    private static List<GameObject> listGoButtons;
    public static List<GameObject> GetListGoButtons()
    {
        return listGoButtons;
    }
    public static void AddButtonToList(GameObject goButtonToAdd)
    {
        listGoButtons.Add(goButtonToAdd);
    }
    private void Start()
    {
        listGoButtons = new List<GameObject>();
    }

    public static void SetAllCellButtonsActive(bool bButtonsActive)
    {
        for (int c = 0; c < listGoButtons.Count; c++)
        {
            listGoButtons[c].GetComponent<IAbleToSetEnable>().SetEnable(bButtonsActive);
        }
    }

    public static void ResetAllCellButtons()
    {
        for (int c = 0; c < listGoButtons.Count; c++)
        {
            listGoButtons[c].GetComponent<IInitializableView>().InitializeView();
        }
    }
}
