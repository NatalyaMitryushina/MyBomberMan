using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameFieldBase : MonoBehaviour {


    protected readonly int _columnCount = 19;
    protected readonly int _rowCount = 13;
	protected readonly int _breakWallsCount;

	public GameFieldBase()
	{
		_breakWallsCount = (_columnCount * _rowCount) / 7;
	}

	public GameFieldBase(int columnCount, int rowCount, int breakWallsCount){
		_columnCount = columnCount;
		_rowCount = rowCount;
		_breakWallsCount = (_columnCount * _rowCount) / 7;
	}

    public abstract void GenerateField();

}
