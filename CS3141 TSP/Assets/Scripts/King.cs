using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : MonoBehaviour
{
    [SerializeField]
    private float tileSize;

    private Collider2D thisCollider;

    private Vector2 position;

    // Start is called before the first frame update
    void Start()
    {
        thisCollider = GetComponent<Collider2D>();
        
        GameController.tileSize = tileSize; 
    }

    // Update is called once per frame
    void Update()
    {
        position = new Vector2(transform.position.x, transform.position.y);

        if (Input.GetKeyDown(KeyCode.W))
            transform.position = position + GetComponent<King>().listMoves()[2];
        if (Input.GetKeyDown(KeyCode.A))
            transform.position = position + GetComponent<King>().listMoves()[4];
        if (Input.GetKeyDown(KeyCode.S))
            transform.position = position + GetComponent<King>().listMoves()[6];
        if (Input.GetKeyDown(KeyCode.D))
            transform.position = position + GetComponent<King>().listMoves()[0];
    }

    public List<Vector2> listMoves()    //returns an array of the relative locations the piece can move
    {
        List<Vector2> moves = new List<Vector2>();
        float tile = GameController.tileSize;
        //King moves
        moves.Add(new Vector2(tile, 0));    //right
        moves.Add(new Vector2(tile, tile)); //top right
        moves.Add(new Vector2(0, tile));    //top
        moves.Add(new Vector2(tile * -1, tile)); //top left
        moves.Add(new Vector2(tile * -1, 0));    //left
        moves.Add(new Vector2(tile * -1, tile * -1));   //bottom left
        moves.Add(new Vector2(0, tile * -1));   //bottom

        for(int i = 0; i < moves.Count; i++)
            if(!GameController.boardColl.OverlapPoint(position + moves[i]))
                moves.Remove(moves[i]);

        return moves;
    }
}
