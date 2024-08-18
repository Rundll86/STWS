using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
public partial class Queues : Node2D
{
	static QueueObject[][] queues;
	public static void AddToQueue(int queue, QueueObject body)
	{
		queues[queue] = queues[queue].Append(body).ToArray();
	}
	public static void InsertToQueue(int queue, QueueObject body, int index)
	{
		List<QueueObject> list = queues[queue].ToList();
		list.Insert(index, body);
		queues[queue] = list.ToArray();
	}
	public static void LeaveQueue(int queue, QueueObject body)
	{
		queues[queue] = queues[queue].Where(x => x != body).ToArray();
	}
	public override void _Ready()
	{
		queues = new QueueObject[6][];
		for (int i = 0; i < 6; i++)
		{
			queues[i] = Array.Empty<QueueObject>();
		}
	}
	public override void _Process(double delta)
	{
	}
}
