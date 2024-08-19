using Godot;
using System;
using FallingShrimp.FrontBridge;
public partial class TimeCalc : Node2D
{
	public static long StartTime;
	public static long RunningTime_Mill = 0;
	public static long RunningTime
	{
		get
		{
			return RunningTime_Mill / 1000;
		}
		set
		{
			RunningTime_Mill = value * 1000;
		}
	}
	public static int StepOne = 100;
	public static long StepMultipiler = 1;
	public static ProgressBar TSProgressBar;
	public static AnimationPlayer TSProgressAnimator;
	public static Label TSTip;

	public static AnimationPlayer TSTipAnimator;
	public static long GetTimeMillSeconds()
	{
		return DateTime.Now.Ticks / 10000;
	}
	public static void StepSeconds()
	{
		RunningTime_Mill += StepOne * StepMultipiler;
		ThreadSleep.SleepAsync(StepOne).Then(e => { StepSeconds(); return null; });
	}
	public static Promise<int> TimeAccelerates(int magnification, int time)
	{
		TSProgressBar.Value = TSProgressBar.MaxValue;
		TSTip.Text = $"时间加速{magnification}x！";
		TSTipAnimator.Play("join");
		TSProgressAnimator.Play("join");
		StepMultipiler = magnification;
		MethodCirculator(() =>
		{
			TSProgressBar.Value -= 1;
		}, time / 200, 200);
		return ThreadSleep.SleepAsync(time).Then(e =>
		{
			StepMultipiler = 1;
			TSTipAnimator.Play("leave");
			TSProgressAnimator.Play("leave");
			return ThreadSleep.SleepAsync(500);
		}).Then(e =>
		{
			TSProgressBar.Value = TSProgressBar.MaxValue;
			return Promise<int>.EmptyInitilizator(0);
		});
	}
	public static Promise<int> MethodCirculator(Action action, int time, int times, bool sleepFirst = false)
	{
		if (sleepFirst)
		{
			return ThreadSleep.SleepAsync(time).Then(e => { MethodCirculator(action, time, times); return null; });
		}
		if (times > 0)
		{
			action();
			return ThreadSleep.SleepAsync(time).Then(e => { MethodCirculator(action, time, times - 1); return null; });
		}
		else
		{
			return Promise<int>.EmptyInitilizator(0);
		};
	}
	public override void _Ready()
	{
		StartTime = GetTimeMillSeconds();
		StepSeconds();
		TSProgressBar = GetNode<ProgressBar>("/root/WorldController/CameraMain/TS Progress");
		TSProgressAnimator = TSProgressBar.GetNode<AnimationPlayer>("Animator");
		TSTip = GetNode<Label>("/root/WorldController/CameraMain/TS Tip");
		TSTipAnimator = TSTip.GetNode<AnimationPlayer>("Animator");
	}
}
