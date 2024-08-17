using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
public partial class Queues : Node2D
{
	static EntityController[][] queues;
	public static void AddToQueue(int queue, EntityController body)
	{
		queues[queue] = queues[queue].Append(body).ToArray();
	}
	public static void InsertToQueue(int queue, EntityController body, int index)
	{
		List<EntityController> list = queues[queue].ToList();
		list.Insert(index, body);
		queues[queue] = list.ToArray();
	}
	public static void LeaveQueue(int queue, EntityController body)
	{
		queues[queue] = queues[queue].Where(x => x != body).ToArray();
	}
	public override void _Ready()
	{
		queues = new EntityController[6][];
		for (int i = 0; i < 6; i++)
		{
			queues[i] = Array.Empty<EntityController>();
		}
	}
	public override void _Process(double delta)
	{
	}
}
