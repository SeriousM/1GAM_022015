using System;
using UnityEngine;

public class Mover : MonoBehaviour
{
	public SpotBase[] Spots = { new SingleSpot(new Vector2(0, 0)) };

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
}

[Serializable]
public class SpotBase
{

}

[Serializable]
public class SingleSpot : SpotBase
{
	public SingleSpot(Vector2 point)
	{
		Point = point;
	}
	public Vector2 Point;
}

[Serializable]
public class SpotGroup : SpotBase
{
	public SingleSpot[] Points = { };
}