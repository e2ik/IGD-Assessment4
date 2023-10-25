
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject tile_1;
    public GameObject tile_2;
    public GameObject tile_3;
    public GameObject tile_4;
    public GameObject tile_5;
    public GameObject tile_6;
    public GameObject tile_7;
    
    private int[,] levelMap =
    {   // OG maze
        {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
        {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
        {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
        {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
        {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
        {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
        {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
        {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
        {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
        {0,0,0,0,0,0,5,0,0,0,4,0,0,0},

        //SR unsolvable test?
// {1,2,2,2,2,2,2,2,7},
// {2,5,5,5,5,5,5,5,4},
// {2,5,3,4,4,4,3,5,4},
// {2,6,4,0,0,0,4,5,4},
// {2,5,3,4,4,4,3,5,3},
// {2,5,5,5,5,5,5,5,5},
// {2,5,3,4,4,3,4,4,4},
// {2,5,3,4,4,3,4,4,3},
// {2,5,5,5,5,5,5,5,4},
// {1,2,2,2,2,4,3,0,4},

        // cookie test
        // {1,2,2,2,2,2,2,2,2,2,2,2,2,1},
        // {2,0,0,0,0,0,0,0,0,0,0,0,0,2},
        // {2,0,0,3,3,0,0,0,3,4,3,0,0,2},
        // {2,0,3,3,3,3,0,0,4,0,3,3,0,2},
        // {2,0,4,0,3,3,0,0,3,3,3,3,0,2},
        // {2,0,3,4,3,0,0,0,0,3,3,0,0,2},
        // {2,0,0,0,0,0,0,0,0,0,0,0,0,2},
        // {2,0,0,0,0,0,0,0,0,0,0,0,0,2},
        // {2,0,0,3,4,3,0,0,0,3,3,0,0,2},
        // {2,0,3,3,0,4,0,0,3,3,3,3,0,2},
        // {2,0,3,3,3,3,0,0,3,3,0,4,0,2},
        // {2,0,0,3,3,0,0,0,0,3,4,3,0,2},
        // {2,0,0,0,0,0,0,0,0,0,0,0,0,2},
        // {1,2,2,2,2,2,2,2,2,2,2,2,2,1},

        // variant with pellets (works)
        // {1,2,2,2,2,2,2,2,2,2,2,2,2,1},
        // {2,0,5,5,5,5,0,5,5,5,5,5,0,2},
        // {2,5,5,3,3,5,5,5,3,4,3,5,5,2},
        // {2,5,3,3,3,3,5,5,4,0,3,3,5,2},
        // {2,5,4,0,3,3,5,5,3,3,3,3,5,2},
        // {2,5,3,4,3,5,5,5,5,3,3,5,5,2},
        // {2,5,5,5,5,5,0,0,5,5,5,5,0,2},
        // {2,5,5,5,5,5,5,0,5,5,5,5,0,2},
        // {2,5,5,3,4,3,5,5,5,3,3,5,5,2},
        // {2,5,3,3,0,4,5,5,3,3,3,3,5,2},
        // {2,5,3,3,3,3,5,5,3,3,0,4,5,2},
        // {2,5,5,3,3,5,5,5,5,3,4,3,5,2},
        // {2,0,5,5,5,5,0,0,5,5,5,5,5,2},
        // {1,2,2,2,2,2,2,2,2,2,2,2,2,1},

        // cookie test 2
        // {1,2,2,2,2,2,7,1,1,2,1},
        // {2,0,0,0,0,0,3,7,7,4,7},
        // {2,0,0,3,3,0,0,1,1,0,2},
        // {2,0,3,3,3,3,0,0,0,0,2},
        // {2,0,4,0,3,3,0,0,0,0,2},
        // {2,0,3,4,3,0,0,0,0,0,2},
        // {2,0,0,0,0,0,0,0,0,0,2},
        // {1,2,2,2,2,2,2,2,2,2,1},

        // cookie test 3
        // {1,2,1,0,1,2,7,1,1,2,1},
        // {2,0,0,0,0,0,3,7,7,4,7},
        // {2,0,0,0,3,3,0,1,1,0,1},
        // {2,0,0,3,3,3,3,0,0,0,0},
        // {2,0,0,4,0,3,3,0,0,0,0},
        // {1,1,0,3,4,3,0,0,0,0,0},
        // {2,1,0,0,0,0,0,0,0,0,0},
        // {0,0,0,0,0,0,0,0,0,0,0},

        // cookie test 4
        // {1,2,1,0,1,2,7,1,1,2,1},
        // {2,0,0,0,0,0,3,7,7,4,7},
        // {2,0,0,0,0,0,0,2,2,0,2},
        // {2,0,0,0,3,3,0,1,1,0,1},
        // {2,0,0,3,3,3,3,0,0,0,0},
        // {1,1,0,4,0,3,3,0,0,0,0},
        // {2,1,0,3,4,3,0,0,0,0,0},
        // {0,0,0,0,0,0,0,0,0,0,0},

        // cookie test 6
        // {1,2,1,0,1,2,7,1,1,2,1},
        // {2,0,0,0,0,0,3,7,7,4,7},
        // {2,0,0,0,0,0,0,7,7,0,2},
        // {2,0,0,0,3,3,0,1,1,0,1},
        // {2,0,0,3,3,3,3,0,0,0,0},
        // {1,1,0,4,0,3,3,0,0,0,0},
        // {2,1,0,3,4,3,0,0,0,0,3},
        // {0,0,0,0,0,0,3,4,0,4,3},
        // {0,0,0,0,0,0,4,0,0,0,0},

        // testing Ts on sides
        // {1,2,2,2,2,2,2,2,2,2,2,2,2,1},
        // {2,5,5,5,5,5,5,5,5,5,5,5,5,2},
        // {2,5,3,4,4,3,5,5,5,5,5,3,4,7},
        // {2,6,4,0,0,4,5,5,5,5,5,3,4,7},
        // {2,5,3,4,4,3,5,5,5,5,5,5,5,1},
        // {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
        // {2,5,5,5,5,5,5,3,4,4,3,5,5,5},
        // {2,5,5,5,5,5,5,4,3,4,3,5,5,5},
        // {2,5,3,3,5,5,5,4,4,5,5,5,5,3},
        // {1,2,7,7,2,1,5,4,3,4,4,3,0,4},
        // {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
        // {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
        // {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
        // {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
        // {0,0,0,0,0,0,5,0,0,0,4,0,0,0},
        
        // checking T junctions and outside corners
        // {1,2,2,2,1,1,2,2,2,2,2,2,2,2},
        // {2,5,5,5,2,2,5,5,5,5,5,5,5,5},
        // {1,2,1,5,2,2,5,3,4,4,4,3,5,3},
        // {1,2,1,5,1,1,5,4,0,0,0,4,5,4},
        // {2,5,5,5,5,5,5,3,4,4,4,3,5,3},
        // {7,4,4,4,3,5,5,5,5,5,5,5,5,5},
        // {2,0,0,0,4,5,3,4,3,5,3,3,5,3},
        // {7,4,4,4,3,5,3,4,3,5,3,3,5,4},
        // {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
        // {2,5,1,1,5,5,5,3,4,4,4,3,0,4},
        // {2,5,2,2,5,5,5,4,3,4,4,3,0,3},
        // {1,2,1,1,2,1,5,4,4,0,0,0,0,0},
        // {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
        // {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
        // {0,0,0,0,0,0,5,0,0,0,4,0,0,0},

        // checking outer wall T shape
        // {1,2,2,2,2,2,1,1,2,2,2,2,2,7},
        // {2,5,5,5,5,5,2,2,5,5,5,5,5,3},
        // {2,5,5,5,5,1,1,1,1,5,5,5,5,5},
        // {2,5,5,5,5,1,2,2,1,5,5,3,4,4},
        // {2,5,1,1,5,5,5,5,5,5,5,3,4,4},
        // {1,2,1,2,5,5,5,5,5,5,5,5,5,5},
        // {1,2,1,2,5,5,3,4,3,5,3,3,5,3},
        // {2,5,1,1,5,5,3,4,3,5,3,3,5,4},
        // {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
        // {2,5,1,1,5,5,5,3,4,4,4,3,0,4},
        // {2,5,2,2,5,5,5,4,3,4,4,3,0,3},
        // {1,2,1,1,2,1,5,4,4,0,0,0,0,0},
        // {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
        // {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
        // {0,0,0,0,0,0,5,0,0,0,4,0,0,0},

        // checking Ts from bottom
        // {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
        // {2,5,5,5,5,5,5,5,5,5,5,5,5,3},
        // {7,4,4,4,4,3,5,3,4,4,4,3,5,5},
        // {7,4,4,4,4,3,5,4,0,0,0,4,5,3},
        // {2,5,5,5,5,5,5,3,4,4,4,3,5,3},
        // {2,5,5,3,3,5,5,5,5,5,5,5,5,5},
        // {2,5,3,3,4,5,3,4,3,5,3,3,5,3},
        // {2,5,4,3,3,5,3,4,3,5,3,3,5,4},
        // {2,5,4,4,5,5,5,5,5,5,5,5,5,4},
        // {1,2,7,7,2,1,5,5,5,5,3,3,0,4},
        // {0,0,0,0,0,2,5,3,3,5,3,3,0,3},
        // {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
        // {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
        // {2,2,2,2,2,1,5,4,4,0,4,0,0,0},
        // {0,0,0,0,0,0,5,4,4,0,4,0,0,0},

        // Ts and pluses
        // {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
        // {2,5,5,5,5,5,5,5,5,5,5,5,5,3},
        // {2,5,3,3,5,5,5,3,4,4,4,3,5,5},
        // {7,4,3,3,3,5,5,4,0,0,0,4,5,3},
        // {7,4,3,3,3,5,5,3,4,4,4,3,5,3},
        // {2,5,3,3,5,5,5,5,5,5,5,5,5,5},
        // {2,5,5,5,5,5,3,4,3,5,3,3,5,3},
        // {2,5,3,3,5,5,3,4,3,5,3,3,5,4},
        // {2,5,4,4,5,5,5,5,5,5,5,5,5,4},
        // {1,2,7,7,2,1,5,3,4,4,4,3,0,4},
        // {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
        // {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
        // {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
        // {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
        // {0,0,0,0,0,0,5,0,0,0,4,0,0,0},

        // just for fun - outside walls
        // {0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        // {0,0,0,1,2,2,1,1,2,2,1,0,0,0},
        // {0,0,1,1,5,5,2,2,5,5,1,2,1,0},
        // {0,0,2,5,5,1,1,1,1,5,5,5,2,0},
        // {0,1,1,5,5,1,1,1,1,5,5,1,1,0},
        // {0,2,5,5,5,5,1,1,5,5,5,2,0,0},
        // {0,1,2,1,5,5,5,5,5,5,5,1,2,2},
        // {0,1,2,1,5,5,5,5,5,5,5,0,0,0},
        // {0,2,5,5,5,5,5,5,5,5,5,1,2,2},
        // {0,2,5,1,2,2,1,5,5,5,5,2,0,0},
        // {0,2,5,1,1,1,1,5,5,5,5,1,1,0},
        // {0,2,5,5,2,2,5,5,5,5,5,5,2,0},
        // {0,1,2,2,1,1,1,0,1,2,1,5,2,0},
        // {0,0,0,0,0,0,2,0,2,0,1,2,1,0},
        // {0,0,0,0,0,0,2,0,2,0,0,0,0,0},

        // test if connecter is at top rather than side
        // {0,0,0,0,0,0,0,0,0,0,0,0,2,5},
        // {0,0,0,1,2,2,1,1,2,2,1,0,2,5},
        // {0,0,1,1,5,5,2,2,5,5,2,0,2,5},
        // {0,0,2,5,5,1,1,1,1,5,1,2,1,5},
        // {0,1,1,5,5,1,2,2,1,5,5,5,5,5},
        // {0,2,5,5,5,5,5,5,5,5,5,1,2,2},
        // {0,1,2,1,5,5,5,5,5,5,5,1,2,2},
        // {0,1,2,1,5,5,5,5,5,5,5,0,0,0},
        // {0,2,5,5,5,5,5,5,5,5,5,1,2,2},
        // {0,2,5,1,2,2,1,5,5,5,5,2,0,0},
        // {0,2,5,1,1,1,1,5,5,5,5,1,1,0},
        // {0,2,5,5,2,2,5,5,5,5,5,5,2,0},
        // {0,1,2,2,1,1,1,0,1,2,1,5,2,0},
        // {0,0,0,0,0,0,2,0,2,0,1,2,1,0},
        // {0,0,0,0,0,0,2,0,2,0,0,0,0,0},

        // {1,2,7,2,7,2,2,2,2,2,2,2,2,7},
        // {2,5,4,0,4,5,5,5,5,5,5,5,5,4},
        // {2,5,3,4,3,5,3,4,4,4,4,3,5,4},
        // {2,5,5,5,5,5,4,3,4,3,0,4,5,4},
        // {2,5,3,4,3,5,3,3,5,3,4,3,5,3},
        // {2,5,3,4,3,5,5,5,5,5,5,5,5,5},
        // {2,5,5,5,5,5,3,4,3,5,3,4,4,4},
        // {2,5,3,3,5,5,3,4,3,5,3,4,4,3},
        // {2,5,4,4,5,5,5,5,5,5,5,5,5,4},
        // {1,2,7,7,2,1,5,3,4,4,4,3,0,4},
        // {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
        // {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
        // {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
        // {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
        // {0,0,0,0,0,0,5,0,0,0,4,0,0,0},

        // this map has a 7 connecting to outer piece
        // so don't worry too much
        // {1,2,7,7,2,7,7,2,2,2,2,2,2,7},
        // {2,5,2,2,5,1,1,5,5,5,5,5,5,4},
        // {2,5,1,1,5,5,5,5,5,3,4,4,4,3},
        // {2,5,5,5,5,5,3,3,5,3,4,4,4,3},
        // {2,5,3,3,5,5,3,3,5,5,5,5,5,3},
        // {2,5,3,3,5,5,5,5,3,4,4,3,5,5},
        // {2,5,5,5,5,5,3,4,3,0,0,3,4,4},
        // {2,5,1,1,5,5,3,4,4,4,4,4,4,3},
        // {2,5,2,2,5,5,5,5,5,5,5,5,5,4},
        // {1,2,7,7,2,1,5,3,4,4,4,3,0,4},
        // {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
        // {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
        // {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
        // {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
        // {0,0,0,0,0,0,5,0,0,0,4,0,0,0},

        // testing 3s
        // {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
        // {2,5,5,5,5,5,5,5,5,5,5,5,5,2},
        // {2,5,5,3,3,5,5,5,5,3,3,5,3,7},
        // {2,5,3,3,3,3,5,5,3,3,4,5,3,7},
        // {2,5,3,3,3,3,5,5,3,3,4,5,5,1},
        // {2,5,5,3,3,5,5,5,5,3,3,5,5,5},
        // {2,5,5,5,5,5,3,4,3,5,5,5,5,5},
        // {2,5,3,3,5,5,3,4,3,5,5,5,5,3},
        // {2,5,4,4,5,5,5,5,5,5,5,5,5,4},
        // {1,2,7,7,2,1,5,3,4,4,4,3,0,4},
        // {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
        // {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
        // {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
        // {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
        // {0,0,0,0,0,0,5,0,0,0,4,0,0,0},

        // zigzag
        // {1,2,2,2,2,1,5,1,2,2,2,2,2,1},
        // {2,5,5,5,5,5,5,5,5,5,5,5,5,2},
        // {2,5,5,5,5,5,5,5,5,3,4,3,5,2},
        // {2,5,5,5,5,5,5,5,3,3,3,3,5,2},
        // {2,5,5,5,5,5,5,3,3,3,3,5,5,2},
        // {2,5,5,5,5,5,3,3,3,3,5,5,5,1},
        // {1,5,5,5,5,3,3,3,3,5,5,5,5,5},
        // {5,5,5,5,3,3,3,3,5,5,5,5,5,5},
        // {1,5,5,3,3,3,3,5,5,5,5,5,5,1},
        // {2,5,3,3,3,3,5,5,5,5,5,5,5,2},
        // {2,5,3,4,3,5,5,5,5,5,5,5,5,2},
        // {2,5,5,5,5,5,5,5,5,5,5,5,5,2},
        // {2,5,5,5,5,5,5,5,5,5,5,5,5,2},
        // {2,5,5,5,5,5,5,5,5,5,5,5,5,2},
        // {1,2,2,2,2,1,5,5,1,2,2,2,2,1},

        //zigzag without pellets
        // {1,2,2,2,2,1,5,1,2,2,2,2,2,1},
        // {2,5,5,5,5,5,5,5,0,0,0,0,0,2},
        // {2,5,5,5,5,5,5,0,0,3,4,3,0,2},
        // {2,5,5,5,5,5,0,0,3,3,3,3,0,2},
        // {2,5,5,5,5,0,0,3,3,3,3,0,0,2},
        // {2,5,5,5,0,0,3,3,3,3,0,0,5,1},
        // {1,5,5,0,0,3,3,3,3,0,0,5,5,5},
        // {5,5,0,0,3,3,3,3,0,5,5,5,5,5},
        // {1,0,0,3,3,3,3,0,0,5,5,5,5,1},
        // {2,0,3,3,3,3,0,0,5,5,5,5,5,2},
        // {2,0,3,4,3,0,0,5,5,5,5,5,5,2},
        // {2,0,0,0,0,0,5,5,5,5,5,5,5,2},
        // {2,5,5,5,5,5,5,5,5,5,5,5,5,2},
        // {2,5,5,5,5,5,5,5,5,5,5,5,5,2},
        // {1,2,2,2,2,1,5,5,1,2,2,2,2,1},

        // test different size (just to see if generating places correctly)
        // {1,2,2,2,2,2,2,2,2,2,2,2,7},
        // {2,5,5,5,5,5,5,5,5,5,5,5,4},
        // {2,5,5,5,3,4,3,5,5,5,5,5,4},
        // {2,5,5,3,3,3,3,5,5,5,5,5,4},
        // {2,5,3,3,3,3,5,5,5,5,5,5,3},
        // {2,5,3,4,3,5,5,5,5,5,5,5,5},
        // {2,5,5,5,5,5,5,5,5,5,5,5,5},
        // {2,5,5,5,5,5,5,5,5,5,5,5,1},
        // {1,2,2,2,2,1,5,5,1,2,2,2,1},

        // connected pluses smol size
        // {1,2,2,2,2,2,2,2,2,2,2,2,2},
        // {2,5,5,5,5,5,5,5,5,5,5,5,5},
        // {2,5,5,3,3,5,3,3,5,3,3,5,3},
        // {2,5,3,3,3,4,3,3,4,3,3,4,3},
        // {2,5,3,3,3,4,3,3,4,3,3,4,3},
        // {2,5,5,3,3,5,3,3,5,3,3,5,3},
        // {2,5,5,5,5,5,5,5,5,5,5,5,5},
        // {1,2,2,2,2,1,5,1,2,2,2,2,2},
        // {0,0,0,0,0,2,5,2,0,0,0,0,0},

        // super smol level
        // {1,2,2,2,2,2,2},
        // {2,5,5,5,5,5,5},
        // {2,5,5,3,3,5,5},
        // {2,5,5,4,4,5,5},

    };
    public int numRows { get; private set; }
    public int numCols { get; private set; }
    private int[] outside = {2,1,7};
    private int[] inside = {3,4};
    private int[] nonWall = {-1, 0, 5, 6};
    private GameObject topLeftQuad;
    private GameObject bottomLeftQuad;

    void Start()
    {
        GameObject level01 = GameObject.Find("Level01");
        if (level01 != null)
        {
            Destroy(level01);
        }
        numRows = levelMap.GetLength(0);
        numCols = levelMap.GetLength(1);
        GenerateLevel();
    }

    bool outsidePieceFoundIn(int cell) {
        foreach (int i in outside) {
            if (cell == i) { return true; }
        }
        return false;
    }

    bool insidePieceFoundIn(int cell) {
        foreach (int i in inside) {
            if (cell == i) { return true; }
        }
        return false;
    }

    bool nonWallFoundIn(int cell) {
        foreach (int i in nonWall) {
            if (cell == i) { return true; }
        }
        return false;
    }

    Quaternion getRotation_1(int cell_right, int cell_left, int cell_up, int cell_down, int cell_leftUp, int cell_leftDown, int cell_rightUp, int cell_rightDown) {

        // All cell 1 logic
        if (outsidePieceFoundIn(cell_left) && outsidePieceFoundIn(cell_down) && !outsidePieceFoundIn(cell_leftDown)) {
            return Quaternion.Euler(0,0,270);
        }

        if (outsidePieceFoundIn(cell_right) && outsidePieceFoundIn(cell_down) && !outsidePieceFoundIn(cell_rightDown)) {
            return Quaternion.Euler(0,0,0);
        }

        if (outsidePieceFoundIn(cell_left) && outsidePieceFoundIn(cell_up) && !outsidePieceFoundIn(cell_leftUp)) {
            return Quaternion.Euler(0,0,180);
        }

        if (outsidePieceFoundIn(cell_left) && outsidePieceFoundIn(cell_down) && !outsidePieceFoundIn(cell_leftDown)) {
            return Quaternion.Euler(0,0,270);
        }

        if (outsidePieceFoundIn(cell_right) && outsidePieceFoundIn(cell_down) && nonWallFoundIn(cell_rightDown)) {
            return Quaternion.Euler(0,0,0);
        }

        if (outsidePieceFoundIn(cell_left) && outsidePieceFoundIn(cell_down) && nonWallFoundIn(cell_leftDown)) {
            return Quaternion.Euler(0,0,270);
        }

        if (outsidePieceFoundIn(cell_right) && outsidePieceFoundIn(cell_up) && nonWallFoundIn(cell_rightUp)) {
            return Quaternion.Euler(0,0,90);
        }

        if (outsidePieceFoundIn(cell_left) && outsidePieceFoundIn(cell_up) && nonWallFoundIn(cell_leftUp)) {
            return Quaternion.Euler(0,0,180);
        }

        if (outsidePieceFoundIn(cell_up) && outsidePieceFoundIn(cell_right)) {
            return Quaternion.Euler(0,0,90);
        }

        if (outsidePieceFoundIn(cell_up) && outsidePieceFoundIn(cell_left)) {
            return Quaternion.Euler(0,0,180);
        }

        if (outsidePieceFoundIn(cell_down) && outsidePieceFoundIn(cell_right)) {
            return Quaternion.Euler(0,0,0);
        }

        if (outsidePieceFoundIn(cell_down) && outsidePieceFoundIn(cell_left)) {
            return Quaternion.Euler(0,0,270);
        }

        if (cell_up == -1) {
            if (outsidePieceFoundIn(cell_left)) {
                return Quaternion.Euler(0,0,180);
            } else {
                return Quaternion.Euler(0,0,90);
            }
        }

        if (cell_left == -1) {
            if (outsidePieceFoundIn(cell_up)) {
                return Quaternion.Euler(0,0,180);
            } else {
                return Quaternion.Euler(0,0,270);
            }
        }

        if (cell_right == -1) {
            if (outsidePieceFoundIn(cell_up)) {
                return Quaternion.Euler(0,0,90);
            } else {
                return Quaternion.Euler(0,0,0);
            }
        }

        if (cell_down == -1) {
            if (outsidePieceFoundIn(cell_left)) {
                return Quaternion.Euler(0,0,270);
            } else {
                return Quaternion.Euler(0,0,0);
            }
        }
        return Quaternion.identity;
    }

    Quaternion getRotation_2(int cell_right, int cell_left, int cell_up, int cell_down, int cell_leftUp, int cell_leftDown, int cell_rightUp, int cell_rightDown) {

        // All cell 2 logic
        if (outsidePieceFoundIn(cell_left) || outsidePieceFoundIn(cell_right)) {
            if (outsidePieceFoundIn(cell_up) && outsidePieceFoundIn(cell_down)) {
                return Quaternion.Euler(0,0,90);
            }
        }

        if (outsidePieceFoundIn(cell_up) || outsidePieceFoundIn(cell_down)) {
            if (outsidePieceFoundIn(cell_left) && outsidePieceFoundIn(cell_right)) {
                return Quaternion.Euler(0,0,0);
            }
        }

        if (nonWallFoundIn(cell_left) && nonWallFoundIn(cell_right)) {
            return Quaternion.Euler(0,0,90);
        } else {
            return Quaternion.Euler(0,0,0);
        }
    }


    Quaternion getRotation_3(int cell_right, int cell_left, int cell_up, int cell_down, int cell_leftUp, int cell_leftDown, int cell_rightUp, int cell_rightDown) {

        // All cell 3 logic 
        GameObject lastTopChild = null;

        if (topLeftQuad != null && topLeftQuad.transform.childCount > 0) {
            lastTopChild = topLeftQuad.transform.GetChild(topLeftQuad.transform.childCount - 1).gameObject;
        }

        Quaternion lastTopRotation = Quaternion.identity;

        if (lastTopChild != null) {
            lastTopRotation = lastTopChild.transform.rotation;
        }

        Quaternion _0 = Quaternion.Euler(0, 0, 0);
        Quaternion _90 = Quaternion.Euler(0, 0, 90);
        Quaternion _180 = Quaternion.Euler(0, 0, 180);
        Quaternion _270 = Quaternion.Euler(0, 0, 270);

        if (lastTopChild != null) {
            if (insidePieceFoundIn(cell_up) && insidePieceFoundIn(cell_down) && insidePieceFoundIn(cell_left) && insidePieceFoundIn(cell_right)) {
                if (lastTopRotation == _0 && nonWallFoundIn(cell_leftUp)) {
                    return _180;
                }
                if (lastTopRotation == _180 && nonWallFoundIn(cell_rightUp)) {
                    return _90;
                }
                if (lastTopRotation == _90 && nonWallFoundIn(cell_leftDown)) {
                    return _270;
                }
                if (lastTopRotation == _270 && nonWallFoundIn(cell_rightDown)) {
                    return _0;
                }
            }
        }

        if (insidePieceFoundIn(cell_up) && insidePieceFoundIn(cell_down) && insidePieceFoundIn(cell_right) && insidePieceFoundIn(cell_left)) {
            if (nonWallFoundIn(cell_rightDown) && cell_leftUp == 0) {
                return Quaternion.Euler(0,0,0);
            }
            if (nonWallFoundIn(cell_leftDown) && cell_rightUp == 0) {
                return Quaternion.Euler(0,0,270);
            }
            if (nonWallFoundIn(cell_rightUp) && cell_leftDown == 0) {
                return Quaternion.Euler(0,0,90);
            }
            if (nonWallFoundIn(cell_leftUp) && cell_rightDown == 0) {
                return Quaternion.Euler(0,0,180);
            }
            
            if (nonWallFoundIn(cell_rightUp) && insidePieceFoundIn(cell_leftUp)) {
                return Quaternion.Euler(0,0,90);
            }
            if (nonWallFoundIn(cell_leftUp) && insidePieceFoundIn(cell_rightUp)) {
                return Quaternion.Euler(0,0,180);
            }
            if (nonWallFoundIn(cell_rightDown) && insidePieceFoundIn(cell_leftDown)) {
                return Quaternion.Euler(0,0,0);
            }
            if (nonWallFoundIn(cell_leftDown) && insidePieceFoundIn(cell_rightDown)) {
                return Quaternion.Euler(0,0,270);
            }
        }



        if (insidePieceFoundIn(cell_up) && insidePieceFoundIn(cell_down) &&
            insidePieceFoundIn(cell_right) && insidePieceFoundIn(cell_left)) {
                if (nonWallFoundIn(cell_leftUp)) {
                    return Quaternion.Euler(0,0,180);
                }
                if (nonWallFoundIn(cell_leftDown)) {
                    return Quaternion.Euler(0,0,270);
                }
                if (nonWallFoundIn(cell_rightUp)) {
                    return Quaternion.Euler(0,0,90);
                }
                if (nonWallFoundIn(cell_rightUp)) {
                    return Quaternion.Euler(0,0,0);
                }
        }

        if (insidePieceFoundIn(cell_left) && insidePieceFoundIn(cell_up) && nonWallFoundIn(cell_leftUp)) {
            return Quaternion.Euler(0,0,180);
        }

        if (insidePieceFoundIn(cell_left) && insidePieceFoundIn(cell_down) && nonWallFoundIn(cell_leftDown)) {
            return Quaternion.Euler(0,0,270);
        }

        if (insidePieceFoundIn(cell_right) && insidePieceFoundIn(cell_up) && nonWallFoundIn(cell_rightUp)) {
            return Quaternion.Euler(0,0,90);
        }

        if (insidePieceFoundIn(cell_right) && insidePieceFoundIn(cell_down) && nonWallFoundIn(cell_rightDown)) {
            return Quaternion.Euler(0,0,0);
        }

        if (nonWallFoundIn(cell_left) && nonWallFoundIn(cell_down)) {
            return Quaternion.Euler(0,0,90);
        }

        if (nonWallFoundIn(cell_left) && nonWallFoundIn(cell_up)) {
            return Quaternion.Euler(0,0,0);
        }

        if (nonWallFoundIn(cell_right) && nonWallFoundIn(cell_down)) {
            return Quaternion.Euler(0,0,180);
        }

        if (nonWallFoundIn(cell_right) && nonWallFoundIn(cell_up)) {
            return Quaternion.Euler(0,0,270);
        }
        return Quaternion.identity;
    }

    Quaternion getRotation_4(int cell_right, int cell_left, int cell_up, int cell_down, int cell_leftUp, int cell_leftDown, int cell_rightUp, int cell_rightDown) {

        // All cell 4 logic
        
        if ((cell_down == -1 || cell_up == -1) && (insidePieceFoundIn(cell_up) || insidePieceFoundIn(cell_down))) {
            return Quaternion.Euler(0,0,0);
        }

        if ((cell_left == -1 || cell_right == -1) && (insidePieceFoundIn(cell_left) || insidePieceFoundIn(cell_right))) {
            return Quaternion.Euler(0,0,90);
        }

        if (outsidePieceFoundIn(cell_right) && outsidePieceFoundIn(cell_left)) {
            return Quaternion.Euler(0,0,90);
        }

        if (outsidePieceFoundIn(cell_up) && outsidePieceFoundIn(cell_down)) {
            return Quaternion.Euler(0,0,0);
        }

        if ((nonWallFoundIn(cell_up) || nonWallFoundIn(cell_down)) &&
            (insidePieceFoundIn(cell_left) || insidePieceFoundIn(cell_right))) {
            return Quaternion.Euler(0,0,90);
        }
        if ((nonWallFoundIn(cell_left) || nonWallFoundIn(cell_right)) &&
            (insidePieceFoundIn(cell_up) || insidePieceFoundIn(cell_down))) {
            return Quaternion.Euler(0,0,0);
        }
        return Quaternion.identity;
    }

    Quaternion getRotation_7(int cell_right, int cell_left, int cell_up, int cell_down, int cell_leftUp, int cell_leftDown, int cell_rightUp, int cell_rightDown) {

        // All cell 7 logic
        if (cell_up == -1) {
            return Quaternion.Euler(0,0,0);
        }

        if (cell_left == -1) {
            return Quaternion.Euler(0,0,90);
        }

        if (cell_right == -1) {
            return Quaternion.Euler(0,0,270);
        }

        if (cell_down == -1) {
            return Quaternion.Euler(0,0,180);
        }

        if (outsidePieceFoundIn(cell_up) && outsidePieceFoundIn(cell_down)) {
            if (nonWallFoundIn(cell_left) && outsidePieceFoundIn(cell_right)) {
                return Quaternion.Euler(0,0,90);
            }
            if (nonWallFoundIn(cell_right) && outsidePieceFoundIn(cell_left)) {
                return Quaternion.Euler(0,0,270);
            }
            if (!outsidePieceFoundIn(cell_left)) {
                return Quaternion.Euler(0,0,270);
            }
            if (!outsidePieceFoundIn(cell_right)) {
                return Quaternion.Euler(0,0,90);
            }
        }

        if (outsidePieceFoundIn(cell_left) && outsidePieceFoundIn(cell_right)) {
            if (nonWallFoundIn(cell_up) && outsidePieceFoundIn(cell_down)) {
                return Quaternion.Euler(0,0,0);
            }
            if (nonWallFoundIn(cell_down) && outsidePieceFoundIn(cell_up)) {
                return Quaternion.Euler(0,0,180);
            }
            if (!outsidePieceFoundIn(cell_up)) {
                return Quaternion.Euler(0,0,180);
            }
            if (!outsidePieceFoundIn(cell_down)) {
                return Quaternion.Euler(0,0,0);
            }
        }

        if (outsidePieceFoundIn(cell_left) || outsidePieceFoundIn(cell_right)) {
            // just incase for some reason outside is used to connect up to 7
            if (insidePieceFoundIn(cell_down) || outsidePieceFoundIn(cell_down)) {
                return Quaternion.Euler(0,0,0);
            }
            if (insidePieceFoundIn(cell_up) || outsidePieceFoundIn(cell_up)) {
                return Quaternion.Euler(0,0,180);
            }
        }

        if (outsidePieceFoundIn(cell_up) || outsidePieceFoundIn(cell_down)) {
            if (insidePieceFoundIn(cell_left) || outsidePieceFoundIn(cell_left)) {
                return Quaternion.Euler(0,0,270);
            }
            if (insidePieceFoundIn(cell_right) || outsidePieceFoundIn(cell_right)) {
                return Quaternion.Euler(0,0,90);
            }
        }
        return Quaternion.identity;
    }




    void GenerateLevel()
    {
        Vector3 startPos = new Vector3(-15f,10f,0.0f);
        GameObject level_1 = new GameObject("Level01_Generated");
        level_1.transform.position = startPos;
        topLeftQuad = new GameObject("TopLeftQuad");
        topLeftQuad.transform.parent = level_1.transform;
        topLeftQuad.transform.localPosition = Vector3.zero;

        for (int row = 0; row < numRows; row++) {
            for (int col = 0; col < numCols; col++) {
                int cell = levelMap[row, col];
                int cell_right = (col < numCols - 1) ? levelMap[row, col + 1] : -1;
                int cell_left = (col > 0) ? levelMap[row, col - 1] : -1;
                int cell_down = (row < numRows - 1) ? levelMap[row + 1, col] : -1;
                int cell_up = (row > 0) ? levelMap[row - 1, col] : -1;
                int cell_leftUp = (row > 0 && col > 0) ? levelMap[row - 1, col - 1] : -1;
                int cell_leftDown = (row < numRows - 1 && col > 0) ? levelMap[row + 1, col - 1] : -1;
                int cell_rightUp = (row > 0 && col < numCols - 1) ? levelMap[row - 1, col + 1] : -1;
                int cell_rightDown = (row < numRows - 1 && col < numCols - 1) ? levelMap[row + 1, col + 1] : -1;

                GameObject tile = GetSegment(cell);

                if (tile != null) {
                    Vector3 pos = level_1.transform.position + new Vector3(col, -row, 0);
                    GameObject segment = null;
                    Quaternion rotation = Quaternion.identity;

                    if (cell == 1) {
                        rotation = getRotation_1(cell_right, cell_left, cell_up, cell_down, cell_leftUp, cell_leftDown, cell_rightUp, cell_rightDown);
                    } else if (cell == 2) {
                        rotation = getRotation_2(cell_right, cell_left, cell_up, cell_down, cell_leftUp, cell_leftDown, cell_rightUp, cell_rightDown);
                    } else if (cell == 3) {
                        rotation = getRotation_3(cell_right, cell_left, cell_up, cell_down, cell_leftUp, cell_leftDown, cell_rightUp, cell_rightDown);
                    } else if (cell == 4) {
                        rotation = getRotation_4(cell_right, cell_left, cell_up, cell_down, cell_leftUp, cell_leftDown, cell_rightUp, cell_rightDown);
                    } else if (cell == 7) {
                        rotation = getRotation_7(cell_right, cell_left, cell_up, cell_down, cell_leftUp, cell_leftDown, cell_rightUp, cell_rightDown);
                    }
                    segment = Instantiate(tile, pos, rotation);

                    if (segment != null) {
                        segment.transform.parent = topLeftQuad.transform;
                    }
                }           
            }
        }

        bottomLeftQuad = Instantiate(topLeftQuad);
        bottomLeftQuad.name = "BottomLeftQuad";
        bottomLeftQuad.transform.parent = level_1.transform;
        bottomLeftQuad.transform.localPosition = Vector3.zero;
        int numChildToDestroy = 0;
        int lastRow = numRows - 1;
        int bottomLeftChildCount = bottomLeftQuad.transform.childCount;
        for (int col = 0; col < numCols; col++) {
            int cell = levelMap[lastRow, col];
            if (cell > 0) {
                numChildToDestroy++;
            }
        }
        for (int i = 0; i < numChildToDestroy && bottomLeftChildCount > 0; i++) {
            Transform lastChild = bottomLeftQuad.transform.GetChild(bottomLeftChildCount -1);
            Destroy(lastChild.gameObject);
            bottomLeftChildCount--;
        }

        // for (int row = 0; row < numRows - 1; row++) {
        //     for (int col = 0; col < numCols; col++) {
        //         int cell = levelMap[row, col];
        //         int cell_right = (col < numCols - 1) ? levelMap[row, col + 1] : -1;
        //         int cell_left = (col > 0) ? levelMap[row, col - 1] : -1;
        //         int cell_down = (row < numRows - 1) ? levelMap[row + 1, col] : -1;
        //         int cell_up = (row > 0) ? levelMap[row - 1, col] : -1;
        //         int cell_leftUp = (row > 0 && col > 0) ? levelMap[row - 1, col - 1] : -1;
        //         int cell_leftDown = (row < numRows - 1 && col > 0) ? levelMap[row + 1, col - 1] : -1;
        //         int cell_rightUp = (row > 0 && col < numCols - 1) ? levelMap[row - 1, col + 1] : -1;
        //         int cell_rightDown = (row < numRows - 1 && col < numCols - 1) ? levelMap[row + 1, col + 1] : -1;

        //         GameObject tile = GetSegment(cell);

        //         if (tile != null) {
        //             Vector3 pos = level_1.transform.position + new Vector3(col, -row, 0);
        //             GameObject segment = null;
        //             Quaternion rotation = Quaternion.identity;

        //             if (cell == 1) {
        //                 rotation = getRotation_1(cell_right, cell_left, cell_up, cell_down, cell_leftUp, cell_leftDown, cell_rightUp, cell_rightDown);
        //             } else if (cell == 2) {
        //                 rotation = getRotation_2(cell_right, cell_left, cell_up, cell_down, cell_leftUp, cell_leftDown, cell_rightUp, cell_rightDown);
        //             } else if (cell == 3) {
        //                 rotation = getRotation_3(cell_right, cell_left, cell_up, cell_down, cell_leftUp, cell_leftDown, cell_rightUp, cell_rightDown);
        //             } else if (cell == 4) {
        //                 rotation = getRotation_4(cell_right, cell_left, cell_up, cell_down, cell_leftUp, cell_leftDown, cell_rightUp, cell_rightDown);
        //             } else if (cell == 7) {
        //                 rotation = getRotation_7(cell_right, cell_left, cell_up, cell_down, cell_leftUp, cell_leftDown, cell_rightUp, cell_rightDown);
        //             }
        //             segment = Instantiate(tile, pos, rotation);

        //             if (segment != null) {
        //                 segment.transform.parent = bottomLeftQuad.transform;
        //             }
        //         }            
        //     }
        // }

        if (topLeftQuad != null) {
            //topRightQuad
            GameObject topRightQuad = Instantiate(topLeftQuad);
            topRightQuad.transform.parent = level_1.transform;
            topRightQuad.name = "TopRightQuad";
            Vector3 og_pos = topLeftQuad.transform.position;
            Vector3 og_scale = topLeftQuad.transform.localScale;
            topRightQuad.transform.position = new Vector3(og_pos.x + numCols*2-1, og_pos.y, og_pos.z);
            topRightQuad.transform.localScale = new Vector3(-og_scale.x, og_scale.y, og_scale.z);

            foreach (Transform child in topRightQuad.transform) {
                if (child.gameObject.name.ToLower().Contains("pellet")) {
                    Vector3 scale = child.localScale;
                    scale.x = -scale.x;
                    child.localScale = scale;
                }
            }
        } else {
            Debug.LogError("no topLeftQad");
        }
        
        if (bottomLeftQuad != null) {
            // FIX LATER
            bottomLeftQuad.transform.localScale = new Vector3(1, -1, 1);
            Vector3 mirror = bottomLeftQuad.transform.position;
            mirror.y = mirror.y - numRows*2+2;
            bottomLeftQuad.transform.position = mirror;

            foreach (Transform child in bottomLeftQuad.transform) {
                if (child.gameObject.name.ToLower().Contains("pellet")) {
                    Vector3 scale = child.localScale;
                    scale.y = -scale.y;
                    child.localScale = scale;
                }
            }

            //bottomRightQuad
            GameObject bottomRightQuad = Instantiate(bottomLeftQuad);
            bottomRightQuad.transform.parent = level_1.transform;
            bottomRightQuad.name = "BottompRightQuad";

            numChildToDestroy = 0;
            lastRow = numRows - 1;
            int bottomRightChildCount = bottomRightQuad.transform.childCount;
            for (int col = 0; col < numCols; col++) {
                int cell = levelMap[lastRow, col];
                if (cell > 0) {
                    numChildToDestroy++;
                }
            }
            for (int i = 0; i < numChildToDestroy && bottomRightChildCount > 0; i++) {
                Transform lastChild = bottomRightQuad.transform.GetChild(bottomRightChildCount -1);
                Destroy(lastChild.gameObject);
                bottomRightChildCount--;
            }

            Vector3 og_pos = bottomLeftQuad.transform.position;
            Vector3 og_scale = bottomLeftQuad.transform.localScale;
            bottomRightQuad.transform.position = new Vector3(og_pos.x + numCols*2-1, og_pos.y, og_pos.z);
            bottomRightQuad.transform.localScale = new Vector3(-og_scale.x, og_scale.y, og_scale.z);

            foreach (Transform child in bottomRightQuad.transform) {
                if (child.gameObject.name.ToLower().Contains("pellet")) {
                    Vector3 scale = child.localScale;
                    scale.x = -scale.x;
                    child.localScale = scale;
                }
            }
        } else {
            Debug.LogError("no bottomLeftQad");
        }
    }

    GameObject GetSegment(int cell) {
        switch (cell) {
            case 1: return tile_1;
            case 2: return tile_2;
            case 3: return tile_3;
            case 4: return tile_4;
            case 5: return tile_5;
            case 6: return tile_6;
            case 7: return tile_7;
            default: return null;
        }
    }
}