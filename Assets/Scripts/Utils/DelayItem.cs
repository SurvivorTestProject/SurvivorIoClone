using System;

public struct DelayItem
{
	public float timeDelay;

	public Action function;

	public DelayItem(float timeDelay, Action function)
	{
		this.timeDelay = timeDelay;
		this.function = function;
	}
}
