using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    public ScreenSizeControllerScript screenSizeController;
    public TurnIndicatorScript turnIndicator;
    public PlayingFieldFillerScript playingFieldFiller;
    public ResultsWindowScript resultsWindow;
    public PlayingFieldCellsController cellsController;

    public GameObject goMainMenu, goResultsWindow;
    public Button buttonPlay, buttonExit, buttonRestart, buttonResultsExit;

    [SerializeField]
    int nRowNumber = 3;
    public int GetRowNumber()
    {
        return nRowNumber;
    }

    List<GameObject> listGoCellButtons;

    CrossAndNoughtsPlayer playerOne, playerTwo;

    void Start()
    {
        buttonPlay.onClick.AddListener(ButtonPlayPressed);
        buttonExit.onClick.AddListener(ButtonExitPressed);
        buttonRestart.onClick.AddListener(ButtonRestartPressed);
        buttonResultsExit.onClick.AddListener(ButtonExitPressed);

        playerOne = new CrossAndNoughtsPlayer("PlayerOne", true);
        playerTwo = new CrossAndNoughtsPlayer("PlayerTwo", false);
        if (!(playerOne is IAbleToSaveAndLoadWinNumber && playerTwo is IAbleToSaveAndLoadWinNumber))
            throw new Exception("player class not implements IAbleToSaveWinNumber");
        CheckInterfaces();
        
        playingFieldFiller.InitializePlayindFieldFillerAtGameStart(nRowNumber);
        GameInitialization();
    }

    void CheckInterfaces()
    {
        if(!(turnIndicator is IInitializable))
            throw new Exception("turn indicator class not implements IInitializable");
        if (!(turnIndicator is ITurnIndicatorShowing))
            throw new Exception("turn indicator class not implements ITurnIndicatorShowing");
        if(!(resultsWindow is IInitializable))
            throw new Exception("results window class not implements IInitializable");
        if (!(resultsWindow is ICanShowWinnerAndStalemate))
            throw new Exception("results window class not implements ICanShowWinnerAndStalemate");
    }

    void GameInitialization()
    {
        //Initialization
        turnIndicator.Initialize();
        cellsController.InitializeCellValuesAndTurn();
        resultsWindow.Initialize();
        goResultsWindow.SetActive(false);
    }

    void StartGame()
    {  //вызывается по нажатии кнопки Играть в основном меню
        playingFieldFiller.DrawPlayingCells();
        playingFieldFiller.DrawLatticeLines();
        listGoCellButtons = CellButtonsHandler.GetListGoButtons();
        for (int c = 0; c < listGoCellButtons.Count; c++)
        {
            listGoCellButtons[c].GetComponent<CellButtonScript>().
                onButtonPressed += CellButtonPressed;
        }
        turnIndicator.ShowTurnIndicator(PlayingFieldCellsController.GetbIsThisCrossesTurn(),
            GetPlayerNameByTurn(PlayingFieldCellsController.GetbIsThisCrossesTurn()));
    }

    public void CellButtonPressed(int nCellIndex)
    {
        if (nCellIndex == -1)
        {
            Debug.Log("cell button with no nCellIndex");
            return;
        }
        //зафиксировать, что на этой кнопке был сделан ход
        cellsController.RegisterACellValueInTurn(nCellIndex);
        if (cellsController.IsThisAWinner())
        { //если на данном ходу определился победитель
            CellButtonsHandler.SetAllCellButtonsActive(false);
            turnIndicator.Initialize();
            goResultsWindow.SetActive(true);
            if (cellsController.IsThisTripletOfNoughts())
            {
                resultsWindow.ShowWinner(bIsCrossesWin: false);
                if (!playerOne.bIsThisTypeIsCrosses)
                    playerOne.SetAndSaveCountOfWinNumber(playerOne.LoadAndGetCountOfWinNumber() + 1);
                if (!playerTwo.bIsThisTypeIsCrosses)
                    playerTwo.SetAndSaveCountOfWinNumber(playerTwo.LoadAndGetCountOfWinNumber() + 1);
                //TODO: магическое число при начислении побед
                //точнее, оно является вполне законной константой для поля 3х3, потому что
                //отражает конкретно победу в этом раунде. Однако, если расширять игру
                //для полей бОльших размеров, то возможно там появится системя счета, а не 
                //просто победил-програл, и тогда здесь можно будет записывать именно 
                //набранное количество очков
            }
            if(cellsController.IsThisTripletOfCrosses())
            {
                resultsWindow.ShowWinner(bIsCrossesWin: true);
                if (playerOne.bIsThisTypeIsCrosses)
                    playerOne.SetAndSaveCountOfWinNumber(playerOne.LoadAndGetCountOfWinNumber() + 1);
                if (playerTwo.bIsThisTypeIsCrosses)
                    playerTwo.SetAndSaveCountOfWinNumber(playerTwo.LoadAndGetCountOfWinNumber() + 1);
            }
            resultsWindow.SetTextStatistics(resultsWindow.PrepareStatisticsString(playerOne, playerTwo));
            return;                
        }
        if (cellsController.IsThisAStalemate())
        {  //если на данном ходу возникла ничья
            CellButtonsHandler.SetAllCellButtonsActive(false);
            turnIndicator.Initialize();
            goResultsWindow.SetActive(true);
            resultsWindow.ShowStalemate();
            resultsWindow.SetTextStatistics(resultsWindow.PrepareStatisticsString(playerOne,playerTwo));
        }
        PlayingFieldCellsController.ChangeTurn();
        turnIndicator.ShowTurnIndicator(PlayingFieldCellsController.GetbIsThisCrossesTurn(),
            GetPlayerNameByTurn(PlayingFieldCellsController.GetbIsThisCrossesTurn()));
    }

    public void ButtonRestartPressed()
    {
        GameInitialization();
        CellButtonsHandler.ResetAllCellButtons();
        CellButtonsHandler.SetAllCellButtonsActive(true);
        turnIndicator.ShowTurnIndicator(PlayingFieldCellsController.GetbIsThisCrossesTurn(), 
            GetPlayerNameByTurn(PlayingFieldCellsController.GetbIsThisCrossesTurn()));
    }

    public void ButtonPlayPressed()
    {
        Debug.Log("button play pressed");
        goMainMenu.SetActive(false);
        StartGame();
    }

    void ButtonExitPressed()
    {
        Debug.Log("button exit pressed");
        Application.Quit();
    }

    public string GetPlayerNameByTurn(bool bIsThisCrossesTurn)
    {
        if (bIsThisCrossesTurn == playerOne.bIsThisTypeIsCrosses)
            return playerOne.sName;
        if (bIsThisCrossesTurn == playerTwo.bIsThisTypeIsCrosses)
            return playerTwo.sName;
        return "";
    }
}
