using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SwipeDirection { None = 0, Left = 1, Right = 2, Up = 4, Down = 8, LeftDown = 9, LeftUp = 5, RightDown = 10, RightUp = 6};

public class SwapeManager : MonoBehaviour {

    private static SwapeManager instance;
    public static SwapeManager Instance{get {return instance;}}

    Vector3 touchPosition;
    float swipeResistenceX = 50.0f;
    float swipeResistenceY = 100.0f;
    public SwipeDirection Direction {get; set;}

    void Start () {
        instance = this;
    }
	

	void Update () {
        Direction = SwipeDirection.None;

		if (Input.GetMouseButtonDown(0))
        {
            touchPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector2 deltaSwipe = touchPosition - Input.mousePosition;

            if (Mathf.Abs(deltaSwipe.x) > swipeResistenceX)
            {
                //Swipe on the X Axis
                Direction |= (deltaSwipe.x < 0) ? SwipeDirection.Right : SwipeDirection.Left;
            }

            if (Mathf.Abs(deltaSwipe.y) > swipeResistenceX)
            {
                //Swipe on the Y Axis
                Direction |= (deltaSwipe.y < 0) ? SwipeDirection.Up : SwipeDirection.Down;
            }
        }
	}

    public bool IsSwiping(SwipeDirection dir)
    {
        return (Direction & dir) == dir;
    }
}