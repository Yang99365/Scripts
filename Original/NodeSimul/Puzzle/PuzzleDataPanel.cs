using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using System;

public class PuzzleDataPanel : MonoBehaviour
{
    [SerializeField] private Transform testCaseContainer;
    [SerializeField] private Button addTestCaseButton;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button openButton;

    [SerializeField] private GameObject testCaseItemPrefab;

    [SerializeField] private PUMPBackground background;

    [SerializeField] private PuzzleTestCasePanel testCasePanel;

    private List<TestCaseController> testCaseControllers = new List<TestCaseController>();
    public PuzzleData currentPuzzleData = new PuzzleData();

    public event Action<PuzzleData> OnPuzzleDataChanged;

    private void Start()
    {
        // ��ư �̺�Ʈ ����
        addTestCaseButton.onClick.AddListener(AddNewTestCase);
        saveButton.onClick.AddListener(SavePuzzleData);
        closeButton.onClick.AddListener(() => gameObject.SetActive(false));
        closeButton.onClick.AddListener(() => openButton.gameObject.SetActive(true));

    }

    public void AdjustTestCasesToBackground()
    {
        if (background == null)
        {
            background = FindAnyObjectByType<PUMPBackground>();
            if (background == null)
            {
                Debug.LogError("PUMPBackground not found. Cannot adjust test cases.");
                return;
            }
        }

        foreach (TestCaseController controller in testCaseControllers)
        {
            controller.AdjustToBackground(background);
        }
    }

    public void AddNewTestCase()
    {
        GameObject newTestCaseItem = Instantiate(testCaseItemPrefab, testCaseContainer);
        TestCaseController controller = newTestCaseItem.GetComponent<TestCaseController>();

        if (controller != null)
        {
            controller.Initialize(this);
            testCaseControllers.Add(controller);

            if (background == null)
            {
                background = FindAnyObjectByType<PUMPBackground>();
            }

            if (background != null)
            {
                controller.AdjustToBackground(background);
            }
        }
        else
        {
            Debug.LogError("�����鿡 TestCaseController ������Ʈ Ȯ�ο��");
        }
    }

    public void RemoveTestCase(TestCaseController controller)
    {
        if (testCaseControllers.Count <= 1)
        {
            Debug.LogWarning("�ּ� �ϳ��̻� �ʿ�");
            return;
        }

        testCaseControllers.Remove(controller);
        Destroy(controller.gameObject);
    }

    public void SavePuzzleData()
    {
        currentPuzzleData.testCases = new List<TestCase>();

        foreach (TestCaseController controller in testCaseControllers)
        {
            TestCase testCase = controller.GetTestCaseData();
            currentPuzzleData.testCases.Add(testCase);
        }

        OnPuzzleDataChanged?.Invoke(currentPuzzleData);
    }
    // ���� �׽�Ʈ ���̽� UI ����
    public void ClearTestCases()
    {
        foreach (var controller in testCaseControllers)
        {
            Destroy(controller.gameObject);
        }
        testCaseControllers.Clear();
    }
    // ���� �����ͷκ��� �׽�Ʈ ���̽� UI �ε�
    public void LoadFromPuzzleData(PuzzleData puzzleData)
    {
        // ���� UI ����
        ClearTestCases();

        // �� ������ ����
        UpdatePuzzleData(puzzleData);

        // �׽�Ʈ ���̽��� ������ �� ���̽� �ϳ� �߰�
        if (puzzleData.testCases == null || puzzleData.testCases.Count == 0)
        {
            AddNewTestCase();
            return;
        }

        // �׽�Ʈ ���̽� UI ����
        foreach (var testCase in puzzleData.testCases)
        {
            // �� �׽�Ʈ ���̽� UI �߰�
            GameObject newTestCaseItem = Instantiate(testCaseItemPrefab, testCaseContainer);
            TestCaseController controller = newTestCaseItem.GetComponent<TestCaseController>();

            if (controller != null)
            {
                controller.Initialize(this);
                controller.SetTestCaseData(testCase);
                testCaseControllers.Add(controller);
            }
        }
    }

    // �׽�Ʈ ���̽� �迭���� ���� UI ���� (PuzzleExtractor���� ���)
    public void LoadFromTestCases(List<TestCase> testCases)
    {
        LoadFromPuzzleData(new PuzzleData { testCases = testCases });
    }
    public void UpdatePuzzleData(PuzzleData newData)
    {
        currentPuzzleData = newData;
        OnPuzzleDataChanged?.Invoke(currentPuzzleData);
    }

}
