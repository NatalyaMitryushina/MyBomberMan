using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameFieldBase : MonoBehaviour {


    protected readonly int _columnCount = 9;
    protected readonly int _rowCount = 11;
	protected readonly int _breakWallsCount;

	public GameFieldBase()
	{
		_breakWallsCount = (_columnCount * _rowCount) / 4;
	}

	public GameFieldBase(int columnCount, int rowCount, int breakWallsCount){
		_columnCount = columnCount;
		_rowCount = rowCount;
		_breakWallsCount = (_columnCount * _rowCount) / 4;
	}

    public abstract void GenerateField();

}
