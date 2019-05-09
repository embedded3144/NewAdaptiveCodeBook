# 인터페이스와 디자인 패턴

`인터페이스(interface)`는 마이크로 소프트 닷넷 프레임워크를 기반으로 한 개발 환경에서는 매우 강력한 구조체이다. `올바른 인터페이스의 활용은 코드의 적응력을 몰라보게 향상시킬 수 있는 다양한 확장점(extension point)을 제공한다.`

그러나 인터페이스가 그 자체로 모든 문제를 해결할 수 있는 것은 아니다. 물론, 인터페이스를 올바른 방향으로 활용하면 프로젝트에 도움이 되는 것은 분명하지만, 자칮 잘못된 방향으로 적용하면 모든 것을 그르칠 수 있다.

## 인터페이스란 무엇인가?

인터페이스는 클래스가 가지는 동작을 정의하지만, 실제로 이 동작을 구현하지는 않는다. 인터페이스는 클래스와는 별개로 취급되지만, 클래스로 하여금 인터페이스를 만족하기 위해 실제로 동작하는 코드를 구현할 것을 요구한다.

### 문법

인터페이스는 interface 키워드를 이용해 정의하며, 클래스처럼, 속성, 메서드 및 이벤트를 선언 할 수 있다. 그러나 인터페이스의 요소들은 접근 한정자(access modifier)를 사용하지 않는다. 또한 인터페이스를 구현하는 클래스는 인터페이스의 모든 멤버를 반드시 public 한정자를 이용해 구현해야 한다.

```cs
public interface ISimpleInterface
{
    void ThisMethodRequiresImplementation();

    string ThisStringPropertyNeedsImplementingToo
    {
        get;
        set;
    }

    int ThisIntergerPropertyOnlyNeedsAGetter
    {
        get;
    }

    event EventHandler<EventArgs> InterfaceCanContainEventsToo;
}
// ...

public class SimpleInterfaceImplementation : ISimpleInterface
{
    public void ThisMethodRequiresImplementation()
    {

    }

    public string ThisStringPropertyNeedsImplementingToo
    {
        get;
        set;
    }

    public int ThisIntergerPropertyOnlyNeedsAGetter
    {
        get
        {
            return this.encapsulatedInteger;
        }
        set
        {
            this.encapsulatedInteger = value;
        }
    }

    public event EventHandler<EventArgs> InterfaceCanContainEventsToo = delegate { };

    private int encapsulatedInteger;
}
```

닷넷 프레임워크는 클래스의 다중 상속을 지원하지 않지만, 하나의 클래스가 여러 개의 인터페이스를 구현하는 것은 허용한다.

클래스가 구현할 수 있는 인터페이스의 개수에는 제한이 없다. 한 클래스가 구현하는 인터페이스의 개수는 그저 상식의 수준을 벗어나지 않는 정도면 된다.

```cs
public interace IInterfaceOne
{
    void MethodOne();
}
//...
public interface IInterfaceTwo
{
    void MethodTwo();
}
// ...
publci class ImplementingMultipleInterfaces : IInterfaceOne, IInterfaceTwo
{
    public void MethodOne()
    {
    }

    public void MethodTwo()
    {
    }
}
```

예제를 통해 보듯이 하나의 클래스는 여러 개의 인터페이스를 구현할 수 있으며, 하나의 인터페이스는 마찬가지로 여러 클래스에 의해 각자 다르게 구현될 수 있다.

### 다이아몬드 상속 문제

닷넷 프레임워크에서 다중 상속을 허용하지 않는 이유 중 하나는 `다이아몬드 상속 문제(diamond ingeritance problem)`이다. 이 문제는 두 개 혹은 그 이상의 클래스가 동일한 클래스를 상속할 때 발생한다.

```plantuml
RootClass <|-- BaseClassOne
RootClass <|-- BaseClassTwo
BaseClassOne <|-- AttemptedMultipleInheritance
BaseClassTwo <|-- AttemptedMultipleInheritance
class RootClass{
  +void RootMethod()
}

class BaseClassOne{
  +void MethodOne()
}

class BaseClassTwo{
  +void MethodTwo()
}

class AttemptedMultipleInheritance{
  +void MethodOne()
  +void MethodTwo()
  +void WhichRootMethod?()
}
```

`BaseClassOne`과 `BaseClassTwo`를 상속받은 `AttemptedMultipleInheritance` 클래스는 둘 중 어느 것의 `RootMethod`를 불러야 하는 문제가 생긴다. 그래서 닷넷 프레임워크에서는 클래스의 다중 상속을 지원하지 않는다.

### 명시적 구현

인터페이스는 `명시적(explicitly)`으로 구현될 수 있다. `명시적 인터페이스 구현(explicit implementation)`이란, 앞의 예제에서 설명한 `묵시적(implicit) 인터페이스` 구현과는 다르다.

```cs
public class ExpliciInterfaceImplementation : ISimpleInterface
{
    public ExpliciInterfaceImplementation()
    {
        this.encapsulatedInteger = 4;
    }

    void ISimpleInterface.ThisMethodRequiresImplementation()
    {
        encapsulatedEvent(this, EventArgs.Empty);
    }

    string ISimpleInterface.ThisStringPropertyNeedsImplementingToo
    {
        get;
        set;
    }

    int ISimpleInterface.ThisIntergerPropertyOnlyNeedsAGetter
    {
        get
        {
            return encapsulatedInteger;
        }
    }

    event EventHandler<EventArgs> ISimpleInterface.InterfaceCanContainEventsToo
    {
        add { encapsulatedEvent += value; }
        remove { encapsulatedEvent -= value; }
    }

    private int encapsulatedInteger;
    private event EventHandler<EventArgs> encapsulatedEvent;
}
```

인터페이스를 명시적으로 구현했다면 클라이언트는 인터페이스를 구현한 클래스의 인스턴스가 아니라 인터페이스 자체의 인스턴스를 참조해야 한다.

```cs
public class ExplicitInterfaceClient
{
    public ExplicitInterfaceClient(ExplicitInterfaceImplementation implementationReference, ISimpleInterface interfaceReference)
    {
        //Uncommenting this will cause compilation errors.
        //var instancePropertyValue = implementationReference.ThisIntegerPropertyOnlyNeedsAGetter;
        //implementationReference.ThisMethodRequiresImplementation();
        //implementationReference.ThisStringPropertyNeedsImplementingToo = "Hello";
        //implementationReference.InterfacesCanContainEventsToo += EventHandler;

        var interfacePropertyValue = interfaceReference.ThisIntegerPropertyOnlyNeedsAGetter;
        interfaceReference.ThisMethodRequiresImplementation();
        interfaceReference.ThisStringPropertyNeedsImplementingToo = "Hello";
        interfaceReference.InterfaceCanContainEventsToo += EventHandler;
    }

    void EventHandler(object sender, EventArgs e)
    {

    }
}
```

명시적 구현은 클래스가 구현해야 하는 인터페이스의 메서드와 동일한 시그너처를 가진 메서드를 이미 정의하고 있어, 시그너처의 충돌이 발생함에 따라 이를 피하고자 하는 경우에만 유용하다. 아래는 `묵시적 인터페이스 구현`이다.

```cs
public class ImplicitInterfaceClient
{
    public ImplicitInterfaceClient(SimpleInterfaceImplementation implementationReference, ISimpleInterface interfaceReference)
    {
        var instancePropertyValue = implementationReference.ThisIntegerPropertyOnlyNeedsAGetter;
        implementationReference.ThisMethodRequiresImplementation();
        implementationReference.ThisStringPropertyNeedsImplementingToo = "Hello";
        implementationReference.InterfaceCanContainEventsToo += EventHandler;

        var interfacePropertyValue = interfaceReference.ThisIntegerPropertyOnlyNeedsAGetter;
        interfaceReference.ThisMethodRequiresImplementation();
        interfaceReference.ThisStringPropertyNeedsImplementingToo = "Hello";
        interfaceReference.InterfaceCanContainEventsToo += EventHandler;
    }

    void EventHandler(object sender, EventArgs e)
    {

    }
}
```

닷넷 환경에서 정의할 수 있는 `모든 메서드는 각자의 메서드 시그너처를 가진다. 이 시그너처는 메서드를 유일하게 식별하고 오버로드(overload)된 메서드를 구분하기 위해 사용된다.` 메서드의 시그너처를 메서드의 이름과 매개변수의 목록으로 구성된다. 메서드의 접근 한정자나 리턴 값, abstract 키워드나 sealed 키워드의 사용 여부는 메서드의 시그너처에 포함되지 않는다는 점을 명심하자.

아래의 예제는 어떤 조건에서 시그너처 충돌이 발생하는지 보여준다.

```cs
public class ClashingMethodSignatures
{
    public void MethodA()
    {

    }

    // 이 메서드는 위에 정의한 매서드의 시그너처와 충돌한다.
    //public void MethodA()
    //{

    //}

    // 이 매서드 역시 리턴 값은 시그너처에 포함되지 않기 때문에 충돌이 발생한다.
    //public int MethodA()
    //{
    //    return 0;
    //}

    public int MethodB(int x)
    {
        return x;
    }

    // 이번에는 매개변수가 다르기 때문에 충돌이 발생하지 않는다.
    // 이는 위에 정의한 MethodB를 오버로드한 형태이다.
    public int MethodB(int x, int y)
    {
        return x + y;
    }
}
```

`속성은 매개변수 목록을 갖지 않기 때문에 그 이름으로만 구분된다.` 따라서 클래스에 같은 이름의 속성을 정의하는 경우 속성의 시그너처 충돌이 발생하게 된다.

아래의 클래스를 살펴보자. 이 클래스는 앞서 언급했던 `InterfaceOne` 인터페이스를 구현해야 한다.

```cs
public class ClassWithMethodSignatureClash
{
    public void MethodOne()
    {
    }
}
```

먼저, 이 클래스에 정의된 메서드의 시그너처가 동일하므로 이런 경우에는 아래와 같이 클래스 선언부에 인터페이스 구현에 대한 표기만 해 주면 된다.

```cs
public class ClassWithMethodSignatureClash : IInterfaceWithSignatureClash
{
    public void MethodOne()
    {
    }
}
```

클라이언트가 이 클래스에 정의된 인터페이스 메서드를 호출하면 이미 존재하는 같은 메서드가 호출된다. 이 방법이 유용한 경우는 WinForms에서 `MVP(Model-View-Presenter) 패턴`을 구현할 때 Form 클래스에 Close 메서드를 구현해야하는 `IView 인터페이스 구현을 추가하는 경우이다.`

또한 아래와 같이 스그니처 충돌이 발생하는 메서드를 명시적으로 구현해서 피할 수 있다.

```cs
public class ClassAvoidingMethodSignatureClash : IInterfaceOne
{
    public void MethodOne()
    {
        // 원래의 구현 코드
    }

    void IInterfaceOne.MethodOne()
    {
        // 새로운 구현 코드
    }
}
```

마찬가지로, 클래스가 서로 관련은 없지만 동일한 시그너처를 가진 메서드를 정의한 두 개의 서로 다른 인터페이스를 구현할 때도 두 인터페이스를 모두 묵시적으로 구현하고 하나의 메서드 구현을 공유할 수 있다.

또는 아래의 예제에서 보는 것과 같이 두 인터페이스를 모두 명시적으로 구현하여 인터페이스별로 특정 구현을 구분할 수 있습니다.

```cs
public class ClassImplementingClashingInterfaces : IInterfaceOne, IAnotherInterfaceOne
{
    void IInterfaceOne.MethodOne()
    {

    }

    void IAnotherInterfaceOne.MethodOne()
    {

    }
}
```

### 다형성

한 타입의 객체를 암묵적으로 다른 태입의 객체처럼 동작하도록 사용하는 방법을 `다향성(polymorphism)`이라고 한다. 아래의 그림은 교통수단의 동작을 표현하는 인터페이스 및 이를 구현하는 자동차, 오토바이 및 고속정 등의 클래스를 저으이하고 있다. 이들 세 종류의 교통 수단은 상당히 다르지만, 같은 기반 인터페이스를 구현하고 있으므로 동일한 동작을 표현할 수 있음에 주목하자.

```plantuml
interface IVehicle
class Car
class Motorcycle
class Speedboat
IVehicle <|-- Car
IVehicle <|-- Motorcycle
IVehicle <|-- Speedboat

Interface IVehicle{
  +StartEngine()
  +StopEngine()
  +Steer()
  +Accelerate()
}

class Car{
  +StartEngine()
  +StopEngie()
  +Steer()
  +Accelerate()
}

class Motorcycle{
  +StartEngine()
  +StopEngie()
  +Steer()
  +Accelerate()
}

class Speedboat{
  +StartEngine()
  +StopEngie()
  +Steer()
  +Accelerate()
}
```

이 예제에서는 모든 교통 수단은 시동을 걸고 끌 수 있는 엔진을 탑재하고 있으며, 조향장치가 장착되어 있고 가속이 가능하다고 가정한다. 다형성을 활용하면 클라이언트 코드가 `IVehicle 인터페이스`의 참조를 확보함으로써 모든 구현체를 같은 방법으로 취급할 수 있게 된다.