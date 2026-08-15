using System;
using Godot;

public static class EventManager
{
	public static Action<Vector2> FireLaserEvent;

	public static void BrodcastFireLaserEvent(Vector2 position)
	{
		FireLaserEvent?.Invoke(position);
	}


}