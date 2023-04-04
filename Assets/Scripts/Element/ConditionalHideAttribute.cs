using UnityEngine;
using System;
 
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
                AttributeTargets.Class | AttributeTargets.Struct)]
public class ConditionalHideAttribute : PropertyAttribute
{
    public string ConditionalSourceField = "";
    public bool HideInInspector;
 
    public ConditionalHideAttribute(string conditionalSourceField, bool hideInInspector)
    {
        ConditionalSourceField = conditionalSourceField;
        HideInInspector = hideInInspector;
    }
}