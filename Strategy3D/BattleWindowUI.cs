using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
 
public class BattleWindowUI : MonoBehaviour
{ //얘는 왜 gui "매니저" 처럼 매니저로 안만든거지..?
	// 전투 결과 표시 창 UI
	public TextMeshProUGUI nameText; // 이름 Text
	public Image hpGageImage; // HP게이지 Image
	public TextMeshProUGUI hpText; // HPText
	public TextMeshProUGUI damageText; // 데미지Text
 
	void Start ()
	{
		// 초기화 시 창 숨기기
		HideWindow ();
	}
 
	/// <summary>
	/// 전투 결과 창 표시하기
	/// </summary>
	/// <param name="charaData">공격당한 캐릭터의 데이터</param>
	/// <param name="damageValue">피해량</param>
	public void ShowWindow (Character charaData, int damageValue)
	{
		// 오브젝트 활성화
		gameObject.SetActive (true);
 
		// 이름 Text 표시
		nameText.text = charaData.charaName;
 
		// 데미지 계산 후 남은 HP를 구한다.
		// (여기서는 대상 캐릭터 데이터의 HP는 변경하지 않는다)
		int currentHP = charaData.currentHP - damageValue;
		// HP가 0~최대치 범위에 들어가도록 보정
		currentHP = Mathf.Clamp (currentHP, 0, charaData.maxHP);
 
		// HP 게이지 표시
		float ratio = (float)currentHP / charaData.maxHP;
		// 최대치에 대한 현재 HP의 비율을 게이지 Image의 fillAmount로 설정한다.
		hpGageImage.fillAmount = ratio;
 
		//  HPText 표시(현재 값과 최대값 모두 표시)
		hpText.text = currentHP + "/" + charaData.maxHP;
		// 피해량 Text 표시
		damageText.text = damageValue + "Damaged!";
	}
	/// <summary>
	/// 전투 결과 창 숨기기
	/// </summary>
	public void HideWindow ()
	{
		// 오브젝트 비활성화
		gameObject.SetActive (false);
	}
}