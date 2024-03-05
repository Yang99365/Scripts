using System.Collections.Generic;
using UnityEngine;

public class GeneralManager : MonoBehaviour
{
    public Dictionary<string, Generals> generals = new Dictionary<string, Generals>(); // 전체 무장 목록

    public Dictionary<string, List<Generals>> factionGenerals= new Dictionary<string, List<Generals>>(); // 세력별 무장 목록
    public List<Generals> userGenerals =new List<Generals>(); // 유저 무장 목록

    public FactionManager factionManager;
    [SerializeField]
    
    void Awake() {
        factionManager = FindObjectOfType<FactionManager>();
        LoadGeneralsFromJson();
    }
    public void Update() {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PrintGenerals();
        }
        if (Input.GetKeyDown(KeyCode.G))
            {

            }
    }

    void PrintGenerals()
    {
        //불러온 무장의 모든 정보 출력
        foreach (KeyValuePair<string, Generals> general in generals)
        {
            Debug.Log(general.Value.Name);
            Debug.Log(general.Value.Code);
            Debug.Log(general.Value.Faction);
        }
    }
    void LoadGeneralsFromJson()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Generals"); 
        string json = textAsset.text;
        JsonWrapper wrapper = JsonUtility.FromJson<JsonWrapper>(json);

        foreach (Generals general in wrapper.Generals)
        {
            generals.Add(general.Code, general); //전체 무장 목록에 무장들 추가

            // 세력별로 무장 추가
            if (!factionGenerals.ContainsKey(general.Faction))
            {
                factionGenerals.Add(general.Faction, new List<Generals>());
            }
            factionGenerals[general.Faction].Add(general);
        }
    }
    public void RemoveGeneralByExecution(string generalCode)
    {
        if (generals.ContainsKey(generalCode))
        {
            generals.Remove(generalCode); //전체 무장 목록에서 무장 제거
        }
    }
    public void RemoveGeneralByFire(int generalIndex)
    {
        if (userGenerals.Count > generalIndex)
        {
            Generals general = userGenerals[generalIndex]; //유저 무장 목록에서 무장 가져오기
            general.Faction = "Null"; //무장의 세력을 Null로 변경
            userGenerals.RemoveAt(generalIndex); //유저 무장 목록에서 무장 제거
        }
    }
    

    public void UpdateGeneralFaction(string generalCode, string newFaction)
    {
        if (generals.ContainsKey(generalCode))
        {
            generals[generalCode].Faction = newFaction;
        }
    }
    public List<Generals> GetGeneralsByFaction(string factionName)
    {
        return factionGenerals.ContainsKey(factionName) ? factionGenerals[factionName] : new List<Generals>();
    }

    [System.Serializable]
    private class JsonWrapper
    {
        public Generals[] Generals;
    }
}