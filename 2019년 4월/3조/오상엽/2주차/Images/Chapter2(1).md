# 의존성과 계층화  
- 모든 소프트웨어는 의존성을 가지고 있다.
- 각 의존성은 같은 기반 코드를 이용하는 코드에 대한 퍼스트파티 의존성, 외부 어셈블리에 대한 섣파티 의존성, 마이크로소프트 닷넷 프레임워크에 대한 보편적인 의존성일 수 있다.
- 의존성은 호출자 코드로부터 기능을 추상화한다.
- 의존성 체인을 제대로 관리하지 못하면 개발자들은 존재할 필요가 없는 의존성 때문에 불필요한 어셈블리 참조로 꼬인 코드를 관리해야 하는 어려움이 있다.
- 변화를 수용할 수 있는 코드를 작성하기 위해서는 의존성을 효과적으로 관리해야 한다.
- 의존성 관리에 대해 순수주의적 접근법을 채택하는 것이 중요하다
- 의존성은 충분한 학습과 이해를 통해 쉽게 관리 될수 있다.
- 단기적으로 애플리케이션을 잘 정리할 수 있도록 도와주는 패턴을 적용하여 장기적으로 변화를 수용할 수 있는 코드를 완성해 나가면 된다
- 계층화는 가장 일반적인 아키텍처 패턴 중 하나이다.

## 의존성의 정의
- 의존성은 별개으 두 엔티티 사이의 연관 관계로 인해 어느 한 엔티티가 다른 엔티티의 기능 없이는(혹은 존재 없이는) 자신의 기능을 실행하지 못하는 관계를 의미한다.
- 엔티티는 주로 어셈블리를 의미함. 어셈블리 a가 다른 어셈블리 B를 사용하고 있으면 A가 B에 의존적이라고 함 
- 예를 들어 A는 클라이언트 B를 서비스라고 가정하면   
(A)클라이언트 -> (B) 서비스로 의존적 표현이 가능함

## 간단한 예제
1. SimpleDependency 이름의 콘솔 프로젝트 생성
2. MessagePrinter 이름의 클래스 라이브러리 프로젝트 생성
3. 콘솔 애플리케이션의 참조 노드를 마우스 오른쪽 버튼으로 클릭하고 참조 추가 메뉴 선택
4. 참조 추가 대화 상자에서 왼쪽 패널 프로젝트 항목을 선택한 후 오른쪽 패널에서 생성했던 클래스 라이브러리 프로젝트에 대한 참조 선택  
=> 이렇게 하면 콘솔 애플리케이션(client)는 클래스 라이브러리(서비스)에 의존성을 갖게 됨  
=> 솔루션 빌드 후 프로젝트의 bin 디렉토리르 보면 SimpleDependency.exe. 파일과 함께 MessagePrinter.dll 파일이 존재함  
<pre><code>Progrem.cs - Main() 수정
namespcae SimpleDependency
{
    class Program
    {
        static void Main()
        {
            Console.ReadKey();
        }
    }
}
</pre></code>
=> 솔루션을 다시 빌드하고 실행시키면 키 입력을 기다리다 키가 입력되면 종료됨. 
=> Console.ReadKey() 메서드를 호출하는 코드에 중단점을 설정하고 디버그 모드로 실행하면 중단점에서 메모리에 로드되어 있는 어셈블리 목록 확인 가능
=> 디버그 -> 창-> 모듈 메뉴 또는 Ctrl+D M을 누르면 됨
=> 여기서 확인할 수 있는 것은 의존성을 제공하는 어셈블리가 반드시 미리 로드되어 있을 필요가 없다는 것
