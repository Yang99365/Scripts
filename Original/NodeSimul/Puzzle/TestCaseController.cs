using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestCaseController : MonoBehaviour
{
    [SerializeField] private Button removeButton;
    [SerializeField] private Transform inputTogglesContainer;
    [SerializeField] private Transform outputTogglesContainer;
    [SerializeField] private Button addInputToggleButton;
    [SerializeField] private Button removeInputToggleButton;
    [SerializeField] private Button addOutputToggleButton;
    [SerializeField] private Button removeOutputToggleButton;

    [SerializeField] private GameObject togglePrefab;

    private PuzzleDataPanel puzzlePanel;
    private PuzzleBackground puzzleBackground;

    private List<Toggle> inputToggles = new List<Toggle>();
    private List<Toggle> outputToggles = new List<Toggle>();

    public void Initialize(PuzzleDataPanel puzzlePanel)
    {
        this.puzzlePanel = puzzlePanel;

        // ��ư �̺�Ʈ ����
        removeButton.onClick.AddListener(() => puzzlePanel.RemoveTestCase(this));

        addInputToggleButton.onClick.AddListener(AddInputToggle);
        removeInputToggleButton.onClick.AddListener(RemoveInputToggle);

        addOutputToggleButton.onClick.AddListener(AddOutputToggle);
        removeOutputToggleButton.onClick.AddListener(RemoveOutputToggle);

    }
    public void AdjustToBackground(PUMPBackground background)
    {
        if (background == null)
            return;

        // ���� ��� ����
        ClearAllToggles();

        // External Input ������ �°� input ��� ����
        int inputCount = background.ExternalInput.GateCount;
        for (int i = 0; i < inputCount; i++)
        {
            AddInputToggle();
        }

        // External Output ������ �°� output ��� ����
        int outputCount = background.ExternalOutput.GateCount;
        for (int i = 0; i < outputCount; i++)
        {
            AddOutputToggle();
        }
    }
    public void AddInputToggle()
    {
        Toggle newToggle = CreateToggle(inputTogglesContainer);
        inputToggles.Add(newToggle);
        UpdateToggleLabels(inputToggles, "IN");
    }

    public void RemoveInputToggle()
    {
        if (inputToggles.Count <= 1)
        {
            Debug.LogWarning("Cannot remove the last input toggle!");
            return;
        }

        Toggle lastToggle = inputToggles[inputToggles.Count - 1];
        inputToggles.Remove(lastToggle);
        Destroy(lastToggle.gameObject);

        UpdateToggleLabels(inputToggles, "IN");
    }

    public void AddOutputToggle()
    {
        Toggle newToggle = CreateToggle(outputTogglesContainer);
        outputToggles.Add(newToggle);
        UpdateToggleLabels(outputToggles, "OUT");
    }

    public void RemoveOutputToggle()
    {
        if (outputToggles.Count <= 1)
        {
            Debug.LogWarning("Cannot remove the last output toggle!");
            return;
        }

        Toggle lastToggle = outputToggles[outputToggles.Count - 1];
        outputToggles.Remove(lastToggle);
        Destroy(lastToggle.gameObject);

        UpdateToggleLabels(outputToggles, "OUT");
    }

    private Toggle CreateToggle(Transform parent)
    {
        GameObject toggleObj = Instantiate(togglePrefab, parent);
        return toggleObj.GetComponent<Toggle>();
    }

    private void UpdateToggleLabels(List<Toggle> toggles, string prefix)
    {
        for (int i = 0; i < toggles.Count; i++)
        {
            TMPro.TextMeshProUGUI label = toggles[i].GetComponentInChildren<TMPro.TextMeshProUGUI>();
            if (label != null)
            {
                label.text = $"{prefix} {i}";
            }
        }
    }
    public TestCase GetTestCaseData()
    {
        TestCase testCase = new TestCase();

        testCase.ExternalInputStates = new List<bool>();
        foreach (Toggle toggle in inputToggles)
        {
            testCase.ExternalInputStates.Add(toggle.isOn);
        }

        testCase.ExternalOutputStates = new List<bool>();
        foreach (Toggle toggle in outputToggles)
        {
            testCase.ExternalOutputStates.Add(toggle.isOn);
        }

        return testCase;
    }
    // �׽�Ʈ ���̽� �����͸� UI�� ����
    public void SetTestCaseData(TestCase testCase)
    {
        // ���� UI ��� ����
        ClearAllToggles();

        // ��ǲ ��� ���� �� ����
        if (testCase.ExternalInputStates != null)
        {
            for (int i = 0; i < testCase.ExternalInputStates.Count; i++)
            {
                // �ʿ��� ��ŭ ��� �߰�
                if (i >= inputToggles.Count)
                {
                    AddInputToggle();
                }

                // ���� ����
                if (i < inputToggles.Count)
                {
                    inputToggles[i].isOn = testCase.ExternalInputStates[i];
                }
            }
        }

        // �ƿ�ǲ ��� ���� �� ����
        if (testCase.ExternalOutputStates != null)
        {
            for (int i = 0; i < testCase.ExternalOutputStates.Count; i++)
            {
                // �ʿ��� ��ŭ ��� �߰�
                if (i >= outputToggles.Count)
                {
                    AddOutputToggle();
                }

                // ���� ����
                if (i < outputToggles.Count)
                {
                    outputToggles[i].isOn = testCase.ExternalOutputStates[i];
                }
            }
        }
    }

    // ��� ��� ����
    private void ClearAllToggles()
    {
        // ��ǲ ��� ����
        foreach (var toggle in inputToggles)
        {
            Destroy(toggle.gameObject);
        }
        inputToggles.Clear();

        // �ƿ�ǲ ��� ����
        foreach (var toggle in outputToggles)
        {
            Destroy(toggle.gameObject);
        }
        outputToggles.Clear();
    }
}
