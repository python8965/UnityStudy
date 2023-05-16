
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum MinoType
{
	L,
	J,
	Z,
	S,
	O,
	I,
	T,
}

public enum Rotation
{
	None = 0,
	One = 1,
	Two = 2,
	Three = 3,
}

struct Cell
{
	public Color _color
	{
		private set => _color = value;
		get => _color;
	}

	Cell(Color color)
	{
		_color = color;
	}

	public bool IsEmpty()
	{
		return _color == Color.clear;
	}
}

public class Position
{
	private int _x;
	public int X
	{
		get => _x;
		set
		{
			_x = value;
		}
	}

	private int _y;
	public int Y{
		get => _y;
		set
		{
			_y = value;
		}
	}
	public Position()
	{

	}
	public Position(int x, int y)
	{
		X = x;
		Y = y;
	}

	public static Position operator +(Position t1, Position t2)
	{
		return new Position(t1.X + t2.X, t1.Y + t2.Y);
	}

	public Position Moved(int dx, int dy)
	{
		return this + new Position(dx, dy);
	}

	public Position Translate(int dx, int dy, Rotation rot)
	{

		(dx, dy) = rot switch
		{

			Rotation.None => ( dx,  dy),
			Rotation.One => (dy, -dx),
			Rotation.Two => (-dx, -dy),
			Rotation.Three => (-dy,  dx),
			_ => throw new ArgumentOutOfRangeException(nameof(rot), rot, null)
		};

		return this.Moved(dx, dy);

		// +x -> -y, -y -> -x, -x -> +y, +y -> -x
	}
}
public class Mino
{
	private Position pos;
	private MinoType _type;
	private Rotation rotation = Rotation.None;

	public Mino(MinoType type)
	{
		_type = type;
		pos = new Position(TetrisBoard.BoardY, TetrisBoard.BoardX);
	}

	public void Rotate(bool direction)
	{
		rotation = direction ? rotation + 1 : rotation - 1;
		rotation = (Rotation)((int)rotation % 4);
	}

	public bool Tick()
	{
		Debug.Log("Tick");
		Debug.Log(pos.Y);
		pos.Y -= 1;
		return pos.Y <= 0;
	}

	public Position Position()
	{
		return pos;
	}
	public Position[] get_shape()
	{
		return _type switch
		{
			MinoType.L => new[]
				{ pos.Translate(-1,1,rotation),pos.Translate(-1, 0, rotation), pos, pos.Translate(1,0,rotation)},
			MinoType.J => new[]
				{ pos.Translate(1, 1, rotation), pos.Translate(-1, 0, rotation), pos, pos.Translate(1, 0, rotation) },
			MinoType.Z => new[]
				{ pos.Translate(-1, 1, rotation), pos.Translate(0, 1, rotation), pos, pos.Translate(1, 0, rotation) },
			MinoType.S => new[]
				{ pos.Translate(-1, 0, rotation), pos.Translate(0, 1, rotation), pos, pos.Translate(1, 1, rotation) },
			MinoType.O => new[]
				{ pos.Translate(0, 1, rotation), pos.Translate(1,0, rotation), pos, pos.Translate(1, 1, rotation) },
			MinoType.I => new[]
				{ pos.Translate(-1, 0, rotation), pos, pos.Translate(1, 0, rotation), pos.Translate(2, 0, rotation) },
			MinoType.T => new[]
				{ pos.Translate(0, 1, rotation), pos.Translate(-1, 0, rotation), pos, pos.Translate(1, 0, rotation) },
			_ => throw new ArgumentOutOfRangeException()
		};
	}
}

public class TetrisBoard : MonoBehaviour
{
	public const int BoardY = 10;
	public const int BoardX = 20 + 7;

	private Cell[,] board = new Cell[BoardY,BoardX];
	private List<GameObject> printCubes = new();
	private Mino current_mino;
	private static readonly int Color1 = Shader.PropertyToID("_color");

	private bool isMinoSpawn = true;

	private bool waitingTick = false;
	private GameObject boxObject;
	void Start()
	{
		boxObject = GameObject.Find("Box");
	}

	void Print()
	{
		foreach (var cube in printCubes)
		{
			Destroy(cube);
		}

		for (var y = 0; y < board.GetLength(1);y++)
		{
			for (var x = 0; x > board.GetLength(0); x++)
			{
				var cell = board[x, y];

				if (!cell.IsEmpty())
				{
					var cell_object = Instantiate(boxObject);
					cell_object.gameObject.transform.position = new Vector3(x, y, 0.0f);
					var meshRenderer = cell_object.gameObject.GetComponent<MeshRenderer>();
					meshRenderer.material.SetColor(0, cell._color);
					printCubes.Add(cell_object);
				}
			}
		}

		foreach (var pos in current_mino.get_shape())
		{
			var cell = Instantiate(boxObject);
			cell.gameObject.transform.position = new Vector3(pos.X, pos.Y, 0.0f);
			var meshRenderer = cell.gameObject.GetComponent<MeshRenderer>();
				meshRenderer.material.SetColor(0, Color.black);
			printCubes.Add(cell);
		}
	}

	MinoType NextMino()
	{
		return MinoType.T;
	}

	void PlaceNewMino()
	{
		
	}

	void Tick()
	{
		Debug.Log("TickInBoard");
		if (current_mino.Tick())
		{
			PlaceNewMino();
		}
		Print();
		waitingTick = false;
	}
	void Update()
	{
		if (isMinoSpawn)
		{
			current_mino = new Mino(NextMino());
			isMinoSpawn = false;
		}
		
		if (!waitingTick)
		{
			Invoke(nameof(Tick), 1.0f);
			
			waitingTick = true;
		}
		
		
		
		if (Input.GetKeyDown(KeyCode.Z))
		{
			Debug.Log("Z");
		}

		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			this.current_mino.Rotate(true);
			Print();
		}
	}
}

public class TetrisGame : MonoBehaviour
{
	private TetrisBoard _tetrisBoard;


	float GetLength(float x1, float y1, float x2, float y2)
	{
		float width = x2 - x1;
		float height = y2 - y1;

		return (float)Math.Sqrt(width * width + height * height);
	}

	// Start is called before the first frame update
	void Start()
	{
		_tetrisBoard = gameObject.AddComponent<TetrisBoard>();
	}

	// Update is called once per frame
	void Update()
	{
		BroadcastMessage("NewMinoSpawn", MinoType.I);
	}

	void NewMinoSpawn(MinoType type)
	{

	}
}
