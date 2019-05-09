# Chapter.2(2주차)

>## 범위

1. 의존성의 정의
2. 의존성 관리하기(구현과 인터페이스의 비교 ~ 객체 생성에 대한 대안)

>## QnA

1. 아키텍처(Architecture, SWA : Software Architect, AA : Application Architect) : 사전적 의미 - 건축물, 컴퓨터 시스템의 구성  
    - `"시스템을 추론하기 위해 필요한 구조의 집합이며, 이 구조는 요소와 관계 그리고 요소 속성과 관계 속성을 포함한다."`
        - 구조란 어떤 관계에 의해 서로 묶인 요소의 집합이다.
        - 아키텍처는 추상화이다. 시스템을 추론하는데 필요하지 않은 정보는 누락시킨다는 의미다.
        - 모든 소프트웨어 시스템은 소프트웨어 아키텍처를 가진다.  
        모든 시스템은 요소와 요소의 관계를 가지고 있으므로 아키텍처가 있다.  
        SW를 구성하는 문서나, 소스코드가 없다고 하더라도, 아키텍처는 존재한다.  
        시스템의 아키텍처와 아키텍처의 표현은 다른 것이다.
        - 아키텍처는 행동을 포함한다.  
        요소는 각자 행동을 가지고 있고, 요소간에도 상호작용이 있다.
    - `"비지니스 요구 사항을 만족하는 시스템을 구축하기 위해서 전체 시스템에 대한 구조를 정의한 문서로, 시스템을 구성하는 컴포넌트와 그 컴포넌트간의 관계, 그리고 컴포넌트가 다루는 정보를 정의한다."`
        - 아키텍처는 비지니스 요구 사항을 기술로 해석해 놓은 것
        - 개발의 방향을 알려주는 지도(방침)
        - 의사 소통의 매개체
        - 정답은 없다, 팀의 수준에 맞게 이해할 수 있는 수준으로.., 그러나 모든 내용이 담겨야 한다.
    - [***MSDN-소포트웨어 아키텍처란?***](https://msdn.microsoft.com/ko-kr/hh144976.aspx) 여기서도.. 언급된다. **`변화`**.. 책도.. MSDN서도 언급되는 걸 보니 중요한 키워드이고, 말하고자 하는 부분이 무엇인지 알 것 같다. 변화를 얼마나 빨리 받아들이냐는.. 결국 유지보수의 속도가 아닌가 싶다. 한번쯤 읽어봐도 좋을 것 같다.

>## 의존성(Dpendency)의 정의

```txt
"의존성 관리에 대해 순수주의(purist)적 접근법을 채택하는 것이 중요하다는 것은 아무리 강조해도 지나치지 않는다.  
중요한 이슈 때문에 타협을 하게 되면 당장은 팀의 업무 속도가 상승할 수 있겠지만,  
장기적으로는 프로젝트에 치명적일 수 있는 악영향을 끼치게 된다.  
코드의 양과 모듈의 개수가 증가함에 따라  
단기적으로 생산성을 높이기 위한 방안이 머지않아 악재가 되는 것은 너무나도 흔한 이야기이다."
```  

*의존성의 정의에 대해 정리하기 전, 개인적으로 윗 글을 이번 Chapter의 서론중 가장 주요한 부분으로 뽑아 보았다.  
타협을 하지 말라고 하지만.. 하다보면 자연스레 타협을 하게된다.  
'남들도 다 저렇게 하는데?', '귀찮아..' 이런 맘이 솔직히 없으랴? 그래도.. 적어도 눈에 보여 들키는 타협은 없어야 하지 않을까?  
들키면.. 쪽팔리니깐.*

1. 별개의 두 엔티티(Entity, 개체-독립체) 사이의 연관 관계로 인해 어느 한 엔티티의 기능 없이는 (혹은 존재 없인) 자신의 기능(기능중 일부)을 실행하지 못하는 관계를 의미한다.
2. 의존성은 같은 기반 코드(내부 어셈블리)를 의존하는 퍼스트파티(first-party) 의존성과 외부 어셈블리에 의존하는 서드파티(third-party) 의존성, 그리고 .Net(.Net framework, .Net standard, .Net core)에 대한 보편적인(프레임워크) 의존성으로 나뉜다.
3. 엔티티는 주로 어셈블리를 의미한다.(어디까지나 '주'일 뿐이고 책의 내용일 뿐이다.)  
어셈블리 A가 다른 어셈블리 B를 참조하고 사용한다면, A는 B에 의존하고 있는 것이다.  
개발을 하며 이미 많은 부분에서 의존성을 띈 개발을 했을 것이다.  
`그림 1`을 보자 책은 A, B로 설명이 되어있지만 여기선 메신저로 진행하겠다.  
PC 메신저 자체 만으로도 수많은 의존성을 갖고 있을 것이다.  
하지만 PC 메신저 자체 만으로는 메신저를 실행 할 수 없다.  
당연히 서버가 존재해야 하고, 서버에 의존적일 수밖에 없다.

    ```mermaid
    graph LR
    A["(PC Messenger)<br/>Client"]--의존적-->B["(G/W Server)<br/>Service"]
    ```

    [그림 1 - 의존도]  

    즉, 클라이언트 엔티티가 있고 서버의 여러 서비스 엔티티가 있는 것이다.  
    메신저를 실행하려면 가장 먼저 개인 계정을 입력하여 로그인을 해야한다.  
    이때 `그림 2`처럼 로그인 서비스에게 입력된 정보로 요청을 하고 등록된 정보인지에 대해 서비스로 부터 응답을 받게 된다.

    ```mermaid
    sequenceDiagram
    PC Messenger->>G/W Server: {"LoginInfo":{"ID": "mirero", "PW": "mirero", ...}, ...}
    G/W Server->>PC Messenger: {"Response":{"Result": "Ok", ...}, ...}
    ```  

    [그림 2 - Login(로그인 서비스) 요청, 응답 예시 시퀀스]  

    여기서 주요한 부분은 PC 메신저는 서버에 의존하지만 서버는 PC 메신저에 의존하지 않는다는 것이다.  
    책의 내용처럼 의존적이어서도 안되고 의존적일리도 없다.  
    (단순히 생각해보자.. 클라이언트 하나가 종료되면 서버가 종료가 되어야 하는가??)
4. 유향 그래프를 이용한 의존성 모델링  
그래프(graph)는 노드(node)와 엣지(edge)로 구성된 수학적 구조체이다. 엣지는 두 노드 사이에만 존재할 수 있으며, 이 둘을 연결하는 역할을 한다.  
모든 노드는 그래프 내에서 여러 개의 노드(자신 포함 - 재귀 함수)와 연결 될 수 있다.  
그래프는 그래프 속성의 변화에 따라 여러 형태 중 한 가지가 될 수 있다.
    - 무향 그래프(undirected graph) : 방향이 없는(화살표가 없는) 엣지로 구성된 그래프(노드와 엣지가 있다는 것이 중요할 뿐)

        ```mermaid
        graph TB
        A((A))---C((C))
        A---B((B))
        B---E((E))
        B---F((F))
        B---D((D))
        D---F
        ```

        [그림 3 - 무향 그래프]

    - 유향 그래프(directed graph) 혹은 다이그래프(digraph) : 지시 방향이 있는(화살표가 있는) 그래프

        ```mermaid
        graph TB
        A((A))-->C((C))
        A-->B((B))
        B-->E((E))
        B-->F((F))
        B-->D((D))
        D-->F
        ```

        [그림 4 - 유향 그래프(다이그래프)]  

        의존성은 두 개의 엔티티로 구성되며 이 두 엔티티 사이에는 종속적인 코드에서 의존성을 제공하는 코드로의 방향이 존재한다. 엔티티를 노드라고 생각하면 종속적인 코드에서 의존성을 제공하는 코드로 방향이 있는 엣지를 그릴 수 있다. 이 방법으로 엔티티들을 그려 나가면 그것이 바로 의존성 다이그래프(dependency digrapgh)가 되는 것이다.
5. 순환 의존성  
방향을 갖는 그래프는 순환되는 형태가 될 수 있다.  
노드에서 다른 노드를 탐색할 수 있는 기능으로 인해 엣지를 따라가다가 자기 자신으로 되돌아오는 것도 가능하기 때문이다.  
`그림1, 4`는 비 순환식 다이그래프(acyclic digraph) 였다.  
`그림 5`는 순환식 다이그래프(cyclic digraph)를 보여 준다.  
노드 D에서 탐색을 시작해 E로 향하는 엣지를 거쳐 B로 이동하고, 마지막에는 D로 다시 되돌아 오는 것을 볼 수 있다.

    ```mermaid
    graph TB
    A((A))-->C((C))
    A-->B((B))
    B-->D((D))
    D-->E((E))
    E-->B
    ```

    [그림 5 - 순환식 다이그래프]  

    `그림 5`의 노드들이 어셈블리라고 가정을 한다면, 즉 D어셈블리는 순환고리(D -> E -> B -> D)에 따라 자기 자신을 참조하게 되는 것과 같다.(B, E도 마찬가지다)  
    실제로 VS에서 D프로젝트에 E어셈블리를 참조하게 된다면(B는 D를 참조한 상태고 E는 B를 참조한 상태에서..) '순환 종속성이 발생할 수 있다' 라는 에러 메시지와 함께 참조를 거부한다.  
    `어셈블리는 명시적으로 자기 자신을 참조`하고 있는 상태이므로 자기 자신을 다시 참조할 필요는 없다.  
    즉 의존성 모델링 과정에서 순환 의존성은 완벽히 차단 되어야 한다.
6. 루프(loop)는 다이그래프 내의 순환 중에서도 조금 특별하다. 어떤 노드의 엣지가 자기 자신에게 연결되어 있는 경우가 바로 해당 노드가 루프가 되는 경우이다.  
`그림 6`은 루프를 가진 그래프를 보여준다.

    ```mermaid
    graph LR
    A((A))-->B((B))
    B-->B
    ```

    [그림 6 - 다이그래프의 루프]  

    어셈블리의 경우 앞에서도 언급 했듯이 명시적으로 자기 자신을 참조하고 있는 상태이다. 그러므로 그래프에 표기하여 관찰할 필요는 없다.  
    `그림 6`과 같은 경우엔 메서드 수준에서 표기하는 경우로 루프는 재귀(recursion) 호출이 발생함을 의미한다.

    ```cs
    public class RecursionLoop
    {
        public void A()
        {
            int x = 6;
            Console.WriteLine("{0} != {1}", x, B(X));
        }

        public int B(int num)
            => num == 0 ? 1 : num * B(num - 1);
    }
    ```  

    [예제 1 - 재귀 함수]  
    `예제 1`을 보자 `그림 6`처럼 A 메소드는 B메소드에 의존적이고 B메소드는 변수 num이 0이 될 때 까지 자기 자신을 계속 호출(의존)한다.  
    결국 변수 num이 0이 되어 리턴한 값 부터 순차적으로 곱해지고 곱해져 콘솔창에 결과 값이 출력 될 것이다.  
    이처럼 다이그래프의 루프 표기는 재귀 함수에 대한 표기임을 알 수 있다.

>## 의존성 관리하기

개발 시작 단계에서 의존성을 관리하고 지속적으로 관심을 두어 문제가 발생하지 않도록 하는 것이 최선이다.  
의존성을 제대로 관리하지 못하면 사소한 문제점이 얼마 지나지 않아 아키텍처 전체의 문제점으로 커질 수도 있다.  

1. 구현과 인터페이스의 비교  
컴파일 시에는 인터페이스의 클라이언트는 해당 인터페이스에 대한 어떤 구현체가 사용되고 있는지에 대해 전혀 알 필요가 없다. 이에 대해 알게 된다면 오히려 잘못된 가정으로 인해 인터페이스의 특정 구현체와 클라이언트 사이의 의존성이 더 높아지게 된다.
2. new 키워드의 코드 스멜  
(code smell, *어떤 코드가 잠재적으로 문제가 있을 수 있음을 표하는 단어로, 뭐가 잘못될 가능성이 있음에 대한 경고이며, 어떤 문제가 발생될 경우 그에 대한 근본적 원인이 된다.*)  
인터페이스는 `어떤` 작업을 수행할 수 있는지를 서술하고, 클래스는 `어떻게` 작업을 수행할 것인지를 서술한다.  
실제 구현에 대한 상세 내용은 오직 클래스만이 알고 있다. 즉, 인터페이스는 해당 작업이 어떻게 수행되는지에 대해 철저히 무관심해야 한다.  
new 키워드가 사용 되는 곳은 여러 곳(메모리 할당, 제약조건, 부모 클래스의 멤버 숨김)이 있지만 여기서 말하는 new 키워드는 메모리 할당과 관련된 부분이다.  
즉, 클래스이고 상세 구현을 취급한다는 것이 코드 스멜이 된다는 것이다.  
어째서?? 여기선 '부적절한 결합'의 한 예가 된다고 말한다.  
결국 클라이언트에서 특정 어셈블리의 특정 클래스의 기능을 바로 호출한다는 것은  
클라이언트 코드에 의도하지 않은 의존성을 요구한다는 것이며..  
변화(유지보수)에 취약하다는 것이다.  

    ```cs
    public class AccountController
    {
        private readonly SecurityService _securityService;
        public AccountController()
        {
            _securityService = new SecurityService();
        }

        [HttpPost]
        public void ChangePassword(Guid userID, string newPW)
        {
            var userRepo = new UserRepository();
            var user = userRepo.GetByID(userID);
            _securityService.ChangeUserPassword(user, newPW);
        }
    }
    ```

    [예제 2 - 코드의 적응성이 저하되는 예제]

    `예제 2` 를 보자.. AccountController 라는 클래스는 클라이언트의 기능 구현중 사용자의 계정에 대한 부분을 담당하고 예제로만 볼땐 비번을 변경하는 작업만 있을 뿐이다.  
    여튼 작동에 문제는 없으리라 생각한다.  
    무엇이 문제일까?? new 키워드가 코드 스멜이라 한다...  
    코드를 보니 생성자와 그리고 ChangePassword 메소드를 통해  
    SecurityService, UserRepository 클래스(구조체일까?)를 할당하는데 new 키워드를 이용해 메모리를 할당하였다.  
    AccountController 클래스에 다른 기능을 구현한 메소드가 있는지는 모르겠지만  
    코드만 보면 ChangePassword를 통해 비번만 변경하면 된다.  
    책에 서술한 코드의 문제점을 보고 해결 방법에 대해선 **`객체 생성에 대한 대안`** 에서 알아보도록 하자.

    ```txt
    - AccountController 클래스는 SecurityService, UserRepository 클래스의 구현에 대해 영원히 의존적이 된다.  
    - SecurityService, UserRepository 클래스가 갖고 있는 의존성은 AccountController 클래스의 잠재적 의존성이 된다.  
    - AccountController는 단위 테스트를 하기가 어려워졌다. 평범한 방법으로는 의존하고 있는 두 클래스의 모의 객체(mocking object)를 만들 수 없기 때문이다.  
    - SecurityService.ChangeUserPassword 메소드는 클라이언트가 User 객체를 로드 할 수밖에 없도록 만든다.
    ```

    - 구현 향상(변경)이 불가능
    SecurityService 구현을 변경할 수 있는 방법은 두 가지가 있다.
        1. AccountController가 새롭게 구현한 클래스를 사용할 수 있도록 수정
        2. 기존의 SecurityService 클래스에 새로운 기능을 추가
    본 장의 목표는 AccountController, SecurityService가 작성된 후 절대로 수정되거나 추가 작업이 발생하지 않도록 하기 위함이다. 고로, 두 가지 방법은 적절하지 않다.
    - 의존성 체인
    SecurityService는 스스로도 어떤 의존성을 가질 수 있다. 이와 관련해 아래 예제를 살펴보자

        ```cs
        public class SecurityService
        {
            public SecurityService()
            {
                Session = SessionFactory.GetSession();
            }
        }
        ```

        [예제 3 - AccountController와 동일한 문제점을 갖는 예제]  

        팩토리 패턴으로 Session 타입의 속성인 Session에 메모리를 할당했다. 어떻게? 문제가 있다는 `예제 2`처럼..  
        책에 보면 SecurityService 클래스는 [객체 관계 매퍼(ORM, Object-Relational Mapper)](https://ko.wikipedia.org/wiki/%EA%B0%9D%EC%B2%B4_%EA%B4%80%EA%B3%84_%EB%A7%A4%ED%95%91)인 NHibernate 프레임워크에 대한 의존성을 갖고 있다고 한다.  
        (우리가 자주 사용하는 'Linq문 - Linq to SQL' 이 이와 흡사하다고 보면 될 것 같다. Linq to SQL 맵핑 방법이 잘 기억이 나질 않지만.. 비주얼 스튜디오가 알아서 다 해줬던 건 확실하다!)  
        즉, Session을 통해 저장소를 조회를 한다는 것인데.. 이는 결국 AccountController이 NHibernate에 대한 의존성(잠제적)을 갖는 것이다.

        ```mermaid
        graph TB
        A(AccountController)-->B(SecurityService)
        A-->C(UserRepository)
        B-->D(NHibernate)
        B-->C
        A-->E(User)
        C-->E
        ```

        [그림 7 - 예제 2, 3 정보로 본 다이그래프]  

        여기서 SecurityService의 기본 생성자의 시그니처(Signature)가 변경된다면 어떨까?  

        ```txt
        시그니처 : 생성자나 메서드의 정의, 예로 A 메서드가 몇 개의 매개변수를 사용하며, 각 매개변수의 타입은 무엇인지를 선언하는데, 이를 시그니처라 한다.
        ```

        SecurityService에서 메모리를 할당하는 Session에서 필요로 하는 인자를 생성자로 받아야 한다면 말이다..  
        이럴 경우 SecurityService는 물론 SecurityService를 할당하는 AccountController에서 해당 인자를 정의하도록 수정해야 한다.  
        위에서 말했듯 AccountController, SecurityService는 작성된 후 절대로 수정되거나 추가 작업이 발생해선 안된다.
        - 테스트 가능성의 부재  
        테스트 가능성(testability)은 매우 중요한 항목이며, 이를 위해서는 코드 자체가 특정한 방식을 통해 디자인되어야 한다. 그렇지 않은 경우에는 테스트 자체가 정말로 어려워질 수 있다.  
        현 상태의 AccountController, SecurityService는 수월하게 테스트를 진행하기가 힘들다.  
        그 이유는 두 클래스가 갖고 있는 의존성을 실제로는 아무런 기능도 수행하지 않는 모의 객체로 교체할 수 없기 때문이다.  
        예를 들자면, SecurityService를 테스트 할 때 실제로 DB에 연결하고 싶지는 않을 것이다.  
        실 DB로의 연결은 불필요하고, 느리며, 테스트가 실패할 수 있는 또 다른 가능성을 내포할 뿐이다.  
        - 보다 부적절한 결합  
        AccountController.ChangePassword 메서드는 UserRepository 클래스의 인스턴스를 생성하고 User 객체를 할당한다.  
        그 이유는 SecurityService.ChangeUserPassword메서드가 User객체를 필요로 하기 때문이다.  
        즉, User 객체를 얻지 못하면 User의 비번을 변경할 수 없다.  
        여기에서 책의 예제를 보며 의존성을 줄여보자..  

        ```cs
        [HttpPost]
        public void ChangePassword(Guid userID, string newPW)//AccountController.ChangePassword
        {
            var userRepo = new UserRepository();
           var user = userRepo.GetByID(userID);
            _securityService.ChangeUserPassword(user, newPW);
        }
        //...
        public void ChangeUserPassword(Guid userID, string newPW)//SecurityService.ChangeUserPassword
        {
            var userRepo = new UserRepository();
            var user = userRepo.GetByID(userID);
            user.ChangePassword(user, newPW);
        }
        ```

        [예제 4 - SecurityService를 향상시키기 위한 코드]

        어떻게 달라졌을까? 다이그램을 그려보자

        ```mermaid
        graph TB
        A(AccountController)-->B(SecurityService)
        B-->C(NHibernate)
        B-->D(UserRepository)
        B-->E(User)
        D-->E
        ```

        [그림 8 - 예제 4 적용 다이그램]

        책에선 AccountController이 개선이 되었다 하는데.. 솔직히 아닌거 같다.  
        AccountController은 여전히 SecurityService에 의존적이고 SecurityService는 변화가 없다.(NHibernate를 생성자에서 계속 객체 할당하는지 모르겠다..;)  
        여전히 UserRepository 객체를 할당하고 UserRepository 객체를 통해 User 객체 또한 할당한다.(책은 어째서 User를 생략하는지 모르겠다.)  

3. 객체 생성에 대한 대안  
    - 인터페이스를 기초로 한 코딩  
    첫 번째로 가장 중요한 변경은 SecurityService 클래스의 구현을 인터페이스 뒤로 숨기는 것이다.  
    이러할 경우 AccountController는 SecurityService의 실제 구현체가 아닌 인터페이스에만 의존하게 만들 수 있다.  
    첫 번째로 진행할 리팩토링은 SecurityService러 부터 인터페이스를 추출하는 일이다.  

        ```cs
        public interface ISecurityService
        {
            void ChangeUserPassword(Guid userID, string newPW);
        }

        public class SecurityService : ISecurityService
        {
            public void ChangeUserPassword(Guid userID, string newPW)//SecurityService.ChangeUserPassword
            {
                //...
            }
        }
        ```

        [예제 5 - SecurityService 인터페이스 생성]

        이제 AccountController를 수정해 보자

        ```cs
        public class AccountController
        {
            private readonly ISecurityService _securityService;
            public AccountController()
            {
                _securityService = new SecurityService();
            }

            [HttpPost]
            public void ChangePassword(Guid userID, string newPW)
            {
                _securityService.ChangeUserPassword(user, newPW);
            }
        }
        ```

        [예제 6]

        어떤가?? 여전히 이상하다.. 실제 구현 객체인 SecurityService를 ISecurityService 할당하고 있다..;  
        여전히 SecurityService에 의존하고 있는 것이다.  
        좀더 완벽해지기 위해 의존성 주입 기법을 활용해 보도록 한다.

    - 의존성 주입(DI, Dependency Injection) 기법 활용하기  
    의존성 주입은 [의존관계 역전 원칙(Dependency Inversion Principle)](https://ko.wikipedia.org/wiki/%EC%9D%98%EC%A1%B4%EA%B4%80%EA%B3%84_%EC%97%AD%EC%A0%84_%EC%9B%90%EC%B9%99)을 기반으로 느슨하게 결합된 모듈들(인터페이스)을 이용해서 응용 프로그램을 구성할 수 있는 기법이다.(상세 내용은 9장에서 진행)  
    예제를 살펴보자

        ```cs
        public class AccountController
        {
            private readonly ISecurityService _securityService;
            public AccountController(ISecurityService securityService)
            {
                if(securityService == null)
                    throw new ArgumentNullException("securityService");

                _securityService = securityService;
            }

            [HttpPost]
            public void ChangePassword(Guid userID, string newPW)
            {
                _securityService.ChangeUserPassword(user, newPW);
            }
        }
        ```

        [예제 7 - 의존성 주입한 AccountController]

        AccountController을 메모리에 할당하는(외부) 곳 에서 SecurityService 객체를 인자로 넘기는데  
        AccountController 생성자의 시그니처 데이터 타입은 ISecurityService이다.  
        SecurityService는 ISecurityService를 상속받는 클래스이므로 전혀 문제가 없고 오히려 구현체를 모르니 SecurityService가 의존하는 부분을 알지 못한다.  
        즉, AccountController는 SecurityService로 인한 잠제적인 의존성으로 부터 해방이 된 것이다.  
        이제 SecurityService도 의존성 주입 기법을 적용하여 보자.

        ```cs
        //UserRepository도 DI를 적용해서 User를 의존하지 말아 보자(못할 이유가 있는가?)
        public class UserRepository : IUserRepository
        {
            private readonly IDbConnection _connection;

            public UserRepostitory(IDbConnection connection)
            {
                if(connection == null)
                    throw new ArgumentNullException("connection");

                _connection = connection;
            }

            public IUser GetByID(Guid userID)
            {
                //가상의 테이블 조회 쿼리
                var query = string.Format("select * from USER_TABLE where ID = {0}", userID);

                //User 클래스는 IUser를 상속 받는다는 가정하에..
                return _connection.QuerySingleOrDefault<User>(query);
            }
        }
        //...
        public class SecurityService : ISecurityService
        {
            private readonly IUserRepository _userRepository;

            public SecurityService(IUserRepository userRepository)
            {
                if(userRepository == null)
                    throw new ArgumentNullException("userRepository");

                _userRepository = userRepository;
            }

            public void ChangeUserPassword(Guid userID, string newPW)
            {
                _userRepository.GetByID(userID)?.ChangePassword(newPW);
            }
        }
        ```

        [예제 8 - 의존성 주입한 UserRepository, SecurityService]

        SecurityService, UserRepository, User 의존성이 전부 제거되었다.(외부에서 메모리에 할당하므로...)