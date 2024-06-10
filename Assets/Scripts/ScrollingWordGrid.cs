using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.ConstrainedExecution;
using Unity.Collections;
using UnityEngine;

public class ScrollingWordGrid : MonoBehaviour
{
  [SerializeField] GameObject PlayerCharacter;
  Transform playerTransform;
  List<List<GameObject>> tileMapChunks;
  const int TILE_SIZE = 32;
  const int CAMERA_FOV = 10;
  const int MAP_LOAD_LOOKAHEAD = 2;
  const float timeToUpdate = 1;
  private float currTime = 1;
  void Awake()
  {
    playerTransform = PlayerCharacter.GetComponent<Transform>();
    tileMapChunks = new List<List<GameObject>>();
    for (int i = 0; i < 3; i++)
    {
      tileMapChunks.Add(new List<GameObject>());
      for (int j = 0; j < 3; j++)
      {
        tileMapChunks[i].Add(transform.GetChild(i * 3 + j).gameObject);
      }
    }
  }
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    // only update the map every 1 second to save computation time
    currTime -= Time.deltaTime;
    if (currTime <= 0)
    {
      UpdateTileMapPositions();
      currTime = timeToUpdate;
    }
  }


  // if gridpos of playerpos + camera + offset != gridpos of playerpos
  // move the "wrapped" tile into gridpos of playerpos + camera + offset 
  // wrapped 
  private void UpdateTileMapPositions()
  {
    Vector3 currGridPosition = GetGridPosition(playerTransform.position);
    // can be optimized by getting direction an using the only directions, also can use for loops.
    Vector3 lookAheadUpGridPos = GetGridPosition(new(playerTransform.position.x,
      playerTransform.position.y + CAMERA_FOV + MAP_LOAD_LOOKAHEAD));
    Vector3 lookAheadDownGridPos = GetGridPosition(new(playerTransform.position.x,
      playerTransform.position.y - CAMERA_FOV - MAP_LOAD_LOOKAHEAD));
    Vector3 lookAheadLeftGridPos = GetGridPosition(new(playerTransform.position.x - CAMERA_FOV - MAP_LOAD_LOOKAHEAD,
      playerTransform.position.y));
    Vector3 lookAheadRightGridPos = GetGridPosition(new(playerTransform.position.x + CAMERA_FOV + MAP_LOAD_LOOKAHEAD,
      playerTransform.position.y));
    Vector3 lookAheadUpRight = GetGridPosition(new(playerTransform.position.x + CAMERA_FOV + MAP_LOAD_LOOKAHEAD,
      playerTransform.position.y + CAMERA_FOV + MAP_LOAD_LOOKAHEAD));
    Vector3 lookAheadDownRight = GetGridPosition(new(playerTransform.position.x + CAMERA_FOV + MAP_LOAD_LOOKAHEAD,
      playerTransform.position.y - CAMERA_FOV - MAP_LOAD_LOOKAHEAD)); ;
    Vector3 lookAheadUpLeft = GetGridPosition(new(playerTransform.position.x - CAMERA_FOV - MAP_LOAD_LOOKAHEAD,
      playerTransform.position.y + CAMERA_FOV + MAP_LOAD_LOOKAHEAD)); ;
    Vector3 lookAheadDownLeft = GetGridPosition(new(playerTransform.position.x - CAMERA_FOV - MAP_LOAD_LOOKAHEAD,
      playerTransform.position.y - CAMERA_FOV - MAP_LOAD_LOOKAHEAD)); ;

    if (currGridPosition != lookAheadUpGridPos)
    {
      Vector3 nextTile = GetGridPosCircularIndex(lookAheadUpGridPos);
      Vector3 nextTileWorldPos = GetWorldPosition(lookAheadUpGridPos);
      tileMapChunks[(int)nextTile.x][(int)nextTile.y].transform.position = nextTileWorldPos;
    }
    if (currGridPosition != lookAheadDownGridPos)
    {
      Vector3 nextTile = GetGridPosCircularIndex(lookAheadDownGridPos);
      Vector3 nextTileWorldPos = GetWorldPosition(lookAheadDownGridPos);
      tileMapChunks[(int)nextTile.x][(int)nextTile.y].transform.position = nextTileWorldPos;
    }
    if (currGridPosition != lookAheadLeftGridPos)
    {
      Vector3 nextTile = GetGridPosCircularIndex(lookAheadLeftGridPos);
      Vector3 nextTileWorldPos = GetWorldPosition(lookAheadLeftGridPos);
      tileMapChunks[(int)nextTile.x][(int)nextTile.y].transform.position = nextTileWorldPos;
    }
    if (currGridPosition != lookAheadRightGridPos)
    {
      Vector3 nextTile = GetGridPosCircularIndex(lookAheadRightGridPos);
      Vector3 nextTileWorldPos = GetWorldPosition(lookAheadRightGridPos);
      tileMapChunks[(int)nextTile.x][(int)nextTile.y].transform.position = nextTileWorldPos;
    }
    if (currGridPosition != lookAheadUpRight)
    {
      Vector3 nextTile = GetGridPosCircularIndex(lookAheadUpRight);
      Vector3 nextTileWorldPos = GetWorldPosition(lookAheadUpRight);
      tileMapChunks[(int)nextTile.x][(int)nextTile.y].transform.position = nextTileWorldPos;
    }
    if (currGridPosition != lookAheadDownRight)
    {
      Vector3 nextTile = GetGridPosCircularIndex(lookAheadDownRight);
      Vector3 nextTileWorldPos = GetWorldPosition(lookAheadDownRight);
      tileMapChunks[(int)nextTile.x][(int)nextTile.y].transform.position = nextTileWorldPos;
    }
    if (currGridPosition != lookAheadUpLeft)
    {
      Vector3 nextTile = GetGridPosCircularIndex(lookAheadUpLeft);
      Vector3 nextTileWorldPos = GetWorldPosition(lookAheadUpLeft);
      tileMapChunks[(int)nextTile.x][(int)nextTile.y].transform.position = nextTileWorldPos;
    }
    if (currGridPosition != lookAheadDownLeft)
    {
      Vector3 nextTile = GetGridPosCircularIndex(lookAheadDownLeft);
      Vector3 nextTileWorldPos = GetWorldPosition(lookAheadDownLeft);
      tileMapChunks[(int)nextTile.x][(int)nextTile.y].transform.position = nextTileWorldPos;
    }
  }

  Vector3 GetGridPosCircularIndex(Vector3 vec3)
  {
    int xRem = (int)vec3.x % 3;
    int yRem = (int)vec3.y % 3;
    return new(xRem < 0 ? xRem + 3 : xRem, yRem < 0 ? yRem + 3 : yRem, 0);
  }

  Vector3 GetWorldPosition(Vector3 gridPos)
  {
    int x = (int)((gridPos.y - 1) * 32);
    int y = (int)((1 - gridPos.x) * 32);
    return new(x, y, 0);
  }

  // note: swaps x and y because we'll be using these for the array
  Vector3 GetGridPosition(Vector3 pos)
  {
    return new Vector3(GetGridPositionY(pos.y), GetGridPositionX(pos.x), 0);
  }

  float GetWorldPositionY(int gridPos)
  {
    return gridPos * TILE_SIZE + 32;
  }

  float GetWorldPositionX(int gridPos)
  {
    return gridPos * TILE_SIZE - 32;
  }

  int GetGridPositionY(float position)
  {
    return (int)Math.Ceiling((position - 48) / TILE_SIZE) * -1;
  }

  int GetGridPositionX(float position)
  {
    return (int)Math.Floor((position + 48) / TILE_SIZE);
  }


}
