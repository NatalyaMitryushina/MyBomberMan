using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Point
{
	public int x;
	public int z;

	public Point()
	{
		x = 0;
		z = 0;
	}

	public Point(int x, int z)
	{
		// TODO: Complete member initialization
		this.x = x;
		this.z = z;
	}

	public Point(Vector3 p)
	{
		this.x = Convert.ToInt32(Math.Truncate(p.x));
		this.z = Convert.ToInt32(Math.Truncate(p.z));
	}

	public static bool operator ==(Point p1, Point p2)
	{
		if(System.Object.ReferenceEquals(p1, p2))
		{
			return true;
		}

		if(((object)p1 == null) || ((object)p2 == null))
		{
			return false;
		}
		return (p1.x == p2.x) && (p1.z == p2.z);
	}

	public static bool operator !=(Point p1, Point p2)
	{
		return !(p1 == p2);
	}
};