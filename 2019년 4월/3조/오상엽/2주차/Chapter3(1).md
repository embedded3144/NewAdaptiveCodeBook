# 인터페이스와 디자인 패턴
- 인터페이스(interface)는 마이크로소프트 닷넷 프레임워크를 기반으로한 개발 환경에서 매우 강력한 구조체임
- 올바른 인터페이스의 활용은 코드의 적응력을 향상시킬 수 있는 다양한 확장점(extension point)를 제공함

## 인터페이스란 무엇인가?
- 인터페이스는 클래스가 가지는 동작을 정의하지만, 실제로 이 동작을 구현하지 않음
- 인터페이스는 클래스와는 별개로 취급되지만, 클래스로 하여금 인터페이스를 만족하기 위해 실제로 동작하는 코드를 구현할 것을 요구함

### 문법
- interface 키워드를 이용해 정의하며, 클래스처럼 속성, 메서드 및 이벤트를 선언할 수 있음
- 인터페이스의 요소들은 접근 한정자(access modifier)를 사용하지 않음
- 인터페이스를 구현하는 클래스는 인터페이스의 모든 멤버를 반드시 public 한정자를 이용해 구현해야 함

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
        get { return this.encapsulatedInteger; }
        set { this.encapsulatedInteger = value; }
    }

    public event EventHandler<EventArgs> InterfaceCanContainEventsToo = delegate { };

    private int encapsulatedInteger;
}
```

- 닷넷 프레임워크는 클래스의 다중 상속을 지원하지 않지만, 하나의 클래스가 여러 개의 인터페이스를 구현하는 것은 허용함

```cs
public interace IInterfaceOne
{
    void MethodOne();
}

public interface IInterfaceTwo
{
    void MethodTwo();
}

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
- 하나의 클래스는 여러개의 인터페이스 구현 가능, 하나의 인터페이스는 여러 클래스에 의해 다르게 구현 가능

### 명시적 구현
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
- 인터페이스를 명ㅅ적으로 구현했다면 클라이언트는 인터페이스를 구현한 클래스의 인스턴스가 아니라 인터페이스 자체의 인스턴스를 참조해야 함

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
- 명시적 구현은 클래스가 구현해야 하는 인터페이스의 메서드와 동일한 시그니처를 가진 메서드를 이미 정의하고 있어, 시그너처의 충돌이 발생함에 따라 이를 피하고자 하는 경우에만 사용함
- 닷넷 환경에서 정의할 수 있는 모든 메서드는 각자의 메서드 시그너처를 가잠 이 시그너처는 메서드를 유일하게 식별하고 오버로드(overload)된 메서드를 구분하기 위해 사용됨 메서드의 시그너처를 메서드의 이름과 매개변수의 목록으로 구성됨. 메서드의 접근 한정자나 리턴 값, abstract 키워드나 sealed 키워드의 사용 여부는 메서드의 시그너처에 포함되지 않음

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
- 속성은 매개변수 목록을 갖지 않기 때문에 이름으로만 구분됨, 따라서 클래스에 ㅏㅌ은 이ㅡㅁ의 속성을 정의하는 경우 속성의 시그너처 충돌이 발생하게 됨

```cs
ex) 3.6 
메서드 시그니처 충돌 야기
public class ClassWithMethodSignatureClash
{
    public void MethodOne()
    {
    }
}
```

```cs
ex) 3.7 
묵시적 인터페이스 구현을 통해 정의된 메서드를 그대로 사용 가능
public class ClassWithMethodSignatureClash : IInterfaceWithSignatureClash
{
    public void MethodOne()
    {
    }
}
```

- 클라이언트가 이 클래스에 정의된 인터페이스 메서드를 호출하면 이미 존재하는 같은 메서드가 호출됨. 이 방법이 유용한 경우는 WinForms에서 `MVP(Model-View-Presenter) 패턴`을 구현할 때 Form 클래스에 Close 메서드를 구현해야하는 `IView 인터페이스 구현을 추가하는 경우임.


```cs
ex) 3.9
인터페이스를 명시적으로 구현하여 메서드 시그너처 충돌을 피할 수 있다
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

- 클래스가 서로 관련은 없지만 동일한 시그너처를 가진 메서드를 정의한 두 개의 서로 다른 인터페이스를 구현할 때도 두 인터페이스를 모두 묵시적으로 구현하고 하나의 메서드 구현을 공유할 수 있음

- 두 인터페이스를 모두 명시적으로 구현하여 인터페이스별로 특정 구현을 구분할 수 있음

```cs
ex) 3-10
동일한 시그너처의 메서드를 정의한 두 개의 인터페이스를 구현할 때는 명시적 구현이 더 적합하다
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
- 한 타입의 객체를 암묵적으로 다른 태입의 객체처럼 동작하도록 사용하는 방법을 다형성(polymorphism)이라고 함.

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