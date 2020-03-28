using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Localization))]
public class LocalizationInpector : Editor {
    public override void OnInspectorGUI()
    {
        Localization local = (Localization) target;
        base.OnInspectorGUI();
        if (GUILayout.Button("Refresh"))
        {
            local.Generate();
        }
    }
}
