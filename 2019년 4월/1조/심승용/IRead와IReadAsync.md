# IRead와 IReadAsync
- 1-2조에서 논의한 내용을 정리 하였습니다.

## Notepad 프로젝트 
- Notepad를 만들때 IRead 사용하여 개발을 합니다.
~~~plantuml
@startuml
class Notepad
interface IRead 
{
    Read()
}
Notepad -> IRead
@enduml
~~~
- IRead의 구현은 다음과 같습니다.
~~~plantuml
@startuml
class Notepad
interface IRead 
{
    Read()
}
class ReadCSharp
class ReadCpp
class ReadJS
Notepad -> IRead

IRead <|-down- ReadCSharp
IRead <|-down- ReadCpp
IRead <|-down- ReadJS
@enduml
~~~

- 프로젝트의 우수한 성과로 Read 클래스들을 재사용 가능한 Mirero.Read 패키지로 만들었습니다.
~~~plantuml
@startuml
class Notepad
package "Mirero.Read" #DDDDDD {
interface IRead 
{
    Read()
}
class ReadCSharp
class ReadCpp
class ReadJS
}

Notepad -> IRead
IRead <|-down- ReadCSharp
IRead <|-down- ReadCpp
IRead <|-down- ReadJS
@enduml
~~~

- Notepad 프로젝트가 종료되었습니다.

## VisualStudio 프로젝트 
- Mirero.Read를 사용하여 개발을 시작합니다.
~~~plantuml
@startuml
class VisualStudio
package "Mirero.Read" #DDDDDD {
interface IRead 
{
    Read()
}
class ReadCSharp
class ReadCpp
class ReadJS
}

VisualStudio -left-> IRead
IRead <|-down- ReadCSharp
IRead <|-down- ReadCpp
IRead <|-down- ReadJS
@enduml
~~~
- 프로젝트를 진행 하는 도중 ReadAsync 기능이 필요해 졌습니다.
   - 1안) IRead 인터페이스에 ReadAsync 함수를 추가
   - 2안) IRead를 상속한 IReadAsync를 추가

### 1안) IRead 인터페이스에 ReadAsync 함수 추가
- VisualStudio에서 필요로 하는 ReadAsync 함수를 IRead에 추가 하였습니다.
- IRead 인터페이스가 변경 되어서 구현 클래스도 모두 ReadAsync 함수를 구현해야 합니다.
~~~plantuml
@startuml
class VisualStudio
package "Mirero.Read" #FFDDDD {
interface IRead 
{
    Read()
    -ReadAsync()
}
class ReadCSharp
{
    -ReadAsync()
}
class ReadCpp
{
    -ReadAsync()
}
class ReadJS
{
    -ReadAsync()
}
}

VisualStudio -left-> IRead
IRead <|-down- ReadCSharp
IRead <|-down- ReadCpp
IRead <|-down- ReadJS
@enduml
~~~
- 새로 만들어진 Mirero.Read 패키지는 기존 Notepad에서 사용 수 있습니다.
~~~plantuml
@startuml
class Notepad
package "Mirero.Read" #FFDDDD {
interface IRead
{
    Read()
    -ReadAsync()
}
}
Notepad -right-> IRead
@enduml
~~~

### 2안) IRead를 상속한 IReadAsync를 추가
- IReadAsync 인터페이스를 추가 하였습니다.
~~~plantuml
@startuml
class Notepad
class VisualStudio
package "Mirero.Read" #DDFFDD {
interface IRead 
{
    Read()
}
interface IReadAsync
{
    -ReadAsync()
}
class ReadCSharp
{
    Read()
}
class ReadCpp
{
    Read()
}
class ReadJS
{
    Read()
}
class ReadCSharpAsync
{
    - ReadAsync()
}
}
Notepad -right-> IRead
IRead <|-down- ReadCSharp
IRead <|-down- ReadCpp
IRead <|-down- ReadJS
IRead <|-right- IReadAsync
VisualStudio -left-> IReadAsync
ReadCSharp <|-down- ReadCSharpAsync
IReadAsync <|-down- ReadCSharpAsync
@enduml
~~~
- VisualStudio는 그동안 사용하던 IRead 대신 IReadAsync 인터페이스를 사용합니다.
- 기존 Notepad는 새로 만들어진 Mirero.Read 패키지를 사용 가능합니다.
- https://docs.microsoft.com/ko-kr/dotnet/csharp/language-reference/keywords/new-modifier
