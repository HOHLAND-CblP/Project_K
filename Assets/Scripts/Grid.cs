using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid<TGridElement>
{
    private Vector2Int gridSize;         // Размер поля для строительства
    private float cellSize;
    private Vector2 originPosition;      // Начальная позиция поля (нижний левый угол поля)
    private TGridElement[,] grid;         // Массив с элементами

    public Grid()
    {
        gridSize = new Vector2Int(1, 1);
        cellSize = 1;
        originPosition = Vector2.zero;
        grid = new TGridElement[1, 1];
    }
    public Grid(Vector2Int size)
    {
        gridSize = size;
        cellSize = 1;
        originPosition = Vector2.zero;
        grid = new TGridElement[gridSize.x, gridSize.y];
    }
    public Grid(int x, int y)
    {
        gridSize = new Vector2Int(x, y);
        cellSize = 1;
        originPosition = Vector2.zero;
        grid = new TGridElement[x, y];
    }
    public Grid(Vector2Int size, float cellSize, Vector2 originPosition)
    {
        gridSize = size;
        this.cellSize = cellSize;
        this.originPosition = originPosition;
        grid = new TGridElement[gridSize.x, gridSize.y];
    }
    public Grid(int x, int y, float cellSize, Vector2 originPosition)
    {
        gridSize = new Vector2Int(x, y);
        this.cellSize = cellSize;
        this.originPosition = originPosition;
        grid = new TGridElement[x, y];
    }





    public void DrawCellsWithGizmo()
    {   
        for (int x = 0; x < gridSize.x; x++)
            for (int y = 0; y < gridSize.y; y++)
            {
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.black, 300);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.black, 300);
            }

        Debug.DrawLine(GetWorldPosition(0, gridSize.y), GetWorldPosition(gridSize.x, gridSize.y), Color.black, 100);
        Debug.DrawLine(GetWorldPosition(gridSize.x, 0), GetWorldPosition(gridSize.x, gridSize.y), Color.black, 100);
    }


    public Vector2[] GetCellsPosition()
    {
        Vector2[] cellsPosition = new Vector2[gridSize.x * gridSize.y];

        for (int x = 0; x < gridSize.x; x++)
            for (int y = 0; y < gridSize.y; y++)
            {
                cellsPosition[x * gridSize.y + y] = GetWorldPosition(x, y);
            }

        return cellsPosition;
    }

    public bool CellAvailable(int x, int y)
    {
        bool available = true;

        if (x >= 0 && y >= 0 && x < gridSize.x && y < gridSize.y)
        {
            if (grid[x, y] != null)
                available = false;
        }
        else
            available = false;

        return available;
    }

    public bool CellInGrid(int x,int y)
    {
        if (x >= 0 && y >= 0 && x < gridSize.x && y < gridSize.y)
            return true;    

        return false;
    }


    public void AddElement(TGridElement element, int x, int y)
    {
        if (x >= 0 && y >= 0 && x < gridSize.x && y < gridSize.y)
        {
            grid[x, y] = element;
        }
    }

    public void AddElement(TGridElement element, List<Vector2Int> XY)
    {
        foreach (var xy in XY)
        {
            grid[xy.x, xy.y] = element;
        }
    }

    public void RemoveElement(int x, int y)
    {
        grid[x, y] = default;
    }
    public void RemoveElement(Vector2Int XY)
    {
        grid[XY.x, XY.y] = default;
    }


    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x * cellSize + originPosition.x, y * cellSize + originPosition.y, 0);
    }

    public Vector3 GetWorldPositionWithPositiveOffset(int x, int y)
    {
        return new Vector3(x * cellSize + originPosition.x + cellSize / 2, y * cellSize + originPosition.y + cellSize / 2, 0);
    }


    public Vector2Int GetGridSize()
    {
        return gridSize;
    }


    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition.x - originPosition.x) / cellSize);
        y = Mathf.FloorToInt((worldPosition.z - originPosition.y) / cellSize);
    }

    public void GetXY(Vector2 position, out int x, out int y)
    {
        x = Mathf.FloorToInt((position.x - originPosition.x) / cellSize);
        y = Mathf.FloorToInt((position.y - originPosition.y) / cellSize);
    }


    public TGridElement GetElement(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < gridSize.x && y < gridSize.y)
            return grid[x, y];
        else
            return default(TGridElement);
    }


    public Vector2 GetOriginPos()
    {
        return originPosition;
    }


    public float GetCellSize()
    {
        return cellSize;
    }


    public TGridElement this[int indexX, int indexY]
    {
        get
        {
            if (indexX >= 0 && indexY >= 0 && indexX < gridSize.x && indexY < gridSize.y)
                return grid[indexX, indexY];
            else
                return default(TGridElement);
        }
    }
}