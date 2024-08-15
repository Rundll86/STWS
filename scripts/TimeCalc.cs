using Godot;
using System;

public partial class TimeCalc : Node2D
{
	public static long StartTime;
	public static long RunningTime = 0;
	public static long StepMultipiler = 1;
	public static long GetTimeSeconds()
	{
		return DateTime.Now.Ticks / 10000000;
	}
	public static void StepSeconds()
	{
		RunningTime += StepMultipiler;
		ThreadSleep.SleepAsync(1000).Then(e => { StepSeconds(); return null; });
	}
	public override void _Ready()
	{
		StartTime = GetTimeSeconds();
		StepSeconds();
	}
}
