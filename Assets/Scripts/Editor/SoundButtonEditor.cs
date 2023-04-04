#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UI;

[CustomEditor(typeof(SoundButton))]
public class SoundButtonEditor : ButtonEditor
{
    private SerializedProperty _clickSound;
    
    protected override void OnEnable()
    {
        base.OnEnable();
        _clickSound = serializedObject.FindProperty("_clickSound");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUILayout.PropertyField(_clickSound);
        serializedObject.ApplyModifiedProperties();
    }
}
#endif