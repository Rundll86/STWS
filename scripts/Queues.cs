using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
public partial class Queues
{
	static QueueObject[][] queues;
	public static void AddToQueue(int queue, QueueObject body)
	{
		queues[queue] = queues[queue].Append(body).ToArray();
		body.queueID = queue;
	}
	public static void InsertToQueue(int queue, QueueObject body, int index)
	{
		Common.PrintJson(queues);
		List<QueueObject> list = queues[queue].ToList();
		list.Insert(index, body);
		queues[queue] = list.ToArray();
		body.queueID = queue;
	}
	public static void LeaveQueue(int queue, QueueObject body)
	{
		queues[queue] = queues[queue].Where(x => x != body).ToArray();
		body.queueID = -1;
	}
	public static QueueObject GetQueueObject(int queue, int index)
	{
		return queues[queue][index];
	}
	public static void Init()
	{
		queues = new QueueObject[6][];
		for (int i = 0; i < 6; i++)
		{
			queues[i] = Array.Empty<QueueObject>();
		}
	}
}
