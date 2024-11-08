using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public enum IconType { None, ExclamationPoint, InterrogationPoint };

public class QuestNPC : MonoBehaviour
{
	public IconType iconType;

	[SerializeField] private GameObject icon;

	[SerializeField] private Sprite exclamationPoint;
	[SerializeField] private Sprite questionMark;

	public void SetIcon(IconType iconType)
	{
		if (iconType == IconType.ExclamationPoint)
		{
			icon.GetComponent<SpriteRenderer>().sprite = exclamationPoint;
			icon.SetActive(true);
		}
		else if (iconType == IconType.InterrogationPoint)
		{
			icon.GetComponent<SpriteRenderer>().sprite = questionMark;
			icon.SetActive(true);
		}
		else
		{
			iconType = IconType.None;
			icon.SetActive(false);
		}
	}
}
