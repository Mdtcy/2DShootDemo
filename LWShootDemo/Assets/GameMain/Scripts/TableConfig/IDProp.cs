using Sirenix.OdinInspector;
using UnityEngine;

public class IDProp : SerializedScriptableObject
{
	[ReadOnly]
	[LabelText("ID")]
	[SerializeField]
	protected int id;

	[LabelText("默认名")]
	[PropertyOrder(-1)]
	[VerticalGroup("默认名")]
	public string DefaultName;
	
	public int ID
	{
		get
		{
			return id;
		}
		set
		{
			id = value;
		}
	}
}
