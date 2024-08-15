using Godot;
using System.Threading.Tasks;

public partial class ThreadSleep : Node2D
{
	public delegate void SleepCallback();
	public static async void CreateSleep(int millSeconds, SleepCallback callback)
	{
		await Task.Delay(millSeconds);
		callback();
	}
	public static Promise<int> SleepAsync(int millSeconds)
	{
		return new Promise<int>(async (resolve, reject) =>
		{
			await Task.Delay(millSeconds);
			resolve(0);
		});
	}
	public override void _Ready()
	{
	}
	public override void _Process(double delta)
	{
	}
}
