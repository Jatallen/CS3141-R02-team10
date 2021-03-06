using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
{
    public override void SetPriority()
    {
        priority = 3;
    }

    public override List<Vector2> ListMoves()    //returns an array of the relative locations the piece can move
    {
        List<Vector2> moves = new List<Vector2>();
        float tile = GameController.tile;
        //Knight moves (defined as 2 1)
        moves.Add(new Vector2(tile, tile * 2));    //up right
        moves.Add(new Vector2(tile * 2, tile)); //right up
        moves.Add(new Vector2(tile * 2, tile * -1));    //right down
        moves.Add(new Vector2(tile, tile * -2)); //down right
        moves.Add(new Vector2(tile * -1, tile * -2));    //down left
        moves.Add(new Vector2(tile * -2, tile * -1));   //left down
        moves.Add(new Vector2(tile * -2, tile));   //left up
        moves.Add(new Vector2(tile * -1, tile * 2));   //up left

        return TestMoves(moves);
    }
}
