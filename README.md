# 조작키
- 플레이어의 이동 : WASD
- 점프 : Space
- 대쉬 : WASD 빠르게 두번 클릭
- 시점 전환 : Tab
- 아이템 사용 : E
- 커서 활성화 : Alt
- 마우스 좌클릭 : 발사대 점프
-------------------------------------------------------------------------------------
# 필수 기능 구현
### - 기본 이동 및 점프
- 플레이어의 기본 이동과 점프를 Input System을 활용해 구현했습니다.
![image](https://github.com/user-attachments/assets/7252b6e5-b51c-4b30-a0b5-b553e0de571b)


#### - 플레이어 이동 :  키입력 → OnMove() → MoveDirection() → Move() 

- OnMove() 매서드로 키의 입력을 받아와 Move() 매서드에서 이동을 구현 했습니다. 나중에 카메라 시점 전환 때 플레이어의 키입력에 대한 방향이 필요해 MoveDirection() 매서드를 추가로 만들어 CameraMovement 클래스에서 사용 할 수 있도록 하였습니다.

#### - 점프 : OnJump()
- 플레이어 점프 : OnJump()로 키 입력을 받아서 isGround() 매서드에서 Ray이 이용해 지면에서만 점프가 가능하도록 구현 했습니다.

### - 체력바 UI
![image-1](https://github.com/user-attachments/assets/159fdb40-06c2-48e6-b4c1-6a0c7551514b)
![image-2](https://github.com/user-attachments/assets/4b595419-b1f0-4f43-9213-128aa8a00f7d)


#### 체력바, 스태미나 : Condition 클래스 → UIConditions 클래스 →  PlayerCondition 클래스
- Health와 Stamina UI에 Condition.cs를 컴포넌트로 넣어주고 UICondition에서 관리 할 수 있도록 구현 했습니다.

### - 동적 환경 조사

![스크린샷 2025-03-11 150751](https://github.com/user-attachments/assets/7e5a9d5e-c9ca-487e-a123-51d9f748df6c)


- 카메라 중심에 Ray를 발사해 아이템의 이름과 정보가 뜨도록 구현했습니다.

### - 점프대
![스크린샷 2025-03-11 151039](https://github.com/user-attachments/assets/4d43f3ad-20f3-4080-b2e6-b8366456b178)


- OnCollisionEnter와 ForceMode를 활용해 점프대를 구현했습니다.

### - 아이템 데이터
![image-3](https://github.com/user-attachments/assets/bf805e29-156a-4c32-b9b2-1ac60944118a)


- 다양한 아이템 데이터를 ScriptableObject로 정의해서 관리 할 수 있도록 구현했습니다.

### - 아이템 사용
![image-5](https://github.com/user-attachments/assets/1b7afc53-071e-4e38-aa1b-2ae965e34082)


- 아이템 타입별로 분류해 아이템 사용을 구현했습니다.
--------------------------------------------------------------------------
# 도전 기능 구현
### - 추가 UI
- 추가 UI는 체력바 생성하는 방식으로 스태미나바를 표시하는 바를 구현했습니다.
### - 3인칭 시점

- 1인칭 시점
![스크린샷 2025-03-11 151728](https://github.com/user-attachments/assets/74bece59-d986-4189-a9fd-13645a7ed6f9)


- 3인칭 시점
![스크린샷 2025-03-11 151739](https://github.com/user-attachments/assets/05c0efae-8d9b-442f-be5d-d16421bdcfea)


- Tab키의 입력에 따라 1인칭과 3인칭이 전환되도록 구현했습니다.

### - 움직이는 플랫폼 구현
![image-6](https://github.com/user-attachments/assets/26b22c8e-d589-4f60-9380-e050cd818dfd)

![image-7](https://github.com/user-attachments/assets/6752ac98-ba56-4699-aeb1-cd59041d0798)

- 일정 간격으로 움직이는 이동하는 플랫폼을 구현했습니다. 플레이어가 위에 있으면 같은 속도로 움직일 수 있도록 속도를 더해주어 같이 움직 일 수 있도록 구현했습니다. 그리고 오브젝트에 움직이는 방향을 정할 수 있도록 구현했습니다.

### - 벽타기 및 매달리기
![스크린샷 2025-03-11 152336](https://github.com/user-attachments/assets/d98b3ea8-5a81-4cb5-812e-13931699b443)


- 벽에 붙으면 위로 힘이 작용하고 애니메이션을 적용해 플레이어기 벽타고 올라가는 모습을 구현했습니다.

### - 다양한 아이템 구현
![스크린샷 2025-03-11 152528](https://github.com/user-attachments/assets/67494fab-f146-4ec7-8750-0ec37956429c)

![image-8](https://github.com/user-attachments/assets/a2b5ed59-7790-433e-a7fe-0817e9796f71)


- 코루틴 함수를 이용해 일정 시간 동안 스피드가 증가하는 아이템을 구현했습니다.

### - 장비 장착
![스크린샷 2025-03-11 152721](https://github.com/user-attachments/assets/e6d5f453-504c-4fe5-9d2e-58f555aa52aa)

![스크린샷 2025-03-11 152749](https://github.com/user-attachments/assets/bcc0c623-866f-4292-b727-d3f8ae15912f)


- 장비를 장착하면 특수한 능력을 가지도록 구현했습니다.

### - 레이저 트랩
![image-9](https://github.com/user-attachments/assets/4203f66d-fac8-49bc-90f4-f1569e4a705e)

![image-10](https://github.com/user-attachments/assets/15e285c5-bf6e-4f73-a5d8-b97bf96fa999)


- Ray를 이용해 불이 나오는 플랫폼과 플레이어어에게 날아오는 오브젝트를 구현했습니다.

### - 상호작용 가능한 오브젝트 표시
![스크린샷 2025-03-11 153422](https://github.com/user-attachments/assets/8c6be5bb-71e1-4e48-bb1e-f512c4743448)


-  유니티에 IPointerEnterHandler, IPointerExitHandler를 이용해 Alt 키를 눌러 커서를 활설화 시킨 후, 상호작용이 가능한 아이템에 올리면 UI가 표시 되도록 구현했습니다. 

### - 플랫폼 발사기
![스크린샷 2025-03-11 153652](https://github.com/user-attachments/assets/65a22b6d-dfd8-4315-bf8c-eb5e8970bf92)

- 플레이어를 발사 할 수 있는 올라가 마우스 좌클릭 버튼을 누르고 있으면, 점프 게이지가 차면서 좌클릭 버튼을 떼면 날아가는 발사대를 구현했습니다.
