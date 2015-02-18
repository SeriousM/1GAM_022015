using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Mover))]
public class MoverEditor : Editor
{
	private Mover mover;
	private SerializedProperty spotsProp;

	public void OnEnable()
	{
		mover = (Mover)target;
		spotsProp = serializedObject.FindProperty("Spots");
	}

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		for (int i = 0; i < spotsProp.arraySize; i++)
		{
			if (mover.Spots[i] is SpotGroup)
			{
				DrawSpotGroup((SpotGroup)mover.Spots[i]);
			}
			else if (mover.Spots[i] is SingleSpot)
			{
				DrawSingleSpot(i, (SingleSpot)mover.Spots[i]);
			}
		}

		if (GUILayout.Button("Clear"))
		{
			// button is clicked
			mover.Spots = new SpotBase[] { };
		}

		if (GUILayout.Button("Add Spot"))
		{
			// button is clicked
			mover.Spots = new List<SpotBase>(mover.Spots) { new SingleSpot(new Vector2(0, 0)) }.ToArray();
		}

		return;




		for (int i = 0; i < spotsProp.arraySize; i++)
		{
			var spotBase = spotsProp.GetArrayElementAtIndex(i);
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("asd" + i);
			if (GUILayout.Button("Remove Spot"))
			{
				// button is clicked
				mover.Spots = new List<SpotBase>(mover.Spots) { new SingleSpot(new Vector2(0, 0)) }.ToArray();
			}
			EditorGUILayout.EndHorizontal();
		}

		if (GUILayout.Button("Add Spot"))
		{
			// button is clicked
			mover.Spots = new List<SpotBase>(mover.Spots) { new SingleSpot(new Vector2(0, 0)) }.ToArray();
		}
		if (GUILayout.Button("Add Group"))
		{
			// button is clicked
			mover.Spots = new List<SpotBase>(mover.Spots) { new SingleSpot(new Vector2(0, 0)) }.ToArray();
		}
	}

	private void DrawSpotGroup(SpotGroup spotGroup)
	{
		//throw new System.NotImplementedException();
	}

	private void DrawSingleSpot(int index, SingleSpot singleSpot)
	{
		EditorGUILayout.BeginHorizontal();
		singleSpot.Point = EditorGUILayout.Vector2Field(index + ":", singleSpot.Point);
		if (GUILayout.Button("-"))
		{
			mover.Spots = mover.Spots.RemoveAt(index);
		}
		EditorGUILayout.EndHorizontal();
	}

	public void OnSceneGUI()
	{
		return;
		if (spotsProp == null) return;

		serializedObject.Update();

		var handleSize = HandleUtility.GetHandleSize(mover.transform.position) * 0.4f;
		for (int i = 0; i < spotsProp.arraySize; i++)
		{
			var prop = spotsProp.GetArrayElementAtIndex(i).vector2Value;
			var newPos = Handles.FreeMoveHandle(prop, Quaternion.identity, handleSize, Vector3.one * 0.05f, Handles.CircleCap);
			Handles.Label(prop, (i + 1).ToString());
			spotsProp.GetArrayElementAtIndex(i).vector2Value = newPos;
		}

		serializedObject.ApplyModifiedProperties();
	}
}

public static class Helper
{
	public static T[] RemoveAt<T>(this T[] source, int index)
	{
		T[] dest = new T[source.Length - 1];
		if (index > 0)
			Array.Copy(source, 0, dest, 0, index);

		if (index < source.Length - 1)
			Array.Copy(source, index + 1, dest, index, source.Length - index - 1);

		return dest;
	}
}