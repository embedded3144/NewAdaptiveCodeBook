# 단위 테스트
- 단위테스트는 모든 프로그래머에게 필수적인 것으로 이식되어야 한다.
- 준비 과정과 단위 테스트의 이름 규칙, 무엇보다도 테스트 가능성 확보 여부가 가장 중요하다. 

## 준비 동작 검증
모든 단위 테스트는 아래의 세 가지 부분으로 구성된다.
- 테스트의 사전 조건을 준비한다. 
- 테스트해야 할 동작을 실행한다.
- 예상되는 동작이 발생했는지를 검증한다.
이 세가지를 일명 AAA(Arrange, Act, Assert) 패턴이라고 한다.

### 사전 조건 준비하기 (Arrange)
- 테스트해야 할 동작을 실행하기에 앞서 테스트를 위한 시나리오를 준비해야 한다. 
- 테스트할 시스템을 구성한다. (클래스의 인스턴스 생성) 
    
```cs
// 예제 4-1
public class AccountTest
{
    [TestMethod]
    public void AddingTransactionChangesBalance()
    {
        // 준비
        var account = new AccountTest();
    }
}
```
### 테스트할 동작 실행하기 (Act)
- 테스트의 동작 단계는 테스트하고자 하는 시스템과 단 한 번의 상호작용을 수행해야 한다.
```cs
// 예제 4-2
public class AccountTest
{
    [TestMethod]
    public void AddingTransactionChangesBalance()
    {
        // 준비
        var account = new AccountTest();

        // 동작
        account.AddTransaction(200m);
    }
}
```

### 예상 결과 검증하기 (Assert)
- 검증 단계에서 테스트 전체의 성공 및 실패 여부를 판단한다.
- 검증은 실제 값과 기댓값의 비교를 통해 가능하다. 
- 새로운 계좌를 생성하고 초기 잔액을 0으로 설정했다면, 200달러를 계좌에 입금했을 때의 기댓값은?
  $0 + $200 = $200
```cs
// 예제 4-3
public class AccountTest
{
    [TestMethod]
    public void AddingTransactionChangesBalance()
    {
        // 준비
        var account = new AccountTest();

        // 동작
        account.AddTransaction(200m);
        
        // 검증
        Assert.AreEqual(200m, account.Balance);
    }
}
```

### 테스트 실행하기
- 테스트의 작성을 마쳤으면 단위 테스트 실행기를 통해 테스트를 실행해야 한다.
- 테스트 프로젝트는 스스로 실행될 수 없기 때문에 단위 테스트 실행기가 필요하다.
- 비주얼 스튜디오 -> 테스트 -> 실행 메뉴의 선택 항목 중 하나를 이용하여 MSTest 실행
(그림 4-1)
- 테스트 할 Account 클래스를 구현하지 않았기 때문에 테스트에 실패한다.
- 
## 테스트 주도 개발 (TEST Driven Development)
- 단위 테스트를 구현하기 위해 테스트할 시스템 전체를 완벽히 구현할 필요는 없다.  
- TDD는 단위 테스트를 먼저 작성 작성한 다음 프로덕션 코드를 작성한다는 것을 의미한다. 
- 프로덕션 코드가 아직 존재하지 않음으로 처음에는 테스트에 실패하게 된다.

### 빨강, 초록, 리팩토링
- 빨강, 초록, 리팩토링의 3단계
1. SUT에 실패하는 테스트 코드를 작성한다. (AAA 패턴)
2. SUT에 필요한 코드만을 구현하여 기존에 성공한 테스트에 영향을 미치치 않고도 새로 구현한 테스트가 성공하도록 한다.
3. SUT의 디자인이나 품질을 향상시키기 위해 리팩토링을 시작한다.
```cs
// 예제4-5 항상 테스트에 성공하기 위한 최소한의 코드
public class Account
{
    public Account()
    {
        Balance = 200m;
    }

    public void AddTransaction(decimal amount)
    {

    }

    public decimal Balance
    {
        get;
        private set;
    }
}
```
(그림 4-2)
- 테스트에 성공했으면 마지막 단계인 리팩토링 단계로 넘어간다.
- 또 다른 예상 동작에 대한 단위 테스트를 추가하여 현재 구현이 올바르지 않다는 것을 증명할 수 있다.
```cs
// 예제 4-6
[TestMethod]
public void AccountHaveAnOpeningBalanceOfZero()
{
    // 준비
    // 동작
    var account = new Account();
`
    // 검증
    Assert.AreEqual(0m, account.Balance);
}
```
(그림 4-3)

- 테스트가 모두 성공하지만 아직도 문제가 있다
```cs
// 예제 4-9
public class Account
{
    public void AddTransaction(decimal amount)
    {
        Balance = amount;
    }

    public decimal Balance
    {
        get;
        private set;
    }
}
```
- 또 다른 문제점?
```cs
// 예제 4-10
[TestMethod]
public void AddingTransactionsCreatesSummationBalance()
{
    // 준비
    var account = new Account();
    account.AddTransaction(50m);

    // 동작
    account.AddTransaction(75m);

    // 검증
    Assert.AreEqual(125m, account.Balance);
}

```
- 이 테스트를 통해 AddTransaction 메서드가 어떻게 동작해야 하는지 알게되었다.

```cs
// 예제 4-11 올바르게 구현한 코드
public class Account
{
    public void AddTransaction(decimal amount)
    {
        Balance += amount;
    }

    public decimal Balance
    {
        get;
        private set;
    }
}
```
- 이 예제로 모든 단위 테스트를 성공하였다^^
(그림 4-4)