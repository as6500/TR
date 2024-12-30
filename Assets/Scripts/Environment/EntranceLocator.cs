using UnityEngine;

public enum EEntranceType
{
	INVALID = -1,
	BunkerOutside,
	RoadOutside,
	CityRoad,
	FarmRoad,
	CityBuilding,
	BuildingInsideBuilding,
	FarmBuilding
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
	Appartment1,
	Appartment2,
	Appartment3,
	Appartment4,
	Appartment5,
	Appartment6,
	Appartment7,
	Appartment8,
	Appartment9,
	Appartment10,
	Appartment11,
	Appartment12,
	Shop1,
	Shop2,
	Shop3,
	Shop4,
	Shop5,
	Shop6,
	Shop7,
	Barn,
	Windmill
}
public class EntranceLocator : MonoBehaviour
{
	[field: SerializeField] public EEntranceType entranceType
	{
		get;
		set;
	}

	[field: SerializeField] public EBuildings buildingType
	{
		get;
		set;
	}
}
