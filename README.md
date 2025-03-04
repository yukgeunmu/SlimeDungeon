# 필수 기능 구현
## 기본 이동 및 점프
1. Input System
![image](https://github.com/user-attachments/assets/86ec70bc-bb75-4286-9fc2-b9e7fbc58b9a)
2. CharacterManager 싱글톤화

```csharp
public class CharacterManager : MonoBehaviour
{
    public static CharacterManager _instance;
		
		// 방어 코드
    public static CharacterManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameObject("CharacterManager").AddComponent<CharacterManager>();

            return _instance;
        }
        
	  private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            if (_instance != null)
                Destroy(gameObject);
        }
    }

}
        
```

   
## 체력바 UI

