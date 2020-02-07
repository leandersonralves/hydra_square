using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public static Grid instance { get; private set; }

    public Cell[][] grid = new Cell[3][];

    int xEmpty, yEmpty = 2;

    public int sizeGrid;

    public GameObject prefabCell;

    public float distanceCell = 2f;

    void Awake ()
    {
        instance = this;
    }

    void Start ()
    {
        for(int i = 0; i < sizeGrid; i++)
        {
            grid[i] = new Cell[sizeGrid];
            for(int j = 0; j < sizeGrid && (j < sizeGrid - 1 || i < sizeGrid - 1); j++)
            {
                grid[i][j] = GameObject.Instantiate(prefabCell).GetComponent<Cell>();
                grid[i][j].transform.SetParent(transform);
                grid[i][j].transform.localPosition = new Vector3(i * distanceCell, -j * distanceCell, 0f);
                grid[i][j].name = grid[i][j].GetComponentInChildren<UnityEngine.UI.Text>().text = (i * sizeGrid + j).ToString("00");
            }
        }
    }

    public void OnMove (Cell cell)
    {
        int xEmpty, yEmpty, xCurrent, yCurrent = -1;
        if (EmptyNeighbor(cell, out xEmpty, out yEmpty, out xCurrent, out yCurrent))
        {
            cell.transform.localPosition = new Vector3(xEmpty * distanceCell, -yEmpty * distanceCell, 0f);
            grid[xEmpty][yEmpty] = cell;
            grid[xCurrent][yCurrent] = null;
        }
    }

    public bool EmptyNeighbor (Cell cellToCheck, out int xEmpty, out int yEmpty, out int xCurrent, out int yCurrent)
    {
        xEmpty = -1; yEmpty = -1; xCurrent = -1; yCurrent = -1;
        for(int i = 0; i < sizeGrid; i++)
        {
            for(int j = 0; j < sizeGrid; j++)
            {
                if (grid[i][j] == cellToCheck)
                {
                    xCurrent = i; yCurrent = j;
                    for(int k = -1; k < 2; k++)
                    {
                        if (k+i < 0 || !(k+i < sizeGrid)) continue;
                        for (int l = -1; l < 2; l++)
                        {
                            if (j+l < 0 || !(j+l < sizeGrid)) continue;
                            if (grid[i+k][j+l] == null)
                            {
                                if (Mathf.Abs(k) != Mathf.Abs(l))
                                {
                                    xEmpty = i+k;
                                    yEmpty = j+l;
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
        }

        return false;
    }
}
