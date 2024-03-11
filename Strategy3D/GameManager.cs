using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private MapManager mapManager;
    private CharactersManager charactersManager;
    private GUIManager guiManager; // GUI 관리 객체

    // 진행 관리 변수
	private Character selectingChara; // 선택 중인 캐릭터(아무도 선택하지 않았다면 false) 이안에 제너럴 넣음
    
    private List<MapBlock> reachableBlocks; // 선택 중인 캐릭터의 이동 가능한 블록 목록
    private List<MapBlock> attackableBlocks; // 선택 중인 캐릭터의 공격 가능한 블록 목록
	// 턴 진행 모드 목록
	private enum Phase
	{
		MyTurn_Start,       // 자신의 턴이 시작될 때
		MyTurn_Moving,      // 자신의 턴: 이동 대상 선택 중
		MyTurn_Command,     // 자신의 턴: 이동 후 명령 선택 중
		MyTurn_Targeting,   //  자신의 턴: 공격 대상 선택 중
		MyTurn_Result,      // 자신의 턴: 행동 결과 표시 중
		EnemyTurn_Start,    // 적의 턴: 적의 턴 시작 시
		EnemyTurn_Result    // 적의 턴: 행동 결과 표시 중
	}
	private Phase nowPhase; // 현재의 턴 진행 모드

    void Start()
    {
        // 컴포넌트 초기화
        mapManager = GetComponent<MapManager> ();
        charactersManager = GetComponent<CharactersManager> ();
        guiManager = GetComponent<GUIManager> ();

        // 리스트 초기화
        reachableBlocks = new List<MapBlock> (); 
        attackableBlocks = new List<MapBlock> ();

        nowPhase = Phase.MyTurn_Start; // 시작 시 진행 모드
    }
    
    void Update()
    {
        // 탭 감지 처리
        if (Input.GetMouseButtonDown (0))
        {// 탭이 발생했을 경우
            // 탭한 위치에 있는 블록을 가져와서 선택 처리를 시작한다
            if (guiManager.battleWindowUI.gameObject.activeInHierarchy)
			{
				// 전투 결과 표시 창을 닫습니다.
				guiManager.battleWindowUI.HideWindow ();
 
				// 진행 모드 진행 (디버깅용)
				ChangePhase (Phase.MyTurn_Start);
				return;
			}
            GetMapBlockByTapPos ();
        }
    }

    /// <summary>
    /// 탭한 위치에 있는 오브젝트를 찾아, 선택 처리 등을 시작한다
    /// </summary>
    private void GetMapBlockByTapPos ()
    {
        GameObject targetObject = null; // 탭 대상의 오브젝트

        // 탭한 방향으로 카메라에서 Ray를 발사한다
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        RaycastHit hit = new RaycastHit ();
        if (Physics.Raycast (ray, out hit))
        {// Ray에 맞는 위치에 존재하는 오브젝트를 가져온다(대상에 Collider가 부착되어 있어야 한다)
            targetObject = hit.collider.gameObject;
        }

        // 대상 오브젝트(맵 블록)가 존재하는 경우의 처리
        if (targetObject != null)
        {
            // 블록 선택 시 처리
            SelectBlock (targetObject.GetComponent<MapBlock> ());
        }
    }

    /// <summary>
    /// 지정한 블록을 선택 상태로 만드는 처리
    /// </summary>
    /// <param name="targetMapBlock">대상 블록 데이터</param>
    private void SelectBlock (MapBlock targetBlock)
    {
        // 현재 진행 모드에 따라 다른 처리 시작
		switch (nowPhase)
		{
			// 자신의 턴이 시작될 때
			case Phase.MyTurn_Start:
                // 모든 블록의 선택 상태 해제
				mapManager.AllSelectionModeClear ();
				// 대상 블록을 선택 상태로 표시

                // 선택된 위치에 있는 캐릭터의 데이터를 가져옵니다.
				targetBlock.SetSelectionMode (MapBlock.Highlight.Select);
				var charaData =
					charactersManager.GetCharacterDataByPos (targetBlock.xPos, targetBlock.zPos);
				if (charaData != null)
				{// 캐릭터가 존재함
					// 선택 중인 캐릭터 정보 저장
					selectingChara = charaData;
                    
                    // 캐릭터의 상태를 UI에 표시한다.
                    guiManager.ShowStatusWindow (selectingChara);

                    // 이동 가능한 위치 목록 가져오기
                    reachableBlocks = mapManager.SearchReachableBlocks (charaData.xPos, charaData.zPos);
                    // 이동 가능한 위치 목록 표시하기
					foreach (MapBlock mapBlock in reachableBlocks)
						mapBlock.SetSelectionMode (MapBlock.Highlight.Reachable);

					// 진행 모드 진행 : 이동처 선택 중
					ChangePhase (Phase.MyTurn_Moving);
				}
                else
                {// 캐릭터가 존재하지 않음
                // 선택 중인 캐릭터 정보 초기화
                ClearSelectingChara ();
                }
				break;

            // 자신의 턴: 이동 대상 선택 중
			case Phase.MyTurn_Moving:
                // 선택된 블록이 이동 가능한 위치 목록 내에 있는 경우 이동 처리 시작
                if (reachableBlocks.Contains (targetBlock))
                {
                    // 선택 중인 캐릭터를 이동시킨다.
				    selectingChara.MovePosition (targetBlock.xPos, targetBlock.zPos);

                    // 이동 가능한 위치 목록 초기화
                    reachableBlocks.Clear ();

                    // 모든 블록의 선택 상태 해제
				    mapManager.AllSelectionModeClear ();

                    // 명령어 버튼 표시하기
					guiManager.ShowCommandButtons ();
                    // 진행 모드 진행: 이동 후 명령어 선택 중
				    ChangePhase (Phase.MyTurn_Command);
                }							
				break;

                // 자신의 턴: 이동 후 명령 선택 중
			case Phase.MyTurn_Command:
				// 캐릭터 공격 처리
				//  (공격 가능한 블록을 선택했을 때 공격 처리 호출)
				if (attackableBlocks.Contains (targetBlock))
				{//  공격 가능한 블록을 탭했을 때
					// 공격 가능한 위치 목록을 초기화한다.
					attackableBlocks.Clear ();
					// 모든 블록의 선택 상태 해제
					mapManager.AllSelectionModeClear ();
 
					// 공격 대상 위치에 있는 캐릭터의 데이터를 가져옵니다.
					var targetChara =
						charactersManager.GetCharacterDataByPos (targetBlock.xPos, targetBlock.zPos);
					if (targetChara != null)
					{// 공격 대상 캐릭터가 존재함
						// 캐릭터 공격 처리
						CharaAttack (selectingChara, targetChara);
 
						// 진행 모드 진행(행동 결과 표시로 이동)
						ChangePhase (Phase.MyTurn_Result);
						return;
					}
					else
					{// 공격 대상이 존재하지 않음
						// 진행 모드 진행(적 턴으로 이동)
						ChangePhase (Phase.EnemyTurn_Start);
					}
				}
				break;
		}
    }

    /// <summary>
	/// 선택 중인 캐릭터 정보 초기화하기
	/// </summary>
	private void ClearSelectingChara ()
	{
		// 선택 중인 캐릭터를 초기화한다.
		selectingChara = null;
		//  캐릭터 상태 UI 숨기기
		guiManager.HideStatusWindow ();
	}


    /// <summary>
	/// 턴 진행 모드 변경하기
	/// </summary>
	/// <param name="newPhase">변경할 모드</param>
	private void ChangePhase (Phase newPhase)
	{
		//  모드 변경 저장
		nowPhase = newPhase;
	}

    
	public void AttackCommand () //공격후 커맨드 숨김
	{
		
		guiManager.HideCommandButtons ();
 
		// 공격 가능한 위치 목록 가져오기
		attackableBlocks = mapManager.SearchAttackableBlocks (selectingChara.xPos, selectingChara.zPos);
		// 공격 가능한 위치 목록 표시
		foreach (MapBlock mapBlock in attackableBlocks)
			mapBlock.SetSelectionMode (MapBlock.Highlight.Attackable);
	}
	public void StandbyCommand () // 턴종료
	{
		
		guiManager.HideCommandButtons ();
		
		ChangePhase (Phase.EnemyTurn_Start);
	}

    /// <summary>
	/// 캐릭터가 다른 캐릭터에게 공격하는 처리
	/// </summary>
	/// <param name="attackChara">공격 측 캐릭터 데이터</param>
	/// <param name="defenseChara">방어측 캐릭터 데이터</param>
	private void CharaAttack (Character attackChara, Character defenseChara)
	{
		// 데미지 계산 처리
		int damageValue; // 피해량
		int attackPoint = attackChara.atk; // 공격측의 공격력
		int defencePoint = defenseChara.def; // 방어측의 방어력
		//  피해량 = 공격력-방어력으로 계산
		damageValue = attackPoint - defencePoint;
		// 피해량이 0 이하인 경우 0으로 설정
		if (damageValue < 0)
			damageValue = 0;
 
		// 전투 결과 표시창 표시 설정
        // (HP 변경 전에 수행)
		guiManager.battleWindowUI.ShowWindow (defenseChara, damageValue);
 
		// 피해량만큼 방어측의 HP 감소 (만약 캐릭터가 반격이 있는경우 공격자에게도 일정량의 데미지를 주도록 개선)
		defenseChara.currentHP -= damageValue;
		// HP가 0~최대치 범위에 들어가도록 보정
		defenseChara.currentHP = Mathf.Clamp (defenseChara.currentHP, 0, defenseChara.maxHP);
 
		// HP가 0이 된 캐릭터 삭제
		if (defenseChara.currentHP == 0)
			charactersManager.DeleteCharaData (defenseChara);
	}

}