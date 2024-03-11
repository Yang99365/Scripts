using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBlock : MonoBehaviour
{
    // 강조 표시 객체
    private GameObject selectionBlockObj;

    // 강조 표시 머티리얼
	[Header ("강조 표시 머티리얼 : 선택 시")]
	public Material selMat_Select; // 선택 시
	[Header ("강조 표시 머티리얼 : 도달 가능")]
	public Material selMat_Reachable; // 캐릭터가 닿을 수 있는 경우
	[Header ("강조 표시 머티리얼: 공격 가능")]
	public Material selMat_Attackable; // 캐릭터가 공격할 수 있습니다.
	// 블록의 강조 표시 모드를 정의한다(열거형).
	public enum Highlight
	{
		Off, // 강조 표시 없음
		Select, // 선택 시
		Reachable, // 캐릭터가 접근할 수 있는 경우
		Attackable, // 캐릭터가 공격가능
	}

    // 블록 데이터
	[HideInInspector] // インスペクタ上で非表示にする属性
	public int xPos; // X 방향의 위치
	[HideInInspector]
	public int zPos; // Z 방향의 위치

    [Header ("통행가능 플래그(true이면 통행 가능)")]
	public bool passable; // 통행 가능 플래그
    void Start ()
    {
        // 강조 표시 객체를 가져옴
        selectionBlockObj = transform.GetChild (0).gameObject; // 첫 번째 자식 객체

        // 초기 상태에서는 강조 표시를 하지 않음
        SetSelectionMode (Highlight.Off);
    }

    /// <summary>
	///  선택 상태 표시 개체 표시/숨기기 설정하기
	/// </summary>
	/// <param name="mode">하이라이트 표시 모드</param>
	public void SetSelectionMode (Highlight mode)
	{
		switch (mode)
		{
			// 강조 표시 없음
			case Highlight.Off:
				selectionBlockObj.SetActive (false);
				break;
			// 선택 시
			case Highlight.Select:
				selectionBlockObj.GetComponent<Renderer> ().material = selMat_Select;
				selectionBlockObj.SetActive (true);
				break;
			// 캐릭터가 도달 가능
			case Highlight.Reachable:
				selectionBlockObj.GetComponent<Renderer> ().material = selMat_Reachable;
				selectionBlockObj.SetActive (true);
				break;
			// 캐릭터가 공격 가능
			case Highlight.Attackable:
				selectionBlockObj.GetComponent<Renderer> ().material = selMat_Attackable;
				selectionBlockObj.SetActive (true);
				break;
		}
	}
}