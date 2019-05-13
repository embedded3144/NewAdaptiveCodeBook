using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter05._5_컴포지트패턴
{
    public interface IComponent
    {
        void Something();
    }

    public class Leaf : IComponent
    {
        public void Something()
        {
        }
    }

    public class CompositeComponent : IComponent
    {
        private ICollection<IComponent> children;

        public void Something()
        {
            foreach (var child in children)
                child.Something();
        }

        public void AddCompoenet(IComponent component)
        {
            children.Add(component);
        }

        public void RemoveComponent(IComponent component)
        {
            children.Remove(component);
        }
    }

    class Program 
    {
        static IComponent component;

        static void Main(string[] args)
        {
            var composite = new CompositeComponent();
            composite.AddCompoenet(new Leaf());
            composite.AddCompoenet(new Leaf());
            composite.AddCompoenet(new Leaf());
            component = composite;
            component.Something();
        }
    }
}
