using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; // UIコンポーネントを扱うのに必要

public class GUIManager : MonoBehaviour
{
	// 상태창 UI
	public GameObject statusWindow; // 상태창 오브젝트
	public TextMeshProUGUI nameText; // 이름 Text
	public Image TroopIcon; // 병종 아이콘 Image
	public Image hpGageImage; // HP게이지 Image
	public TextMeshProUGUI hpText; // HPText
	public TextMeshProUGUI atkText; // 공격력Text
	public TextMeshProUGUI defText; // 방어력Text
	// 병종 아이콘 이미지
	public Sprite[] sprites; // 병종 아이콘 스프라이트 배열

	public GameObject commandButtons;
	public BattleWindowUI battleWindowUI;
	
	void Start ()
	{
		// UI초기화
		HideStatusWindow (); // 상태창을 숨긴다.
		HideCommandButtons ();
	}

	/// <summary>
	/// 상태창을 표시한다
	/// </summary>
	/// <param name="charaData">표시할 캐릭터 데이터</param>
	public void ShowStatusWindow (Character charaData)
	{
		//  객체 활성화
		statusWindow.SetActive (true);

		// 이름 Text 표시
		nameText.text = charaData.charaName;

		// 병종 아이콘 표시 (개선해야함)
		switch (charaData.troopType)
        {
            case Troops.Infantry:
                TroopIcon.sprite = sprites[0];
                break;
            case Troops.Archer:
                TroopIcon.sprite = sprites[1];
                break;
            case Troops.Cavalry:
                TroopIcon.sprite = sprites[2];
                break;
        }
		// HP게이지 표시
		// 최대치에 대한 현재 HP의 비율을 게이지 Image의 fillAmount로 설정한다.
		float ratio = (float)charaData.currentHP / charaData.maxHP;
		hpGageImage.fillAmount = ratio;

		// HPText 표시(현재값과 최대값 모두 표시)
		hpText.text = charaData.currentHP + " / " + charaData.maxHP;
		// 공격력 Text 표시(int에서 string으로 변환)
		atkText.text = charaData.atk.ToString ();
		// 방어력 Text 표시(int에서 string으로 변환)
		defText.text = charaData.def.ToString ();
	}
	/// <summary>
	/// 상태창 숨기기
	/// </summary>
	public void HideStatusWindow ()
	{
		// 객체 비활성화
		statusWindow.SetActive (false);
	}

	public void ShowCommandButtons ()
	{
		commandButtons.SetActive (true);
	}
	
	public void HideCommandButtons ()
	{
		commandButtons.SetActive (false);
	}
}