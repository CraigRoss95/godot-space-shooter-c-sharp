using System;
using Godot;

public static class EventManager
{
	public static Action<Vector2> FireLaserEvent;
	public static Action MeteorImpactEvent;

	public static void BrodcastFireLaserEvent(Vector2 position)
	{
		FireLaserEvent?.Invoke(position);
	}

	public static void BrodcastMeteorImpact()
	{
		MeteorImpactEvent?.Invoke();
	}


}