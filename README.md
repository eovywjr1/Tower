# Tower
  개인 90일 프로젝트입니다.
  
  프로젝트의 이름은 Tower로, 게임 배경의 Tower에서 유래했습니다.
<br/><br/>

## 목차
  1. 게임 소개
  2. 게임 정보
  2. 기술 스택
  3. 구현 기능
  <br/><br/>
  
## 게임 소개
  스토리는 없고, 탑에서 층마다 12지신 보스를 격파하는 게임입니다.
<br/><br/>
  
## 게임 정보
  - 플랫폼 : PC
  - 장르 : RPG
  - 탑뷰
<br/><br/>

## 기술 스택
  - 게임 엔진 : Unity (2020.3.21f1 LTS)
  - 프로그래밍 언어 : C#
<br/><br/>

## 구현 기능

#### 캐릭터
![타워캐릭터](https://user-images.githubusercontent.com/40791869/216668203-81ff285b-e225-495c-b697-56c00e1d54ca.gif)
    
    이동, 공격, 대쉬, 체력, 공격력
<br/><br/>
    
#### 타이틀 화면
![타워타이틀설정](https://user-images.githubusercontent.com/40791869/216666772-93cdca82-d651-4b96-84b8-4702a65d44c5.gif)

    스테이지 선택, 보스 / 플레이어 체력 설정, 배경 음악 On/Off, 소리 크기 조절
<br/><br/>
    
#### 1층(쥐, 튜토리얼)
![타워1층](https://user-images.githubusercontent.com/40791869/216669684-377ccca9-26d6-4fc4-bae1-836030b92ea0.gif)

    네비게이션, 공격 X
<br/><br/>
      
#### 2층(소)
![타워2층](https://user-images.githubusercontent.com/40791869/216676478-a8bdd45b-9ca1-4a41-91d4-3e5d2122434c.gif)

    내려찍은 후 충격파 발생, 돌진, 똥 냄새 발생하여 공격 유효하지 않음, 동시에 다른 패턴 가능

![타워2층_1](https://user-images.githubusercontent.com/40791869/216676674-e36c98e9-6c23-49b8-9380-1e109861747d.gif)

    사료를 생성하여 보스가 먹으면 hp 회복, 파괴가능
<br/><br/>
      
#### 3층(호랑이)
![타워3층](https://user-images.githubusercontent.com/40791869/216750126-3b2c4a3c-6154-4194-bf18-56ee81bde648.gif)


    포효(기절), 물어뜯기, 할퀴기
<br/><br/>
    
#### 4층(토끼)
![타워4층](https://user-images.githubusercontent.com/40791869/216750180-429908ca-ea20-4c43-bc79-3f3fcc25c8c7.gif)
    공격은 하지 않되, 이동속도가 빠름, 시간 제한
<br/><br/>
    
#### 5층(용)
      - 제자리 불 뿜기
      - 날아다니며 불 뿜기
      
    - 6층(뱀)
      - 물어뜯기
      - 꼬리치기
      - 조이기
      - 이동하는 동안 공격 유효하지 않음
