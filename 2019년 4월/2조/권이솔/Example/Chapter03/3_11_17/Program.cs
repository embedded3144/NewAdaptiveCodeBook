using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter03._3_11
{
    class Program
    {
        static IUserRepository userRepository = new UserRepository();
        static void Main(string[] ags)
        {
            var user = userRepository.GetById(Guid.NewGuid());
            // 널 객체 패턴을 사용하지 않으면 다음 코드는 예외를 발생하게 된다.
            //user.IncrementSessionTicket();

            // 3_12 정말 모든 클라이언트가 null 값을 검사할 책임을 가져야 할까?
            //if (user != null)
            //    user.IncrementSessionTicket();

            user.IncrementsSessionTicket();

            string userName;
            if(!user.IsNull)
            {
                userName = user.Name;
            }
            else
            {
                userName = "알 수 없음";
            }

            Console.WriteLine("사용자의 이름은 {0} 입니다.", userName);
            Console.ReadKey();
        }
    }
}
