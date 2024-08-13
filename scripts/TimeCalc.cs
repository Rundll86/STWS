using Godot;
using System;

public partial class TimeCalc : Node2D
{
	public static long ExtraTime = 0;
	public static long StartTime;
	public static long RunningTime
	{
		get
		{
			return GetTimeSeconds() - StartTime + ExtraTime;
		}
	}
	public static long GetTimeSeconds()
	{
		return DateTime.Now.Ticks / 10000000;
	}
	public override void _Ready()
	{
		StartTime = GetTimeSeconds();
	}
}
