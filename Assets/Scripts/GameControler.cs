using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameControler : MonoBehaviour
{
    //Parametrs
    int state;
    bool firstPlayerTurn;
    Grid<TicTacToeElement> grid;
    GameObject[,] objs;
    
    Button curSizeButton;


    Player firstPlayer;
    Player secondPlayer;


    int curSIze;
    int curSizeFP;
    int curSizeSP;



    [Header("Grid Parametrs")]
    public int gridSize;
    public Sprite cellSprite;
    public GameObject cellsContainer;

    [Header("Parametrs")]
    public Color firstPlayerColor;
    public Color secondPlayerColor;
    bool isFirstPlayerChangeColor;


    [Header("UI")]
    public TextMeshProUGUI header;
    public TMP_InputField firstPlayerIF;
    public TMP_InputField secondPlayerIF;
    public Notification notifiction;

    public TextMeshProUGUI[] circleCountText;


    [Header ("UI Panels")]
    public GameObject enterNamePanel;
    public ColorPanel colorPanel;
    public BottomPanel bottomPanel;
    public GameObject gameEndPanel;


    [Header("Buttons")]
    public Button smallCircleButton;
    public Button mediumCircleButton;
    public Button bigCircleButton;


    [Header("Sprites")]
    public Sprite figureSprite;
    
    //cash
    Camera mainCamera;


    void Start()
    {
        firstPlayerTurn = true;

        grid = new Grid<TicTacToeElement>(gridSize, gridSize, 1.1f, new Vector2(-(float)gridSize/2 * 1.1f, -(float)gridSize/2 * 1.1f));
        

        objs = new GameObject[gridSize, gridSize];


        firstPlayer = new Player(0, gridSize);
        secondPlayer = new Player(1, gridSize);


        colorPanel.onSelectColor.AddListener(SetNewPlayerColor);


        curSizeFP = 1;
        curSizeSP = 1;

        mainCamera = Camera.main;

        state = 0;
    }

    void Update()
    {
        switch(state)
        {
        // State 0
            case 0:
                if(Input.GetKeyDown(KeyCode.Tab))
                {
                    if (firstPlayerIF.isFocused)
                        secondPlayerIF.ActivateInputField();
                    else
                        firstPlayerIF.ActivateInputField();
                }

                if (Input.GetKeyDown(KeyCode.Return))
                    StartGame();
                break;

        // State 1
            case 1:
                if (Input.GetMouseButtonDown(0))
                {
                    grid.GetXY((Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition), out int x, out int y);

                    if (grid.CellInGrid(x, y))
                    {
                        TicTacToeElement ttte = grid.GetElement(x, y);

                        if (ttte == null || ttte != null && ttte.size < curSIze)
                        {
                            if (firstPlayerTurn)
                            {
                                firstPlayerTurn = false;

                                PlayerTurn(firstPlayer, x, y);

                                StartPlayerTurn(secondPlayer);
                            }
                            else
                            {
                                firstPlayerTurn = true;

                                PlayerTurn(secondPlayer, x, y);

                                StartPlayerTurn(firstPlayer);
                            }
                        }
                    }
                }
                break;
        // State 1 end ======================================================================

            case 2:
                break;
        }
    }



    public void StartGame()
    {
        if (firstPlayer.name == "" || secondPlayer.name == "")
        {
            notifiction.NewNotification("Name not entered");
            return;
        }

        firstPlayer.name = firstPlayer.name.ToLower();
        secondPlayer.name = secondPlayer.name.ToLower();

        if (firstPlayer.name == secondPlayer.name)
        {
            notifiction.NewNotification("Same names entered");
            return;
        }
        if(firstPlayerColor == secondPlayerColor)
        {
            notifiction.NewNotification("Same colors selected");
            return;
        }


        firstPlayer.color = firstPlayerColor;
        secondPlayer.color = secondPlayerColor;

        firstPlayer.figureSprite = figureSprite;
        secondPlayer.figureSprite = figureSprite;
        

        Vector2[] cellsPosition = grid.GetCellsPosition();

        for (int i = 0; i < cellsPosition.Length; i++)
        {
            GameObject cell = new GameObject("Cell " + i.ToString(), typeof(SpriteRenderer));
            cell.GetComponent<SpriteRenderer>().sprite = cellSprite;
            cell.transform.position = cellsPosition[i];
            cell.transform.localScale = cell.transform.localScale * grid.GetCellSize();
            cell.transform.SetParent(cellsContainer.transform);
        }


        enterNamePanel.SetActive(false);
        bottomPanel.gameObject.SetActive(true);

        StartPlayerTurn(firstPlayer);

        state = 1;
    }



    void StartPlayerTurn(Player player)
    {
        if (firstPlayerTurn)
        {
            curSIze = curSizeFP;
            bottomPanel.ChangeColor(firstPlayer.color);
        }
        else
        {
            curSIze = curSizeSP;
            bottomPanel.ChangeColor(secondPlayer.color);
        }


        header.text = player.name + " is moving";


        int countOfEndedCircle = 0;


        if (player.GetCountElem(0) == 0)
        {
            smallCircleButton.interactable = false;
            countOfEndedCircle++;

            if (curSIze == 1)
                curSIze++;
        }
        else
        {
            smallCircleButton.interactable = true;
        }

        if (player.GetCountElem(1) == 0)
        {
            mediumCircleButton.interactable = false;
            countOfEndedCircle++;

            if (curSIze == 2)
                curSIze++;
        }
        else
        {
            mediumCircleButton.interactable = true;
        }

        if (player.GetCountElem(2) == 0)
        {
            bigCircleButton.interactable = false;
            countOfEndedCircle++;

            if (curSIze == 3)
                if (countOfEndedCircle == 3)
                    GameEndDraw();
                else if (player.GetCountElem(0) != 0)
                    curSIze = 1;
                else
                    curSIze = 2;
        }
        else
        {
            bigCircleButton.interactable = true;
        }


        switch (curSIze)
        {
            case 1:
                SetBackgroundSizeButton(smallCircleButton);
                break;
            case 2:
                SetBackgroundSizeButton(mediumCircleButton);
                break;
            case 3:
                SetBackgroundSizeButton(bigCircleButton);
                break;
        }


        for (int i = 0; i < gridSize; i++)
        {
            circleCountText[i].text = player.GetCountElem(i).ToString();
        }
    }


    void PlayerTurn(Player player, int x, int y)
    {
        Destroy(objs[x, y]);
        grid.RemoveElement(x, y);


        GameObject playersFigure = new GameObject("Player " + player.name, typeof(SpriteRenderer));
        playersFigure.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        playersFigure.transform.localScale *= curSIze;
        playersFigure.transform.position = grid.GetWorldPositionWithPositiveOffset(x, y);

        SpriteRenderer sr = playersFigure.GetComponent<SpriteRenderer>();
        sr.sprite = player.figureSprite;
        sr.color = player.color;

        TicTacToeElement ttte = new TicTacToeElement(player.id, curSIze);



        switch (curSIze)
        {
            case 1:
                player.Substruct(0);
                break;
            case 2:
                player.Substruct(1);
                break;
            case 3:
                player.Substruct(2);
                break;
        }


        objs[x, y] = playersFigure;
        grid.AddElement(ttte, x, y);

        EndOfTurn(player.id);
    }



    void EndOfTurn(int idPlayer)
    {
        int numberCoincidences;


        for (int i = 0; i < gridSize * 2 + 2; i++)
        {
            numberCoincidences = 0;

            if (i < gridSize)
            {
                for (int j = 0; j < gridSize; j++)
                    if (grid.GetElement(i, j) != null)
                        if (grid.GetElement(i, j).idPlayer == idPlayer)
                            numberCoincidences++;
                        else
                            break;
            }
            else if (i < gridSize * 2)
            {
                for (int j = 0; j < gridSize; j++)
                    if (grid.GetElement(j, i % gridSize) != null) 
                        if (grid.GetElement(j, i % gridSize).idPlayer == idPlayer)
                            numberCoincidences++;
                        else
                            break;
            }
            else if (i == gridSize * 2)
            {
                for (int j = 0; j < gridSize; j++)
                    if (grid.GetElement(j, j) != null)
                        if (grid.GetElement(j, j).idPlayer == idPlayer)
                            numberCoincidences++;
                        else
                            break;
            }
            else
                for (int j = 0, k = gridSize - 1; j < gridSize && k >= 0; j++, k--)
                    if (grid.GetElement(j, k) != null)
                        if (grid.GetElement(j, k).idPlayer == idPlayer)
                            numberCoincidences++;
                        else
                            break;


            if (numberCoincidences == gridSize)
            {
                GameEnd(idPlayer);
                return;
            }
        }
    }


    public void Revansh()
    {
        foreach(var obj in objs)
        {
            Destroy(obj);
        }

        firstPlayer.Reload();
        secondPlayer.Reload();

        grid = new Grid<TicTacToeElement>(gridSize, gridSize, 1.1f, new Vector2(-(float)gridSize / 2 * 1.1f, -(float)gridSize / 2 * 1.1f));

        gameEndPanel.SetActive(false);

        firstPlayerTurn = true;
        StartGame();
    }


    public void SetNewPlayerName(int id)
    {
        if (id == 0)
        {
            firstPlayer.name = firstPlayerIF.text;
        }
        else
        {
            secondPlayer.name = secondPlayerIF.text;
        }
    }

    public void SetNewPlayerColor(Color color)
    {
        if (isFirstPlayerChangeColor)
            firstPlayerColor = color;
        else
            secondPlayerColor = color;
    }

    public void ChangeColor(int id)
    {
        switch (id)
        {
            case 0:
                isFirstPlayerChangeColor = true;
                break;
            case 1:
                isFirstPlayerChangeColor = false;
                break;
        }
    }


    //!!!!!!!!!!!
    void GameEnd(int idPlayer)
    {
        gameEndPanel.SetActive(true);

        if(firstPlayer.id == idPlayer)
            gameEndPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = firstPlayer.name + " Win";
        else
            gameEndPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = secondPlayer.name + " Win";
    }

    void GameEndDraw()
    {
        gameEndPanel.SetActive(true);
        gameEndPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Ничья";
    }

    public void SetSize(int size)
    {
        curSIze = size;

        if (firstPlayerTurn)
        {
            curSizeFP = size;
        }
        else
        {
            curSizeSP = size;
        }
    }

    public void SetBackgroundSizeButton(Button button)
    {
        if (curSizeButton != false)
            curSizeButton.transform.GetChild(2).gameObject.SetActive(false);

        curSizeButton = button;
        curSizeButton.transform.GetChild(2).gameObject.SetActive(true);
    }
}