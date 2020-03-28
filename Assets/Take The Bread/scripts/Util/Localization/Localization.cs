using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Localization : Singleton<Localization>
{
    public event System.Action onLocalize;

    public TextAsset[] languages;
    private Dictionary<string, Dictionary<string, string>> data;
    public Dictionary<string, string> CurrentData { get; private set; }

    public static string Get(string key) { return Instance.GetString(key); }

    public void Generate()
    {
        data = new Dictionary<string, Dictionary<string, string>>();
        for (int i = 0; i < languages.Length; i++)
        {
            Dictionary<string, string> loc = new Dictionary<string, string>();
            string[] lines = languages[i].text.Split('\n');
            foreach (string line in lines)
            {
                int pos = line.IndexOf('=');
                if (pos == -1) continue;
                string key = line.Substring(0, pos).Trim();
                string value = line.Substring(pos + 1, line.Length - pos - 1).Trim();

                while (value.IndexOf(@"\n") >= 0)
                {
                    int index = value.IndexOf(@"\n");
                    value = value.Substring(0, index) + '\n' + value.Substring(index + 2, value.Length - index - 2);
                }
                loc.Add(key, value);
            }
            data.Add(languages[i].name, loc);
        }
    }

    public void SetLanguage(int id)
    {
        if (data == null) Generate();
        if (id >= languages.Length) return;
        CurrentData = data[languages[id].name];
        if (onLocalize != null) onLocalize();
    }

    public string GetString(string key)
    {
        if (CurrentData == null && languages.Length > 0)
        {
            SetLanguage(0);
        }
        if (CurrentData.ContainsKey(key))
        {
            return CurrentData[key];
        }
        return key;
    }
}
