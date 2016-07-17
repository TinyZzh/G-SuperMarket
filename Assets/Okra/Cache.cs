using UnityEngine;
using System.Collections;

public abstract class Cache : ICache{
	
	public abstract void init();
	
	public abstract void dispose();
}
