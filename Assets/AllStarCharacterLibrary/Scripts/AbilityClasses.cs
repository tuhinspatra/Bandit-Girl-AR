
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Ability
{
	public int weaponstate;
	[SerializeField]
	public List<AbilityCollision> collChecks;
}

[Serializable]
public class AbilityCollision
{
	public int type;			// 0=angleRange, 1=radial, 2=ray, 3=missile
	public Vector3 position;
	public Vector3 rotation;
	public float range;
	public float angle;
	public float speed;
	public float damage;
	public Transform missile;
}