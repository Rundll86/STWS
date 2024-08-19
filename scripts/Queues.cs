using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
public class Queues
{
	static QueueObject[] queues;
	public static void AddToQueue(int queue, QueueObject body)
	{
		queues[queue] = body;
	}
	public static void LeaveQueue(int queue)
	{
		queues[queue] = null;
	}
	public static QueueObject GetQueueObject(int queue)
	{
		return queues[queue];
	}
	public static void Init()
	{
		queues = new QueueObject[6];
	}
}
