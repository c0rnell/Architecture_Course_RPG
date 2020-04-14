using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PlayButton : MonoBehaviour
{
    public static string LevelToLoad;
    
    [SerializeField]
    private string m_LevelName;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => LevelToLoad = m_LevelName);
    }
}
