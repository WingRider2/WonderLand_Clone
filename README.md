# 기괴한 나라의 엘리스

엘리스가 다시 한 번 꿈속에 들어가 탈출을 시도하는 3D 퍼즐 어드벤처 게임입니다.  
Unity 기반으로 제작되었으며, 다양한 아이템과 기믹을 활용해 맵을 클리어해 나갑니다.

---

## 📌 목차
- [소개](#소개)
- [기능](#기능)
- [아이템](#아이템)
- [기술 스택](#기술-스택)
- [영상예시](#-영상예시)
- [버그 및 에러](#에러-및-이슈)

---

## 소개

이 프로젝트는 "이상한 나라의 앨리스"를 모티브로 한 게임으로,  
엘리스가 꿈속에서 다양한 기묘한 공간을 탐험하며 탈출을 시도하는 내용을 담고 있습니다.

---

## 기능

### 🎮 조작
- `WASD` : 이동  
- `스페이스바` : 점프  
- `E` : 아이템 습득  
- `마우스 좌/우 클릭` : 아이템 조작  
- `숫자 키 (1~9)` : 슬롯에 있는 아이템 장착
- `EXC` : 설정창 열기

---

## 아이템

- **잡기 아이템** : 오브젝트를 집거나 던질 수 있습니다.
- **크기 조절 아이템** : 엘리스의 크기를 키우거나 줄일 수 있습니다.
- **시간 조작 아이템** : 특정 오브젝트의 시간을 되돌릴 수 있습니다.

---

## 기술 스택

- Unity `2022.3.17f1`
- .NET8.0
- GitHub

---

## 📸 영상예시

| 잡기 아이템 |
|-----------|
| [![게임 플레이 영상](https://img.youtube.com/vi/tuTkrrDmda8/hqdefault.jpg)](https://www.youtube.com/watch?v=tuTkrrDmda8) |
| 크기 조절 아이템 |
| [![게임 플레이 영상](https://img.youtube.com/vi/_2X0z7_6pKg/hqdefault.jpg)](https://www.youtube.com/watch?v=_2X0z7_6pKg) |
| 시간 조작 아이템 |
| [![게임 플레이 영상](https://img.youtube.com/vi/byBp0D_e7UQ/hqdefault.jpg)](https://www.youtube.com/watch?v=byBp0D_e7UQ) |
| [![게임 플레이 영상](https://img.youtube.com/vi/-37UZP0vzss/hqdefault.jpg)](https://www.youtube.com/watch?v=-37UZP0vzss) |
| 맵회전 |
| [![게임 플레이 영상](https://img.youtube.com/vi/m8gV3JLoujg/hqdefault.jpg)](https://www.youtube.com/watch?v=m8gV3JLoujg) |
| [![게임 플레이 영상](https://img.youtube.com/vi/ujXdlmAeqIM/hqdefault.jpg)](https://www.youtube.com/watch?v=ujXdlmAeqIM) |


---

## 💡 기타

플레이어는 다양한 아이템과 조작을 통해 공간의 기묘함을 체험하고,  
기믹을 이용해 퍼즐을 해결하며 게임을 클리어해 나갑니다.

---

## 🐞에러 및 이슈
- 맵이 회전하는 과정에서 충돌 판정이 정확하지 않아 플레이어 또는 오브젝트가 맵 밖으로 튕겨 나가는 현상이 발생함
- 맵 회전 중 Joint로 연결된 오브젝트가 물리적으로 불안정해져 맵 외부로 빠져나가며, 이에 따라 특정 기믹이 정상적으로 작동하지 않는 문제가 있음
