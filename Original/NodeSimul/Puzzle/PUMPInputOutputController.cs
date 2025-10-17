using TMPro;
using UnityEngine;

/// <summary>
/// �Է� �� ��� ����Ʈ���� ��� ���� �����ϴ� Ŭ����
/// PUMPCanvas �����տ� �߰��Ͽ� ���
/// </summary>
public class PUMPInputOutputController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_InputField inputCountField;
    [SerializeField] private TMP_InputField outputCountField;

    [Header("Configuration")]
    [SerializeField] private int minNodeCount = 1;
    [SerializeField] private int maxNodeCount = 8;

    [SerializeField] private PuzzleDataPanel puzzleDataPanel;

    private PUMPBackground _pumpBackground;
    private int _currentInputCount = 2;
    private int _currentOutputCount = 2;
    private bool _isInitializing = true;

    private void Start()
    {
        // PUMPBackground ���� ã��
        _pumpBackground = Object.FindAnyObjectByType<PUMPBackground>();

        if (_pumpBackground == null)
        {
            return;
        }

        // ���� ����Ʈ���� ��� ���� Ȯ�� �� �ʱ�ȭ
        if (_pumpBackground.ExternalInput != null)
        {
            _currentInputCount = _pumpBackground.ExternalInput.GateCount;
            inputCountField.text = _currentInputCount.ToString();
        }

        if (_pumpBackground.ExternalOutput != null)
        {
            _currentOutputCount = _pumpBackground.ExternalOutput.GateCount;
            outputCountField.text = _currentOutputCount.ToString();
        }

        // InputField �̺�Ʈ ����
        inputCountField.onValueChanged.AddListener(OnInputCountChanged);
        outputCountField.onValueChanged.AddListener(OnOutputCountChanged);

        _isInitializing = false;
    }


    private void OnInputCountChanged(string value)
    {
        // �ʱ�ȭ �߿��� ó������ ����
        if (_isInitializing)
            return;
            
        if (!int.TryParse(value, out int result))
        {
            // ���� �Է��ߴٰ� ���� ��� �� �ƹ��͵� ���� ����
            return;
        }

        // ���� ��� ���� ���� ����
        int clampedValue = Mathf.Clamp(result, minNodeCount, maxNodeCount);

        // ���� ����� ��쿡�� ����
        if (clampedValue != _currentInputCount)
        {
            _currentInputCount = clampedValue;

            // UI�� ǥ�õǴ� ���� �ٸ� ��� ����ȭ
            if (int.TryParse(inputCountField.text, out int currentDisplayedValue) && 
                currentDisplayedValue != clampedValue)
            {
                // Ŀ�� ��ġ ����
                int caretPosition = inputCountField.caretPosition;
                inputCountField.text = clampedValue.ToString();
                // �����ϸ� Ŀ�� ��ġ ����
                if (caretPosition <= inputCountField.text.Length)
                    inputCountField.caretPosition = caretPosition;
            }

            ApplyChanges();

            // Input ������ ����Ǿ����Ƿ� �׽�Ʈ ���̽� �ʱ�ȭ
            ClearTestCases();
        }
    }
    
    private void OnOutputCountChanged(string value)
    {
        // �ʱ�ȭ �߿��� ó������ ����
        if (_isInitializing)
            return;
            
        if (!int.TryParse(value, out int result))
        {
            // ���� �Է��ߴٰ� ���� ��� �� �ƹ��͵� ���� ����
            return;
        }

        // ���� ��� ���� ���� ����
        int clampedValue = Mathf.Clamp(result, minNodeCount, maxNodeCount);

        // ���� ����� ��쿡�� ����
        if (clampedValue != _currentOutputCount)
        {
            _currentOutputCount = clampedValue;

            // UI�� ǥ�õǴ� ���� �ٸ� ��� ����ȭ
            if (int.TryParse(outputCountField.text, out int currentDisplayedValue) && 
                currentDisplayedValue != clampedValue)
            {
                // Ŀ�� ��ġ ����
                int caretPosition = outputCountField.caretPosition;
                outputCountField.text = clampedValue.ToString();
                // �����ϸ� Ŀ�� ��ġ ����
                if (caretPosition <= outputCountField.text.Length)
                    outputCountField.caretPosition = caretPosition;
            }
            ApplyChanges();

            ClearTestCases();
        }
    }

    private void ApplyChanges()
    {
        // �ʱ�ȭ �÷��� �������� �̺�Ʈ �ڵ鷯�� �ߺ� ȣ��Ǵ� �� ����
        _isInitializing = true;


        
        //_pumpBackground.Initialize(_currentInputCount, _currentOutputCount);
        _pumpBackground.ExternalOutput.GateCount = _currentOutputCount;
        _pumpBackground.ExternalInput.GateCount = _currentInputCount;
        
        // ���� �� ���� ����� �� Ȯ�� �� ����ȭ
        if (_pumpBackground.ExternalInput != null)
        {
            _currentInputCount = _pumpBackground.ExternalInput.GateCount;
            inputCountField.text = _currentInputCount.ToString();
        }

        if (_pumpBackground.ExternalOutput != null)
        {
            _currentOutputCount = _pumpBackground.ExternalOutput.GateCount;
            outputCountField.text = _currentOutputCount.ToString();
        }
        //_pumpBackground.ResetBackground();


        _isInitializing = false;
    }

    private void ClearTestCases()
    {
        if (puzzleDataPanel != null)
        {
            // ���� �׽�Ʈ ���̽� UI ����
            puzzleDataPanel.ClearTestCases();

            // �����͵� �ʱ�ȭ
            puzzleDataPanel.currentPuzzleData = new PuzzleData();

            // ���ο� �� �׽�Ʈ ���̽� �߰� (�ڵ����� ���� ����� ������ �°� ����)
            puzzleDataPanel.AddNewTestCase();
        }
    }
}