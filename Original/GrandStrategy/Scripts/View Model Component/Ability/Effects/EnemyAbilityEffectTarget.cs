using UnityEngine;
using System.Collections;
public class EnemyAbilityEffectTarget : AbilityEffectTarget 
{
	Alliance alliance;
	void Start ()
	{
		alliance = GetComponentInParent<Alliance>();
	}
	public override bool IsTarget (Tile tile)
	{
		if (tile == null || tile.content == null)
			return false;
		Alliance other = tile.content.GetComponentInChildren<Alliance>();
		return alliance.IsMatch(other, Targets.Foe);
	}
}
// 이 스크립트는 적의 능력이 특정 타일을 대상으로 할 수 있는지를 판단하는 데 사용된다. 