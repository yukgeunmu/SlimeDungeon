# 필수 기능 구현
## 기본 이동 및 점프
1. Input System을 활용하여 Input Action을 준비 했습니다.
![image](https://github.com/user-attachments/assets/86ec70bc-bb75-4286-9fc2-b9e7fbc58b9a)

2. 캐릭터의 전체적인 관리를 하는 CharacterManager를 싱글톤 패턴으로 구현했습니다. 그리고 방어 코드를 작성해 CharacterManager가 없으면 오브젝트를 생성하도록 코드를 작성했습니다.
-  방어 코드
```csharp
    public static CharacterManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameObject("CharacterManager").AddComponent<CharacterManager>();
            return _instance;
        }
    }        
```
- 싱글톤 패턴
```csharp
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
```

3. 플레이어의 이동을 관리하는 PlayerContoller 스크립트를 만들어 플레이어의 이동과 점프를 구현했습니다.
4. 점프는 LayerMask와 Ray 캐스트를 사용해 지면에 닿을 때만 점프를 할 수 있도록 구현했습니다.
   
## 체력바 UI
1. U

