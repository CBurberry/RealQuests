using System;
using UnityEngine;
using UnityEngine.UI;

public class CooldownEntry : MonoBehaviour
{
    [SerializeField]
    private InputField dayInput;
    [SerializeField]
    private InputField hourInput;

    private void Start()
    {
        dayInput.onValidateInput += delegate(string input, int charIndex, char addedChar) { return ValidatePositiveIntField(addedChar); };
    }

    public void SetEntryValues(uint dayValue, uint hourValue)
    {
        dayInput.text = dayValue.ToString();
        hourInput.text = hourValue.ToString();
    }

    public bool IsInputValid()
    {
        return !(string.IsNullOrEmpty(dayInput.text) || string.IsNullOrEmpty(hourInput.text));
    }

    public TimeSpan GetCooldownTimeSpan()
    {
        if (!IsInputValid()) 
        {
            throw new InvalidOperationException("Cooldown input values can not be empty!");
        }

        return new TimeSpan(int.Parse(dayInput.text), int.Parse(hourInput.text), 0, 0);
    }

    private char ValidatePositiveIntField(char input)
    {
        if (input >= '0' && input <= '9') 
        {
            return input;
        }

        //Return a null character value if the entry is not 0123456789 contained.
        return '\0';
    }
}
