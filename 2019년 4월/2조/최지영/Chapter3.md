# Chapter 3. 인터페이스와 디자인 패턴

## 1. 인터페이스란 무엇인가? 

### 1-1. 문법

- 인터페이스는 클래스가 가지는 동작을 정의하지만 실제로 이 동작을 구현하지는 않는다.
- `interface` 키워드 사용, 속성, 메서드, 이벤트를 선언할 수 있다.
- 인터페이스의 요소들은 접근지정자를 사용하지 않는다.
- 인터페이스를 구현하는 클래스는 인터페이스의 모든 멤버를 반드시 public 지정자를 이용해 구현해야 한다.
- 닷넷 프레임워크에서는 클래스의 다중상속을 지원하지 않지만, 여러개의 인터페이스를 구현하는 것은 허용한다. 인터페이스의 개수에는 제한이 없다.

### 1-2. 명시적 구현

- 명시적 구현은 클래스가 구현해야 하는 인터페이스의 메서드와 동일한 시그너처를 가진 메서드를 이미 정의하고 있어, 시그너처의 충돌을 피하고자 하는 경우에만 유용하다.

- 동일한 시그너처의 메서드를 정의한 두 개의 인터페이스를 구현할 때는 명시적 구현이 더 적합하다.

```cs
public class A : IInterfaceA, IInterfaceB
{
    public void IInterfaceA.MethodA()
    {

    }

    public void IInterfaceB.MethodA()
    {

    }
}
```

### 1-3. 다형성

- 한 타입의 객체를 암묵적으로 다른 타입의 객체처럼 동작하도록 사용하는 방법을 다형성(polymorphism)이라고 한다.
- 클라이언트 코드는 실제로는 다른 타입의 객체인 것을 자신이 원하는 특정 객체인 것처럼 처리할 수 있다.

> ★ 객체지향 특성 : 추상화, 캡슐화, 상속, 다형성, 은닉 ...

## 2. 적응형 디자인 패턴

- GoF 디자인 패턴.
- 안티패턴
- 너무 남용하지도 말고, 충분히 적용되지 않아서도 안되고, 적절한 곳에 적절한 패턴을 적용해야 한다.

### 2-1. 널 객체 패턴 

- 널 객체 패턴(Null Object pattern) 의 목적은 불시에 NullReferenceException 예외가 발생하는 것을 방지하고 코드에서 null 값을 가진 객체를 검사하기 위한 코드를 제거하는 것이다.

- 예제

```cs
    public interface IUser
    {
        void IncrementSessionTicket();

        string Name
        {
            get;
        }
    }

    public interface IUserRepository
    {
        IUser GetByID(Guid userID);
    }

    public class NullUser : IUser
    {
        public void IncrementSessionTicket()
        {
            //do nothing
        }

        public string Name
        {
            get
            {
                return "unknown";
            }
        }
    }

    class User : IUser
    {
        public Guid ID { get; private set; }
        public string Name { get; private set; }
        private DateTime _sessionExpiry;
        public User(Guid id, string name)
        {
            ID = id;
            Name = name;
            _sessionExpiry = DateTime.Now;
            IncrementSessionTicket();
        }

        public void IncrementSessionTicket()
        {
            _sessionExpiry.AddMinutes(30);
        }
    }

    public class UserRepository : IUserRepository
    {
        private ICollection<User> _users;
        public UserRepository()
        {
            _users = new List<User>
            {
                new User(Guid.NewGuid(), "Bob"),
                new User(Guid.NewGuid(), "Henry"),
                new User(Guid.NewGuid(), "Charles"),
                new User(Guid.NewGuid(), "Rothbard")
            };
        }

        public IUser GetByID(Guid userID)
        {
            IUser userFound = _users.SingleOrDefault(user => user.ID == userID);
            if (userFound == null)
            {
                userFound = new NullUser();
            }
            return userFound;
        }
    }

    class Program
    {
        static IUserRepository _userRepository = new UserRepository();
        static void Main(string[] args)
        {
            var user = _userRepository.GetByID(Guid.NewGuid());

            user.IncrementSessionTicket();

            Console.WriteLine("The user's name is {0}", user.Name);

            Console.ReadKey();
        }
    }
```

### 2-2. 어댑터 패턴

- 실제로는 객체가 구현하고 있지 않은 인터페이스의 인스턴스를 클라이언트 코드에 전달할 수 있는 방법.
- 인터페이스의 실제 구현은 다른 객체가 제공하는 메서드에 위임하게된다.
- 주로 대상 클래스가 필요한 인터페이스로 전환하기에 적절하지 않는 경우, 즉 sealed 클래스이거나 소스코드를 보유하지 않은 어셈블리에 구현된 클래스인 경우에 활용된다.

- 예제

```cs
    public interface IExpectedInterface
    {
        void MethodA();
    }

    public class Adapter : IExpectedInterface
    {
        private TargetClass _target;

        public Adapter(TargetClass target)
        {
            _target = target;
        }

        public void MethodA()
        {
            _target.MethodB();
        }
    }

    public class TargetClass
    {
        //실제 동작을 여기에 위임한다.
        public void MethodB()
        {
            Console.WriteLine("Method B");
        }
    }

    class Program
    {
        static IExpectedInterface dependency = new Adapter(new TargetClass());
        static void Main(string[] args)
        {
            dependency.MethodA();
        }
    }
```

### 2-3. 전략 패턴
- 코드를 재컴파일하지 않고도, 심지어 런타임에 실행중인 상황에서도 클래스의 동작을 원하는 대로 변경하기 위한 패턴이다.
- 객체의 상태에 따라 다양한 동작을 수행하는 클래스가 필요할 때 사용한다.

- 예제 : 직급에 따라 페이를 받는 비율이 다를 때의 예제 코드를 작성해보았다.

```cs
    public interface IPayMoney
    {
        void GetPay(int money);
    }

    public class Clerk : IPayMoney
    {
        public void GetPay(int money)
        {
            Console.WriteLine("Clerk Money : {0}", money + money * (0.1));
        }
    }

    public class Manager : IPayMoney
    {
        public void GetPay(int money)
        {
            Console.WriteLine("Manager Money : {0}", money + money * (0.3));
        }
    }

    public class Calculator
    {
        private IPayMoney _currentStrategy;

        public Calculator(IPayMoney currentStrategy)
        {
            _currentStrategy = currentStrategy;
        }

        public void Calculate(int money)
        {
            _currentStrategy.GetPay(money);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int money = 100;

            Calculator clerkMoney = new Calculator(new Clerk());
            clerkMoney.Calculate(money);

            Calculator MangerMoney = new Calculator(new Manager());
            MangerMoney.Calculate(money);
        }
    }
```

## 3. 인터페이스의 또 다른 활용법

### 3-1. 믹스인

#### 확장 메서드의 사용

- 확장 메서드는 .NET Framework 3.5부터 지원되며, 이미 존재하는 타입에 새로운 기능을 추가하기 위한 기능을 제공한다. 
- 타입의 소스 코드를 직접 수정하지 않아도 되고, 해당 타입이 partial 타입으로 지정되어 있지 않아도 원하는 타입을 확장할 수 있다.
- 정적 클래스는 모의객체로의 교체가 원활하지 않아서 확장 메서드를 사용하는 모든 클라이언트는 적절한 단위 테스트의 수행이 어려워진다. (정적메서드는 테스트가 어렵다..)
- 확장 메서드는 정적 클래스로 구현되기 때문에 객체의 인스턴스 별 상태를 관리할 수 없다. 
- 첫 번째 파라미터에 `this` 사용

```cs
using System;
using System.Text;

namespace MySystem
{
   // static class를 정의
   public static class ExClass
   {
      // static 확장메서드를 정의. 첫번째 파라미터는 어떤 클래스가 사용할 지만 지정. 
      public static string ToChangeCase(this String str)
      {
         StringBuilder sb = new StringBuilder();
         foreach (var ch in str)
         {
            if (ch >= 'A' && ch <= 'Z')
               sb.Append((char)('a' + ch - 'A'));
            else if (ch >= 'a' && ch <= 'x')
               sb.Append((char)('A' + ch - 'a'));
            else
               sb.Append(ch);
         }
         return sb.ToString();
      }

      // 이 확장메서드는 파라미터 ch가 필요함
      public static bool Found(this String str, char ch)
      {
         int position = str.IndexOf(ch);
         return position >= 0;
      }
   }

   class Program
   {
      static void Main(string[] args)
      {
         string s = "This is a Test";
         
         // s객체 즉 String객체가 확장메서드의 첫 파리미터임
         // 실제 ToChangeCase() 메서드는 파라미터를 갖지 않는다.
         string s2 = s.ToChangeCase();

         // String 객체가 사용하는 확장메서드이며 z 값을 파라미터로 사용
         bool found = s.Found('z');
      }
   }
}

```

### 3-2. 능동형 인터페이스 (Fluent Interface)

- 인터페이스가 능동적(fluent)으로 동작하기 위해서는 하나 혹은 그 이상의 정의된 메서드들이 인터페이스 자신을 리턴해야한다.(`return this;`) 
- 메서드를 연결하여 호출할 수 있다.

```cs
    public interface IFluentInterface
    {
        IFluentInterface FluentA();
        IFluentInterface FleuntB();
        IFluentInterface FleuntC();
        void ThisMethodIsNotFluent();
    }

    public class FluentImplementation : IFluentInterface
    {
        public IFluentInterface FleuntB()
        {
            return this;
        }

        public IFluentInterface FleuntC()
        {
            return this;
        }

        public IFluentInterface FluentA()
        {
            return this;
        }

        public void ThisMethodIsNotFluent()
        {
            
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IFluentInterface fluent = new FluentImplementation();

            fluent.FluentA()
                .FleuntB()
                .FleuntC()
                .FluentA()
                .FleuntB()
                .ThisMethodIsNotFluent();
        }
    }
```