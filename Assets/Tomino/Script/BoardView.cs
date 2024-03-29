﻿using System;
using UnityEngine;
using Tomino;

public class BoardView : MonoBehaviour
{
    enum Layer
    {
        Blocks, PieceShadow, TargetOutline
    }

    public GameObject blockPrefab;
    public Sprite[] blockSprites;
    public Sprite shadowBlockSprite;
    public Sprite targetOutlineSprite;
    public TouchInput touchInput = new TouchInput();

    Board gameBoard;
    TargetOutline targetOutline;
    GameObjectPool<BlockView> blockViewPool;
    RectTransform rectTransform;

    public void SetBoard(Board board)
    {
        gameBoard = board;
        int size = board.width * board.height + 10;
        blockViewPool = new GameObjectPool<BlockView>(blockPrefab, size, gameObject);
    }

    public void RenderGameBoard()
    {
        blockViewPool.DeactivateAll();
        RenderPieceShadow();
        RenderBlocks();
        RenderTargetOutline();
    }

    void RenderBlocks()
    {
        foreach (var block in gameBoard.Blocks)
        {
            RenderBlock(BlockSprite(block.BlockNum), block.Position, Layer.Blocks);
        }
    }

    void RenderPieceShadow()
    {
        foreach (var position in gameBoard.GetPieceShadow())
        {
            RenderBlock(shadowBlockSprite, position, Layer.PieceShadow);
        }
    }

    void RenderTargetOutline()
    {
        foreach (var position in gameBoard.targetOutline.positions)
        {
            RenderBlock(targetOutlineSprite, position, Layer.TargetOutline);
        }
    }

    void RenderBlock(Sprite sprite, Position position, Layer layer)
    {
        var view = blockViewPool.GetAndActivate();
        view.SetSprite(sprite);
        view.SetSize(BlockSize());
        view.SetPosition(BlockPosition(position.Row, position.Column, layer));
    }

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        touchInput.blockSize = BlockSize();
        RenderGameBoard();
    }

    Vector3 BlockPosition(int row, int column, Layer layer)
    {
        var size = BlockSize();

        var numLayers = Enum.GetNames(typeof(Layer)).Length;
        var zLayer = (float)layer / (numLayers - 1);

        var position = new Vector3(column * size, row * size, zLayer);
        var offset = new Vector3(size / 2, size / 2, 0);
        return position + offset - PivotOffset();
    }

    public float BlockSize()
    {
        var boardWidth = rectTransform.rect.size.x;
        return boardWidth / gameBoard.width;
    }

    public Sprite BlockSprite(int value)
    {
        return blockSprites[value];
    }

    public Vector3 PivotOffset()
    {
        var pivot = rectTransform.pivot;
        var boardSize = rectTransform.rect.size;
        return new Vector3(boardSize.x * pivot.x, boardSize.y * pivot.y);
    }
}
