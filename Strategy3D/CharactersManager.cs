using System.Collections.Generic;
using UnityEngine;

public class CharactersManager : MonoBehaviour
{
    // 객체
    public Transform charactersParent; // 모든 캐릭터 객체의 부모 객체 Transform

    // 모든 캐릭터 데이터
    [HideInInspector]
    public List<Character> characters;

    void Start ()
    {
        // 맵 상의 모든 캐릭터 데이터를 가져옴
        // (charactersParent 아래의 모든 Character 컴포넌트를 검색하여 리스트에 저장)
        characters = new List<Character> ();
        charactersParent.GetComponentsInChildren (characters);
    }

    /// <summary>
    /// 지정된 위치에 존재하는 캐릭터 데이터를 검색하여 반환
    /// </summary>
    /// <param name="xPos">X 위치</param>
    /// <param name="zPos">Z 위치</param>
    /// <returns>대상의 캐릭터 데이터</returns>
    public Character GetCharacterDataByPos (int xPos, int zPos)
    {
        // 검색 처리
        // (foreach로 맵 내의 모든 캐릭터 데이터를 하나씩 동일한 처리를 수행)
        foreach (Character charaData in characters)
        {
            // 캐릭터의 위치가 지정된 위치와 일치하는지 확인
            if ((charaData.xPos == xPos) && // X 위치가 같음
                (charaData.zPos == zPos)) // Z 위치가 같음
            {// 위치가 일치함
                return charaData; // 데이터를 반환하고 종료
            }
        }
        // 데이터를 찾지 못하면 null을 반환
        return null;
    }
    /// <summary>
	/// 지정한 캐릭터 삭제하기
	/// </summary>
	/// <param name="charaData">対象キャラデータ</param>
	public void DeleteCharaData (Character charaData)
	{
		// 목록에서 데이터 삭제
		characters.Remove (charaData);
		// 객체 삭제
		Destroy (charaData.gameObject);
	}
}