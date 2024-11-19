using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EEntranceType
{
	INVALID = -1,
	BunkerOutside,
	RoadOutside,
	CityRoad,
	CityBuilding,
	FarmRoad,
}

public enum EBuildings
{
	INVALID = -1,
	Building1,
	Building2,
	Building3,
	Building4,
	Building5,
	Building6,
	Building7,
	Building8,
	Building9,
	Building10,
	Building11,
	Building12,
	Building13,
	Building14,
	Building15,
}

public class EntranceLocator : MonoBehaviour
{
	[field: SerializeField] public EEntranceType entranceType
	{
		get;
		set;
	}
}
